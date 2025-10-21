namespace SGI_Igreja.LinhasModeloGRID
{
    partial class Lst_Modelo
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lst_Modelo));
            ecTurbo_Label1 = new ECTurbo.Controles.ECTurbo_Label();
            ecTurbo_Botao1 = new ECTurbo.Controles.ECTurbo_Botao();
            ecTurbo_Botao2 = new ECTurbo.Controles.ECTurbo_Botao();
            SuspendLayout();
            // 
            // ecTurbo_Label1
            // 
            ecTurbo_Label1.Angulo = 90;
            ecTurbo_Label1.AtivarSombra = false;
            ecTurbo_Label1.AutoSize = true;
            ecTurbo_Label1.BackColor = Color.Transparent;
            ecTurbo_Label1.Cor1 = Color.SteelBlue;
            ecTurbo_Label1.Cor2 = Color.MidnightBlue;
            ecTurbo_Label1.CorSombra = Color.Black;
            ecTurbo_Label1.EspacoTexto = 5;
            ecTurbo_Label1.IconSize = new Size(16, 16);
            ecTurbo_Label1.Location = new Point(15, 8);
            ecTurbo_Label1.Name = "ecTurbo_Label1";
            ecTurbo_Label1.QuebraTexto = false;
            ecTurbo_Label1.Size = new Size(142, 15);
            ecTurbo_Label1.TabIndex = 0;
            ecTurbo_Label1.Text = "\"ECTurbo\" Edivam Cabral";
            ecTurbo_Label1.TextAlign = ContentAlignment.MiddleLeft;
            ecTurbo_Label1.X = 2;
            ecTurbo_Label1.Y = 2;
            // 
            // ecTurbo_Botao1
            // 
            ecTurbo_Botao1.Angulo = 1;
            ecTurbo_Botao1.Arred = 20;
            ecTurbo_Botao1.AtivarSombra = false;
            ecTurbo_Botao1.BackColor = Color.Transparent;
            ecTurbo_Botao1.Cor1 = Color.Transparent;
            ecTurbo_Botao1.Cor2 = Color.Transparent;
            ecTurbo_Botao1.CorBorda = Color.Gray;
            ecTurbo_Botao1.CorSombra = Color.Black;
            ecTurbo_Botao1.DistIcone = 0;
            ecTurbo_Botao1.Image = (Image)resources.GetObject("ecTurbo_Botao1.Image");
            ecTurbo_Botao1.Location = new Point(217, 3);
            ecTurbo_Botao1.Name = "ecTurbo_Botao1";
            ecTurbo_Botao1.PosicaoImagem = ECTurbo.Controles.ECTurbo_Botao.Posicoes.Esquerda;
            ecTurbo_Botao1.Size = new Size(24, 24);
            ecTurbo_Botao1.TabIndex = 1;
            ecTurbo_Botao1.TamanhoIcone = 20;
            ecTurbo_Botao1.TamanhoSombra = 3;
            ecTurbo_Botao1.TamBorda = 0;
            ecTurbo_Botao1.UseVisualStyleBackColor = true;
            // 
            // ecTurbo_Botao2
            // 
            ecTurbo_Botao2.Angulo = 1;
            ecTurbo_Botao2.Arred = 20;
            ecTurbo_Botao2.AtivarSombra = false;
            ecTurbo_Botao2.BackColor = Color.Transparent;
            ecTurbo_Botao2.Cor1 = Color.Transparent;
            ecTurbo_Botao2.Cor2 = Color.Transparent;
            ecTurbo_Botao2.CorBorda = Color.Gray;
            ecTurbo_Botao2.CorSombra = Color.Black;
            ecTurbo_Botao2.DistIcone = 0;
            ecTurbo_Botao2.Image = (Image)resources.GetObject("ecTurbo_Botao2.Image");
            ecTurbo_Botao2.Location = new Point(247, 3);
            ecTurbo_Botao2.Name = "ecTurbo_Botao2";
            ecTurbo_Botao2.PosicaoImagem = ECTurbo.Controles.ECTurbo_Botao.Posicoes.Esquerda;
            ecTurbo_Botao2.Size = new Size(24, 24);
            ecTurbo_Botao2.TabIndex = 1;
            ecTurbo_Botao2.TamanhoIcone = 20;
            ecTurbo_Botao2.TamanhoSombra = 3;
            ecTurbo_Botao2.TamBorda = 0;
            ecTurbo_Botao2.UseVisualStyleBackColor = true;
            // 
            // Lst_Modelo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ecTurbo_Botao2);
            Controls.Add(ecTurbo_Botao1);
            Controls.Add(ecTurbo_Label1);
            Name = "Lst_Modelo";
            Size = new Size(280, 30);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label1;
        private ECTurbo.Controles.ECTurbo_Botao ecTurbo_Botao1;
        private ECTurbo.Controles.ECTurbo_Botao ecTurbo_Botao2;
    }
}
