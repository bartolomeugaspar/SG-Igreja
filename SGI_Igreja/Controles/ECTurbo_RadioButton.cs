using System.ComponentModel;
using System.Drawing; // Adicionado para Color eSizeF
using System.Drawing.Drawing2D;
using System.Windows.Forms; // Adicionado para Control, RadioButton, Timer, Cursors, etc.

using ECTurbo.Codigos; // Supondo que Funcoes está aqui

namespace ECTurbo.Controles
{
    public class ECTurbo_RadioButton : RadioButton
    {
        private System.Windows.Forms.Timer animationTimer;
        private float currentMarkerSize;
        private bool isAnimating;
        private bool expanding; // Controla se o marcador está aumentando ou diminuindo

        public ECTurbo_RadioButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, false); // Importante para transparência

            this.BackColor = Color.Transparent; // <--- ADICIONADO: Definir o BackColor como Transparente

            Tag = "";

            DoubleBuffered = true;
            Cursor = Cursors.Hand;

            // Inicializar o temporizador de animação
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 15; // Intervalo para a animação, ajustável
            animationTimer.Tick += AnimationTimer_Tick;

            // Definir o tamanho inicial do marcador
            currentMarkerSize = 0; // Começa com o marcador invisível
            expanding = false;
            isAnimating = false;

