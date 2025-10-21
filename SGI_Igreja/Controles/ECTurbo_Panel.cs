using System.Drawing.Drawing2D;
using ECTurbo.Codigos;
using System.ComponentModel;


namespace ECTurbo.Controles
{
    public class ECTurbo_Panel : Panel
    {
        public ECTurbo_Panel()
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
        }

        private int vRaio1 = 10;
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


        private int vRaio2 = 10;
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

        private int vRaio3 = 10;
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

        private int vRaio4 = 10;
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

        private Color vCorBorda = Color.Gray;
        [DisplayName("_Borda Cor")]
        public Color CorBorda
        {
            get { return vCorBorda; }
            set { vCorBorda = value; Invalidate(); }
        }

        private int vTamanhoBorda;
        [DisplayName("_Borda Tamanho")]
        public int TamanhoBorda
        {
            get { return vTamanhoBorda; }
            set
            {
                if (value < 0) value = 0;
                vTamanhoBorda = value;
                Invalidate();
            }
        }


        private Image vImagem;
        [DisplayName("_Imagem")]
        public Image Imagem
        {
            get { return vImagem; }
            set { vImagem = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(Parent.BackColor);

            RectangleF Base = ClientRectangle;

            int t = TamanhoBorda / 2 < 1 ? 1 : TamanhoBorda / 2;

            if (AtivarSombra == true)
                t = t + Espacamento;

            Base.Inflate(-t, -t);
            Base.Width--;
            Base.Height--;

            Base.Height -= PanelEspacoSuperior;
            Base.Y += PanelEspacoSuperior;

            EfeitoSombra(g, Base);

            using (GraphicsPath path = Funcoes.CriarPath(Base, 1, Raio1, Raio2, Raio3, Raio4))
            using (Pen Caneta = new Pen(CorBorda, TamanhoBorda))
            using (SolidBrush Pincel = new SolidBrush(BackColor))
            {
                g.FillPath(Pincel, path);
                if (TamanhoBorda > 0)
                    g.DrawPath(Caneta, path);
            }


            if (Imagem != null)
            {
                int vXImagem = EspImagem;
                float aspectRatio = (float)Imagem.Width / Imagem.Height;
                int alturaImagem = Height - 10; // Altura desejada, com margem de 5 pixels em cima e embaixo
                int larguraImagem = (int)(alturaImagem * aspectRatio); // Calcula a largura proporcional

                // Ajuste da posição horizontal conforme a posição escolhida
                if (vPosicaoImagem == PosImagem.Centro)
                    vXImagem = (Width - larguraImagem) / 2;
                else if (vPosicaoImagem == PosImagem.Direita)
                    vXImagem = Width - larguraImagem - EspImagem;

                // Desenha a imagem com a largura proporcional
                g.DrawImage(Imagem, vXImagem, 5, larguraImagem, alturaImagem);
            }


        }



        public enum PosImagem
        {
            Centro,
            Esquerda,
            Direita
        }
        private int vEspImagem = 10;
        [DisplayName("_Imagem Espaçamento")]
        public int EspImagem
        {
            get { return vEspImagem; }
            set { vEspImagem = value; Invalidate(); }
        }


        private PosImagem vPosicaoImagem = PosImagem.Esquerda;
        [DisplayName("_Imagem Posição")]
        public PosImagem PosicaoImagem
        {
            get { return vPosicaoImagem; }
            set { vPosicaoImagem = value; Invalidate(); }
        }


        private int vPanelEspacoSuperior = 0;
        [DisplayName("_Espaço Superior")]
        public int PanelEspacoSuperior
        {
            get { return vPanelEspacoSuperior; }
            set { vPanelEspacoSuperior = value; Invalidate(); }
        }


        // INICIO - EFEITO SOMBRA
        private bool vAtivarSombra = false;
        [Category("_ECTurbo")]
        [DisplayName("_Sombra Ativar")]
        public bool AtivarSombra
        {
            get { return vAtivarSombra; }
            set { vAtivarSombra = value; Invalidate(); }
        }

        private int vEspacamento = 0;
        [DisplayName("_Sombra Espaçamento")]
        public int Espacamento
        {
            get { return vEspacamento; }
            set { vEspacamento = value; Invalidate(); }
        }

        private float vSombraForca = 8;
        [DisplayName("_Sombra Intensidade")]
        public float SombraForca
        {
            get { return vSombraForca; }
            set { vSombraForca = value; Invalidate(); }
        }

        private Color vSombraCor = Color.Black;
        [DisplayName("_Sombra Cor")]
        public Color SombraCor
        {
            get { return vSombraCor; }
            set { vSombraCor = value; Invalidate(); }
        }

        private void EfeitoSombra(Graphics g, RectangleF r)
        {
            if (AtivarSombra == false)
                return;

            float shadowBlur = SombraForca;
            Color shadowColor = Color.FromArgb(128, SombraCor);

            RectangleF shadowRect = new RectangleF(r.X - shadowBlur, r.Y - shadowBlur, r.Width + 2 * shadowBlur, r.Height + 2 * shadowBlur);

            using (GraphicsPath shadowPath = Funcoes.CriarPath(shadowRect, 1, Raio1, Raio2, Raio3, Raio4))
            using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
            {
                shadowBrush.CenterColor = shadowColor;
                shadowBrush.SurroundColors = new Color[] { Color.Transparent };
                shadowBrush.CenterPoint = new PointF(r.X + r.Width / 2, r.Y + r.Height / 2);
                shadowBrush.FocusScales = new PointF(0.5f, 0.5f);

                g.FillPath(shadowBrush, shadowPath);
            }
        }
        // FIM - EFEITO SOMBRA

    }
}