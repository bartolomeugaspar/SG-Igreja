using System.Data;

using ECTurbo.Codigos;

using SGI_Igreja.LinhasModeloGRID;
using SGI_Igreja.Properties;

namespace SGI_Igreja.Formularios
{
    public partial class FrmGRID_Modelo : Form
    {
        public FrmGRID_Modelo()
        {
            InitializeComponent();
        }

        private void FrmGRID_Modelo_Load(object sender, EventArgs e)
        {
            if (Grid_X.Controls.Count == 0)
            {
                GRID.MontarGrid(Grid_X, new LinhasModeloGRID.Lst_Modelo(), QtdLinhasGrid_X, Ver: true);
                BuscaGeral_X();
            }
        }

        #region Busca e Paginação GRID _X

        private int QtdLinhasGrid_X = 10;
        private int PagAtual_X = 1;
        private DataTable dt_X;
        private int TotalPaginas_X = 0;
        private void BuscaGeral_X(bool VoltarPag1 = true)
        {
            /*
            
            SuaClasse_Banco_de_Dados.Sql = "Comando SQL da Busca";

            GerarCriterios_Busca_X();

            //SuaClasse_Banco_de_Dados.Sql += IconeOrderBy_X.Tag.ToString();

            dt_X = SuaClasse_Banco_de_Dados.BuscaSQL();

            TotalPaginas_X = (int)Math.Ceiling((decimal)dt_X.Rows.Count / QtdLinhasGrid_X);
            
            if (VoltarPag1 == true)
                _X_PrimeiraPagina_Click(null, null);
            else
                Paginacao_X();

            */
        }

        private void GerarCriterios_Busca_X()
        {
            //Aqui vai os critérios da busca se houver algum
        }

        private void Paginacao_X()
        {
            if (PagAtual_X > TotalPaginas_X)
                PagAtual_X = TotalPaginas_X;

            if (PagAtual_X == 0)
                PagAtual_X = 1;

            int i = PagAtual_X * QtdLinhasGrid_X - QtdLinhasGrid_X;

            foreach (Lst_Modelo lst in Grid_X.Controls)
            {
                if (i < dt_X.Rows.Count)
                {
                    DataRow lin;
                    lin = dt_X.Rows[i];
                    
                    //Preenchimento dos dados dentro do GRID




                    lst.Visible = true;

                }
                else
                    lst.Visible = false;

                i++;
            }

            if (TotalPaginas_X == 0)
                InfoPaginacao_X.Text = "--";
            else
                InfoPaginacao_X.Text = "Pág: " + PagAtual_X + " de " + TotalPaginas_X;
        }

        private void _X_PaginaAnterior_Click(object sender, EventArgs e)
        {
            if (PagAtual_X == 1 || TotalPaginas_X == 0)
                return;

            PagAtual_X--;
            Paginacao_X();
        }

        private void _X_ProximaPagina_Click(object sender, EventArgs e)
        {
            if (PagAtual_X == TotalPaginas_X || TotalPaginas_X == 0)
                return;

            PagAtual_X++;
            Paginacao_X();
        }

        private void _X_PrimeiraPagina_Click(object sender, EventArgs e)
        {
            if (dt_X == null)
                return;

            PagAtual_X = 1;
            Paginacao_X();
        }

        private void _X_UltimaPagina_Click(object sender, EventArgs e)
        {
            if (TotalPaginas_X == 0)
                return;

            PagAtual_X = TotalPaginas_X;
            Paginacao_X();
        }

        private void OrderBy_X(string campo, Control Ctr)
        {
            IconeOrderBy_X.Left = Ctr.Left - 18;

            if (IconeOrderBy_X.Tag.ToString().ToUpper() == $"ORDER BY {campo} ASC".ToUpper())
            {
                IconeOrderBy_X.Tag = $"ORDER BY {campo} DESC";
                IconeOrderBy_X.Image = Resources.order_desc;
            }
            else
            {
                IconeOrderBy_X.Tag = $"ORDER BY {campo} ASC";
                IconeOrderBy_X.Image = Resources.order_asc;
            }

            BuscaGeral_X();
        }

        #endregion

    }
}
