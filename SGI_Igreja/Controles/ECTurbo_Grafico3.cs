using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ECTurbo.Controles
{
    public class ECTurbo_Grafico3 : Control
    {
        private System.Windows.Forms.Timer animationTimer;
        private float targetPercentual;

        public ECTurbo_Grafico3()
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

            // Configurando o Timer para animação
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 5; // Intervalo em milissegundos
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private Color vCorFundoBarra = Color.Gainsboro;
        [DisplayName("_Cor de fundo da barra")]
        public Color CorFundoBarra
        {
            get { return vCorFundoBarra; }
            set { vCorFundoBarra = value; Invalidate(); }
        }

        private int vLarguraBarraFundo = 10;
        [DisplayName("_Largura da barra de fundo")]
        public int LarguraBarraFundo
        {
            get { return vLarguraBarraFundo; }
            set {
                
                if (value < 6) value = 6;
                if (value > Height / 2) value = Height / 2;

                vLarguraBarraFundo = value; 
                Invalidate(); 
            }
        }

        private float vPercentual = 0;
        [DisplayName("_Valor Percentual")]
        public float Percentual
        {
            get { return vPercentual; }
            set
            {
                targetPercentual = Math.Max(0, Math.Min(100, value)); // Limita o valor entre 0 e 100
                animationTimer.Start(); // Inicia a animação
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Anima o valor de vPercentual em direção ao targetPercentual
            if (vPercentual < targetPercentual)
            {
                vPercentual += 1; // Aumenta gradualmente
                if (vPercentual > targetPercentual)
                    vPercentual = targetPercentual; // Garante que não passe do alvo
            }
            else if (vPercentual > targetPercentual)
            {
                vPercentual -= 1; // Diminui gradualmente
                if (vPercentual < targetPercentual)
                    vPercentual = targetPercentual; // Garante que não passe do alvo
            }

            if (vPercentual == targetPercentual)
                animationTimer.Stop(); // Para o Timer ao atingir o valor desejado

            Invalidate(); // Atualiza o controle para redesenho
        }

        private Color vCorBarra = Color.PaleGreen;
        [DisplayName("_Cor da Barra Gradiente 1")]
        public Color CorBarra
        {
            get { return vCorBarra; }
            set { vCorBarra = value; Invalidate(); }
        }

        private Color vCorBarra2 = Color.Green;
        [DisplayName("_Cor da Barra Gradiente 2")]
        public Color CorBarra2
        {
            get { return vCorBarra2; }
            set { vCorBarra2 = value; Invalidate(); }
        }


        private int vLarguraBarra = 14;
        [DisplayName("_Largura da barra")]
        public int LarguraBarra
        {
            get { return vLarguraBarra; }
            set
            {

                if (value < 2) value = 2;
                if (value > Height / 2) value = Height / 2;

                vLarguraBarra = value;
                Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            //g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF Base = ClientRectangle;

            int t = LarguraBarraFundo / 2;
            if (LarguraBarra > LarguraBarraFundo)
                t += LarguraBarra - LarguraBarraFundo;

            Base.Inflate(-t, -t);
            Base.Width--;
            Base.Height--;

            Base.Height = Base.Height * 2;

            using(LinearGradientBrush PincelGradiente = new
                LinearGradientBrush(ClientRectangle, CorBarra, CorBarra2,1))
            using (Pen CanetaGradiente = new Pen(PincelGradiente, LarguraBarra))
            using (Pen Caneta = new Pen(CorFundoBarra, LarguraBarraFundo))
            using (SolidBrush Pincel = new SolidBrush(ForeColor))
            {
                Caneta.StartCap = LineCap.Round;
                Caneta.EndCap = LineCap.Round;

                g.DrawArc(Caneta, Base, 180, 180);

                CanetaGradiente.StartCap = LineCap.Round;
                CanetaGradiente.EndCap = LineCap.Round;
                g.DrawArc(CanetaGradiente, Base, 180, 180 * (Percentual / 100));

                SizeF tTexto = g.MeasureString(Percentual + "%", Font);

                g.DrawString(Percentual + "%", Font, Pincel,
                    (Width - tTexto.Width) / 2,
                    Height - tTexto.Height);

            }
        }
    }

}