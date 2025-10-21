using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ECTurbo.Codigos;

using ECTurbo_CRUD;

using SGI_Igreja.Properties;

namespace SGI_Igreja.Formularios
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.Alt && e.KeyCode == Keys.B)
            {
                Funcoes.Modal(new FrmConexaoMySQL());
                Application.Exit();
            }
        }
        private void btSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btEntrar_Click(object sender, EventArgs e)
        {

            MYSQL.Sql = "SELECT * FROM USUARIOS";

            string senha = Funcoes.Criptografar(TxtSenha.Text);

            MYSQL.ParamTextoIgual("USUARIO", TxtUsuario);
            MYSQL.ParamTextoIgual("SENHA", senha);

            Funcoes.DtLogin = MYSQL.BuscaSQL();

            if (Funcoes.DtLogin.Rows.Count > 0)
                Dispose();
        }

        private void btEsqueceSenha_Click(object sender, EventArgs e)
        {
            TabConteudo.SelectedTab = PagReSenha;
        }

        private void btSairRec_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
