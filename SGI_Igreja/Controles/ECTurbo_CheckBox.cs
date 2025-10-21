using System.ComponentModel;
using System.Drawing; // Adicionado para Color, RectangleF, SizeF
using System.Drawing.Drawing2D;
using System.Windows.Forms; // Adicionado para CheckBox, Timer, Cursors, etc.

using ECTurbo.Codigos; // Supondo que Funcoes está aqui

namespace ECTurbo.Controles
{
    public class ECTurbo_CheckBox : CheckBox
    {
        private System.Windows.Forms.Timer animationTimer; // Renomeado para consistência, mas 'timer' também funciona
        private float animationProgress;
        private const float animationStep = 0.05f; // Define a velocidade da animação

        public ECTurbo_CheckBox()
        {
            // Estilos para suportar transparência e pintura customizada
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, false);

            this.BackColor = Color.Transparent; // <--- ADICIONADO: Essencial para transparência

            Tag = "";

            // DoubleBuffered já estava, mas é bom confirmar. OtimizedDoubleBuffer é geralmente preferível.
            // DoubleBuffered = true; // Coberto por SetStyle(ControlStyles.OptimizedDoubleBuffer, true)

            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 15; // Intervalo da animação em milissegundos (1ms é muito rápido, ajustei para 15ms)
            animationTimer.Tick += Timer_Tick;

            // Inicializar o estado da animação
            animationProgress = Checked ? 1f : 0f; // Se já começar checado, animação completa

            // Se o texto padrão for vazio e houver textos específicos, defina o texto inicial
            if (string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(TextoDesMarcado))
            {
                this.Text = TextoDesMarcado;
            }
            else if (string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(TextoMarcado))
            {
                // Se desmarcado estiver vazio mas marcado não, e estiver checado
                if (this.Checked) this.Text = TextoMarcado;
            }
        }

        private string vColuna = "";
        [DisplayName("(DB.1 Coluna Tabela)")]
        public string Coluna
        {
            get { return vColuna; }
            set
            {
                vColuna = value;
                string valor = Funcoes.PegarTag(this, "col");
                Tag = Tag.ToString().Replace("col=" + valor, "");
                if (!string.IsNullOrEmpty(value))
                    Tag = "col=" + value + Tag.ToString();
            }
        }

        private string vValorV;
        [DisplayName("(DB.2 Salvar este valor quando marcado)")]
        public string ValorV
        {
            get { return vValorV; }
            set
            {
                vValorV = value;
                string vAntigo = Funcoes.PegarTag(this, "valor");
                Tag = Tag.ToString().Replace("|valor=" + vAntigo, "");
                if (!string.IsNullOrEmpty(value))
                    Tag += "|valor=" + value;
            }
        }

        private string vValorF;
        [DisplayName("(DB.3 Salvar este valor quando Desmarcado)")]
        public string ValorF
        {
            get { return vValorF; }
            set
            {
                vValorF = value;
                string vAntigo = Funcoes.PegarTag(this, "valorF");
                Tag = Tag.ToString().Replace("|valorF=" + vAntigo, "");
                if (!string.IsNullOrEmpty(value))
                    Tag += "|valorF=" + value;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //ForeColor = Config.CorPrimaria; // Se Config.CorPrimaria existir e for desejado
            Cursor = Cursors.Hand;
            // Se AutoSize for true, precisamos garantir que o tamanho seja recalculado
            // A classe base CheckBox já faz isso bem, mas com UserPaint, precisamos de cuidado.
            // Vamos chamar OnTextChanged para que o texto inicial seja considerado no tamanho se AutoSize.
            OnTextChanged(EventArgs.Empty);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Checked) // Animando para o estado marcado
            {
                animationProgress += animationStep;
                if (animationProgress >= 1f)
                {
                    animationProgress = 1f;
                    animationTimer.Stop();
                }
            }
            else // Animando para o estado desmarcado (check sumindo)
            {
                animationProgress -= animationStep;
                if (animationProgress <= 0f)
                {
                    animationProgress = 0f;
                    animationTimer.Stop();
                }
            }
            Invalidate();
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);

            // Atualiza o texto baseado no estado do controle
            if (!string.IsNullOrEmpty(TextoMarcado) && !string.IsNullOrEmpty(TextoDesMarcado))
            {
                Text = Checked ? TextoMarcado : TextoDesMarcado;
            }
            else if (!string.IsNullOrEmpty(TextoMarcado) && Checked)
            {
                Text = TextoMarcado;
            }
            else if (!string.IsNullOrEmpty(TextoDesMarcado) && !Checked)
            {
                Text = TextoDesMarcado;
            }
            // Se apenas um deles estiver definido, e o outro não, o Text pode ficar "preso"
            // A lógica acima tenta cobrir isso.

