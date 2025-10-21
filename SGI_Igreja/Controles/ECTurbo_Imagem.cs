using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using ECTurbo.Codigos; // Mantenha este using para Funcoes
using System.ComponentModel;
using System; // Adicionado para Math.Abs

namespace ECTurbo.Controles
{
    public class ECTurbo_Imagem : PictureBox
    {
        // Nova enumeração para o estilo da borda
        public enum BorderDashStyle
        {
            Solid,
            Dash,
            Dot,
            DashDot,
            DashDotDot
        }

        public ECTurbo_Imagem()
        {
            // Configura os estilos de controle para permitir transparência e desenho personalizado
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            // Crucial: define o controle como NÃO opaco, permitindo que o fundo do pai seja visível
            SetStyle(ControlStyles.Opaque, false);

            DoubleBuffered = true;
            Size = new Size(50, 50);
            BackColor = Color.Transparent; // Define o fundo como transparente por padrão
            Tag = "";
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

        private string vPadrao;
        [DisplayName("(DB.2 Foto Padrão)")]
        public string Padrao
        {
            get { return vPadrao; }
            set
            {
                vPadrao = value;
                string valor = Funcoes.PegarTag(this, "padrao");
                Tag = Tag.ToString().Replace("|padrao=" + valor, "");
                if (string.IsNullOrEmpty(value) == false)
                    Tag += "|padrao=" + value;
            }
        }

        private bool vObgt = false;
        [DisplayName("(DB.3 Campo Obrigatório)")]
        public bool Obgt
        {
            get { return vObgt; }
            set
            {
                vObgt = value;
                if (value == true)
                {
                    if (!Tag.ToString().Contains("|obgt"))
                        Tag += "|obgt";
                }
                else
                    Tag = Tag.ToString().Replace("|obgt", "");
            }
        }

        private int vRaio1 = 10;
        [DisplayName("_Canto Superior Esquerdo")]
        public int Raio1
        {
            get { return vRaio1; }
            set { vRaio1 = value; Invalidate(); }
        }

        private int vRaio2 = 10;
        [DisplayName("_Canto Superior Direito")]
        public int Raio2
        {
            get { return vRaio2; }
            set { vRaio2 = value; Invalidate(); }
        }

        private int vRaio3 = 10;
        [DisplayName("_Canto Inferior Direito")]
        public int Raio3
        {
            get { return vRaio3; }
            set { vRaio3 = value; Invalidate(); }
        }

        private int vRaio4 = 10;
        [DisplayName("_Canto Inferior Esquerdo")]
        public int Raio4
        {
            get { return vRaio4; }
            set { vRaio4 = value; Invalidate(); }
        }

        private int vTamanhoBorda = 1;
        [DisplayName("_Tamanho da Borda")]
        public int TamanhoBorda
        {
            get { return vTamanhoBorda; }
            set { vTamanhoBorda = value; Invalidate(); }
        }

        private Color vCor1 = Color.Red;
        [DisplayName("_Borda Cor 1")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }

        private Color vCor2 = Color.Blue;
        [DisplayName("_Borda Cor 2")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }

        // Nova propriedade para o estilo da borda
        private BorderDashStyle vBordaEstilo = BorderDashStyle.Solid;
        [DisplayName("_Estilo da Borda")]
        public BorderDashStyle BordaEstilo
        {
            get { return vBordaEstilo; }
            set { vBordaEstilo = value; Invalidate(); }
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            // NÃO chame base.OnPaint(pe);
            // NÃO chame g.Clear(BackColor);

            Graphics g = pe.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias; // Para bordas e imagens suaves

            RectangleF Base = ClientRectangle;

            // Ajusta o retângulo para a borda. Use float para precisão.
            float inflateAmount = TamanhoBorda / 2f;
            if (TamanhoBorda > 0)
            {
                Base.Inflate(-inflateAmount -1, -inflateAmount - 1);
            }

            // Garante que o retângulo não tenha dimensões zero ou negativas
            if (Base.Width < 1) Base.Width = 1;
            if (Base.Height < 1) Base.Height = 1;

            // Define o DashStyle da caneta com base na propriedade BordaEstilo
            DashStyle dashStyle = DashStyle.Solid;
            switch (BordaEstilo)
            {
                case BorderDashStyle.Dash:
                    dashStyle = DashStyle.Dash;
                    break;
                case BorderDashStyle.Dot:
                    dashStyle = DashStyle.Dot;
                    break;
                case BorderDashStyle.DashDot:
                    dashStyle = DashStyle.DashDot;
                    break;
                case BorderDashStyle.DashDotDot:
                    dashStyle = DashStyle.DashDotDot;
                    break;
                case BorderDashStyle.Solid:
                default:
                    dashStyle = DashStyle.Solid;
                    break;
            }

            // Determina se o objetivo é desenhar um círculo perfeito
            // É um círculo se o controle é um quadrado E os raios são grandes o suficiente para formá-lo.
            // Uma pequena tolerância (0.1f) é usada para a comparação de floats.
            bool isCircular = (Math.Abs(Base.Width - Base.Height) < 0.1f &&
                               Raio1 >= (Base.Width / 2f - 1f) &&
                               Raio2 >= (Base.Width / 2f - 1f) &&
                               Raio3 >= (Base.Width / 2f - 1f) &&
                               Raio4 >= (Base.Width / 2f - 1f));

            using (LinearGradientBrush Pincel =
                new LinearGradientBrush(ClientRectangle, Cor1, Cor2, 135))
            using (Pen Caneta = new Pen(Pincel, TamanhoBorda))
            {
                Caneta.DashStyle = dashStyle; // Aplica o estilo de borda

                if (isCircular)
                {
                    // Se for um círculo perfeito, usa a função AddEllipse diretamente
                    using (GraphicsPath circlePath = new GraphicsPath())
                    {
                        circlePath.AddEllipse(Base); // Cria um caminho de elipse perfeito

                        if (Image != null)
                        {
                            g.SetClip(circlePath); // Recorta a imagem para o caminho circular
                            g.DrawImage(Image, Base.X, Base.Y, Base.Width, Base.Height);
                            g.ResetClip(); // Reseta o recorte para desenhar a borda em todo o path
                        }

                        // Desenha a borda do círculo
                        if (TamanhoBorda > 0)
                        {
                            g.DrawEllipse(Caneta, Base);
                        }
                    }
                }
                else
                {
                    // Se não for um círculo perfeito (é um retângulo arredondado), usa a função CriarPath
                    // Assumindo que Funcoes.CriarPath recebe 4 raios (topLeft, topRight, bottomRight, bottomLeft)
                    using (GraphicsPath roundedRectPath = ECTurbo.Codigos.Funcoes.CriarPath(Base, Raio1, Raio2, Raio3, Raio4))
                    {
                        if (Image != null)
                        {
                            g.SetClip(roundedRectPath); // Recorta a imagem para o caminho do retângulo arredondado
                            g.DrawImage(Image, Base.X, Base.Y, Base.Width, Base.Height);
                            g.ResetClip(); // Reseta o recorte para desenhar a borda em todo o path
                        }

                        // Desenha a borda do retângulo arredondado
                        if (TamanhoBorda > 0)
                        {
                            g.DrawPath(Caneta, roundedRectPath);
                        }
                    }
                }
            }
        }
    }
}