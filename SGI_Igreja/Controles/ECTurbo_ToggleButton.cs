using System.ComponentModel;
using System.Drawing.Drawing2D;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_ToggleButton : CheckBox
    {
        private float animationProgress = 1f;
        private const float animationStep = 0.05f; // Velocidade da animação
        private System.Windows.Forms.Timer animationTimer;

        public ECTurbo_ToggleButton()
        {
            Tag = "";
            DoubleBuffered = true;
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 1; // Intervalo de atualização em milissegundos
            animationTimer.Tick += AnimationTimer_Tick;
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

                if (string.IsNullOrEmpty(value) == false)
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

                if (string.IsNullOrEmpty(value) == false)
                    Tag += "|valorF=" + value;

            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            AjustarTamanho();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            AjustarTamanho();
        }

        private void AjustarTamanho()
        {
            using (Graphics g = CreateGraphics())
            {
                // Tamanho do texto
                SizeF tamanhoTexto = g.MeasureString(Text, Font);

                // Calcule a largura necessária para a base e a bolinha
                float larguraNecessaria = Height * 1.4f + tamanhoTexto.Width + 12; // ajuste o multiplicador para outros modelos se necessário

                // Ajuste a largura do controle
                if (AutoSize)
                    MinimumSize = new Size((int)larguraNecessaria, (int)tamanhoTexto.Height + 4);

            }
        }

        // Evento Tick do Timer para animar a bolinha
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animationProgress += animationStep;

            if (animationProgress >= 1f)
            {
                animationProgress = 1f;
                animationTimer.Stop();
            }

            Invalidate(); // Redesenha o controle para mostrar a animação
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);

            // Atualiza o texto baseado no estado do controle
            if (TextoMarcado != string.Empty && TextoDesMarcado != string.Empty)
                Text = Checked ? TextoMarcado : TextoDesMarcado;

            animationProgress = 0f;
            animationTimer.Start();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            //ForeColor = Config.CorPrimaria;
            //Font = Config.FontePadrao;
            Cursor = Cursors.Hand;
        }

        private string vTextoMarcado = string.Empty;
        [DisplayName("_Marcado Texto")]
        public string TextoMarcado
        {
            get { return vTextoMarcado; }
            set
            {
                vTextoMarcado = value;

                if (Checked && value != string.Empty)
                    Text = value; Invalidate();
            }
        }

        private string vTextoDesMarcado = string.Empty;
        [DisplayName("_Desmarcado Texto")]
        public string TextoDesMarcado
        {
            get { return vTextoDesMarcado; }
            set
            {
                vTextoDesMarcado = value;

                if (!Checked && value != string.Empty)
                {
                    Text = value;
                }
                else
                {
                    if (TextoMarcado != string.Empty)
                        Text = TextoMarcado;
                }

                Invalidate();
            }
        }

        private Color vCorFundoM = Color.MediumSeaGreen;
        [DisplayName("_Marcado Fundo")]
        public Color CorFundoM
        {
            get { return vCorFundoM; }
            set { vCorFundoM = value; Invalidate(); }
        }

        private Color vCorFundoD = Color.Silver;
        [DisplayName("_DesMarcado Fundo")]
        public Color CorFundoD
        {
            get { return vCorFundoD; }
            set { vCorFundoD = value; Invalidate(); }
        }

        private Color vCorMarcadorM = Color.DarkGreen;
        [DisplayName("_Marcado Bolinha")]
        public Color CorMarcadorM
        {
            get { return vCorMarcadorM; }
            set { vCorMarcadorM = value; Invalidate(); }
        }

        private Color vCorMarcadorD = Color.DimGray;
        [DisplayName("_DesMarcado Bolinha")]
        public Color CorMarcadorD
        {
            get { return vCorMarcadorD; }
            set { vCorMarcadorD = value; Invalidate(); }
        }


        private Color vCorTextoD = Color.DimGray;
        [DisplayName("_DesMarcado Texto Cor")]
        public Color CorTextoD
        {
            get { return vCorTextoD; }
            set { vCorTextoD = value; Invalidate(); }
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(BackColor);

            if (ToggleModelo == Modelo.Modelo1)
                Modelo1(g);
            else if (ToggleModelo == Modelo.Modelo2)
                Modelo2(g);
            else if (ToggleModelo == Modelo.Modelo3)
                Modelo3(g);

        }

        public enum Modelo
        {
            Modelo1,
            Modelo2,
            Modelo3
        }

        private Modelo vModelo = Modelo.Modelo1;
        [DisplayName("_Modelo")]
        public Modelo ToggleModelo
        {
            get { return vModelo; }
            set { vModelo = value; Invalidate(); }
        }


        private void Modelo1(Graphics g)
        {
            RectangleF Base = new RectangleF(0, 0, Height * 1.4f, Height);
            Base.Inflate(-1, -1);

            RectangleF bFundo = Base;
            bFundo.Inflate(0, -4);

            Color CorFundo = Checked ? CorFundoM : CorFundoD;
            Color CorMarcador = Checked ? CorMarcadorM : CorMarcadorD;

            SizeF TamanhoTexto = g.MeasureString(Text, Font);

            using (GraphicsPath path = Funcoes.CriarPath(bFundo, 50))
            using (SolidBrush Pincel = new SolidBrush(CorFundo))
            {
                g.FillPath(Pincel, path);

                Pincel.Color = CorTextoD;

                if (Checked)
                    Pincel.Color = ForeColor;

                g.DrawString(Text, Font, Pincel, Base.X + Base.Width + 4, Base.Y + (Base.Height - TamanhoTexto.Height) / 2);
            }

            // Define as dimensões da bolinha
            bFundo.Width = bFundo.Height;
            bFundo.Inflate(2, 2);

            // Calcula a posição X da bolinha com base no progresso da animação
            float startX = 1;
            float endX = Base.Width - bFundo.Width + 1;
            bFundo.X = Checked ? startX + (endX - startX) * animationProgress
                               : endX - (endX - startX) * animationProgress;

            // Desenha a bolinha animada
            using (GraphicsPath path = Funcoes.CriarPath(bFundo, 50))
            using (SolidBrush Pincel = new SolidBrush(CorMarcador))
            {
                g.FillPath(Pincel, path);
            }
        }

        private void Modelo2(Graphics g)
        {
            RectangleF Base = new RectangleF(0, 0, Height * 1.7f, Height);
            Base.Inflate(-1, -1);

            RectangleF bFundo = Base;

            //bFundo.Inflate(0, -4);

            Color CorFundo = Checked ? CorFundoM : CorFundoD;
            Color CorMarcador = Checked ? CorMarcadorM : CorMarcadorD;

            SizeF TamanhoTexto = g.MeasureString(Text, Font);

            using (GraphicsPath path = Funcoes.CriarPath(bFundo, 50))
            using (SolidBrush Pincel = new SolidBrush(CorFundo))
            {
                g.FillPath(Pincel, path);

                Pincel.Color = CorTextoD;

                if (Checked)
                    Pincel.Color = ForeColor;

                g.DrawString(Text, Font, Pincel, Base.X + Base.Width + 4, Base.Y + (Base.Height - TamanhoTexto.Height) / 2);
            }

            // Define as dimensões da bolinha
            bFundo.Width = bFundo.Height;
            bFundo.Inflate(-2, -2);

            // Define as posições inicial e final da bolinha
            float startX = 4;
            float endX = Base.Width - bFundo.Width - 2;

            // Calcula a posição X da bolinha com base no progresso da animação
            bFundo.X = Checked ? startX + (endX - startX) * animationProgress
                               : endX - (endX - startX) * animationProgress;

            using (GraphicsPath path = Funcoes.CriarPath(bFundo, 50))
            using (SolidBrush Pincel = new SolidBrush(CorMarcador))
            {
                // Desenha a bolinha animada
                g.FillPath(Pincel, path);
            }
        }

        private void Modelo3(Graphics g)
        {
            RectangleF Base = new RectangleF(0, 0, Height * 1.4f, Height);
            Base.Inflate(-1, -1);

            RectangleF bFundo = Base;

            bFundo.Inflate(0, -2);

            Color CorFundo = Checked ? CorFundoM : CorFundoD;
            Color CorMarcador = Checked ? CorMarcadorM : CorMarcadorD;

            SizeF TamanhoTexto = g.MeasureString(Text, Font);

            using (GraphicsPath path = Funcoes.CriarPath(bFundo, 50))
            using (SolidBrush Pincel = new SolidBrush(CorTextoD))
            using (Pen Caneta = new Pen(CorFundo, 1))
            {
                g.DrawPath(Caneta, path);

                if (Checked)
                    Pincel.Color = ForeColor;

                g.DrawString(Text, Font, Pincel, Base.X + Base.Width + 4, Base.Y + (Base.Height - TamanhoTexto.Height) / 2);
            }

            bFundo.Width = bFundo.Height;
            bFundo.Inflate(-2, -2);
            bFundo.X = 4;

            // Define as posições inicial e final da bolinha
            float startX = 4;
            float endX = Base.X + Base.Width - bFundo.Width - 2;

            // Calcula a posição X da bolinha com base no progresso da animação
            bFundo.X = Checked ? startX + (endX - startX) * animationProgress
                               : endX - (endX - startX) * animationProgress;

            using (GraphicsPath path = Funcoes.CriarPath(bFundo, 50))
            using (SolidBrush Pincel = new SolidBrush(CorMarcador))
            {
                g.FillPath(Pincel, path);
            }
        }
    }
}
