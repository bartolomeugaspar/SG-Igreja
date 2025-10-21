using System.ComponentModel;
using System.Drawing.Drawing2D;


using ECTurbo.Codigos; // Certifique-se de que este using está presente


namespace ECTurbo.Controles
{
    public class ECTurbo_Botao : Button
    {
        public ECTurbo_Botao()
        {
            // Configura os estilos de controle para permitir transparência e desenho personalizado
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            // Crucial: define o controle como NÃO opaco, permitindo que o fundo do pai seja visível
            SetStyle(ControlStyles.Opaque, false);

            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent; // Define o fundo como transparente por padrão

        }

        private int vTamBorda = 1;
        [DisplayName("_Largura da Borda")]
        public int TamBorda
        {
            get { return vTamBorda; }
            set
            {
                if (value < 0) value = 0;
                if (value > 3) value = 3;

                vTamBorda = value; Invalidate();
            }
        }

        private int vDistIcone = 0;
        [DisplayName("_Distancia do Icone")]
        public int DistIcone
        {
            get { return vDistIcone; }
            set
            {
                if (value < 0) value = 0;
                vDistIcone = value; Invalidate();
            }
        }

        private int vArred = 20;
        [DisplayName("_Arredondamento")]
        public int Arred
        {
            get { return vArred; }
            set { vArred = value; Invalidate(); }
        }

        private Color vCorBorda = Color.Gray;
        [DisplayName("_Cor da Borda")]
        public Color CorBorda
        {
            get { return vCorBorda; }
            set { vCorBorda = value; Invalidate(); }
        }

        private Color vCor1 = Color.Purple;
        [DisplayName("_Cor de Fundo 1")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }

        private int vAngulo = 1;
        [DisplayName("_Angulo do Gradiente")]
        public int Angulo
        {
            get { return vAngulo; }
            set
            {
                if (value < 1) value = 1;

                vAngulo = value;
                Invalidate();
            }
        }

        private Color vCor2 = Color.Pink;
        [DisplayName("_Cor de Fundo 2")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }

        public enum Posicoes
        {
            Esquerda,
            Direita,
            Inferior,
            Superior
        }

        private Posicoes vPosicaoImagem = Posicoes.Esquerda;
        [DisplayName("_Posição da Imagem")]
        public Posicoes PosicaoImagem
        {
            get { return vPosicaoImagem; }
            set { vPosicaoImagem = value; Invalidate(); }
        }

        private int vTamanhoIcone = 0;
        [DisplayName("_Tamanho Icone")]
        public int TamanhoIcone
        {
            get { return vTamanhoIcone; }
            set { vTamanhoIcone = value; Invalidate(); }
        }

        private bool vAtivarSombra = false;
        [DisplayName("_Sombra Inferior - Ativar")]
        public bool AtivarSombra
        {
            get { return vAtivarSombra; }
            set { vAtivarSombra = value; Invalidate(); }
        }

        private Color vCorSombra = Color.Black;
        [DisplayName("_Sombra Inferior - Cor")]
        public Color CorSombra
        {
            get { return vCorSombra; }
            set { vCorSombra = value; Invalidate(); }
        }