            // Ajustar o tamanho inicial se AutoSize for verdadeiro
            if (AutoSize)
            {
                AjustarTamanho();
            }
        }

        private bool vValorPadrao = false;
        [DisplayName("_Definir Valor Padrão")]
        public bool valorPadrao
        {
            get { return vValorPadrao; }
            set
            {
                vValorPadrao = value;
                if (value)
                {
                    if (!Tag.ToString().Contains("|valor_padrao"))
                        Tag += "|valor_padrao";
                }
                else
                {
                    Tag = Tag.ToString().Replace("|valor_padrao", "");
                }
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
                if (string.IsNullOrEmpty(value) == false)
                    Tag = "col=" + value + Tag.ToString();
            }
        }

        private bool vObgt = false;
        [DisplayName("(DB.2 Campo Obrigatório)")]
        public bool Obgt
        {
            get { return vObgt; }
            set
            {
                vObgt = value;
                if (value)
                {
                    if (!Tag.ToString().Contains("|obgt"))
                        Tag += "|obgt";
                }
                else
                {
                    Tag = Tag.ToString().Replace("|obgt", "");
                }
            }
        }

        private bool vMarcarX = false;
        [DisplayName("(DB.3 Marcar Campo Obrigatório)")]
        public bool MarcarX
        {
            get { return vMarcarX; }
            set
            {
                vMarcarX = value;
                if (value)
                {
                    if (!Tag.ToString().Contains("|x"))
                        Tag += "|x";
                }
                else
                {
                    Tag = Tag.ToString().Replace("|x", "");
                }
            }
        }

        private string vValorV;
        [DisplayName("(DB.4 Salvar este valor quando marcado)")]
        public string ValorV
        {
            get { return vValorV; }
            set
            {
                vValorV = value;
                string vAntigo = Funcoes.PegarTag(this, "valor");
                Tag = Tag.ToString().Replace("|valor=" + vAntigo, "");
                if (string.IsNullOrEmpty(value) == false)
                    Tag += "|valor=" + value;
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            AjustarTamanho();
        }

        // Sobrescrever OnTextChanged para ajustar o tamanho quando o texto muda
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            AjustarTamanho();
        }

        // Sobrescrever OnSizeChanged para ajustar o tamanho se a altura mudar
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            // Se AutoSize for verdadeiro, permita que AjustarTamanho recalcule a largura baseada na nova altura
            // Se AutoSize for falso, o usuário definiu o tamanho, então não precisamos ajustar automaticamente aqui,
            // a menos que queiramos garantir uma proporção ou tamanho mínimo para o ícone.
            // No entanto, AjustarTamanho atualmente afeta MaximumSize se AutoSize=true,
            // ou não faz nada em relação à largura se AutoSize=false.
            // Para o ícone, sempre recalcular partes dependentes da altura, como TamanhoIcone em OnPaint.
            Invalidate(); // Para redesenhar com base na nova altura
        }


        private void AjustarTamanho()
        {
            if (IsHandleCreated && !Disposing && AutoSize) // Só ajuste se AutoSize for true e o handle existir
            {
                using (Graphics g = CreateGraphics())
                {
                    // Tamanho do texto
                    SizeF tamanhoTexto = g.MeasureString(Text, Font);

                    // A altura do controle RadioButton geralmente é determinada pela fonte.
                    // Vamos calcular a largura necessária para o ícone (círculo) + texto.
                    // O ícone tem 'Height - 1' de diâmetro.
                    float diametroIcone = Height - 1;
                    if (diametroIcone < 10) diametroIcone = 10; // Um mínimo para não sumir

                    // Largura total: diâmetro do ícone + espaçamento + largura do texto + um pouco de padding
                    float larguraNecessaria = diametroIcone + 4 + tamanhoTexto.Width + 4; // 4 é o espaçamento texto-ícone, mais 4 de padding final

                    // Ajuste a largura do controle
                    // Em vez de MaximumSize, se AutoSize=true, devemos tentar definir o Width.
                    // No entanto, RadioButton padrão com AutoSize=true ajusta altura e largura.
                    // Para um UserPaint, é mais comum controlar o Width e deixar Height ser pela fonte ou fixo.
                    // Se você quer que a altura também seja automática baseada na fonte:
                    // this.Height = (int)Math.Max(tamanhoTexto.Height + 4, diametroIcone +1); // +4 para padding vertical ou min da altura do icone
                    // E então recalcular diametroIcone se a altura mudou.

                    // Se AutoSize é true, o ideal é que o próprio sistema defina Width e Height.
                    // Como estamos fazendo UserPaint, precisamos dar um tamanho.
                    // O RadioButton padrão com AutoSize=true define sua altura com base na fonte.
                    // E a largura com base no ícone + texto.

                    // Para simplificar, se AutoSize = true, vamos definir Width e Height.
                    // A altura pode ser baseada na fonte, com um mínimo para o ícone.
                    int alturaCalculada = (int)Math.Max(tamanhoTexto.Height + 4, 16); // Ex: min 16px para o ícone, ou altura do texto + padding

                    // Se a altura atual for diferente da calculada e AutoSize for true,
                    // mudar a altura pode causar recursão ou comportamento inesperado se não tratado com cuidado.
                    // É mais seguro assumir que a Height é dada (seja por padrão da fonte ou setada pelo usuário)
                    // e ajustar apenas a Width se AutoSize=true.

                    // Se AutoSize for true, o controle deve ajustar sua largura.
                    // A altura é geralmente herdada da fonte ou definida explicitamente.
                    // O código original definia MaximumSize, o que é um pouco incomum para AutoSize.
                    // AutoSize geralmente significa que o controle *define* seu próprio tamanho.
                    this.Width = (int)Math.Ceiling(larguraNecessaria);
                }
                Invalidate(); // Redesenhar após ajuste
            }
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            expanding = Checked;
            isAnimating = true;
            animationTimer.Start();

            if (Checked)
            {
                if (Parent != null)
                {
                    foreach (Control Ctr in Parent.Controls)
                    {
                        if (Ctr != this && Ctr is ECTurbo_RadioButton && Ctr.Tag != null) // Garante que não é ele mesmo
                        {
                            // Lógica para desmarcar outros radios no mesmo grupo (se necessário,
                            // mas o RadioButton padrão já faz isso se estiverem no mesmo container)
                            // A sua lógica aqui é específica para "Funcoes.RemoverLabel"
                            if (Ctr.Tag.ToString().Contains("|x"))
                            {
                                if (Funcoes.PegarTag(Ctr, "col") == Funcoes.PegarTag(this, "col"))
                                    Funcoes.RemoverLabel(Ctr);
                            }
                        }
                    }
                }
            }

            base.OnCheckedChanged(e); // Importante para disparar eventos e comportamento padrão (como desmarcar outros)
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int TamanhoIcone = Height - 1;
            if (TamanhoIcone < 0) TamanhoIcone = 0; // Evitar negativo

            float targetMarkerSize;
            if (expanding)
            {
                // O marcador interno preenche quase todo o espaço, menos a borda e o TamanhoMarcador
                targetMarkerSize = TamanhoIcone - (TamanhoMarcador * 2) - 2; // -2 para um pequeno espaço da borda do círculo externo
                if (targetMarkerSize < 0) targetMarkerSize = 0;

                currentMarkerSize += 2; // Aumentar a bolinha (velocidade da animação)
                if (currentMarkerSize >= targetMarkerSize)
                {
                    currentMarkerSize = targetMarkerSize;
                    isAnimating = false;
                    animationTimer.Stop();
                }
            }
            else
            {
                targetMarkerSize = 0;
                currentMarkerSize -= 2; // Diminuir a bolinha
                if (currentMarkerSize <= targetMarkerSize)
                {
                    currentMarkerSize = targetMarkerSize;
                    isAnimating = false;
                    animationTimer.Stop();
                }
            }
            Invalidate();
        }

        #region Propriedades

        private int vArredondamento = 50; // Percentual para arredondamento
        [DisplayName("_Arredondamento")]
        [Category("_ECTurbo")]
        [Description("Define o nível de arredondamento do círculo externo (0-100%). 100% para círculo perfeito.")]
        public int Arredondamento
        {
            get { return vArredondamento; }
            set
            {
                vArredondamento = Math.Max(0, Math.Min(100, value)); // Limita entre 0 e 100
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
        [DisplayName("_Marcado Cor 1")]
        [Category("_ECTurbo")]
        public Color CorFundo
        {
            get { return vCorFundo; }
            set { vCorFundo = value; Invalidate(); }
        }

        private Color vCorFundo2 = Color.DarkGreen;
        [DisplayName("_Marcado Cor 2")]
        [Category("_ECTurbo")]
        public Color CorFundo2
        {
            get { return vCorFundo2; }
            set { vCorFundo2 = value; Invalidate(); }
        }

        private Color vCorTextoDesmarcado = Color.Gray;
        [DisplayName("_Desmarcado Cor Texto")]
        [Category("_ECTurbo")]
        public Color CorTextoDesmarcado
        {
            get { return vCorTextoDesmarcado; }
            set { vCorTextoDesmarcado = value; Invalidate(); }
        }

        private int vTamanhoMarcador = 2; // Espessura da "borda" interna entre o círculo externo e o marcador preenchido
        [DisplayName("_Tamanho Borda Interna Marcador")]
        [Category("_ECTurbo")]
        [Description("Define a espessura da borda entre o círculo externo e o marcador interno quando checado.")]
        public int TamanhoMarcador // Renomeado para ser mais claro, era "Tamanho do Marcador"
        {
            get { return vTamanhoMarcador; }
            set
            {
                vTamanhoMarcador = Math.Max(1, Math.Min(Height / 4, value)); // Garante que seja razoável
                Invalidate();
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            // NÃO CHAMAR base.OnPaint(pevent) para permitir transparência total do fundo.
            // base.OnPaint(pevent); // <--- REMOVIDO / COMENTADO

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Importante: Como não chamamos base.OnPaint, o fundo do controle não é limpo.
            // Se BackColor = Color.Transparent, o que estiver "atrás" do controle será visível.
            // Se você quisesse um fundo sólido customizado, você desenharia aqui:
            // using (SolidBrush backBrush = new SolidBrush(this.BackColor))
            // {
            //    g.FillRectangle(backBrush, this.ClientRectangle);
            // }
            // Mas para transparência, não fazemos isso.

            // O diâmetro do círculo externo é baseado na altura do controle
            float diametroIcone = Math.Max(0, Height - 2); // -2 para um pequeno padding, ou -1 como antes.
            RectangleF baseRect = new RectangleF(1, 1, diametroIcone, diametroIcone); // (1,1) para não cortar a borda

            Color corDaBordaAtual = Checked ? CorBorda : CorBordaDesmarcado;
            Color corDoTextoAtual = Checked ? ForeColor : CorTextoDesmarcado;

            // Desenha o círculo externo (a "caixa" do rádio)
            using (Pen penBorda = new Pen(corDaBordaAtual, 1.5f)) // Aumentei a espessura da borda para melhor visualização
            {
                // Para o Arredondamento ser um percentual do diâmetro para formar um círculo:
                float raioArredondamento = baseRect.Height * (vArredondamento / 100.0f);
                if (vArredondamento >= 50) // Para ser um círculo/elipse perfeita
                {
                    g.DrawEllipse(penBorda, baseRect);
                }
                else // Para cantos arredondados mais complexos (se Funcoes.CriarPath fizesse isso)
                {
                    // Se Funcoes.CriarPath for para cantos arredondados de um retângulo:
                    // using (GraphicsPath path = Funcoes.CriarPath(baseRect, (int)raioArredondamento)) // Supondo que o segundo param é o raio
                    // {
                    //    g.DrawPath(penBorda, path);
                    // }
                    // Se Funcoes.CriarPath é para um círculo/elipse, use DrawEllipse diretamente como acima.
                    // Vou assumir que Funcoes.CriarPath(rect, 50 ou mais) retorna um path elíptico/circular.
                    // E que Arredondamento como "50" já significa um círculo no seu Funcoes.CriarPath.
                    // Para simplificar, se Arredondamento for alto, é um círculo:
                    if (vArredondamento >= 45)
                    { // Ajuste o limiar conforme o comportamento de CriarPath
                        g.DrawEllipse(penBorda, baseRect);
                    }
                    else
                    {
                        // Comportamento para arredondamento menor (retângulo com cantos arredondados)
                        // Precisa de uma implementação de Funcoes.CriarPath que suporte isso.
                        // Exemplo de GraphicsPath para retângulo com cantos arredondados (simplificado):
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            // Simples, para exemplo:
                            path.AddArc(baseRect.X, baseRect.Y, raioArredondamento, raioArredondamento, 180, 90);
                            path.AddArc(baseRect.Right - raioArredondamento, baseRect.Y, raioArredondamento, raioArredondamento, 270, 90);
                            path.AddArc(baseRect.Right - raioArredondamento, baseRect.Bottom - raioArredondamento, raioArredondamento, raioArredondamento, 0, 90);
                            path.AddArc(baseRect.X, baseRect.Bottom - raioArredondamento, raioArredondamento, raioArredondamento, 90, 90);
                            path.CloseFigure();
                            g.DrawPath(penBorda, path);
                        }
                    }
                }
            }

            // Desenha o texto
            if (!string.IsNullOrEmpty(Text))
            {
                using (SolidBrush brushTexto = new SolidBrush(corDoTextoAtual))
                {
                    SizeF tamanhoTexto = g.MeasureString(Text, Font);
                    float textoY = (Height - tamanhoTexto.Height) / 2;
                    g.DrawString(Text, Font, brushTexto, baseRect.Right + 4, textoY); // 4px de espaçamento
                }
            }

            // Desenha o marcador interno (a "bolinha" ou preenchimento) se estiver checado ou animando
            if (Checked || isAnimating)
            {
                // O currentMarkerSize agora representa o diâmetro do marcador interno animado.
                // O retângulo para o marcador interno:
                if (currentMarkerSize > 0)
                {
                    // Centraliza o marcador interno dentro do baseRect
                    float markerX = baseRect.X + (baseRect.Width - currentMarkerSize) / 2;
                    float markerY = baseRect.Y + (baseRect.Height - currentMarkerSize) / 2;
                    RectangleF markerRect = new RectangleF(markerX, markerY, currentMarkerSize, currentMarkerSize);

                    // Lógica de arredondamento similar ao externo, mas para o marcador interno
                    // Normalmente o marcador interno é sempre um círculo perfeito.
                    using (GraphicsPath pathMarcador = new GraphicsPath())
                    {
                        pathMarcador.AddEllipse(markerRect); // Marcador interno é sempre um círculo

                        using (LinearGradientBrush brushMarcador = new LinearGradientBrush(markerRect, CorFundo, CorFundo2, 90f))
                        {
                            g.FillPath(brushMarcador, pathMarcador);
                        }
                    }
                }
            }
        }
    }
}