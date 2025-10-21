using ECTurbo.Codigos;

using ECTurbo_CRUD;

using SGI_Igreja.Properties;

namespace SGI_Igreja.Formularios
{
    public partial class FrmConexaoMySQL : Form
    {
        public FrmConexaoMySQL()
        {
            InitializeComponent();
        }

        private void BtSalvar_Click(object sender, EventArgs e)
        {
            if (Validar() == false)
                return;

            Funcoes.CriarLabel(BtSalvar, "Aguarde, salvando e testando a conexão", "info");

            Application.DoEvents();

            Settings.Default.MySQL_BD = TxtBanco.Text;
            Settings.Default.MySQL_Porta = TxtPorta.Text;
            Settings.Default.MySQL_Senha = TxtSenha.Text;
            Settings.Default.MySQL_Servidor = TxtServidor.Text;
            Settings.Default.MySQL_Usuario = TxtUsuario.Text;

            Settings.Default.Save();

            string Resposta = MYSQL.TestarConexao();

            if (Resposta == string.Empty)
            {
                Funcoes.MsgOk("Conexão Realizada com Sucesso.");
                Application.Restart();
            }
            else if (Resposta.Contains("Unknown database"))
                Funcoes.MsgErro($"O Banco de Dados {TxtBanco.Text} não existe.");
            else if (Resposta == "Unable to connect to any of the specified MySQL hosts.")
                Funcoes.MsgErro("Servidor não iniciado ou então Servidor ou Porta inválidos");
            else if (Resposta == $"Value '{TxtPorta.Text}' is not of the correct type.")
                Funcoes.MsgErro("Porta inválida");
            else if (Resposta.Contains("Access denied for user"))
                Funcoes.MsgErro("Usuário ou senha incorretos");
            else
                Funcoes.MsgErro(Resposta);

            Funcoes.RemoverLabel(BtSalvar);

        }

        private bool Validar()
        {

            bool Validado = true;

            if (TxtBanco.Text == string.Empty)
            {
                Validado = false;
                Funcoes.CriarLabel(TxtBanco, "Obrigatório!");
            }

            if (TxtServidor.Text == string.Empty)
            {
                Validado = false;
                Funcoes.CriarLabel(TxtServidor, "Obrigatório!");
            }

            if (TxtPorta.Text == string.Empty)
            {
                Validado = false;
                Funcoes.CriarLabel(TxtPorta, "Obrigatório!");
            }

            if (TxtUsuario.Text == string.Empty)
            {
                Validado = false;
                Funcoes.CriarLabel(TxtUsuario, "Obrigatório!");
            }

            return Validado;
        }

    }
}