        private int vTamanhoSombra = 3;
        [DisplayName("_Sombra Inferior - Cor")]
        public int TamanhoSombra
        {
            get { return vTamanhoSombra; }
            set
            {
                if (value < 1) value = 1;
                if (value > 5) value = 5;

                vTamanhoSombra = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            // Remova a chamada a base.OnPaint(pevent);
            // Remova g.Clear(BackColor);

            Graphics g = pevent.Graphics;

            // Configurações para melhor renderização de texto e gráficos
            g.SmoothingMode = SmoothingMode.AntiAlias; // Para bordas suaves do botão e imagem
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit; // Para texto mais nítido
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half; // Para melhor alinhamento de pixels

            RectangleF Base = ClientRectangle;
            if (TamBorda > 1)
                Base.Inflate(-(TamBorda / 2), -(TamBorda / 2));

            Base.Width--;
            Base.Height--;

            // Desenha a sombra primeiro, se ativada
            if (AtivarSombra == true)
            {
                // CriarPath é de Funcoes. Ajuste o namespace se necessário.
                using (GraphicsPath path = ECTurbo.Codigos.Funcoes.CriarPath(Base, Arred))
                using (SolidBrush Pincel = new SolidBrush(CorSombra))
                {
                    g.FillPath(Pincel, path);
                }
                Base.Height = Base.Height - TamanhoSombra; // Ajusta a base para o preenchimento principal
            }

            // Desenha o fundo, borda, texto e imagem
            using (GraphicsPath path = ECTurbo.Codigos.Funcoes.CriarPath(Base, Arred)) // CriarPath é de Funcoes. Ajuste o namespace se necessário.
            using (LinearGradientBrush PincelFundo =
            new LinearGradientBrush(ClientRectangle, Cor1, Cor2, Angulo))
            using (Pen Caneta = new Pen(CorBorda, TamBorda))
            using (SolidBrush PincelTexto = new SolidBrush(ForeColor)) // Usa a cor de texto padrão do botão
            {
                g.FillPath(PincelFundo, path);

                if (TamBorda > 0)
                    g.DrawPath(Caneta, path);

                SizeF TamanhoTexto = g.MeasureString(Text, Font);
                SizeF TamanhoImagem = new SizeF(0, 0);

                if (Image != null)
                {
                    TamanhoImagem = new SizeF(TamanhoIcone, TamanhoIcone);
                }

                // Calculando a posição do texto e da imagem
                float tX = 0, tY = 0, iX = 0, iY = 0;

                // Calcule a largura e altura total do conteúdo (texto + imagem)
                float totalContentWidth = TamanhoTexto.Width + (Image != null ? TamanhoIcone + DistIcone : 0);
                float totalContentHeight = Math.Max(TamanhoTexto.Height, TamanhoIcone);


                // O alinhamento vertical do conteúdo (texto e imagem combinados)
                float contentY = Base.Y + (Base.Height - totalContentHeight) / 2;

                // Alinhamento horizontal inicial para o bloco combinado de conteúdo
                float contentX = 0;
                switch (TextAlign) // TextAlign é uma propriedade herdada de Button
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.BottomLeft:
                        contentX = Base.X + Arred / 2; // Pequeno padding à esquerda
                        break;
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.BottomCenter:
                        contentX = Base.X + (Base.Width - totalContentWidth) / 2;
                        break;
                    case ContentAlignment.TopRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.BottomRight:
                        contentX = Base.Right - totalContentWidth - Arred / 2; // Pequeno padding à direita
                        break;
                }

                // Ajusta posições baseadas na PosicaoImagem
                switch (PosicaoImagem)
                {
                    case Posicoes.Esquerda:
                        iX = contentX;
                        iY = contentY + (totalContentHeight - TamanhoIcone) / 2;
                        tX = contentX + TamanhoIcone + DistIcone;
                        tY = contentY + (totalContentHeight - TamanhoTexto.Height) / 2;
                        break;
                    case Posicoes.Direita:
                        tX = contentX;
                        tY = contentY + (totalContentHeight - TamanhoTexto.Height) / 2;
                        iX = contentX + TamanhoTexto.Width + DistIcone;
                        iY = contentY + (totalContentHeight - TamanhoIcone) / 2;
                        break;
                    case Posicoes.Superior:
                        totalContentHeight = TamanhoTexto.Height + TamanhoIcone + DistIcone;
                        contentY = Base.Y + (Base.Height - totalContentHeight) / 2;

                        iX = Base.X + (Base.Width - TamanhoIcone) / 2;
                        iY = contentY;
                        tX = Base.X + (Base.Width - TamanhoTexto.Width) / 2;
                        tY = contentY + TamanhoIcone + DistIcone;
                        break;
                    case Posicoes.Inferior:
                        totalContentHeight = TamanhoTexto.Height + TamanhoIcone + DistIcone;
                        contentY = Base.Y + (Base.Height - totalContentHeight) / 2;

                        tX = Base.X + (Base.Width - TamanhoTexto.Width) / 2;
                        tY = contentY;
                        iX = Base.X + (Base.Width - TamanhoIcone) / 2;
                        iY = contentY + TamanhoTexto.Height + DistIcone;
                        break;
                }

                // Desenha o texto
                g.DrawString(Text, Font, PincelTexto, tX, tY);

                // Desenha a imagem, se existir
                if (Image != null)
                {
                    g.DrawImage(Image, iX, iY, TamanhoIcone, TamanhoIcone);
                }
            }
        }

        // --- Eventos de Mouse (mantidos como estão, mas com a correção do default para Cor1Original/Cor2Original) ---

        private Color Cor1Original = Color.Empty; // Use Color.Empty para verificar se foi inicializado
        private Color Cor2Original = Color.Empty;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (Cor1Original == Color.Empty) // Verifica se as cores originais já foram salvas
            {
                Cor1Original = Cor1;
                Cor2Original = Cor2;
            }

            Cor1 = Funcoes.CorTransparente(Cor1Original, 40); // Use Cor1Original para o cálculo
            Cor2 = Funcoes.CorTransparente(Cor2Original, 40); // Use Cor2Original para o cálculo
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            Cor1 = Cor1Original;
            Cor2 = Cor2Original;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (Cor1Original == Color.Empty)
            {
                Cor1Original = Cor1;
                Cor2Original = Cor2;
            }

            Cor1 = Funcoes.CorTransparente(Cor1Original, 20);
            Cor2 = Funcoes.CorTransparente(Cor2Original, 20);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            Cor1 = Cor1Original;
            Cor2 = Cor2Original;
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (Cor1Original == Color.Empty)
            {
                Cor1Original = Cor1;
                Cor2Original = Cor2;
            }

            Cor1 = Funcoes.CorTransparente(Cor1Original, 90);
            Cor2 = Funcoes.CorTransparente(Cor2Original, 90);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            OnMouseLeave(e);
        }
    }
}