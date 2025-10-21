using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ECTurbo.Controles
{
    public class ECTurbo_Grafico4 : Control
    {
        public ECTurbo_Grafico4()
        {
            // Configura os estilos de controle para permitir transparência e desenho personalizado
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            // Crucial: define o controle como NÃO opaco, permitindo que o fundo do pai seja visível
            SetStyle(ControlStyles.Opaque, false);

            BackColor = Color.Transparent; // Define o fundo como transparente por padrão

            DoubleBuffered = true;
            Size = new Size(100, 50);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
        }

        private Control vCtrAssociado;
        [DisplayName("_Controle Associado")]
        public Control CtrAssociado
        {
            get { return vCtrAssociado; }
            set { vCtrAssociado = value; }
        }


        private Color vCor1 = Color.ForestGreen;
        [DisplayName("_Cor1")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }

        private Color vCor2 = Color.GreenYellow;
        [DisplayName("_Cor2")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }


        private Color vCor3 = Color.Gold;
        [DisplayName("_Cor3")]
        public Color Cor3
        {
            get { return vCor3; }
            set { vCor3 = value; Invalidate(); }
        }

        private Color vCor4 = Color.DarkOrange;
        [DisplayName("_Cor4")]
        public Color Cor4
        {
            get { return vCor4; }
            set { vCor4 = value; Invalidate(); }
        }

        private Color vCor5 = Color.OrangeRed;
        [DisplayName("_Cor5")]
        public Color Cor5
        {
            get { return vCor5; }
            set { vCor5 = value; Invalidate(); }
        }

        private Color vCor6 = Color.Crimson;
        [DisplayName("_Cor6")]
        public Color Cor6
        {
            get { return vCor6; }
            set { vCor6 = value; Invalidate(); }
        }

        private Color vCorFundo = Color.White;
        [DisplayName("_Cor Fundo")]
        public Color CorFundo
        {
            get { return vCorFundo; }
            set { vCorFundo = value; Invalidate(); }
        }

        private Color vCorPonteiro = Color.Black;
        [DisplayName("_Cor Ponteiro")]
        public Color CorPonteiro
        {
            get { return vCorPonteiro; }
            set { vCorPonteiro = value; Invalidate(); }
        }

        private int vEspaco = 15;
        [DisplayName("_Largura do Grafico")]
        public int Espaco
        {
            get { return vEspaco; }
            set { vEspaco = value; Invalidate(); }
        }

        private float vPercentual = 0;
        [DisplayName("_Percentual")]
        public float Percentual
        {
            get { return vPercentual; }
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 100;

                targetPercentual = value; // Armazena o valor de destino para animação
                timer.Start(); // Inicia o timer para começar a animação
            }
        }

        private int vDeslocar = 0;
        [DisplayName("_Deslocamento do fundo")]
        public int Deslocar { get; set; } = 0;

        private System.Windows.Forms.Timer timer;
        private float targetPercentual;
        private float step = 1;

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Incrementa ou decrementa o valor de Percentual gradualmente até atingir o targetPercentual
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

            // Para o timer quando o valor desejado for atingido
            if (vPercentual == targetPercentual)
                timer.Stop();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            //g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF Base = ClientRectangle;

            Base.Height = Base.Height * 2;

            Base.Inflate(-1, -1);
            Base.Width--;
            Base.Height--;

            using(SolidBrush Pincel = new SolidBrush(Cor1))
            {
                int p = 180; int f = 30;
                g.FillPie(Pincel, Base, p, f);

                p += f;
                Pincel.Color = Cor2;
                g.FillPie(Pincel, Base, p, f);

                p += f;
                Pincel.Color = Cor3;
                g.FillPie(Pincel, Base, p, f);

                p += f;
                Pincel.Color = Cor4;
                g.FillPie(Pincel, Base, p, f);

                p += f;
                Pincel.Color = Cor5;
                g.FillPie(Pincel, Base, p, f);

                p += f;
                Pincel.Color = Cor6;
                g.FillPie(Pincel, Base, p, f);

                Base.Inflate(-Espaco, -Espaco);
                Pincel.Color = CorFundo;
                Base.X -= Deslocar;
                g.FillEllipse(Pincel, Base);


                Base.X += Deslocar;
                Base.Inflate(Espaco - 2, Espaco - 2);
                Pincel.Color = CorPonteiro;

                g.FillPie(Pincel, Base, 180 + (174 * (Percentual / 100)), 6);


            }
        }
    }
}
