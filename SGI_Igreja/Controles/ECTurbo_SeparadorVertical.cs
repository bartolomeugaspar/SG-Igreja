using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ECTurbo.Controles
{
    public class ECTurbo_SeparadorVertical : Control
    {
        public ECTurbo_SeparadorVertical()
        {
            DoubleBuffered = true;
            Size = new Size(1, 150);
            MinimumSize = new Size(1, 0);
            MaximumSize = new Size(1, 0);
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
            Base.Height = Base.Height / 2;

            using (LinearGradientBrush Pincel =
                new LinearGradientBrush(Base, Color.Transparent, Cor, 90))
            {
                g.FillRectangle(Pincel, Base);
            }

            Base.Y = Base.Height - 1;
            using (LinearGradientBrush Pincel =
    new LinearGradientBrush(Base, Cor, Color.Transparent, 90))
            {
                g.FillRectangle(Pincel, Base);
            }
        }
    }
}

