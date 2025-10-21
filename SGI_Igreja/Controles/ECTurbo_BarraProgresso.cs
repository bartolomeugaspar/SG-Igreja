using System.Drawing.Drawing2D;
using ECTurbo.Codigos;
using System.ComponentModel;

namespace ECTurbo.Controles
{
    public class ECTurbo_BarraProgresso : Control
    {
        private System.Windows.Forms.Timer timer;
        private float targetPercentual;
        private float step = 1; // Passo de incremento da animação

        public ECTurbo_BarraProgresso()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, false);

            BackColor = Color.Transparent;

            DoubleBuffered = true;
            MinimumSize = new Size(50, 12);

            // Configura o timer para a animação
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1; // Intervalo de tempo em milissegundos
            timer.Tick += Timer_Tick;
        }

        private Control vCtrAssociado;
        [DisplayName("_Controle Associado")]
        public Control CtrAssociado
        {
            get { return vCtrAssociado; }
            set { vCtrAssociado = value; }
        }

        private float vPercentual = 0;
        [DisplayName("_Percentual da Barra")]
        public float Percentual
        {
            get { return vPercentual; }
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 100;

                targetPercentual = value; // Definir o alvo para a animação
                timer.Start(); // Iniciar o timer para animar a barra
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Incrementa ou decrementa o percentual atual para atingir o targetPercentual
            if (vPercentual < targetPercentual)
            {
                vPercentual += step;
                if (vPercentual > targetPercentual)
                    vPercentual = targetPercentual;
            }
            else if (vPercentual > targetPercentual)
            {
                vPercentual -= step;
                if (vPercentual < targetPercentual)
                    vPercentual = targetPercentual;
            }

            if (CtrAssociado != null)
                CtrAssociado.Text = vPercentual.ToString() + "%";

            Invalidate(); // Redesenha o controle

            // Para o timer quando atingir o valor alvo
            if (vPercentual == targetPercentual)
                timer.Stop();
        }

        private int vArredBorda = 20;
        [DisplayName("_Arredondamento da Barra")]
        public int ArredBorda
        {
            get { return vArredBorda; }
            set
            {
                if (value < 1) value = 1;
                vArredBorda = value;
                Invalidate();
            }
        }

        private int vEspaco = 0;
        [DisplayName("_Espaçamento Interno")]
        public int Espaco
        {
            get { return vEspaco; }
            set
            {
                if (value < 0) value = 0;
                vEspaco = value;
                Invalidate();
            }
        }

        private int vArredMarcador = 20;
        [DisplayName("_Arredondamento do Marcador")]
        public int ArredMarcador
        {
            get { return vArredMarcador; }
            set
            {
                if (value < 1) value = 1;
                if (value > Height) value = Height;

                vArredMarcador = value;
                Invalidate();
            }
        }

        private int vTamMarcador = 10;
        [DisplayName("_Largura do Marcador")]
        public int TamMarcador
        {
            get { return vTamMarcador; }
            set
            {
                if (value < 4) value = 4;
                if (value > Height) value = Height;

                vTamMarcador = value;
                Invalidate();
            }
        }

        private bool vMostrarMarcador = true;
        [DisplayName("_Mostrar Marcador")]
        public bool MostrarMarcador
        {
            get { return vMostrarMarcador; }
            set { vMostrarMarcador = value; Invalidate(); }
        }

        private Color vCorBarra1 = Color.Orange;
        [DisplayName("_Cor da Barra 1")]
        public Color CorBarra1
        {
            get { return vCorBarra1; }
            set { vCorBarra1 = value; Invalidate(); }
        }

        private Color vCorBarra2 = Color.Purple;
        [DisplayName("_Cor da Barra 2")]
        public Color CorBarra2
        {
            get { return vCorBarra2; }
            set { vCorBarra2 = value; Invalidate(); }
        }

        private Color vCorFundo = Color.Gainsboro;
        [DisplayName("_Cor de Fundo")]
        public Color CorFundo
        {
            get { return vCorFundo; }
            set { vCorFundo = value; Invalidate(); }
        }

        private Color vCorBorda = Color.Black;
        [DisplayName("_Cor da Borda Externa")]
        public Color CorBorda
        {
            get { return vCorBorda; }
            set { vCorBorda = value; Invalidate(); }
        }

        private Color vContornoMarcador = Color.Black;
        [DisplayName("_Cor da Borda do Marcador")]
        public Color ContornoMarcador
        {
            get { return vContornoMarcador; }
            set { vContornoMarcador = value; Invalidate(); }
        }

        private Color vCorMarcador1 = Color.Yellow;
        [DisplayName("_Cor do Marcador 1")]
        public Color CorMarcador1
        {
            get { return vCorMarcador1; }
            set { vCorMarcador1 = value; Invalidate(); }
        }

        private Color vCorMarcador2 = Color.Red;
        [DisplayName("_Cor do Marcador 2")]
        public Color CorMarcador2
        {
            get { return vCorMarcador2; }
            set { vCorMarcador2 = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.Clear(BackColor);

            RectangleF Base = ClientRectangle;
            Base.Inflate(-2, -4);

            RectangleF Barra = Base;
            Barra.Width = Base.Width * (Percentual / 100);

            if (Barra.Width < Base.Height) Barra.Width = Base.Height;
            if (Barra.Height < Base.Height) Barra.Height = Base.Height;

            if (Height - 8 - Espaco - Espaco >= 0)
                Barra.Inflate(-Espaco, -Espaco);

            RectangleF M = Base;
            M.Width = TamMarcador;
            M.Inflate(2, 2);

            float AreaM = Base.X + Base.Width - M.Width;
            M.X = AreaM * (Percentual / 100);

            using (GraphicsPath path = Funcoes.CriarPath(Base, ArredBorda))
            using (GraphicsPath pathBarra = Funcoes.CriarPath(Barra, ArredBorda))
            using (GraphicsPath pathM = Funcoes.CriarPath(M, ArredMarcador))
            using (SolidBrush Pincel = new SolidBrush(CorFundo))
            using (Pen Caneta = new Pen(CorBorda, 1))
            using (LinearGradientBrush Gradiente = new LinearGradientBrush(Barra, CorBarra1, CorBarra2, 1))
            using (LinearGradientBrush GradienteMarcador = new LinearGradientBrush(M, CorMarcador1, CorMarcador2, 45))
            {
                g.FillPath(Pincel, path);

                if (Percentual > 0)
                {
                    g.FillPath(Gradiente, pathBarra);
                }

                g.DrawPath(Caneta, path);

                if (MostrarMarcador == true)
                {
                    g.FillPath(GradienteMarcador, pathM);
                    Caneta.Color = ContornoMarcador;
                    g.DrawPath(Caneta, pathM);
                }
            }
        }
    }
}
