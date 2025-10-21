using System.ComponentModel;
using SGI_Igreja.Properties;
using System.Text.RegularExpressions;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_TextBoxSenha : ECTurbo_TextBox
    {
        private PictureBox BtVerSenha;

        public ECTurbo_TextBoxSenha()
        {
            UseSystemPasswordChar = true;
            Layout += ECTurbo_TextBoxSenha_Layout;
        }

        private bool vAtivarCriptografia = true;
        [DisplayName("_DB Salvar Criptografado")]
        public bool AtivarCriptografia
        {
            get { return vAtivarCriptografia; }
            set { vAtivarCriptografia = value; }
        }

        private bool vSalvarSenhaVazia = false;
        [DisplayName("_Salvar Senhas em Branco")]
        public bool SalvarSenhaVazia
        {
            get { return vSalvarSenhaVazia; }
            set { vSalvarSenhaVazia = value; }
        }

        private bool vTrazerSenhaBanco = false;
        [DisplayName("_DB Carregar Senha ao Editar")]
        public bool TrazerSenhaBanco
        {
            get { return vTrazerSenhaBanco; }
            set { vTrazerSenhaBanco = value; }
        }

        [DisplayName("_Senha Minimo de Caracteres")]
        public int MinimoCaracteres { get; set; } = 0;

        [DisplayName("_Senha requer maiúsculas")]
        public bool RequerMaiuscula { get; set; } = false;

        [DisplayName("_Senha requer minúsculas")]
        public bool RequerMinuscula { get; set; } = false;

        [DisplayName("_Senha requer números")]
        public bool RequerNumero { get; set; } = false;

        [DisplayName("_Senha requer caracteres especiais")]
        public bool RequerEspecial { get; set; } = false;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Garante que o layout já tenha sido aplicado
            BeginInvoke((MethodInvoker)delegate
            {
                CriarBotao();
                AtualizarPosicaoBotao();
            });
        }


        private void CriarBotao()
        {
            if (BtVerSenha == null)
            {
                BtVerSenha = new PictureBox
                {
                    Cursor = Cursors.Hand,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Image = Resources.icone_senha_mostrar,
                    BackColor = Color.Transparent
                };

                BtVerSenha.MouseDown += BtVerSenhaMouseDown;
                BtVerSenha.MouseUp += BtVerSenhaMouseUp;

                Controls.Add(BtVerSenha);
                BtVerSenha.BringToFront();

                AtualizarPosicaoBotao();
            }
        }

        private void ECTurbo_TextBoxSenha_Layout(object sender, EventArgs e)
        {
            AtualizarPosicaoBotao();
        }

        private void AtualizarPosicaoBotao()
        {
            if (BtVerSenha != null)
            {
                BtVerSenha.Top = ((Height - BtVerSenha.Height) / 2) - 2;
                BtVerSenha.Left = Width - BtVerSenha.Width - 5;
                AjustarPaddingTexto();
            }
        }

        private const int EM_SETMARGINS = 0xD3;
        private const int EC_RIGHTMARGIN = 0x2;

        private void AjustarPaddingTexto()
        {
            if (IsHandleCreated)
            {
                int margemDireita = BtVerSenha?.Width + 10 ?? 20;
                SendMessage(Handle, EM_SETMARGINS, EC_RIGHTMARGIN, margemDireita << 16);
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);


        private void BtVerSenhaMouseUp(object sender, MouseEventArgs e)
        {
            BtVerSenha.Image = Resources.icone_senha_mostrar;
            UseSystemPasswordChar = true;
        }

        private void BtVerSenhaMouseDown(object sender, MouseEventArgs e)
        {
            BtVerSenha.Image = Resources.icone_senha_ocultar;
            UseSystemPasswordChar = false;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (BtVerSenha != null)
            {
                BtVerSenha.MouseDown -= BtVerSenhaMouseDown;
                BtVerSenha.MouseUp -= BtVerSenhaMouseUp;
                BtVerSenha.Dispose();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            ValidarSenha();
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            if (Parent.Controls["lbl_" + Name] != null)
                e.Cancel = true;
        }




        private void ValidarSenha()
        {
            Funcoes.RemoverLabel(this);
            string senha = Text;

            if (senha.Length == 0)
                return;

            string a = "";

            if (MinimoCaracteres > 0 && senha.Length < MinimoCaracteres)
            {
                a = a + $"no mínimo {MinimoCaracteres} caracteres, ";
            }

            if (RequerMaiuscula && !Regex.IsMatch(senha, "[A-Z]"))
            {
                a = a + "letras maiúsculas, ";
            }

            if (RequerMinuscula && !Regex.IsMatch(senha, "[a-z]"))
            {
                a = a + "letras minúsculas, ";
            }

            if (RequerNumero && !Regex.IsMatch(senha, @"\d"))
            {
                a = a + "números, ";
            }

            if (RequerEspecial && !Regex.IsMatch(senha, @"[!@#$%^&*(),.?""':{}|<>_\-+=/\\\[\]]"))
            {
                a = a + "caracteres especiais, ";
            }

            if (a == "")
                return;

            Funcoes.CriarLabel(this, $"Senha não aceita", descricao: "A senha informada deve conter " + (a + ".").Replace(", .", ""));
        }
    }
}
