using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing; // Adicionado para Color, Size, RectangleF
using System.Windows.Forms; // Adicionado para Control, Timer, EventArgs, PaintEventArgs
using System; // Adicionado para Math

// Supondo que ECTurbo.Codigos.Funcoes exista, embora não seja usado diretamente neste controle.
// Se houvesse, o using seria mantido: using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_Grafico1 : Control
    {
        private System.Windows.Forms.Timer animationTimer;
        private float targetPercentual;

        public ECTurbo_Grafico1()
        {
            // Configura os estilos de controle para permitir transparência e desenho personalizado
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            // Crucial: define o controle como NÃO opaco, permitindo que o fundo do pai seja visível
            SetStyle(ControlStyles.Opaque, false);

            DoubleBuffered = true;
            Size = new Size(100, 100);
            BackColor = Color.Transparent; // Define o fundo como transparente por padrão

            // Configurando o Timer para animação
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 5; // Intervalo em milissegundos
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private int vLargura = 10;
        [DisplayName("_Largura da Barra")]
        public int Largura
        {
            get { return vLargura; }
            set
            {
                if (value < 1) value = 1;
                // A largura da barra não pode ser maior que a metade da menor dimensão do controle - 2.
                // Isso evita que a barra se sobreponha completamente ao centro ou tenha problemas visuais.
                int maxLargura = Math.Min(Width, Height) / 2 - 2;
                if (value > maxLargura) value = maxLargura;

                vLargura = value;
                Invalidate();
            }
        }

        private Color vCorFundoBarra = Color.Gainsboro;
        [DisplayName("_Cor de Fundo da Barra")]
        public Color CorFundoBarra
        {
            get { return vCorFundoBarra; }
            set { vCorFundoBarra = value; Invalidate(); }
        }


        private Color vCorBarra = Color.Purple;
        [DisplayName("_Cor da Barra")]
        public Color CorBarra
        {
            get { return vCorBarra; }
            set { vCorBarra = value; Invalidate(); }
        }

        private Color vCorFundo = Color.White;
        [DisplayName("_Cor de Fundo Central")] // Nome mais claro para a propriedade
        public Color CorFundo
        {
            get { return vCorFundo; }
            set { vCorFundo = value; Invalidate(); }
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
            float step = 1.0f; // Define o tamanho do passo da animação
            if (Math.Abs(targetPercentual - vPercentual) < step)
            {
                vPercentual = targetPercentual; // Evita "overshoot" e garante que atinja o alvo
                animationTimer.Stop();
            }
            else if (vPercentual < targetPercentual)
            {
                vPercentual += step;
            }
            else // vPercentual > targetPercentual
            {
                vPercentual -= step;
            }

            Invalidate(); // Atualiza o controle para redesenho
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Remova base.OnPaint(e);
            // Remova g.Clear(BackColor); - Com ControlStyles.Opaque = false, o fundo do pai já é visível.

            Graphics g = e.Graphics;

            // Configurações para melhor renderização de gráficos e texto
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit; // Para texto mais nítido
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half; // Para melhor alinhamento de pixels

            // Retângulo base para o círculo externo
            RectangleF circleRect = ClientRectangle;

            // Inflar para ajustar o tamanho da borda e centralizar.
            // Tamanho da borda do círculo externo e o espaçamento interno.
            float padding = 1f; // Um pequeno padding para a borda externa não ser cortada
            circleRect.Inflate(-padding, -padding);


            using (SolidBrush Pincel = new SolidBrush(CorFundoBarra))
            {
                // 1. Desenha o círculo de fundo da barra (parte "vazia")
                g.FillEllipse(Pincel, circleRect);

                // 2. Desenha a parte preenchida da barra (o "progresso")
                if (vPercentual > 0)
                {
                    Pincel.Color = CorBarra;
                    // A base para o FillPie deve ser a mesma do FillEllipse do fundo da barra
                    g.FillPie(Pincel, circleRect, 270, 360 * (vPercentual / 100)); // 270 graus é o topo
                }

                // 3. Desenha o círculo de fundo central (o "buraco" do donut)
                Pincel.Color = CorFundo;
                // Infla o retângulo base para o círculo central, com base na Largura da Barra
                RectangleF innerCircleRect = circleRect;
                innerCircleRect.Inflate(-(Largura + padding), -(Largura + padding)); // Ajusta inflação para dentro

                // Garante que o círculo interno não seja muito pequeno ou negativo
                if (innerCircleRect.Width < 1) innerCircleRect.Width = 1;
                if (innerCircleRect.Height < 1) innerCircleRect.Height = 1;

                g.FillEllipse(Pincel, innerCircleRect);

                // 4. Desenha o texto do percentual
                Pincel.Color = ForeColor; // Usa a cor de texto padrão do controle
                string percentText = vPercentual.ToString("0") + "%"; // Formata para exibir sem casas decimais
                SizeF textSize = g.MeasureString(percentText, Font);

                // Centraliza o texto no controle
                float textX = (Width - textSize.Width) / 2f;
                float textY = (Height - textSize.Height) / 2f;

                // Ajuste fino visual (opcional, pode variar com a fonte)
                // textY += 1; // Experimente um pequeno ajuste se o texto parecer um pouco alto

                g.DrawString(percentText, Font, Pincel, textX, textY);
            }
        }

        // Sobrescreve OnResize para invalidar quando o controle muda de tamanho
        // Isso é importante porque a largura da barra e as posições dependem do tamanho.
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Revalida a Largura para garantir que não exceda os novos limites de tamanho
            Largura = vLargura;
            Invalidate();
        }
    }
}