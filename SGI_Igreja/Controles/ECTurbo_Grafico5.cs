using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ECTurbo.Controles
{
    public class ECTurbo_Grafico5 :  Control
    {
        public ECTurbo_Grafico5()
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
            Size = new Size(100, 100);
        }

        private float vPercentual = 0;
        [DisplayName("_Percentual")]
        public float Percentual
        {
            get { return vPercentual; }
            set {

                if (value < 0) value = 0;
                if (value > 100) value = 100;

                vPercentual = value;

                Invalidate();
            }
        }


        private int vEspaco = 20;
        [DisplayName("_Espaçamento Interno")]
        public int Espaco
        {
            get { return vEspaco; }
            set { vEspaco = value; Invalidate(); }
        }


        private Color vCorFundo = Color.White;
        [DisplayName("_Cor de Fundo")]
        public Color CorFundo
        {
            get { return vCorFundo; }
            set { vCorFundo = value; Invalidate(); }
        }


        private Color vCorBarra = Color.Purple;
        [DisplayName("_Cor da Barra")]
        public Color CorBarra
        {
            get { return vCorBarra; }
            set { vCorBarra = value; Invalidate(); }
        }


        private Color vCorContraBarra = Color.Gainsboro;
        [DisplayName("_Cor da Contra Barra")]
        public Color CorContraBarra
        {
            get { return vCorContraBarra; }
            set { vCorContraBarra = value; Invalidate(); }
        }


        private Color vCorFundoPontilhado = Color.White;
        [DisplayName("_Cor do Fundo Fatiado")]
        public Color CorFundoPontilhado
        {
            get { return vCorFundoPontilhado; }
            set { vCorFundoPontilhado = value; Invalidate(); }
        }

        private Image vIcone;
        [DisplayName("_Icone")]
        public Image Icone
        {
            get { return vIcone; }
            set { vIcone = value; Invalidate(); }
        }

        private int vTamanhoIcone = 24;
        [DisplayName("_Icone Tamanho")]
        public int TamanhoIcone
        {
            get { return vTamanhoIcone; }
            set {

                if (value < 8) value = 8;

                vTamanhoIcone = value; Invalidate(); }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            //g.Clear(BackColor);

            RectangleF Base = ClientRectangle;
            Base.Inflate(-1, -1);
            Base.Width--;
            Base.Height--;

            using (SolidBrush Pincel = new SolidBrush(CorContraBarra))
            {
                g.FillEllipse(Pincel, Base);

                Pincel.Color = CorBarra;

                g.FillPie(Pincel, Base, 270, 360 * (Percentual / 100));

                Pincel.Color = CorFundoPontilhado;

                for (int i = 0; i < 360; i+=10)
                {
                    g.FillPie(Pincel, Base, i, 5);
                }

                Base.Inflate(-Espaco, -Espaco);

                Pincel.Color = CorFundo;

                g.FillEllipse(Pincel, Base);

                if (Icone == null)
                {
                    Pincel.Color = ForeColor;
                    SizeF tTexto = g.MeasureString(Percentual + "%", Font);

                    g.DrawString(Percentual + "%", Font, Pincel,
                        (Width - tTexto.Width) / 2,
                        (Height - tTexto.Height) / 2);
                }
                else
                {
                    int t = TamanhoIcone;

                    g.DrawImage(Icone, (Width - t) / 2, (Height - t) / 2, t, t);
                }
            }

        }

    }
}
