namespace SGI_Igreja.Formularios
{
    partial class FrmLogin
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
            TabConteudo = new TabControl();
            PagLogin = new TabPage();
            btSair = new ECTurbo.Controles.ECTurbo_Label();
            btEsqueceSenha = new ECTurbo.Controles.ECTurbo_Label();
            btEntrar = new ECTurbo.Controles.ECTurbo_Botao();
            TxtSenha = new ECTurbo.Controles.ECTurbo_TextBoxSenha();
            TxtUsuario = new ECTurbo.Controles.ECTurbo_TextBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            ecTurbo_Label3 = new ECTurbo.Controles.ECTurbo_Label();
            ecTurbo_Label2 = new ECTurbo.Controles.ECTurbo_Label();
            ecTurbo_Label1 = new ECTurbo.Controles.ECTurbo_Label();
            pictureBox1 = new PictureBox();
            PagBoasVindas = new TabPage();
            ecTurbo_BarraProgresso1 = new ECTurbo.Controles.ECTurbo_BarraProgresso();
            ecTurbo_Label5 = new ECTurbo.Controles.ECTurbo_Label();
            ecTurbo_Label4 = new ECTurbo.Controles.ECTurbo_Label();
            pictureBox4 = new PictureBox();
            lblBoasVindas = new ECTurbo.Controles.ECTurbo_Label();
            PagReSenha = new TabPage();
            btSairRec = new ECTurbo.Controles.ECTurbo_Label();
            ecTurbo_Label8 = new ECTurbo.Controles.ECTurbo_Label();
            ecTurbo_Botao1 = new ECTurbo.Controles.ECTurbo_Botao();
            TxtEmailRec = new ECTurbo.Controles.ECTurbo_TextBox();
            ecTurbo_Label7 = new ECTurbo.Controles.ECTurbo_Label();
            ecTurbo_Label6 = new ECTurbo.Controles.ECTurbo_Label();
            PagBloqueio = new TabPage();
            pictureBox5 = new PictureBox();
            TabConteudo.SuspendLayout();
            PagLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            PagBoasVindas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            PagReSenha.SuspendLayout();
            PagBloqueio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // TabConteudo
            // 
            TabConteudo.Alignment = TabAlignment.Bottom;
            TabConteudo.Controls.Add(PagLogin);
            TabConteudo.Controls.Add(PagBoasVindas);
            TabConteudo.Controls.Add(PagReSenha);
            TabConteudo.Controls.Add(PagBloqueio);
            TabConteudo.Location = new Point(-5, -5);
            TabConteudo.Name = "TabConteudo";
            TabConteudo.SelectedIndex = 0;
            TabConteudo.Size = new Size(586, 419);
            TabConteudo.TabIndex = 0;
            // 
            // PagLogin
            // 
            PagLogin.BackColor = Color.FromArgb(192, 192, 255);
            PagLogin.Controls.Add(btSair);
            PagLogin.Controls.Add(btEsqueceSenha);
            PagLogin.Controls.Add(btEntrar);
            PagLogin.Controls.Add(TxtSenha);
            PagLogin.Controls.Add(TxtUsuario);
            PagLogin.Controls.Add(pictureBox3);
            PagLogin.Controls.Add(pictureBox2);
            PagLogin.Controls.Add(ecTurbo_Label3);
            PagLogin.Controls.Add(ecTurbo_Label2);
            PagLogin.Controls.Add(ecTurbo_Label1);
            PagLogin.Controls.Add(pictureBox1);
            PagLogin.Location = new Point(4, 4);
            PagLogin.Name = "PagLogin";
            PagLogin.Padding = new Padding(3);
            PagLogin.Size = new Size(578, 391);
            PagLogin.TabIndex = 0;
            PagLogin.Text = "LOGIN";
            // 
            // btSair
            // 
            btSair.Angulo = 90;
            btSair.AtivarSombra = false;
            btSair.AutoSize = true;
            btSair.BackColor = Color.Transparent;
            btSair.Cor1 = Color.MidnightBlue;
            btSair.Cor2 = Color.Crimson;
            btSair.CorSombra = Color.Black;
            btSair.Cursor = Cursors.Hand;
            btSair.EspacoTexto = 5;
            btSair.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btSair.IconSize = new Size(16, 16);
            btSair.Location = new Point(506, 354);
            btSair.Name = "btSair";
            btSair.QuebraTexto = false;
            btSair.Size = new Size(37, 21);
            btSair.TabIndex = 19;
            btSair.Text = "Sair";
            btSair.TextAlign = ContentAlignment.MiddleLeft;
            btSair.X = 2;
            btSair.Y = 2;
            btSair.Click += btSair_Click;
            // 
            // btEsqueceSenha
            // 
            btEsqueceSenha.Angulo = 90;
            btEsqueceSenha.AtivarSombra = false;
            btEsqueceSenha.AutoSize = true;
            btEsqueceSenha.BackColor = Color.Transparent;
            btEsqueceSenha.Cor1 = Color.SteelBlue;
            btEsqueceSenha.Cor2 = Color.MidnightBlue;
            btEsqueceSenha.CorSombra = Color.Black;
            btEsqueceSenha.Cursor = Cursors.Hand;
            btEsqueceSenha.EspacoTexto = 5;
            btEsqueceSenha.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btEsqueceSenha.IconSize = new Size(16, 16);
            btEsqueceSenha.Location = new Point(333, 354);
            btEsqueceSenha.Name = "btEsqueceSenha";
            btEsqueceSenha.QuebraTexto = false;
            btEsqueceSenha.Size = new Size(148, 21);
            btEsqueceSenha.TabIndex = 18;
            btEsqueceSenha.Text = "Esquece a Senha!";
            btEsqueceSenha.TextAlign = ContentAlignment.MiddleLeft;
            btEsqueceSenha.X = 2;
            btEsqueceSenha.Y = 2;
            btEsqueceSenha.Click += btEsqueceSenha_Click;
            // 
            // btEntrar
            // 
            btEntrar.Angulo = 90;
            btEntrar.Arred = 20;
            btEntrar.AtivarSombra = true;
            btEntrar.BackColor = Color.Transparent;
            btEntrar.Cor1 = Color.SteelBlue;
            btEntrar.Cor2 = Color.MidnightBlue;
            btEntrar.CorBorda = Color.Black;
            btEntrar.CorSombra = Color.Black;
            btEntrar.Cursor = Cursors.Hand;
            btEntrar.DistIcone = 0;
            btEntrar.Font = new Font("Century Gothic", 13F);
            btEntrar.ForeColor = Color.White;
            btEntrar.Location = new Point(333, 251);
            btEntrar.Name = "btEntrar";
            btEntrar.PosicaoImagem = ECTurbo.Controles.ECTurbo_Botao.Posicoes.Esquerda;
            btEntrar.Size = new Size(209, 38);
            btEntrar.TabIndex = 17;
            btEntrar.TamanhoIcone = 0;
            btEntrar.TamanhoSombra = 2;
            btEntrar.TamBorda = 0;
            btEntrar.Text = "Entrar";
            btEntrar.UseVisualStyleBackColor = false;
            btEntrar.Click += btEntrar_Click;
            // 
            // TxtSenha
            // 
            TxtSenha.AtivarCriptografia = true;
            TxtSenha.BackColor = Color.White;
            TxtSenha.Coluna = "";
            TxtSenha.Font = new Font("Century Gothic", 12F);
            TxtSenha.Limpeza = true;
            TxtSenha.Location = new Point(333, 202);
            TxtSenha.MinimoCaracteres = 0;
            TxtSenha.Name = "TxtSenha";
            TxtSenha.Obgt = false;
            TxtSenha.RequerEspecial = false;
            TxtSenha.RequerMaiuscula = false;
            TxtSenha.RequerMinuscula = false;
            TxtSenha.RequerNumero = false;
            TxtSenha.SalvarSenhaVazia = false;
            TxtSenha.Size = new Size(209, 27);
            TxtSenha.TabIndex = 16;
            TxtSenha.Tag = "";
            TxtSenha.TrazerSenhaBanco = false;
            TxtSenha.Unico = "";
            TxtSenha.UseSystemPasswordChar = true;
            // 
            // TxtUsuario
            // 
            TxtUsuario.BackColor = Color.AliceBlue;
            TxtUsuario.Coluna = "";
            TxtUsuario.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtUsuario.Limpeza = true;
            TxtUsuario.Location = new Point(333, 130);
            TxtUsuario.Name = "TxtUsuario";
            TxtUsuario.Obgt = false;
            TxtUsuario.Size = new Size(209, 27);
            TxtUsuario.TabIndex = 15;
            TxtUsuario.Tag = "";
            TxtUsuario.Unico = "";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.icons8_key_24;
            pictureBox3.Location = new Point(333, 172);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(24, 24);
            pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox3.TabIndex = 13;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.icons8_usuário_24;
            pictureBox2.Location = new Point(333, 103);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 24);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 14;
            pictureBox2.TabStop = false;
            // 
            // ecTurbo_Label3
            // 
            ecTurbo_Label3.Angulo = 90;
            ecTurbo_Label3.AtivarSombra = false;
            ecTurbo_Label3.AutoSize = true;
            ecTurbo_Label3.BackColor = Color.Transparent;
            ecTurbo_Label3.Cor1 = Color.SteelBlue;
            ecTurbo_Label3.Cor2 = Color.MidnightBlue;
            ecTurbo_Label3.CorSombra = Color.Black;
            ecTurbo_Label3.EspacoTexto = 5;
            ecTurbo_Label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ecTurbo_Label3.IconSize = new Size(16, 16);
            ecTurbo_Label3.Location = new Point(360, 175);
            ecTurbo_Label3.Name = "ecTurbo_Label3";
            ecTurbo_Label3.QuebraTexto = false;
            ecTurbo_Label3.Size = new Size(59, 21);
            ecTurbo_Label3.TabIndex = 11;
            ecTurbo_Label3.Text = "Senha";
            ecTurbo_Label3.TextAlign = ContentAlignment.MiddleLeft;
            ecTurbo_Label3.X = 2;
            ecTurbo_Label3.Y = 2;
            // 
            // ecTurbo_Label2
            // 
            ecTurbo_Label2.Angulo = 90;
            ecTurbo_Label2.AtivarSombra = false;
            ecTurbo_Label2.AutoSize = true;
            ecTurbo_Label2.BackColor = Color.Transparent;
            ecTurbo_Label2.Cor1 = Color.SteelBlue;
            ecTurbo_Label2.Cor2 = Color.MidnightBlue;
            ecTurbo_Label2.CorSombra = Color.Black;
            ecTurbo_Label2.EspacoTexto = 5;
            ecTurbo_Label2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ecTurbo_Label2.IconSize = new Size(16, 16);
            ecTurbo_Label2.Location = new Point(360, 104);
            ecTurbo_Label2.Name = "ecTurbo_Label2";
            ecTurbo_Label2.QuebraTexto = false;
            ecTurbo_Label2.Size = new Size(66, 21);
            ecTurbo_Label2.TabIndex = 12;
            ecTurbo_Label2.Text = "Usuario";
            ecTurbo_Label2.TextAlign = ContentAlignment.MiddleLeft;
            ecTurbo_Label2.X = 2;
            ecTurbo_Label2.Y = 2;
            // 
            // ecTurbo_Label1
            // 
            ecTurbo_Label1.Angulo = 90;
            ecTurbo_Label1.AtivarSombra = true;
            ecTurbo_Label1.AutoSize = true;
            ecTurbo_Label1.BackColor = Color.Transparent;
            ecTurbo_Label1.Cor1 = Color.SteelBlue;
            ecTurbo_Label1.Cor2 = Color.MidnightBlue;
            ecTurbo_Label1.CorSombra = Color.Black;
            ecTurbo_Label1.EspacoTexto = 5;
            ecTurbo_Label1.Font = new Font("Cooper Black", 36F);
            ecTurbo_Label1.IconSize = new Size(16, 16);
            ecTurbo_Label1.Location = new Point(348, 22);
            ecTurbo_Label1.Name = "ecTurbo_Label1";
            ecTurbo_Label1.QuebraTexto = false;
            ecTurbo_Label1.Size = new Size(189, 55);
            ecTurbo_Label1.TabIndex = 10;
            ecTurbo_Label1.Text = "LOGIN";
            ecTurbo_Label1.TextAlign = ContentAlignment.MiddleLeft;
            ecTurbo_Label1.X = 2;
            ecTurbo_Label1.Y = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(311, 389);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // PagBoasVindas
            // 
            PagBoasVindas.BackColor = Color.FromArgb(0, 0, 64);
            PagBoasVindas.Controls.Add(ecTurbo_BarraProgresso1);
            PagBoasVindas.Controls.Add(ecTurbo_Label5);
            PagBoasVindas.Controls.Add(ecTurbo_Label4);
            PagBoasVindas.Controls.Add(pictureBox4);
            PagBoasVindas.Controls.Add(lblBoasVindas);
            PagBoasVindas.Location = new Point(4, 4);
            PagBoasVindas.Name = "PagBoasVindas";
            PagBoasVindas.Padding = new Padding(3);
            PagBoasVindas.Size = new Size(578, 391);
            PagBoasVindas.TabIndex = 1;
            PagBoasVindas.Text = "BOAS VINDAS";
            // 
            // ecTurbo_BarraProgresso1
            // 
            ecTurbo_BarraProgresso1.ArredBorda = 20;
            ecTurbo_BarraProgresso1.ArredMarcador = 12;
            ecTurbo_BarraProgresso1.BackColor = Color.Transparent;
            ecTurbo_BarraProgresso1.ContornoMarcador = Color.Black;
            ecTurbo_BarraProgresso1.CorBarra1 = Color.DodgerBlue;
            ecTurbo_BarraProgresso1.CorBarra2 = Color.Indigo;
            ecTurbo_BarraProgresso1.CorBorda = Color.Black;
            ecTurbo_BarraProgresso1.CorFundo = Color.Black;
            ecTurbo_BarraProgresso1.CorMarcador1 = Color.Yellow;
            ecTurbo_BarraProgresso1.CorMarcador2 = Color.Red;
            ecTurbo_BarraProgresso1.CtrAssociado = null;
            ecTurbo_BarraProgresso1.Espaco = 0;
            ecTurbo_BarraProgresso1.Location = new Point(31, 352);
            ecTurbo_BarraProgresso1.MinimumSize = new Size(50, 12);
            ecTurbo_BarraProgresso1.MostrarMarcador = false;
            ecTurbo_BarraProgresso1.Name = "ecTurbo_BarraProgresso1";
            ecTurbo_BarraProgresso1.Percentual = 50F;
            ecTurbo_BarraProgresso1.Size = new Size(507, 14);
            ecTurbo_BarraProgresso1.TabIndex = 17;
            ecTurbo_BarraProgresso1.TamMarcador = 4;
            ecTurbo_BarraProgresso1.Text = "ecTurbo_BarraProgresso1";
            // 
            // ecTurbo_Label5
            // 
            ecTurbo_Label5.Angulo = 90;
            ecTurbo_Label5.AtivarSombra = false;
            ecTurbo_Label5.AutoSize = true;
            ecTurbo_Label5.BackColor = Color.Transparent;
            ecTurbo_Label5.Cor1 = Color.White;
            ecTurbo_Label5.Cor2 = Color.White;
            ecTurbo_Label5.CorSombra = Color.Black;
            ecTurbo_Label5.EspacoTexto = 5;
            ecTurbo_Label5.Font = new Font("Century Gothic", 14F);
            ecTurbo_Label5.ForeColor = Color.Coral;
            ecTurbo_Label5.IconSize = new Size(16, 16);
            ecTurbo_Label5.Location = new Point(190, 112);
            ecTurbo_Label5.Name = "ecTurbo_Label5";
            ecTurbo_Label5.QuebraTexto = false;
            ecTurbo_Label5.Size = new Size(184, 22);
            ecTurbo_Label5.TabIndex = 16;
            ecTurbo_Label5.Text = "Seja Bem Vindo(a)!";
            ecTurbo_Label5.TextAlign = ContentAlignment.MiddleLeft;
            ecTurbo_Label5.X = 2;
            ecTurbo_Label5.Y = 2;
            // 
            // ecTurbo_Label4
            // 
            ecTurbo_Label4.Angulo = 90;
            ecTurbo_Label4.AtivarSombra = false;
            ecTurbo_Label4.AutoSize = true;
            ecTurbo_Label4.BackColor = Color.Transparent;
            ecTurbo_Label4.Cor1 = Color.White;
            ecTurbo_Label4.Cor2 = Color.White;
            ecTurbo_Label4.CorSombra = Color.Black;
            ecTurbo_Label4.EspacoTexto = 5;
            ecTurbo_Label4.Font = new Font("Century Gothic", 14F, FontStyle.Bold);
            ecTurbo_Label4.ForeColor = Color.Coral;
            ecTurbo_Label4.IconSize = new Size(16, 16);
            ecTurbo_Label4.Location = new Point(129, 317);
            ecTurbo_Label4.Name = "ecTurbo_Label4";
            ecTurbo_Label4.QuebraTexto = false;
            ecTurbo_Label4.Size = new Size(311, 23);
            ecTurbo_Label4.TabIndex = 16;
            ecTurbo_Label4.Text = "Centro Nossa Senhora de Fátima";
            ecTurbo_Label4.TextAlign = ContentAlignment.MiddleLeft;
            ecTurbo_Label4.X = 2;
            ecTurbo_Label4.Y = 2;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources._1000049720_removebg_preview2;
            pictureBox4.Location = new Point(185, 137);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(199, 177);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 15;
            pictureBox4.TabStop = false;
            // 
            // lblBoasVindas
            // 
            lblBoasVindas.Angulo = 90;
            lblBoasVindas.AtivarSombra = true;
            lblBoasVindas.BackColor = Color.Transparent;
            lblBoasVindas.Cor1 = Color.CornflowerBlue;
            lblBoasVindas.Cor2 = Color.MediumSlateBlue;
            lblBoasVindas.CorSombra = Color.Black;
            lblBoasVindas.EspacoTexto = 5;
            lblBoasVindas.Font = new Font("Cooper Black", 30F);
            lblBoasVindas.IconSize = new Size(16, 16);
            lblBoasVindas.Location = new Point(155, 32);
            lblBoasVindas.Name = "lblBoasVindas";
            lblBoasVindas.QuebraTexto = false;
            lblBoasVindas.Size = new Size(259, 55);
            lblBoasVindas.TabIndex = 11;
            lblBoasVindas.Text = "BOM DIA!";
            lblBoasVindas.TextAlign = ContentAlignment.MiddleCenter;
            lblBoasVindas.X = 2;
            lblBoasVindas.Y = 2;
            // 
            // PagReSenha
            // 
            PagReSenha.BackColor = Color.FromArgb(0, 0, 64);
            PagReSenha.Controls.Add(btSairRec);
            PagReSenha.Controls.Add(ecTurbo_Label8);
            PagReSenha.Controls.Add(ecTurbo_Botao1);
            PagReSenha.Controls.Add(TxtEmailRec);
            PagReSenha.Controls.Add(ecTurbo_Label7);
            PagReSenha.Controls.Add(ecTurbo_Label6);
            PagReSenha.Location = new Point(4, 4);
            PagReSenha.Name = "PagReSenha";
            PagReSenha.Padding = new Padding(3);
            PagReSenha.Size = new Size(578, 391);
            PagReSenha.TabIndex = 2;
            PagReSenha.Text = "RECUPERACAO";
            // 
            // btSairRec
            // 
            btSairRec.Angulo = 90;
            btSairRec.AtivarSombra = false;
            btSairRec.AutoSize = true;
            btSairRec.BackColor = Color.Transparent;
            btSairRec.Cor1 = Color.Silver;
            btSairRec.Cor2 = Color.Silver;
            btSairRec.CorSombra = Color.Black;
            btSairRec.Cursor = Cursors.Hand;
            btSairRec.EspacoTexto = 5;
            btSairRec.Font = new Font("Century Gothic", 14F);
            btSairRec.IconSize = new Size(16, 16);
            btSairRec.Location = new Point(549, 6);
            btSairRec.Name = "btSairRec";
            btSairRec.QuebraTexto = false;
            btSairRec.Size = new Size(22, 22);
            btSairRec.TabIndex = 20;
            btSairRec.Text = "X";
            btSairRec.TextAlign = ContentAlignment.MiddleLeft;
            btSairRec.X = 2;
            btSairRec.Y = 2;
            btSairRec.Click += btSairRec_Click;
            // 
            // ecTurbo_Label8
            // 
            ecTurbo_Label8.Angulo = 90;
            ecTurbo_Label8.AtivarSombra = false;
            ecTurbo_Label8.AutoSize = true;
            ecTurbo_Label8.BackColor = Color.Transparent;
            ecTurbo_Label8.Cor1 = Color.Maroon;
            ecTurbo_Label8.Cor2 = Color.Sienna;
            ecTurbo_Label8.CorSombra = Color.Black;
            ecTurbo_Label8.EspacoTexto = 5;
            ecTurbo_Label8.Font = new Font("Century Gothic", 14F);
            ecTurbo_Label8.IconSize = new Size(16, 16);
            ecTurbo_Label8.Location = new Point(178, 323);
            ecTurbo_Label8.Name = "ecTurbo_Label8";
            ecTurbo_Label8.QuebraTexto = false;
            ecTurbo_Label8.Size = new Size(214, 22);
            ecTurbo_Label8.TabIndex = 19;
            ecTurbo_Label8.Text = "Contagem Regressiva";
            ecTurbo_Label8.TextAlign = ContentAlignment.MiddleLeft;
            ecTurbo_Label8.X = 2;
            ecTurbo_Label8.Y = 2;
            // 
            // ecTurbo_Botao1
            // 
            ecTurbo_Botao1.Angulo = 90;
            ecTurbo_Botao1.Arred = 20;
            ecTurbo_Botao1.AtivarSombra = true;
            ecTurbo_Botao1.BackColor = Color.Transparent;
            ecTurbo_Botao1.Cor1 = Color.SteelBlue;
            ecTurbo_Botao1.Cor2 = Color.MidnightBlue;
            ecTurbo_Botao1.CorBorda = Color.Black;
            ecTurbo_Botao1.CorSombra = Color.Black;
            ecTurbo_Botao1.DistIcone = 0;
            ecTurbo_Botao1.Font = new Font("Century Gothic", 13F);
            ecTurbo_Botao1.ForeColor = Color.White;
            ecTurbo_Botao1.Location = new Point(162, 244);
            ecTurbo_Botao1.Name = "ecTurbo_Botao1";
            ecTurbo_Botao1.PosicaoImagem = ECTurbo.Controles.ECTurbo_Botao.Posicoes.Esquerda;
            ecTurbo_Botao1.Size = new Size(247, 38);
            ecTurbo_Botao1.TabIndex = 18;
            ecTurbo_Botao1.TamanhoIcone = 0;
            ecTurbo_Botao1.TamanhoSombra = 2;
            ecTurbo_Botao1.TamBorda = 0;
            ecTurbo_Botao1.Text = "Entrar";
            ecTurbo_Botao1.UseVisualStyleBackColor = false;
            // 
            // TxtEmailRec
            // 
            TxtEmailRec.BackColor = Color.AliceBlue;
            TxtEmailRec.Coluna = "";
            TxtEmailRec.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtEmailRec.Limpeza = true;
            TxtEmailRec.Location = new Point(95, 180);
            TxtEmailRec.Name = "TxtEmailRec";
            TxtEmailRec.Obgt = false;
            TxtEmailRec.Size = new Size(381, 27);
            TxtEmailRec.TabIndex = 17;
            TxtEmailRec.Tag = "";
            TxtEmailRec.Unico = "";
            // 
            // ecTurbo_Label7
            // 
            ecTurbo_Label7.Angulo = 90;
            ecTurbo_Label7.AtivarSombra = false;
            ecTurbo_Label7.AutoSize = true;
            ecTurbo_Label7.BackColor = Color.Transparent;
            ecTurbo_Label7.Cor1 = Color.White;
            ecTurbo_Label7.Cor2 = Color.White;
            ecTurbo_Label7.CorSombra = Color.Black;
            ecTurbo_Label7.EspacoTexto = 5;
            ecTurbo_Label7.Font = new Font("Century Gothic", 14F);
            ecTurbo_Label7.IconSize = new Size(16, 16);
            ecTurbo_Label7.Location = new Point(199, 150);
            ecTurbo_Label7.Name = "ecTurbo_Label7";
            ecTurbo_Label7.QuebraTexto = false;
            ecTurbo_Label7.Size = new Size(172, 22);
            ecTurbo_Label7.TabIndex = 16;
            ecTurbo_Label7.Text = "Digite o seu E-mail";
            ecTurbo_Label7.TextAlign = ContentAlignment.MiddleLeft;
            ecTurbo_Label7.X = 2;
            ecTurbo_Label7.Y = 2;
            // 
            // ecTurbo_Label6
            // 
            ecTurbo_Label6.Angulo = 90;
            ecTurbo_Label6.AtivarSombra = true;
            ecTurbo_Label6.BackColor = Color.Transparent;
            ecTurbo_Label6.Cor1 = Color.CornflowerBlue;
            ecTurbo_Label6.Cor2 = Color.CornflowerBlue;
            ecTurbo_Label6.CorSombra = Color.Black;
            ecTurbo_Label6.EspacoTexto = 5;
            ecTurbo_Label6.Font = new Font("Cooper Black", 24F);
            ecTurbo_Label6.IconSize = new Size(16, 16);
            ecTurbo_Label6.Location = new Point(49, 54);
            ecTurbo_Label6.Name = "ecTurbo_Label6";
            ecTurbo_Label6.QuebraTexto = false;
            ecTurbo_Label6.Size = new Size(472, 55);
            ecTurbo_Label6.TabIndex = 12;
            ecTurbo_Label6.Text = "RECUPERÇÃO DE SENHA!";
            ecTurbo_Label6.TextAlign = ContentAlignment.MiddleCenter;
            ecTurbo_Label6.X = 2;
            ecTurbo_Label6.Y = 2;
            // 
            // PagBloqueio
            // 
            PagBloqueio.BackColor = Color.FromArgb(192, 192, 255);
            PagBloqueio.Controls.Add(pictureBox5);
            PagBloqueio.Location = new Point(4, 4);
            PagBloqueio.Name = "PagBloqueio";
            PagBloqueio.Padding = new Padding(3);
            PagBloqueio.Size = new Size(578, 391);
            PagBloqueio.TabIndex = 3;
            PagBloqueio.Text = "TELA  DE BLOQUEIO";
            // 
            // pictureBox5
            // 
            pictureBox5.Dock = DockStyle.Fill;
            pictureBox5.Image = Properties.Resources.login_bloqueado;
            pictureBox5.Location = new Point(3, 3);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(572, 385);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 0;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 192, 255);
            ClientSize = new Size(571, 418);
            Controls.Add(TabConteudo);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmPrincipal";
            KeyDown += FrmPrincipal_KeyDown;
            TabConteudo.ResumeLayout(false);
            PagLogin.ResumeLayout(false);
            PagLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            PagBoasVindas.ResumeLayout(false);
            PagBoasVindas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            PagReSenha.ResumeLayout(false);
            PagReSenha.PerformLayout();
            PagBloqueio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl TabConteudo;
        private TabPage PagLogin;
        private TabPage PagBoasVindas;
        private ECTurbo.Controles.ECTurbo_Label btSair;
        private ECTurbo.Controles.ECTurbo_Label btEsqueceSenha;
        private ECTurbo.Controles.ECTurbo_Botao btEntrar;
        private ECTurbo.Controles.ECTurbo_TextBoxSenha TxtSenha;
        private ECTurbo.Controles.ECTurbo_TextBox TxtUsuario;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label3;
        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label2;
        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label1;
        private PictureBox pictureBox1;
        private ECTurbo.Controles.ECTurbo_Label lblBoasVindas;
        private PictureBox pictureBox4;
        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label4;
        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label5;
        private ECTurbo.Controles.ECTurbo_BarraProgresso ecTurbo_BarraProgresso1;
        private TabPage PagReSenha;
        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label6;
        private ECTurbo.Controles.ECTurbo_TextBox TxtEmailRec;
        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label7;
        private ECTurbo.Controles.ECTurbo_Botao ecTurbo_Botao1;
        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label8;
        private ECTurbo.Controles.ECTurbo_Label btSairRec;
        private TabPage PagBloqueio;
        private PictureBox pictureBox5;
    }
}