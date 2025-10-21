using ECTurbo.Codigos;

namespace ECTurbo.Formularios
{
    public partial class FormMsg : Form
    {
        public FormMsg()
        {
            InitializeComponent();

            BtSim.Click += new EventHandler(BtSim_Click);
            BtNao.Click += new EventHandler(BtNao_Click);
            BtOkSucesso.Click += new EventHandler(BtOkSucesso_Click);
            BtOkAlertas.Click += new EventHandler(BtOkSucesso_Click);
            BtOkErros.Click += new EventHandler(BtOkSucesso_Click);
            MpConteudo.KeyDown += new KeyEventHandler(MpConteudo_KeyDown);
            Load += new EventHandler(FormMsg_Load);
        }

        private void BtOkSucesso_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MpConteudo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (MpConteudo.SelectedTab != pagConfirmar && e.KeyCode == Keys.Enter))
            {
                Close();
            }

        }

        private void FormMsg_Load(object sender, EventArgs e)
        {
            Funcoes.Resposta = false;
        }

        private void BtSim_Click(object sender, EventArgs e)
        {
            Funcoes.Resposta = true;
            Close();
        }

        private void BtNao_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