            // Inicia a animação (para marcar ou desmarcar)
            // Não precisamos resetar animationProgress para 0 aqui se a animação for bidirecional
            animationTimer.Start();
        }

        // Adicionado para recalcular o tamanho quando o texto ou fonte muda, se AutoSize=true
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (AutoSize)
            {
                AjustarTamanhoAutomatico();
            }
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (AutoSize)
            {
                AjustarTamanhoAutomatico();
            }
            Invalidate();
        }

        private void AjustarTamanhoAutomatico()
        {
            if (!IsHandleCreated || Disposing || !AutoSize) return;

            using (Graphics g = CreateGraphics())
            {
                SizeF textSize = g.MeasureString(Text, Font);
                // A caixa do checkbox tem aproximadamente a altura da fonte ou um valor fixo (Height).
                // Para UserPaint, se Height não for fixo, ela pode ser baseada na fonte.
                // O CheckBox padrão tem uma caixa quadrada de aproximadamente Font.Height.
                // Vamos usar a altura atual do controle para a caixa.
                float boxSize = Math.Max(12, this.Height * 0.8f); // Um tamanho razoável para a caixa, baseado na altura do controle. Ou use Font.Height.

                // Largura: tamanho da caixa + espaçamento + texto
                this.Width = (int)Math.Ceiling(boxSize + 4 + textSize.Width + 4); // 4 para padding
                // Altura: O CheckBox padrão AutoSize ajusta a altura para caber a fonte.
                // Se você quiser comportamento similar:
                // this.Height = (int)Math.Max(textSize.Height + 4, boxSize + 2); // +4 para padding vertical ou altura da caixa
            }
        }


        #region Propriedades

        private int vArredondamento = 5;
        [DisplayName("_Arredondamento")]
        [Category("_ECTurbo")]
        [Description("Define o nível de arredondamento da caixa do checkbox.")]
        public int Arredondamento
        {
            get { return vArredondamento; }
            set
            {
                if (value < 0) value = 0;
                // O arredondamento máximo efetivo depende do tamanho da caixa.
                // Se a caixa é Height x Height, então max é Height / 2.
                // if (value > Height / 2) value = Height / 2; // Ajuste conforme necessário
                vArredondamento = value;
                Invalidate();
            }
        }

        private Color vCorBorda = Color.Green;
        [DisplayName("_Marcado Cor Borda")]
        [Category("_ECTurbo")]
        public Color CorBorda
        {
            get { return vCorBorda; }
            set { vCorBorda = value; Invalidate(); }
        }

        private Color vCorBordaDesmarcado = Color.Gray;
        [DisplayName("_Desmarcado Cor Borda")]
        [Category("_ECTurbo")]
        public Color CorBordaDesmarcado
        {
            get { return vCorBordaDesmarcado; }
            set { vCorBordaDesmarcado = value; Invalidate(); }
        }

        private Color vCorFundo = Color.MediumAquamarine;
        [DisplayName("_Marcado Cor Fundo 1")]
        [Category("_ECTurbo")]
        public Color CorFundo
        {
            get { return vCorFundo; }
            set { vCorFundo = value; Invalidate(); }
        }

        private Color vCorFundo2 = Color.DarkGreen;
        [DisplayName("_Marcado Cor Fundo 2")]
        [Category("_ECTurbo")]
        public Color CorFundo2
        {
            get { return vCorFundo2; }
            set { vCorFundo2 = value; Invalidate(); }
        }

        private Color vCorFundoDesmarcado = Color.Transparent; // Mantido como transparente
        [DisplayName("_Desmarcado Cor Fundo")]
        [Category("_ECTurbo")]
        public Color CorFundoDesmarcado
        {
            get { return vCorFundoDesmarcado; }
            set { vCorFundoDesmarcado = value; Invalidate(); }
        }

        private Color vCorIcone = Color.White;
        [DisplayName("_Marcado Cor do Ícone (V)")]
        [Category("_ECTurbo")]
        public Color CorIcone
        {
            get { return vCorIcone; }
            set { vCorIcone = value; Invalidate(); }
        }

        private string vTextoMarcado = string.Empty;
        [DisplayName("_Texto Quando Marcado")]
        public string TextoMarcado
        {
            get { return vTextoMarcado; }
            set
            {
                vTextoMarcado = value;
                if (Checked && !string.IsNullOrEmpty(value)) Text = value;
                else if (Checked && string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(TextoDesMarcado)) Text = TextoDesMarcado; // fallback
                Invalidate();
            }
        }

        private string vTextoDesMarcado = string.Empty;
        [DisplayName("_Texto Quando Desmarcado")]
        public string TextoDesMarcado
        {
            get { return vTextoDesMarcado; }
            set
            {
                vTextoDesMarcado = value;
                if (!Checked && !string.IsNullOrEmpty(value)) Text = value;
                else if (!Checked && string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(TextoMarcado)) Text = TextoMarcado; // fallback
                Invalidate();
            }
        }

        private Color vCorTextoDesmarcado = Color.Gray;
        [DisplayName("_Desmarcado Cor do Texto")]
        [Category("_ECTurbo")]
        public Color CorTextoDesmarcado
        {
            get { return vCorTextoDesmarcado; }
            set { vCorTextoDesmarcado = value; Invalidate(); }
        }

        private bool vValorPadrao = false;
        [DisplayName("_Definir Valor Padrão")]
        public bool valorPadrao
        {
            get { return vValorPadrao; }
            set
            {
                vValorPadrao = value;
                if (value == true)
                {
                    if (!Tag.ToString().Contains("|valor_padrao"))
                        Tag += "|valor_padrao";
                }
                else
                    Tag = Tag.ToString().Replace("|valor_padrao", "");
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            // NÃO CHAMAR base.OnPaint(pevent) para permitir transparência total do fundo.
            // base.OnPaint(pevent); // <--- REMOVIDO

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // NÃO LIMPAR o fundo se quisermos transparência.
            // g.Clear(BackColor); // <--- REMOVIDO

            // Define o retângulo para a "caixa" do checkbox.
            // Vamos usar uma proporção da altura, e posicioná-lo verticalmente centralizado.
            float boxSize = Math.Min(Width, Height) * 0.8f; // Tamanho da caixa, 80% da menor dimensão (ou Height - padding)
            if (boxSize < 10) boxSize = 10; // Tamanho mínimo

            float boxY = (Height - boxSize) / 2;
            RectangleF checkBoxRect = new RectangleF(2, boxY, boxSize, boxSize); // X=2 para um pequeno padding esquerdo

            // Cores baseadas no estado
            Color corDaBordaAtual = Checked ? CorBorda : CorBordaDesmarcado;
            Color corDoTextoAtual = Checked ? ForeColor : CorTextoDesmarcado;
            Color corFundoAtual = Checked ? CorFundo : CorFundoDesmarcado; // Usado para preenchimento
            Color corFundo2Atual = Checked ? CorFundo2 : CorFundoDesmarcado; // Para gradiente

            // Desenha o fundo da caixa do checkbox
            using (GraphicsPath path = Funcoes.CriarPath(checkBoxRect, Arredondamento)) // Supondo que Arredondamento é o raio
            {
                // Preenchimento da caixa
                // Se CorFundoDesmarcado for Color.Transparent, o preenchimento não será visível quando desmarcado.
                if (Checked)
                {
                    using (LinearGradientBrush PincelFundo = new LinearGradientBrush(checkBoxRect, corFundoAtual, corFundo2Atual, 90))
                    {
                        g.FillPath(PincelFundo, path);
                    }
                }
                else if (corFundoAtual != Color.Transparent) // Só preenche se não for transparente
                {
                    using (SolidBrush PincelFundoSolido = new SolidBrush(corFundoAtual))
                    {
                        g.FillPath(PincelFundoSolido, path);
                    }
                }

                // Desenha a borda da caixa
                using (Pen penBorda = new Pen(corDaBordaAtual, 1.5f))
                {
                    g.DrawPath(penBorda, path);
                }
            }

            // Desenho animado do ícone "v" (check mark)
            // Só desenha se animationProgress > 0 (ou seja, começando a marcar ou totalmente marcado)
            if (animationProgress > 0f)
            {
                using (Pen penIcone = new Pen(CorIcone, 2f)) // Espessura do "V"
                {
                    DrawAnimatedCheckMark(g, checkBoxRect, penIcone, animationProgress);
                }
            }

            // Desenha o texto
            if (!string.IsNullOrEmpty(Text))
            {
                using (SolidBrush brushTexto = new SolidBrush(corDoTextoAtual))
                {
                    SizeF tamanhoTexto = g.MeasureString(Text, Font);
                    float textoY = (Height - tamanhoTexto.Height) / 2;
                    // Posiciona o texto à direita da caixa do checkbox
                    g.DrawString(Text, Font, brushTexto, checkBoxRect.Right + 5, textoY); // 5px de espaçamento
                }
            }
        }

        private void DrawAnimatedCheckMark(Graphics g, RectangleF baseRect, Pen pen, float progress)
        {
            if (progress <= 0f) return; // Não desenha nada se o progresso for zero ou negativo

            // Pontos para o "V" normalizados dentro do baseRect
            PointF p1 = new PointF(baseRect.X + baseRect.Width * 0.20f, baseRect.Y + baseRect.Height * 0.50f);
            PointF p2 = new PointF(baseRect.X + baseRect.Width * 0.45f, baseRect.Y + baseRect.Height * 0.75f); // Ponto de inflexão do "V"
            PointF p3 = new PointF(baseRect.X + baseRect.Width * 0.80f, baseRect.Y + baseRect.Height * 0.25f);

            float firstSegmentProgress = Math.Min(1f, progress * 2f); // Progresso da primeira linha (0 a 1)

            PointF currentP1End = new PointF(
                p1.X + (p2.X - p1.X) * firstSegmentProgress,
                p1.Y + (p2.Y - p1.Y) * firstSegmentProgress
            );
            g.DrawLine(pen, p1, currentP1End);

            if (progress > 0.5f)
            {
                float secondSegmentProgress = Math.Min(1f, (progress - 0.5f) * 2f); // Progresso da segunda linha (0 a 1)
                PointF currentP2End = new PointF(
                    p2.X + (p3.X - p2.X) * secondSegmentProgress,
                    p2.Y + (p3.Y - p2.Y) * secondSegmentProgress
                );
                g.DrawLine(pen, p2, currentP2End);
            }
        }
    }
}