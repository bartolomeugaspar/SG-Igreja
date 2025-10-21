using System.Drawing;
using System.Windows.Forms;

namespace ECTurbo.Formularios
{
    partial class FormMsg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMsg));
            this.MpConteudo = new System.Windows.Forms.TabControl();
            this.pagSucesso = new System.Windows.Forms.TabPage();
            this.MsgSucesso = new ECTurbo.Controles.ECTurbo_Label();
            this.TituloSucesso = new ECTurbo.Controles.ECTurbo_Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pagAlertas = new System.Windows.Forms.TabPage();
            this.MsgAlerta = new ECTurbo.Controles.ECTurbo_Label();
            this.TituloALerta = new ECTurbo.Controles.ECTurbo_Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pagErros = new System.Windows.Forms.TabPage();
            this.MsgErro = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.TituloErro = new ECTurbo.Controles.ECTurbo_Label();
            this.pagConfirmar = new System.Windows.Forms.TabPage();
            this.MsgPergunta = new ECTurbo.Controles.ECTurbo_Label();
            this.TituloPergunta = new ECTurbo.Controles.ECTurbo_Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.BtOkSucesso = new System.Windows.Forms.Button();
            this.BtOkAlertas = new System.Windows.Forms.Button();
            this.BtOkErros = new System.Windows.Forms.Button();
            this.BtSim = new System.Windows.Forms.Button();
            this.BtNao = new System.Windows.Forms.Button();
            this.MpConteudo.SuspendLayout();
            this.pagSucesso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pagAlertas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pagErros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pagConfirmar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // MpConteudo
            // 
            this.MpConteudo.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.MpConteudo.Controls.Add(this.pagSucesso);
            this.MpConteudo.Controls.Add(this.pagAlertas);
            this.MpConteudo.Controls.Add(this.pagErros);
            this.MpConteudo.Controls.Add(this.pagConfirmar);
            this.MpConteudo.Location = new System.Drawing.Point(-5, -9);
            this.MpConteudo.Name = "MpConteudo";
            this.MpConteudo.SelectedIndex = 0;
            this.MpConteudo.Size = new System.Drawing.Size(440, 256);
            this.MpConteudo.TabIndex = 0;
            // 
            // pagSucesso
            // 
            this.pagSucesso.BackColor = System.Drawing.Color.White;
            this.pagSucesso.Controls.Add(this.BtOkSucesso);
            this.pagSucesso.Controls.Add(this.MsgSucesso);
            this.pagSucesso.Controls.Add(this.TituloSucesso);
            this.pagSucesso.Controls.Add(this.pictureBox1);
            this.pagSucesso.Location = new System.Drawing.Point(4, 4);
            this.pagSucesso.Name = "pagSucesso";
            this.pagSucesso.Padding = new System.Windows.Forms.Padding(3);
            this.pagSucesso.Size = new System.Drawing.Size(432, 230);
            this.pagSucesso.TabIndex = 0;
            this.pagSucesso.Text = "Sucessos";
            // 
            // MsgSucesso
            // 
            this.MsgSucesso.Angulo = 45;
            this.MsgSucesso.AtivarSombra = false;
            this.MsgSucesso.Cor1 = System.Drawing.Color.Teal;
            this.MsgSucesso.Cor2 = System.Drawing.Color.Green;
            this.MsgSucesso.CorSombra = System.Drawing.Color.Black;
            this.MsgSucesso.EspacoTexto = 5;
            this.MsgSucesso.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MsgSucesso.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.MsgSucesso.Location = new System.Drawing.Point(166, 68);
            this.MsgSucesso.Name = "MsgSucesso";
            this.MsgSucesso.QuebraTexto = true;
            this.MsgSucesso.Size = new System.Drawing.Size(247, 98);
            this.MsgSucesso.TabIndex = 3;
            this.MsgSucesso.Text = "Aqui vai o texto referente a mensagem desejada";
            this.MsgSucesso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MsgSucesso.X = 1;
            this.MsgSucesso.Y = 1;
            // 
            // TituloSucesso
            // 
            this.TituloSucesso.Angulo = 1;
            this.TituloSucesso.AtivarSombra = true;
            this.TituloSucesso.Cor1 = System.Drawing.Color.DeepSkyBlue;
            this.TituloSucesso.Cor2 = System.Drawing.Color.SpringGreen;
            this.TituloSucesso.CorSombra = System.Drawing.Color.DarkGreen;
            this.TituloSucesso.EspacoTexto = 5;
            this.TituloSucesso.Font = new System.Drawing.Font("Showcard Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TituloSucesso.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.TituloSucesso.Location = new System.Drawing.Point(33, 19);
            this.TituloSucesso.Name = "TituloSucesso";
            this.TituloSucesso.QuebraTexto = false;
            this.TituloSucesso.Size = new System.Drawing.Size(389, 46);
            this.TituloSucesso.TabIndex = 0;
            this.TituloSucesso.Text = "SUCESSO!";
            this.TituloSucesso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TituloSucesso.X = 2;
            this.TituloSucesso.Y = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 140);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pagAlertas
            // 
            this.pagAlertas.BackColor = System.Drawing.Color.White;
            this.pagAlertas.Controls.Add(this.BtOkAlertas);
            this.pagAlertas.Controls.Add(this.MsgAlerta);
            this.pagAlertas.Controls.Add(this.TituloALerta);
            this.pagAlertas.Controls.Add(this.pictureBox2);
            this.pagAlertas.Location = new System.Drawing.Point(4, 4);
            this.pagAlertas.Name = "pagAlertas";
            this.pagAlertas.Padding = new System.Windows.Forms.Padding(3);
            this.pagAlertas.Size = new System.Drawing.Size(432, 230);
            this.pagAlertas.TabIndex = 1;
            this.pagAlertas.Text = "Alertas";
            // 
            // MsgAlerta
            // 
            this.MsgAlerta.Angulo = 45;
            this.MsgAlerta.AtivarSombra = false;
            this.MsgAlerta.Cor1 = System.Drawing.Color.Orange;
            this.MsgAlerta.Cor2 = System.Drawing.Color.Salmon;
            this.MsgAlerta.CorSombra = System.Drawing.Color.Black;
            this.MsgAlerta.EspacoTexto = 5;
            this.MsgAlerta.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MsgAlerta.ForeColor = System.Drawing.Color.DarkOrange;
            this.MsgAlerta.Location = new System.Drawing.Point(166, 68);
            this.MsgAlerta.Name = "MsgAlerta";
            this.MsgAlerta.QuebraTexto = true;
            this.MsgAlerta.Size = new System.Drawing.Size(247, 98);
            this.MsgAlerta.TabIndex = 7;
            this.MsgAlerta.Text = "Aqui vai o texto referente a mensagem desejada";
            this.MsgAlerta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MsgAlerta.X = 2;
            this.MsgAlerta.Y = 2;
            // 
            // TituloALerta
            // 
            this.TituloALerta.Angulo = 90;
            this.TituloALerta.AtivarSombra = true;
            this.TituloALerta.Cor1 = System.Drawing.Color.DarkOrange;
            this.TituloALerta.Cor2 = System.Drawing.Color.Crimson;
            this.TituloALerta.CorSombra = System.Drawing.Color.Indigo;
            this.TituloALerta.EspacoTexto = 5;
            this.TituloALerta.Font = new System.Drawing.Font("Showcard Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TituloALerta.ForeColor = System.Drawing.Color.DarkOrange;
            this.TituloALerta.Location = new System.Drawing.Point(33, 19);
            this.TituloALerta.Name = "TituloALerta";
            this.TituloALerta.QuebraTexto = false;
            this.TituloALerta.Size = new System.Drawing.Size(389, 46);
            this.TituloALerta.TabIndex = 4;
            this.TituloALerta.Text = "ATENÇÃO!";
            this.TituloALerta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TituloALerta.X = 2;
            this.TituloALerta.Y = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(9, 56);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(152, 140);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pagErros
            // 
            this.pagErros.BackColor = System.Drawing.Color.White;
            this.pagErros.Controls.Add(this.BtOkErros);
            this.pagErros.Controls.Add(this.MsgErro);
            this.pagErros.Controls.Add(this.pictureBox3);
            this.pagErros.Controls.Add(this.TituloErro);
            this.pagErros.Location = new System.Drawing.Point(4, 4);
            this.pagErros.Name = "pagErros";
            this.pagErros.Padding = new System.Windows.Forms.Padding(3);
            this.pagErros.Size = new System.Drawing.Size(432, 230);
            this.pagErros.TabIndex = 2;
            this.pagErros.Text = "Erros";
            // 
            // MsgErro
            // 
            this.MsgErro.BackColor = System.Drawing.Color.White;
            this.MsgErro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgErro.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MsgErro.ForeColor = System.Drawing.Color.Crimson;
            this.MsgErro.Location = new System.Drawing.Point(173, 83);
            this.MsgErro.Multiline = true;
            this.MsgErro.Name = "MsgErro";
            this.MsgErro.ReadOnly = true;
            this.MsgErro.Size = new System.Drawing.Size(240, 80);
            this.MsgErro.TabIndex = 7;
            this.MsgErro.Text = "Aqui vai o texto referente a mensagem desejada";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(9, 56);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(152, 140);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // TituloErro
            // 
            this.TituloErro.Angulo = 30;
            this.TituloErro.AtivarSombra = true;
            this.TituloErro.Cor1 = System.Drawing.Color.BlueViolet;
            this.TituloErro.Cor2 = System.Drawing.Color.Red;
            this.TituloErro.CorSombra = System.Drawing.Color.Black;
            this.TituloErro.EspacoTexto = 5;
            this.TituloErro.Font = new System.Drawing.Font("Showcard Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TituloErro.ForeColor = System.Drawing.Color.Crimson;
            this.TituloErro.Location = new System.Drawing.Point(33, 19);
            this.TituloErro.Name = "TituloErro";
            this.TituloErro.QuebraTexto = false;
            this.TituloErro.Size = new System.Drawing.Size(389, 46);
            this.TituloErro.TabIndex = 4;
            this.TituloErro.Text = "ERRO!";
            this.TituloErro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TituloErro.X = 2;
            this.TituloErro.Y = 2;
            // 
            // pagConfirmar
            // 
            this.pagConfirmar.BackColor = System.Drawing.Color.White;
            this.pagConfirmar.Controls.Add(this.BtSim);
            this.pagConfirmar.Controls.Add(this.BtNao);
            this.pagConfirmar.Controls.Add(this.MsgPergunta);
            this.pagConfirmar.Controls.Add(this.TituloPergunta);
            this.pagConfirmar.Controls.Add(this.pictureBox4);
            this.pagConfirmar.Location = new System.Drawing.Point(4, 4);
            this.pagConfirmar.Name = "pagConfirmar";
            this.pagConfirmar.Padding = new System.Windows.Forms.Padding(3);
            this.pagConfirmar.Size = new System.Drawing.Size(432, 230);
            this.pagConfirmar.TabIndex = 3;
            this.pagConfirmar.Text = "Confirmações";
            // 
            // MsgPergunta
            // 
            this.MsgPergunta.Angulo = 90;
            this.MsgPergunta.AtivarSombra = false;
            this.MsgPergunta.Cor1 = System.Drawing.Color.SteelBlue;
            this.MsgPergunta.Cor2 = System.Drawing.Color.MidnightBlue;
            this.MsgPergunta.CorSombra = System.Drawing.Color.Black;
            this.MsgPergunta.EspacoTexto = 5;
            this.MsgPergunta.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MsgPergunta.ForeColor = System.Drawing.Color.SteelBlue;
            this.MsgPergunta.Location = new System.Drawing.Point(166, 68);
            this.MsgPergunta.Name = "MsgPergunta";
            this.MsgPergunta.QuebraTexto = false;
            this.MsgPergunta.Size = new System.Drawing.Size(247, 98);
            this.MsgPergunta.TabIndex = 7;
            this.MsgPergunta.Text = "Aqui vai o texto referente a mensagem desejada";
            this.MsgPergunta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MsgPergunta.X = 2;
            this.MsgPergunta.Y = 2;
            // 
            // TituloPergunta
            // 
            this.TituloPergunta.Angulo = 90;
            this.TituloPergunta.AtivarSombra = true;
            this.TituloPergunta.Cor1 = System.Drawing.Color.SteelBlue;
            this.TituloPergunta.Cor2 = System.Drawing.Color.MidnightBlue;
            this.TituloPergunta.CorSombra = System.Drawing.Color.Black;
            this.TituloPergunta.EspacoTexto = 5;
            this.TituloPergunta.Font = new System.Drawing.Font("Showcard Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TituloPergunta.ForeColor = System.Drawing.Color.SteelBlue;
            this.TituloPergunta.Location = new System.Drawing.Point(33, 19);
            this.TituloPergunta.Name = "TituloPergunta";
            this.TituloPergunta.QuebraTexto = false;
            this.TituloPergunta.Size = new System.Drawing.Size(389, 46);
            this.TituloPergunta.TabIndex = 4;
            this.TituloPergunta.Text = "CONFIRMAÇÃO!";
            this.TituloPergunta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TituloPergunta.X = 2;
            this.TituloPergunta.Y = 2;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(9, 56);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(152, 140);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // BtOkSucesso
            // 
            this.BtOkSucesso.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtOkSucesso.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.BtOkSucesso.Location = new System.Drawing.Point(336, 181);
            this.BtOkSucesso.Name = "BtOkSucesso";
            this.BtOkSucesso.Size = new System.Drawing.Size(77, 29);
            this.BtOkSucesso.TabIndex = 3;
            this.BtOkSucesso.Text = "Ok";
            this.BtOkSucesso.UseVisualStyleBackColor = true;
            // 
            // BtOkAlertas
            // 
            this.BtOkAlertas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtOkAlertas.ForeColor = System.Drawing.Color.DarkOrange;
            this.BtOkAlertas.Location = new System.Drawing.Point(322, 181);
            this.BtOkAlertas.Name = "BtOkAlertas";
            this.BtOkAlertas.Size = new System.Drawing.Size(77, 29);
            this.BtOkAlertas.TabIndex = 8;
            this.BtOkAlertas.Text = "Ok";
            this.BtOkAlertas.UseVisualStyleBackColor = true;
            // 
            // BtOkErros
            // 
            this.BtOkErros.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtOkErros.ForeColor = System.Drawing.Color.Crimson;
            this.BtOkErros.Location = new System.Drawing.Point(336, 183);
            this.BtOkErros.Name = "BtOkErros";
            this.BtOkErros.Size = new System.Drawing.Size(77, 29);
            this.BtOkErros.TabIndex = 8;
            this.BtOkErros.Text = "Ok";
            this.BtOkErros.UseVisualStyleBackColor = true;
            // 
            // BtSim
            // 
            this.BtSim.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtSim.ForeColor = System.Drawing.Color.SteelBlue;
            this.BtSim.Location = new System.Drawing.Point(166, 169);
            this.BtSim.Name = "BtSim";
            this.BtSim.Size = new System.Drawing.Size(77, 29);
            this.BtSim.TabIndex = 8;
            this.BtSim.Text = "Sim";
            this.BtSim.UseVisualStyleBackColor = true;
            // 
            // BtNao
            // 
            this.BtNao.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtNao.ForeColor = System.Drawing.Color.SteelBlue;
            this.BtNao.Location = new System.Drawing.Point(336, 169);
            this.BtNao.Name = "BtNao";
            this.BtNao.Size = new System.Drawing.Size(77, 29);
            this.BtNao.TabIndex = 9;
            this.BtNao.Text = "Não";
            this.BtNao.UseVisualStyleBackColor = true;
            // 
            // FormMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 218);
            this.Controls.Add(this.MpConteudo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMsg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MpConteudo.ResumeLayout(false);
            this.pagSucesso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pagAlertas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pagErros.ResumeLayout(false);
            this.pagErros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pagConfirmar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        public ECTurbo.Controles.ECTurbo_Label MsgSucesso;
        public ECTurbo.Controles.ECTurbo_Label MsgAlerta;
        public System.Windows.Forms.TabControl MpConteudo;
        public System.Windows.Forms.TabPage pagSucesso;
        public System.Windows.Forms.TabPage pagAlertas;
        public System.Windows.Forms.TabPage pagErros;
        public System.Windows.Forms.TabPage pagConfirmar;
        public ECTurbo.Controles.ECTurbo_Label MsgPergunta;
        public ECTurbo.Controles.ECTurbo_Label TituloSucesso;
        public ECTurbo.Controles.ECTurbo_Label TituloALerta;
        public ECTurbo.Controles.ECTurbo_Label TituloErro;
        public ECTurbo.Controles.ECTurbo_Label TituloPergunta;
        public TextBox MsgErro;
        private Button BtOkSucesso;
        private Button BtOkAlertas;
        private Button BtOkErros;
        private Button BtSim;
        private Button BtNao;
    }
}