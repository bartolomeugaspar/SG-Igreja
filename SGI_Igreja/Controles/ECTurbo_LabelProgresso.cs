using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using ECTurbo.Codigos; // Supondo que Funcoes está aqui

namespace ECTurbo.Controles
{
    public class ECTurbo_LabelProgresso : Label
    {
        public ECTurbo_LabelProgresso()
        {
            // Estilos para suportar transparência e pintura customizada
            SetStyle(ControlStyles.UserPaint |             // Indica que o controle se pinta sozinho
                     ControlStyles.AllPaintingInWmPaint |  // Reduz flicker
                     ControlStyles.OptimizedDoubleBuffer | // Usa double buffering para pintura suave
                     ControlStyles.ResizeRedraw |          // Redesenha ao ser redimensionado
                     ControlStyles.SupportsTransparentBackColor, true); // Habilita BackColor transparente
            SetStyle(ControlStyles.Opaque, false);         // Indica que o controle não é opaco

            this.BackColor = Color.Transparent; // <--- Define a cor de fundo como transparente

            // DoubleBuffered já estava, mas OptimizedDoubleBuffer é geralmente melhor e cobre isso.
            // DoubleBuffered = true;

            // Definir um texto padrão ao adicionar ao formulário, se desejado
            this.Text = "Progresso";
        }

