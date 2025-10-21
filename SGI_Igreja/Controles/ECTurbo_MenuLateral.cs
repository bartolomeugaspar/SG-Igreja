using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ECTurbo.Controles
{
    public class ECTurbo_MenuLateral : Panel
    {
        public ECTurbo_MenuLateral()
        {
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        private int vRaio1 = 1;
        [DisplayName("_Raio Esquerda Superior")]
        public int Raio1
        {
            get { return vRaio1; }
            set
            {

                if (value < 1) value = 1;
                if (value > Height) value = Height;

                vRaio1 = value; Invalidate();
            }
        }


        private int vRaio2 = 30;
        [DisplayName("_Raio Direita Superior")]
        public int Raio2
        {
            get { return vRaio2; }
            set
            {

                if (value < 1) value = 1;
                if (value > Height) value = Height;

                vRaio2 = value; Invalidate();
            }
        }

        private int vRaio3 = 30;
        [DisplayName("_Raio Direita Inferior")]
        public int Raio3
        {
            get { return vRaio3; }
            set
            {

                if (value < 1) value = 1;
                if (value > Height) value = Height;

                vRaio3 = value; Invalidate();
            }
        }

        private int vRaio4 = 1;
        [DisplayName("_Raio Esquerda Inferior")]
        public int Raio4
        {
            get { return vRaio4; }
            set
            {

                if (value < 1) value = 1;
                if (value > Height) value = Height;

                vRaio4 = value; Invalidate();
            }
        }

        private Color vCor1 = Color.Gainsboro;
        [DisplayName("_Cor1")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }


        private Color vCor2 = Color.Gray;
        [DisplayName("_Cor2")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }

        private int vAngulo = 1;
        [DisplayName("_Angulo do Gradiente")]
        public int Angulo
        {
            get { return vAngulo; }
            set { 
                if(value < 1) value = 1;
                vAngulo = value; Invalidate(); }
        }

        private int vEspaco = 35;
        [DisplayName("_Tamanho Efeito Menu")]
        public int Espaco
        {
            get { return vEspaco; }
            set
            {

                if (value < 16) value = 16;
                if (value > 50) value = 50;

                vEspaco = value; Invalidate();
            }
        }

        private int vPosicao = 150;
        [DisplayName("_Posição do Feito no Menu")]
        public int Posicao
        {
            get { return vPosicao; }
            set { vPosicao = value; Invalidate(); }
        }


        private int vEspacoLateral = 20;
        [DisplayName("_Espaçamento Lateral")]
        public int EspacoLateral
        {
            get { return vEspacoLateral; }
            set { vEspacoLateral = value; Invalidate(); }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            //g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF Base = new RectangleF(1, 1, Raio1, Raio1);

            RectangleF Efeito = new RectangleF(1, 1, Espaco, Espaco);

            using (GraphicsPath path = new GraphicsPath())
            using (LinearGradientBrush Pincel = 
                new LinearGradientBrush(ClientRectangle, Cor1, Cor2, Angulo))
            {
                //Arco Superior Esquerdo
                path.AddArc(Base, 180, 90);

                //Arco Superior Direito
                Base.Width = Raio2;
                Base.Height = Raio2;
                Base.X = Width - Base.Width;
                path.AddArc(Base, 270, 90);

                //1º Arco do Efeito
                Efeito.X = Width - Efeito.Width;
                Efeito.Y = Posicao;                
                path.AddArc(Efeito, 0, 90);

                //2º Arco do Efeito
                Efeito.X = EspacoLateral;
                Efeito.Y = Efeito.Y + Efeito.Height;
                path.AddArc(Efeito, 270, -180);


                //Ultimo Arco do efeito
                Efeito.X = Width - Efeito.Width;
                Efeito.Y = Efeito.Y + Efeito.Height;
                path.AddArc(Efeito, 270, 90);
                //path.AddRectangle(Efeito);


                //Arco Direita Inferior
                Base.Width = Raio3;
                Base.Height = Raio3;
                Base.X = Width - Base.Width;
                Base.Y= Height - Base.Height;
                path.AddArc(Base, 0, 90);


                Base.X = 1;
                Base.Width = Raio4;
                Base.Height = Raio4;
                Base.Y = Height - Base.Height;
                path.AddArc(Base, 90, 90);

                path.CloseFigure();

                g.FillPath(Pincel, path);
            }
        }
    }
}
