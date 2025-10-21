using System; // Para Math
using System.ComponentModel;
using System.Drawing; // Para PointF, SizeF, RectangleF, Color, Font, Image, Graphics, StringFormat, etc.
using System.Drawing.Drawing2D; // Para LinearGradientBrush, SmoothingMode, PixelOffsetMode
using System.Windows.Forms; // Para Label, ContentAlignment, PaintEventArgs, ControlStyles

namespace ECTurbo.Controles
{
    public class ECTurbo_Label : Label
    {
        public ECTurbo_Label()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, false);

            DoubleBuffered = true;
            base.TextAlign = ContentAlignment.MiddleLeft; // Use base.TextAlign
            BackColor = Color.Transparent;
        }

        private bool vQuebraTexto = false;
        [DisplayName("_Quebra de Texto")]
        public bool QuebraTexto
        {
            get { return vQuebraTexto; }
            set { vQuebraTexto = value; Invalidate(); }
        }

        // A propriedade TextAlign já é herdada de Label.
        // Se você precisar de um comportamento diferente ao definir TextAlign,
        // você pode sobrescrevê-la com 'new' ou 'override' (se fosse virtual).
        // Por agora, vamos assumir que o comportamento padrão de Invalidate() da propriedade base é suficiente.

        private Color vCor1 = Color.SteelBlue;
        [DisplayName("_Cor 1")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }

        private Color vCor2 = Color.MidnightBlue;
        [DisplayName("_Cor 2")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }

        private int vAngulo = 90;
        [DisplayName("_Angulo do Gradiente")]
        public int Angulo
        {
            get { return vAngulo; }
            set
            {
                if (value < 0) value = 0; // Ângulo pode ser 0
                vAngulo = value;
                Invalidate();
            }
        }

        private bool vAtivarSombra = false;
        [DisplayName("_Ativar Sombra")]
        public bool AtivarSombra
        {
            get { return vAtivarSombra; }
            set { vAtivarSombra = value; Invalidate(); }
        }

        private int vX = 2;
        [DisplayName("_Distancia Sombra Eixo X")]
        public int X
        {
            get { return vX; }
            set { vX = value; Invalidate(); }
        }

        private int vY = 2;
        [DisplayName("_Distancia Sombra Eixo Y")]
        public int Y
        {
            get { return vY; }
            set { vY = value; Invalidate(); }
        }

        private Color vCorSombra = Color.Black;
        [DisplayName("_Cor da Sombra")]
        public Color CorSombra
        {
            get { return vCorSombra; }
            set { vCorSombra = value; Invalidate(); }
        }

        private int vEspacoTexto = 5;
        [DisplayName("_Espaçamento Imagem x Texto")]
        public int EspacoTexto
        {
            get { return vEspacoTexto; }
            set { vEspacoTexto = value; Invalidate(); }
        }

        // Não vamos mais usar GetTextAndImagePosition, vamos integrar a lógica no OnPaint
        // para maior clareza e controle específico para MiddleCenter.

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.SmoothingMode = SmoothingMode.AntiAlias; // Para a imagem, se houver

            Rectangle clientRect = ClientRectangle;
            if (clientRect.Width <= 0 || clientRect.Height <= 0) return;

            // Medidas
            SizeF textSize;
            if (QuebraTexto)
            {
                // Se houver imagem, a largura disponível para o texto quebrado é menor
                int availableWidthForTextWrap = clientRect.Width;
                if (Image != null)
                {
                    availableWidthForTextWrap -= (IconSize.Width + EspacoTexto + Padding.Left + Padding.Right);
                }
                else
                {
                    availableWidthForTextWrap -= (Padding.Left + Padding.Right);
                }
                if (availableWidthForTextWrap < 1) availableWidthForTextWrap = 1;
                textSize = g.MeasureString(Text, Font, availableWidthForTextWrap);
            }
            else
            {
                textSize = g.MeasureString(Text, Font);
            }

            // Preparar StringFormat para alinhamento
            StringFormat sf = new StringFormat();

            if (QuebraTexto)
            {
                // sf.Trimming = StringTrimming.Word; // Quebra por palavra já é o padrão se não houver NoWrap
            }
            else if (AutoEllipsis)
            {
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags |= StringFormatFlags.NoWrap;
            }
            else
            {
                sf.FormatFlags |= StringFormatFlags.NoWrap;
            }

            // Coordenadas de desenho
            float drawX = Padding.Left;
            float drawY = Padding.Top;
            float imageDrawX = Padding.Left;
            float imageDrawY = Padding.Top;

            RectangleF drawingRectForTextAndImage = new RectangleF(
                Padding.Left, Padding.Top,
                clientRect.Width - Padding.Horizontal,
                clientRect.Height - Padding.Vertical
            );

            // Se não houver imagem e o alinhamento for MiddleCenter, usamos uma lógica especial.
            if (Image == null && base.TextAlign == ContentAlignment.MiddleCenter)
            {
                // Para MiddleCenter e sem imagem, o retângulo de desenho para o StringFormat é o clientRect inteiro
                // (ou a área de preenchimento, se houver).
                // O StringFormat cuidará da centralização.
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                // Retângulo de desenho do texto é a área cliente menos o padding.
                RectangleF textOnlyRect = new RectangleF(Padding.Left, Padding.Top,
                                                         clientRect.Width - Padding.Horizontal,
                                                         clientRect.Height - Padding.Vertical);

                using (LinearGradientBrush Pincel = new LinearGradientBrush(clientRect, Cor1, Cor2, Angulo))
                {
                    if (AtivarSombra)
                    {
                        RectangleF shadowRect = textOnlyRect;
                        shadowRect.Offset(X, Y);
                        g.DrawString(Text, Font, new SolidBrush(CorSombra), shadowRect, sf);
                    }
                    g.DrawString(Text, Font, Pincel, textOnlyRect, sf);
                }
                return; // Desenho concluído para este caso especial
            }

            // Lógica para outros alinhamentos ou quando há imagem
            // (Esta parte é uma simplificação da sua GetTextAndImagePosition e pode precisar de ajustes)

            float contentWidth = textSize.Width;
            float contentHeight = textSize.Height;

            if (Image != null)
            {
                contentWidth += IconSize.Width + EspacoTexto;
                contentHeight = Math.Max(textSize.Height, IconSize.Height);

                // Alinhamento vertical da imagem
                imageDrawY = Padding.Top + (drawingRectForTextAndImage.Height - IconSize.Height) / 2f;
            }

            // Alinhamento Horizontal Geral do Bloco (Imagem + Texto)
            switch (base.TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    drawX = Padding.Left;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    drawX = Padding.Left + (drawingRectForTextAndImage.Width - contentWidth) / 2f;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    drawX = clientRect.Width - Padding.Right - contentWidth;
                    break;
            }

            // Alinhamento Vertical Geral do Bloco (Imagem + Texto)
            switch (base.TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    drawY = Padding.Top;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    drawY = Padding.Top + (drawingRectForTextAndImage.Height - contentHeight) / 2f;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    drawY = clientRect.Height - Padding.Bottom - contentHeight;
                    break;
            }

            // Posicionar elementos dentro do bloco
            float currentX = drawX;
            float textActualY = drawY + (contentHeight - textSize.Height) / 2f; // Centraliza texto dentro do bloco de conteúdo

            RectangleF textDrawRect;

            if (Image != null)
            {
                // Determinar qual vem primeiro baseado no ImageAlign (não temos essa prop, assumindo imagem à esquerda do texto)
                imageDrawX = currentX;
                // Ajusta imageDrawY para centralizar a imagem verticalmente dentro da altura do conteúdo
                imageDrawY = drawY + (contentHeight - IconSize.Height) / 2f;

                g.DrawImage(Image, imageDrawX, imageDrawY, IconSize.Width, IconSize.Height);
                currentX += IconSize.Width + EspacoTexto;
            }

            // O retângulo de texto começa em currentX e tem a largura restante disponível
            // ou a largura medida se não houver quebra.
            float textRectWidth = QuebraTexto ? drawingRectForTextAndImage.Width - (currentX - Padding.Left) : textSize.Width;
            if (textRectWidth < 0) textRectWidth = 0;

            textDrawRect = new RectangleF(currentX, textActualY, textRectWidth, textSize.Height);

            // Configura StringFormat para o alinhamento do texto DENTRO de seu próprio bloco/retângulo.
            // Para a maioria dos casos, exceto MiddleCenter sem imagem, o texto alinha Near/Near dentro de seu rect.
            // O alinhamento geral já foi tratado.
            sf.Alignment = StringAlignment.Near; // Texto alinha à esquerda dentro de seu retângulo de desenho
            sf.LineAlignment = StringAlignment.Near; // Texto alinha ao topo dentro de seu retângulo de desenho
            if (QuebraTexto) sf.LineAlignment = StringAlignment.Near; // Garante que texto quebrado comece do topo


            using (LinearGradientBrush Pincel = new LinearGradientBrush(clientRect, Cor1, Cor2, Angulo))
            {
                if (AtivarSombra)
                {
                    RectangleF shadowRect = textDrawRect;
                    shadowRect.Offset(X, Y);
                    // Para sombra de texto quebrado, precisamos de um retângulo, não ponto
                    if (QuebraTexto)
                        g.DrawString(Text, Font, new SolidBrush(CorSombra), shadowRect, sf);
                    else
                        g.DrawString(Text, Font, new SolidBrush(CorSombra), shadowRect.Location, sf);
                }

                if (QuebraTexto)
                    g.DrawString(Text, Font, Pincel, textDrawRect, sf);
                else
                    g.DrawString(Text, Font, Pincel, textDrawRect.Location, sf); // Usa PointF para texto não quebrado
            }
        }

        // Propriedade IconSize para compatibilidade, se você planeja adicionar imagem no futuro
        [Category("Appearance")]
        [Description("O tamanho do ícone, se uma imagem for definida.")]
        public Size IconSize { get; set; } = new Size(16, 16); // Tamanho padrão
    }
}