        // Para definir o texto padrão no designer (opcional, mas bom para consistência)
        [DefaultValue("Progresso")]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; Invalidate(); /* Invalidate para redesenhar se o texto mudar */ }
        }

        private bool vImagemDireita = false;
        [DisplayName("_Alinhar Imagem à Direita")]
        [Category("Aparência Customizada")]
        public bool ImagemDireita
        {
            get { return vImagemDireita; }
            set { vImagemDireita = value; Invalidate(); }
        }

        private bool vTextoDireita = false;
        [DisplayName("_Alinhar Texto à Direita")]
        [Category("Aparência Customizada")]
        public bool TextoDireita
        {
            get { return vTextoDireita; }
            set { vTextoDireita = value; Invalidate(); }
        }

        private float vPercentual = 33;
        [DisplayName("_Valor Percentual (0-100)")]
        [Category("Aparência Customizada")]
        [DefaultValue(33f)]
        public float Percentual
        {
            get { return vPercentual; }
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 100;
                vPercentual = value; Invalidate();
            }
        }

        private int vEspaco = 1;
        [DisplayName("_Espaçamento Interno (Barra)")]
        [Category("Aparência Customizada")]
        [DefaultValue(1)]
        public int Espaco
        {
            get { return vEspaco; }
            set
            {
                if (value < 0) value = 0;
                vEspaco = value;
                Invalidate();
            }
        }

        private int vNivel1 = 25;
        [DisplayName("_Nivel 1 até (%)")]
        [Category("Cores Progresso")]
        [DefaultValue(25)]
        public int Nivel1
        {
            get { return vNivel1; }
            set
            {
                if (value < 1) value = 1;
                vNivel1 = value;
                Invalidate();
            }
        }

        private int vNivel2 = 50;
        [DisplayName("_Nivel 2 até (%)")]
        [Category("Cores Progresso")]
        [DefaultValue(50)]
        public int Nivel2
        {
            get { return vNivel2; }
            set
            {
                if (value < Nivel1 + 1) value = Nivel1 + 1;
                vNivel2 = value;
                Invalidate();
            }
        }

        private int vNivel3 = 75;
        [DisplayName("_Nivel 3 até (%)")]
        [Category("Cores Progresso")]
        [DefaultValue(75)]
        public int Nivel3
        {
            get { return vNivel3; }
            set
            {
                if (value < Nivel2 + 1) value = Nivel2 + 1;
                vNivel3 = value;
                Invalidate();
            }
        }

        private Color vCor1 = Color.PaleGreen;
        [DisplayName("_Nivel 1 Cor")]
        [Category("Cores Progresso")]
        public Color Cor1
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }

        private Color vCor2 = Color.Green;
        [DisplayName("_Nivel 2 Cor")]
        [Category("Cores Progresso")]
        public Color Cor2
        {
            get { return vCor2; }
            set { vCor2 = value; Invalidate(); }
        }

        private Color vCor3 = Color.Orange;
        [DisplayName("_Nivel 3 Cor")]
        [Category("Cores Progresso")]
        public Color Cor3
        {
            get { return vCor3; }
            set { vCor3 = value; Invalidate(); }
        }

        private Color vCor4 = Color.Red;
        [DisplayName("_Nivel 4 Cor (Acima do Nivel 3)")]
        [Category("Cores Progresso")]
        public Color Cor4
        {
            get { return vCor4; }
            set { vCor4 = value; Invalidate(); }
        }

        private int vTamanhoIcone = 16;
        [DisplayName("_Tamanho do Icone")]
        [Category("Aparência Customizada")]
        [DefaultValue(16)]
        public int TamanhoIcone
        {
            get { return vTamanhoIcone; }
            set
            {
                if (value < 10) value = 10;
                vTamanhoIcone = value;
                Invalidate();
            }
        }

        // Para que Cor1, Cor2, etc., tenham valores padrão no designer
        // É preciso definir um valor inicial e também o DefaultValue, ou usar métodos ShouldSerialize/Reset.
        // Exemplo para Cor1 (aplicar similarmente às outras cores se desejar reset no designer):
        [DefaultValue(typeof(Color), "PaleGreen")]
        public Color Cor1_Designer // Propriedade separada para designer se necessário, ou aplicar diretamente
        {
            get { return vCor1; }
            set { vCor1 = value; Invalidate(); }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            // NÃO chamar base.OnPaint(e) se você está fazendo toda a pintura.
            // base.OnPaint(e); // <--- REMOVIDO OU COMENTADO

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // NÃO limpar com BackColor se o objetivo é transparência.
            // g.Clear(BackColor); // <--- REMOVIDO

            // O fundo do controle pai será visível aqui.

            RectangleF areaTotalCliente = ClientRectangle;
            RectangleF areaBarra = areaTotalCliente;
            areaBarra.Inflate(0, -Espaco); // Aplica espaçamento vertical para a barra

            if (areaBarra.Height < 4) areaBarra.Height = 4; // Altura mínima para a barra

            // Cópias para cálculo, para não modificar o original antes da hora
            RectangleF barraProgressoRect = areaBarra;

            SizeF tamanhoTexto = g.MeasureString(this.Text, Font);
            float alturaIcone = (Image != null) ? TamanhoIcone : 0;
            float larguraIcone = alturaIcone; // Supondo ícone quadrado

            // Calcular posições X para texto e imagem
            float textoX, imageX = 0;
            float espacamentoImagemTexto = 4; // Espaço entre imagem e texto, ou borda e elemento

            if (Image != null)
            {
                // Ajusta a área disponível para a barra de progresso se houver imagem
                barraProgressoRect.Width -= (larguraIcone + espacamentoImagemTexto);
                if (ImagemDireita)
                {
                    imageX = areaTotalCliente.Width - larguraIcone - 2; // 2 para padding da borda direita
                    barraProgressoRect.X = 2; // Barra começa da esquerda
                }
                else // Imagem à esquerda
                {
                    imageX = 2; // 2 para padding da borda esquerda
                    barraProgressoRect.X = imageX + larguraIcone + espacamentoImagemTexto;
                }
            }
            else
            {
                // Sem imagem, barra ocupa mais espaço horizontal
                barraProgressoRect.X = 2; // Pequeno padding inicial
                barraProgressoRect.Width -= 4; // Paddings laterais
            }


            // Posição do Texto
            if (TextoDireita)
            {
                textoX = areaTotalCliente.Width - tamanhoTexto.Width - espacamentoImagemTexto;
                if (Image != null && ImagemDireita) // Se imagem também à direita, texto à esquerda da imagem
                {
                    textoX = imageX - tamanhoTexto.Width - espacamentoImagemTexto;
                }
            }
            else // Texto à esquerda
            {
                textoX = espacamentoImagemTexto;
                if (Image != null && !ImagemDireita) // Se imagem também à esquerda, texto à direita da imagem
                {
                    textoX = imageX + larguraIcone + espacamentoImagemTexto;
                }
            }
            // Garante que o texto não saia da área visível
            if (textoX < 2) textoX = 2;
            if (textoX + tamanhoTexto.Width > areaTotalCliente.Width - 2) textoX = areaTotalCliente.Width - tamanhoTexto.Width - 2;


            // Desenha a barra de progresso
            if (Percentual > 0)
            {
                RectangleF barraPreenchida = barraProgressoRect;
                barraPreenchida.Width *= (Percentual / 100f);

                if (barraPreenchida.Width > 0) // Só desenha se houver algo para preencher
                {
                    Color corProgresso = Cor1;
                    if (Percentual > Nivel1) corProgresso = Cor2;
                    if (Percentual > Nivel2) corProgresso = Cor3;
                    if (Percentual > Nivel3) corProgresso = Cor4;

                    using (SolidBrush pincelProgresso = new SolidBrush(corProgresso))
                    using (GraphicsPath pathBarra = Funcoes.CriarPath(barraPreenchida, 6)) // 6 é o arredondamento da barra
                    {
                        g.FillPath(pincelProgresso, pathBarra);
                    }
                }
            }

            // Desenha a Imagem
            if (Image != null)
            {
                float imageY = (areaTotalCliente.Height - alturaIcone) / 2;
                g.DrawImage(Image, imageX, imageY, larguraIcone, alturaIcone);
            }

            // Desenha o Texto
            if (!string.IsNullOrEmpty(this.Text))
            {
                float textoY = (areaTotalCliente.Height - tamanhoTexto.Height) / 2;
                using (SolidBrush pincelTexto = new SolidBrush(ForeColor))
                {
                    g.DrawString(this.Text, Font, pincelTexto, textoX, textoY);
                }
            }
        }
    }
}