using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System; // Adicionado para Math

// Supondo que ECTurbo.Codigos.Funcoes exista, embora não seja usado diretamente neste controle.
// Se houvesse, o using seria mantido: using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_Grafico2 : Control
    {
        private System.Windows.Forms.Timer timer;
        private float targetPercentual;
        private float step = 1.0f; // Mantenha como float para consistência

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Incrementa ou decrementa o valor de Percentual gradualmente até atingir o targetPercentual
            if (Math.Abs(targetPercentual - vPercentual) < step)
            {
                vPercentual = targetPercentual; // Evita "overshoot" e garante que atinja o alvo
                timer.Stop();
            }
            else if (vPercentual < targetPercentual)
            {
                vPercentual += step;
            }
            else // vPercentual > targetPercentual
            {
                vPercentual -= step;
            }

            Invalidate(); // Redesenha o controle
        }

        public ECTurbo_Grafico2()
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

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5; // Intervalo mais suave para animação
            timer.Tick += Timer_Tick;
        }

        private int vLarguraFundo = 10;
        [DisplayName("_Largura da Barra de Fundo")]
        public int LarguraFundo
        {
            get { return vLarguraFundo; }
            set
            {
                if (value < 4) value = 4;
                // A largura da barra não pode ser maior que a metade da menor dimensão do controle - 2.
                int maxLargura = Math.Min(Width, Height) / 2 - 2;
                if (value > maxLargura) value = maxLargura;

                vLarguraFundo = value; Invalidate();
            }
        }

        private int vLarguraBarra = 10;
        [DisplayName("_Largura da Barra com Valor")]
        public int LarguraBarra
        {
            get { return vLarguraBarra; }
            set
            {
                if (value < 1) value = 1;
                // A largura da barra não pode ser maior que a metade da menor dimensão do controle - 2.
                int maxLargura = Math.Min(Width, Height) / 2 - 2;
                if (value > maxLargura) value = maxLargura;

                vLarguraBarra = value; Invalidate();
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
        [DisplayName("_Percentual")]
        public float Percentual
        {
            get { return vPercentual; }
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 100;
                targetPercentual = value; // Armazena o valor de destino para animação
                timer.Start(); // Inicia o timer para começar a animação
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            // Remova base.OnPaint(e);
            // Remova g.Clear(BackColor);

            Graphics g = e.Graphics;

            // Configurações para melhor renderização de gráficos e texto
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit; // Para texto mais nítido
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half; // Para melhor alinhamento de pixels

            // Retângulo base para os arcos e o círculo interno
            RectangleF baseRect = ClientRectangle;

            // Calcula a inflação necessária para a borda mais externa (considerando a maior entre LarguraFundo e LarguraBarra)
            // Isso garante que o arco não seja cortado.
            float outerBorderPadding = Math.Max(LarguraFundo, LarguraBarra) / 2f + 1f; // +1f para um pequeno espaço extra
            baseRect.Inflate(-outerBorderPadding, -outerBorderPadding);

            // Garante que o retângulo base não seja negativo ou muito pequeno
            if (baseRect.Width < 1) baseRect.Width = 1;
            if (baseRect.Height < 1) baseRect.Height = 1;


            using (SolidBrush PincelFundoCentral = new SolidBrush(CorFundo))
            {
                // 1. Desenha o círculo de fundo central (o "buraco" do donut)
                // O retângulo para o círculo central será o baseRect inflado para dentro pela maior largura da barra.
                RectangleF innerCircleRect = baseRect;
                // Infla para dentro pela maior largura das barras, menos o padding já aplicado
                // para o baseRect, para criar o "buraco".
                // No seu código original, você preenchia e depois desenhava o arco de fundo.
                // Vou manter a lógica de preencher o centro e depois desenhar os arcos por cima
                // do fundo do controle (que agora é transparente).

                // Se você quer que o fundo central seja preenchido, este é o local.
                // Se CorFundo for transparente, este FillEllipse não terá efeito visual, o que é bom para transparência total.
                g.FillEllipse(PincelFundoCentral, innerCircleRect);
            }

            // 2. Desenha o arco de fundo
            using (Pen CanetaFundo = new Pen(CorFundoBarra, LarguraFundo))
            {
                // O arco de fundo cobre 360 graus
                g.DrawArc(CanetaFundo, baseRect, 0, 360);
            }

            // 3. Desenha o arco do percentual
            if (vPercentual > 0)
            {
                using (Pen CanetaBarra = new Pen(CorBarra, LarguraBarra))
                {
                    CanetaBarra.StartCap = LineCap.Round; // Pontas arredondadas
                    CanetaBarra.EndCap = LineCap.Round;   // Pontas arredondadas

                    float startAngle = 270f; // Topo do círculo
                    float sweepAngle = 360f * (vPercentual / 100f);

                    // Ajuste para evitar que os caps se sobreponham no início e no fim quando vPercentual é pequeno
                    // ou para garantir que não haja um "gap" visual quando vPercentual é próximo de 100.
                    // Este é um ajuste visual e pode precisar de refinamento dependendo do efeito desejado.
                    // A lógica original de `270 + (LarguraBarra / 2)` e `(360 - LarguraBarra - 2)`
                    // pode precisar ser revista para um comportamento mais preciso com LineCap.Round.
                    // Por agora, vamos simplificar para a matemática direta do ângulo:
                    if (vPercentual == 100)
                    {
                        // Para 100%, desenhar um círculo completo com DrawArc pode deixar um pequeno gap
                        // devido à natureza do arco. Uma alternativa seria FillPie se for um anel preenchido.
                        // Mas como é um arco, vamos desenhar 360.
                        g.DrawArc(CanetaBarra, baseRect, startAngle, 359.99f); // 359.99f para evitar problemas de fechamento
                    }
                    else
                    {
                        g.DrawArc(CanetaBarra, baseRect, startAngle, sweepAngle);
                    }
                }
            }

            // 4. Desenha o texto do percentual
            using (SolidBrush PincelTexto = new SolidBrush(ForeColor))
            {
                string percentText = vPercentual.ToString("0") + "%"; // Formata para exibir sem casas decimais
                SizeF textSize = g.MeasureString(percentText, Font);

                // Centraliza o texto no controle
                float textX = (Width - textSize.Width) / 2f;
                float textY = (Height - textSize.Height) / 2f;

                // Ajuste fino visual (opcional, pode variar com a fonte)
                // textY += 1; // Experimente um pequeno ajuste se o texto parecer um pouco alto

                g.DrawString(percentText, Font, PincelTexto, textX, textY);
            }
        }

        // Sobrescreve OnResize para invalidar quando o controle muda de tamanho
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Revalida as Larguras para garantir que não excedam os novos limites de tamanho
            LarguraFundo = vLarguraFundo;
            LarguraBarra = vLarguraBarra;
            Invalidate();
        }
    }
}