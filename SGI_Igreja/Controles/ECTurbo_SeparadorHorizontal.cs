using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ECTurbo.Controles
{
    public class ECTurbo_SeparadorHorizontal : Control
    {
        public ECTurbo_SeparadorHorizontal()
        {
            DoubleBuffered = true;
            Size = new Size(150, 1);
            MinimumSize = new Size(0, 1);
            MaximumSize = new Size(0, 1);
        }

        private Color vCor = Color.Gray;
        [DisplayName("_Cor do Separador")]
        public Color Cor
        {
            get { return vCor; }
            set { vCor = value; Invalidate(); }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.Clear(BackColor);

            //g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF Base = ClientRectangle;
            Base.Width = Base.Width / 2;

            using(LinearGradientBrush Pincel = 
                new LinearGradientBrush(Base, Color.Transparent, Cor, 1))
            {
                g.FillRectangle(Pincel, Base);
            }

            Base.X = Base.Width - 1;
            using (LinearGradientBrush Pincel =
    new LinearGradientBrush(Base, Cor, Color.Transparent, 1))
            {
                g.FillRectangle(Pincel, Base);
            }
        }
    }
}
