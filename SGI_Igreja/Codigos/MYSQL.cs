using System.Data;
using System.Globalization;

using ECTurbo.Codigos;
using ECTurbo.Controles;

using MySql.Data.MySqlClient;

using SGI_Igreja.Properties;

namespace ECTurbo_CRUD
{
    public class MYSQL
    {
        public static string TestarConexao()
        {
            try
            {
                using (MySqlConnection Teste = Conectar())
                {
                    Teste.Open();
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static MySqlConnection Conectar()
        {
            return new MySqlConnection($"Server={Settings.Default.MySQL_Servidor};" +
                                       $"Port={Settings.Default.MySQL_Porta};" +
                                       $"Database={Settings.Default.MySQL_BD};" +
                                       $"User={Settings.Default.MySQL_Usuario};" +
                                       $"Password={Settings.Default.MySQL_Senha}");
        }


        public static string Sql;

        public static Dictionary<string, object> Param = new Dictionary<string, object>();

        public static void ParamMultiplos(object Ctr, params string[] ListaCampos)
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string aux = "(";

            foreach (string Campo in ListaCampos)
            {
                string NomeParametro = "P" + Param.Count;
                aux += $"{Campo} LIKE @{NomeParametro} OR ";

                Param.Add(NomeParametro, $"%{v}%");
            }

            aux += ")";

            aux = aux.Replace(" OR )", ")");

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += " AND " + aux;
        }

        public static void ParamExpressao(string Expressao, object Valor, string Operador = "=")
        {
            if (Valor == null)
                return;

            if (Valor.ToString() == string.Empty)
                return;

            string NomeParametro = "P" + Param.Count;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Expressao} {Operador} @{NomeParametro} ";
            Param.Add(NomeParametro, Valor);

        }

        public static void ParamNumeros(string Campo, object Ctr, string Operador = "=")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            try
            {
                v = Funcoes.NormalizarNumero(v);

                string NomeParametro = "P" + Param.Count;
                Param.Add(NomeParametro, Convert.ToDouble(v));

                string t = Sql.Replace(" ", "").ToUpper();

                if (t.Contains("WHERE") == false)
                    Sql += " WHERE 1=1 ";

                Sql += $" AND {Campo} {Operador} @{NomeParametro} ";
            }
            catch (Exception)
            {
            }
        }

        public static void ParamDataEntre(string Campo, object CtrInicio, object CtrFim)
        {
            string vI = CtrInicio.ToString();
            string vF = CtrFim.ToString();

            if (CtrInicio is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                vI = c.Text;
            }

            if (CtrFim is Control cF)
            {
                if (cF.Text == string.Empty)
                    return;

                vF = cF.Text;
            }

            if (vI == "" || vF == "")
                return;
            try
            {
                DateTime DataInicio = Convert.ToDateTime(vI);
                DateTime DataFinal = Convert.ToDateTime(vF);

                if (vF.Contains(":") == false)
                {
                    DataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);
                }


                Param.Add($"{Campo}Inicio", DataInicio);
                Param.Add($"{Campo}Fim", DataFinal);

                string t = Sql.Replace(" ", "").ToUpper();

                if (t.Contains("WHERE") == false)
                    Sql += " WHERE 1=1 ";

                Sql += $" AND {Campo} >= @{Campo}Inicio  AND {Campo} <= @{Campo}Fim ";
            }
            catch (Exception)
            {
            }
        }

        public static void ParamDatas(string Campo, object Ctr, string Operador = "=")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            try
            {
                string NomeParametro = "P" + Param.Count;

                DateTime DataBusca = Convert.ToDateTime(v);

                if (Operador.Contains("<"))
                {
                    if (v.Contains(":") == false)
                    {
                        DataBusca = new DateTime(DataBusca.Year, DataBusca.Month, DataBusca.Day, 23, 59, 59);
                    }
                }

                Param.Add(NomeParametro, DataBusca);

                string t = Sql.Replace(" ", "").ToUpper();

                if (t.Contains("WHERE") == false)
                    Sql += " WHERE 1=1 ";

                Sql += $" AND {Campo} {Operador} @{NomeParametro} ";
            }
            catch (Exception)
            {
            }
        }

        public static void ParamTextoIgual(string Campo, object Ctr, string Desconsiderar = "")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string NomeParametro = "P" + Param.Count;

            if (Desconsiderar != string.Empty)
            {
                Campo = ReplaceSQL(Campo, Desconsiderar);

                foreach (char ch in Desconsiderar)
                {
                    v = v.Replace(ch.ToString(), "");
                }
            }

            if (v.Trim() == "")
                return;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Campo} LIKE @{NomeParametro} ";
            Param.Add(NomeParametro, v);

        }

        public static void ParamTextoContendo(string Campo, object Ctr, string Desconsiderar = "")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string NomeParametro = "P" + Param.Count;

            if (Desconsiderar != string.Empty)
            {
                Campo = ReplaceSQL(Campo, Desconsiderar);

                foreach (char ch in Desconsiderar)
                {
                    v = v.Replace(ch.ToString(), "");
                }
            }

            if (v.Trim() == "")
                return;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Campo} LIKE @{NomeParametro} ";
            Param.Add(NomeParametro, $"%{v}%");
        }

        public static void ParamTextoIniciaCom(string Campo, object Ctr, string Desconsiderar = "")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string NomeParametro = "P" + Param.Count;

            if (Desconsiderar != string.Empty)
            {
                Campo = ReplaceSQL(Campo, Desconsiderar);

                foreach (char ch in Desconsiderar)
                {
                    v = v.Replace(ch.ToString(), "");
                }
            }

            if (v.Trim() == "")
                return;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Campo} LIKE @{NomeParametro} ";
            Param.Add(NomeParametro, $"{v}%");

        }

        public static void ParamTextoTerminaCom(string Campo, object Ctr, string Desconsiderar = "")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string NomeParametro = "P" + Param.Count;

            if (Desconsiderar != string.Empty)
            {
                Campo = ReplaceSQL(Campo, Desconsiderar);

                foreach (char ch in Desconsiderar)
                {
                    v = v.Replace(ch.ToString(), "");
                }
            }

            if (v.Trim() == "")
                return;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Campo} LIKE @{NomeParametro} ";
            Param.Add(NomeParametro, $"%{v}");

        }

        public static void ParamDataDia(string Campo, object Valor, string Operador = "=")
        {
            if (Valor is Control Ctr)
            {
                if (Ctr.Text.Replace(" ", "") == string.Empty)
                    return;
            }

            if (Valor.ToString() == string.Empty)
                return;


            string NomeParametro = "P" + Param.Count;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND DAY({Campo}) {Operador} @{NomeParametro} ";

            if (Valor is Control ctr)
                Param.Add(NomeParametro, ctr.Text);
            else
                Param.Add(NomeParametro, Valor.ToString());
        }

        public static void ParamDataMes(string Campo, object Valor, string Operador = "=")
        {
            string v = "";

            if (Valor is ComboBox cb)
            {
                if (cb.Text == string.Empty)
                    return;

                if (cb.SelectedIndex == -1)
                    return;

                v = (cb.SelectedIndex + 1).ToString();
            }
            else
            {
                if (Valor is Control c)
                    v = c.Text;
                else
                    v = Valor.ToString();
            }

            if (v.Trim() == "")
                return;

            string NomeParametro = "P" + Param.Count;
            Param.Add(NomeParametro, v);

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND Month({Campo}) {Operador} @{NomeParametro} ";

        }


        public static void ParamDataAno(string Campo, object Valor, string Operador = "=")
        {
            string v = "";

            if (Valor is Control Ctr)
            {
                if (Ctr.Text == string.Empty)
                    return;

                v = Ctr.Text;
            }
            else
            {
                v = Valor.ToString();
            }

            if (v.Trim() == "")
                return;


            string NomeParametro = "P" + Param.Count;
            Param.Add(NomeParametro, v);

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND Year({Campo}) {Operador} @{NomeParametro} ";

        }


        //=====================================================

        public static void FiltroTexto_MultiplosCampos(object Ctr, params string[] ListaCampos)
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string aux = "(";

            foreach (string Campo in ListaCampos)
            {
                string NomeParametro = "P" + Param.Count;
                aux += $"{Campo} LIKE @{NomeParametro} OR ";

                Param.Add(NomeParametro, $"%{v}%");
            }

            aux += ")";

            aux = aux.Replace(" OR )", ")");

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += " AND " + aux;
        }

        public static void Filtro_Expressao(string Expressao, object Valor, string Operador = "=")
        {
            if (Valor == null)
                return;

            if (Valor.ToString() == string.Empty)
                return;

            string NomeParametro = "P" + Param.Count;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Expressao} {Operador} @{NomeParametro} ";
            Param.Add(NomeParametro, Valor);

        }

        public static void FiltroNumero(string Campo, object Ctr, string Operador = "=")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            try
            {
                v = Funcoes.NormalizarNumero(v);

                string NomeParametro = "P" + Param.Count;
                Param.Add(NomeParametro, Convert.ToDouble(v));

                string t = Sql.Replace(" ", "").ToUpper();

                if (t.Contains("WHERE") == false)
                    Sql += " WHERE 1=1 ";

                Sql += $" AND {Campo} {Operador} @{NomeParametro} ";
            }
            catch (Exception)
            {
            }
        }

        public static void FiltroData_Periodo(string Campo, object CtrInicio, object CtrFim)
        {
            string vI = CtrInicio.ToString();
            string vF = CtrFim.ToString();

            if (CtrInicio is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                vI = c.Text;
            }

            if (CtrFim is Control cF)
            {
                if (cF.Text == string.Empty)
                    return;

                vF = cF.Text;
            }

            if (vI == "" || vF == "")
                return;
            try
            {
                DateTime DataInicio = Convert.ToDateTime(vI);
                DateTime DataFinal = Convert.ToDateTime(vF);

                if (vF.Contains(":") == false)
                {
                    DataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);
                }


                Param.Add($"{Campo}Inicio", DataInicio);
                Param.Add($"{Campo}Fim", DataFinal);

                string t = Sql.Replace(" ", "").ToUpper();

                if (t.Contains("WHERE") == false)
                    Sql += " WHERE 1=1 ";

                Sql += $" AND {Campo} >= @{Campo}Inicio  AND {Campo} <= @{Campo}Fim ";
            }
            catch (Exception)
            {
            }
        }

        public static void FiltroData(string Campo, object Ctr, string Operador = "=")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            try
            {
                string NomeParametro = "P" + Param.Count;

                DateTime DataBusca = Convert.ToDateTime(v);

                if (Operador.Contains("<"))
                {
                    if (v.Contains(":") == false)
                    {
                        DataBusca = new DateTime(DataBusca.Year, DataBusca.Month, DataBusca.Day, 23, 59, 59);
                    }
                }

                Param.Add(NomeParametro, DataBusca);

                string t = Sql.Replace(" ", "").ToUpper();

                if (t.Contains("WHERE") == false)
                    Sql += " WHERE 1=1 ";

                Sql += $" AND {Campo} {Operador} @{NomeParametro} ";
            }
            catch (Exception)
            {
            }
        }

        public static void FiltroTexto_Igual(string Campo, object Ctr, string Desconsiderar = "")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string NomeParametro = "P" + Param.Count;

            if (Desconsiderar != string.Empty)
            {
                Campo = ReplaceSQL(Campo, Desconsiderar);

                foreach (char ch in Desconsiderar)
                {
                    v = v.Replace(ch.ToString(), "");
                }
            }

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Campo} LIKE @{NomeParametro} ";
            Param.Add(NomeParametro, v);

        }

        public static void FiltroTexto_Contendo(string Campo, object Ctr, string Desconsiderar = "")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string NomeParametro = "P" + Param.Count;

            if (Desconsiderar != string.Empty)
            {
                Campo = ReplaceSQL(Campo, Desconsiderar);

                foreach (char ch in Desconsiderar)
                {
                    v = v.Replace(ch.ToString(), "");
                }
            }

            if (v.Trim() == "")
                return;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Campo} LIKE @{NomeParametro} ";
            Param.Add(NomeParametro, $"%{v}%");
        }

        public static void FiltroTexto_IniciaCom(string Campo, object Ctr, string Desconsiderar = "")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string NomeParametro = "P" + Param.Count;

            if (Desconsiderar != string.Empty)
            {
                Campo = ReplaceSQL(Campo, Desconsiderar);

                foreach (char ch in Desconsiderar)
                {
                    v = v.Replace(ch.ToString(), "");
                }
            }

            if (v.Trim() == "")
                return;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Campo} LIKE @{NomeParametro} ";
            Param.Add(NomeParametro, $"{v}%");

        }

        public static void FiltroTexto_TerminaCom(string Campo, object Ctr, string Desconsiderar = "")
        {
            string v = Ctr.ToString();

            if (Ctr is Control c)
            {
                if (c.Text == string.Empty)
                    return;

                v = c.Text;
            }

            if (v.Trim() == "")
                return;

            string NomeParametro = "P" + Param.Count;

            if (Desconsiderar != string.Empty)
            {
                Campo = ReplaceSQL(Campo, Desconsiderar);

                foreach (char ch in Desconsiderar)
                {
                    v = v.Replace(ch.ToString(), "");
                }
            }

            if (v.Trim() == "")
                return;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND {Campo} LIKE @{NomeParametro} ";
            Param.Add(NomeParametro, $"%{v}");

        }

        public static void FiltroData_PorDia(string Campo, object Valor, string Operador = "=")
        {
            if (Valor is Control Ctr)
            {
                if (Ctr.Text.Replace(" ", "") == string.Empty)
                    return;
            }

            if (Valor.ToString() == string.Empty)
                return;


            string NomeParametro = "P" + Param.Count;

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND DAY({Campo}) {Operador} @{NomeParametro} ";

            if (Valor is Control ctr)
                Param.Add(NomeParametro, Convert.ToInt32(ctr.Text));
            else
                Param.Add(NomeParametro, Convert.ToInt32(Valor));
        }

        public static void FiltroData_PorMes(string Campo, object Valor, string Operador = "=")
        {
            string v = "";

            if (Valor is ComboBox cb)
            {
                if (cb.Text == string.Empty)
                    return;

                if (cb.SelectedIndex == -1)
                    return;

                v = (cb.SelectedIndex + 1).ToString();
            }
            else
            {
                if (Valor is Control c)
                    v = c.Text;
                else
                    v = Valor.ToString();
            }

            if (v.Trim() == "")
                return;

            int iMes = PegarMes(v.ToString());

            if (iMes > -1)
                v = (iMes + 1).ToString();

            string NomeParametro = "P" + Param.Count;

            if (int.TryParse(v, out int mes))
                Param.Add(NomeParametro, mes);
            else
                Param.Add(NomeParametro, v);

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND Month({Campo}) {Operador} @{NomeParametro} ";

        }

        private static int PegarMes(string Mes)
        {
            string[] meses = { "JANEIRO", "FEVEREIRO", "MARÇO", "ABRIL", "MAIO", "JUNHO", "JULHO", "AGOSTO", "SETEMBRO", "OUTUBRO", "NOVEMBRO", "DEZEMBRO" };
            return Array.IndexOf(meses, Mes.ToUpper());
        }
        public static void FiltroData_PorAno(string Campo, object Valor, string Operador = "=")
        {
            string v = "";

            if (Valor is Control Ctr)
            {
                if (Ctr.Text == string.Empty)
                    return;

                v = Ctr.Text;
            }
            else
            {
                v = Valor.ToString();
            }

            if (v.Trim() == "")
                return;


            string NomeParametro = "P" + Param.Count;
            Param.Add(NomeParametro, v);

            string t = Sql.Replace(" ", "").ToUpper();

            if (t.Contains("WHERE") == false)
                Sql += " WHERE 1=1 ";

            Sql += $" AND Year({Campo}) {Operador} @{NomeParametro} ";

        }


        private static string ReplaceSQL(string Campo, string Desconsiderar)
        {
            string r = "";

            foreach (char c in Desconsiderar)
            {
                if (r == string.Empty)
                    r = $"Replace({Campo}, '{c}', '')";
                else
                    r = $"Replace({r}, '{c}', '')";
            }

            return r;
        }

        private static string INSERT_INTO(string Tabela)
        {
            string Campos = "";
            string Parametros = "";

            foreach (Control Ctr in Controles)
            {
                if (Ctr.Tag.ToString().Contains("|nao_salvar"))
                    continue;

                string Coluna = PegarTag(Ctr, "col").Replace(" ", "");

                if (Campos.Contains(Coluna + ","))
                    continue;

                if (Ctr is ECTurbo_TextBoxSenha txtS)
                {
                    if (txtS.Text != string.Empty)
                    {
                        Campos += Coluna + ", ";
                        Parametros += "@" + Coluna + ", ";
                    }
                }
                else if (Ctr is RadioButton rb)
                {
                    if (rb.Checked == true)
                    {
                        Campos += Coluna + ", ";
                        Parametros += "@" + Coluna + ", ";
                    }
                }
                else
                {
                    Campos += Coluna + ", ";
                    Parametros += "@" + Coluna + ", ";
                }

            }

            Campos = Campos.TrimEnd(' ').TrimEnd(',');
            Parametros = Parametros.TrimEnd(' ').TrimEnd(',');

            return $"INSERT INTO {Tabela} ({Campos}) VALUES ({Parametros})";
        }


        private static string UPDATE(string Tabela)
        {
            string Campos = "";

            foreach (Control Ctr in Controles)
            {
                if (Ctr.Tag.ToString().Contains("|nao_salvar"))
                    continue;

                string Coluna = PegarTag(Ctr, "col").Replace(" ", "");

                if (Campos.Contains(Coluna + " = @" + Coluna))
                    continue;

                if (Ctr is ECTurbo_TextBoxSenha txtS)
                {
                    if (txtS.Text != string.Empty)
                    {
                        Campos += Coluna + " = @" + Coluna + ", ";
                    }
                }
                else if (Ctr is RadioButton rb)
                {
                    if (rb.Checked == true)
                    {
                        Campos += Coluna + " = @" + Coluna + ", ";
                    }
                }
                else
                    Campos += Coluna + " = @" + Coluna + ", ";

            }

            Campos = Campos.TrimEnd(' ').TrimEnd(',');

            return $"UPDATE {Tabela} SET {Campos} WHERE id = @id";
        }



        private static List<Control> Controles = new List<Control>();
        private static void PegarListaCampos(Control CtrPai)
        {
            foreach (Control Ctr in CtrPai.Controls)
            {
                if (Ctr.Controls.Count > 0)
                    PegarListaCampos(Ctr);

                if (Ctr.Tag != null)
                {
                    if (Ctr is ECTurbo_TextBoxSenha txtSenha)
                    {
                        if (txtSenha.SalvarSenhaVazia == false && txtSenha.Text.Trim() == string.Empty)
                            continue;
                    }

                    if (Funcoes.PegarTag(Ctr, "col") == "id")
                        continue;

                    if (Ctr.Tag.ToString().ToLower().Contains("col="))
                    {
                        Controles.Add(Ctr);
                    }
                }
            }
        }

        public static string PegarTag(Control Ctr, string Propriedade)
        {
            string[] Configuracoes = Ctr.Tag.ToString().Split('|');

            foreach (string Config in Configuracoes)
            {
                string[] p = Config.Split('=');
                if (p[0].ToLower() == Propriedade.ToLower())
                    return p[1];
            }

            return "";
        }


        public static bool Salvar(string Tabela,
                                  Control CtrPai,
                                  TextBox CtrID,
                                  bool Limpar = false,
                                  string Msg = "",
                                  bool MostrarMsg = true)
        {
            Controles.Clear();

            PegarListaCampos(CtrPai);

            if (Controles.Count == 0)
            {
                Funcoes.MsgErro("Configure a propriedade\r\n(DB.1 Coluna Tabela)\r\nPara um controle pelo menos.");
                return false;
            }

            if (ValidacoesGerais(Tabela, CtrID) == true)
                return false;

            string ListaColunas = "";

            bool DeuCerto;
            bool Novo = false;

            using (MySqlConnection con = Conectar())
            {
                con.Open();

                using (MySqlCommand cmd = con.CreateCommand())
                {
                    if (CtrID.Text == string.Empty)
                        cmd.CommandText = INSERT_INTO(Tabela);
                    else
                        cmd.CommandText = UPDATE(Tabela);

                    foreach (Control Ctr in Controles)
                    {
                        if (Ctr.Tag.ToString().Contains("|nao_salvar"))
                            continue;

                        string Coluna = PegarTag(Ctr, "col");

                        if (ListaColunas.Contains(":" + Coluna + "|") && Ctr is RadioButton == false)
                        {
                            string[] f = ListaColunas.Split('|');

                            foreach (string item in f)
                            {
                                if (item != "")
                                {
                                    if (Coluna == item.Split(':')[1])
                                    {
                                        Funcoes.MsgErro($"Coluna '{Coluna}' foi usada duas vezes, controles:\r\n'{item.Split(':')[0]}' e '{Ctr.Name}'");
                                    }
                                }
                            }

                            return false;
                        }

                        ListaColunas += Ctr.Name + ":" + Coluna + "|";

                        if (Ctr is ECTurbo_TextBoxSenha txtS)
                        {

                            if (txtS.AtivarCriptografia == true && txtS.Text != string.Empty)
                            {
                                cmd.Parameters.AddWithValue("@" + Coluna, Funcoes.Criptografar(txtS.Text));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@" + Coluna, txtS.Text);
                            }

                            continue;
                        }

                        if (Ctr is PictureBox pc)
                        {
                            string resource = PegarTag(Ctr, "padrao");

                            Image ImgPadrao = (Image)Resources.ResourceManager.GetObject(resource);

                            if (Funcoes.CompararImagens(pc.Image, ImgPadrao) == false)
                            {
                                byte[] img = File.ReadAllBytes(Funcoes.MiniaturaImagem(pc));
                                cmd.Parameters.AddWithValue("@" + Coluna, img);
                            }
                            else
                                cmd.Parameters.AddWithValue("@" + Coluna, DBNull.Value);

                            continue;
                        }

                        if (Ctr is RadioButton rb)
                        {
                            if (rb.Checked == true)
                            {
                                if (rb.Tag.ToString().ToLower().Contains("|valor="))
                                    cmd.Parameters.AddWithValue("@" + Coluna, PegarTag(rb, "valor"));
                                else
                                    cmd.Parameters.AddWithValue("@" + Coluna, rb.Text);
                            }

                            continue;
                        }


                        if (Ctr is CheckBox ck)
                        {
                            if (ck.Checked == true)
                            {
                                if (ck.Tag.ToString().ToLower().Contains("|valor="))
                                    cmd.Parameters.AddWithValue("@" + Coluna, PegarTag(ck, "valor"));
                                else
                                    cmd.Parameters.AddWithValue("@" + Coluna, "S");
                            }
                            else
                            {
                                if (ck.Tag.ToString().ToLower().Contains("|valorf="))
                                    cmd.Parameters.AddWithValue("@" + Coluna, PegarTag(ck, "valorF"));
                                else
                                    cmd.Parameters.AddWithValue("@" + Coluna, "N");
                            }

                            continue;
                        }

                        //Para quando for numerico
                        if (Ctr.Tag.ToString().ToLower().Contains("|numero"))
                        {
                            string v = Funcoes.NormalizarNumero(Ctr.Text);

                            if (v == string.Empty)
                                cmd.Parameters.AddWithValue("@" + Coluna, DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@" + Coluna, Convert.ToDouble(v));

                            continue;
                        }


                        //Para quando for datas
                        if (Ctr.Tag.ToString().ToLower().Contains("|data"))
                        {
                            if (Ctr.Text == string.Empty)
                                cmd.Parameters.AddWithValue("@" + Coluna, DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@" + Coluna, Convert.ToDateTime(Ctr.Text));

                            continue;
                        }


                        if (Ctr is ComboBox cb)
                        {
                            if (string.IsNullOrEmpty(cb.ValueMember) == false)
                            {
                                if (cb.SelectedValue == null)
                                    cmd.Parameters.AddWithValue("@" + Coluna, DBNull.Value);
                                else
                                    cmd.Parameters.AddWithValue("@" + Coluna, cb.SelectedValue);

                                continue;
                            }
                        }

                        if (Ctr is MaskedTextBox mk)
                        {
                            MaskFormat formatoAtual = mk.TextMaskFormat;

                            mk.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                            if (mk.Text == "")
                            {
                                cmd.Parameters.AddWithValue("@" + Coluna, "");
                                mk.TextMaskFormat = formatoAtual;
                                continue;
                            }

                            mk.TextMaskFormat = formatoAtual;
                            cmd.Parameters.AddWithValue("@" + Coluna, Ctr.Text);
                            continue;
                        }

                        cmd.Parameters.AddWithValue("@" + Coluna, Ctr.Text);
                    }

                    if (CtrID.Text != string.Empty)
                        cmd.Parameters.AddWithValue("@id", CtrID.Text);


                    try
                    {
                        if (CtrID.Text == string.Empty)
                        {
                            Novo = true;
                            cmd.CommandText += "; SELECT @@IDENTITY";
                            CtrID.Text = cmd.ExecuteScalar().ToString();
                        }
                        else
                            cmd.ExecuteNonQuery();

                        DeuCerto = true;
                    }
                    catch (Exception e)
                    {
                        //Unknown column 'nom' in 'field list'
                        DeuCerto = false;

                        string erro = e.Message;

                        if (erro.Contains("Unknown column '"))
                        {
                            erro = erro.Replace("Unknown column '", "")
                                       .Replace("' in 'field list'", "");
                        }
                        else
                        {
                            Funcoes.MsgErro(erro);
                        }

                        string[] f = ListaColunas.Split('|');

                        foreach (string item in f)
                        {
                            if (item != "")
                            {
                                if (erro.ToUpper() == item.Split(':')[1].ToUpper())
                                {
                                    Funcoes.MsgErro($"Nome de Coluna Errado ({erro}):\r\n\r\nConfira o controle: {item.Split(':')[0]}");
                                }
                            }
                        }

                    }
                }
            }

            if (MostrarMsg == true)
            {


                //Aqui
                if (DeuCerto == true)
                {

                    if (Msg != "")
                    {
                        Funcoes.MsgOk(Msg);
                    }
                    else
                    {
                        if (Novo == true)
                            Funcoes.MsgOk("Salvo com sucesso!");
                        else
                            Funcoes.MsgOk("Alterado com sucesso!");
                    }
                }
            }

            if (DeuCerto && Limpar)
                Funcoes.LimparControles(CtrPai, false);


            return DeuCerto;
        }


        public static DataTable PegarDados(string Tabela,
                                      Control CtrPai,
                                      Control CtrId = null,
                                      string CampoBusca = "id",
                                      string ValorBusca = "")
        {
            DataTable dt = new DataTable();

            if (CampoBusca != "id" && ValorBusca == string.Empty)
                return dt;

            if (CtrId.Text == string.Empty && ValorBusca == "")
                return dt;


            using (MySqlConnection con = Conectar())
            {
                con.Open();

                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {Tabela} WHERE {CampoBusca} = @{CampoBusca}";

                    if (CampoBusca == "id")
                        cmd.Parameters.AddWithValue("@" + CampoBusca, CtrId.Text);
                    else
                        cmd.Parameters.AddWithValue("@" + CampoBusca, ValorBusca);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            if (dt.Rows.Count == 0)
            {
                if (ValorBusca == "")
                    Funcoes.MsgErro($"O id '{CtrId.Text}' não existe na tabela {Tabela}");

                Funcoes.LimparControles(CtrPai, false);

                return dt;
            }

            Controles.Clear();
            PegarListaCampos(CtrPai);

            foreach (Control Ctr in Controles)
            {
                string Coluna = PegarTag(Ctr, "col");

                if (Ctr is ECTurbo_TextBoxSenha txtS)
                {
                    if (txtS.TrazerSenhaBanco)
                    {
                        Ctr.Text = dt.Rows[0][Coluna].ToString();
                    }
                    else
                    {
                        Ctr.Text = "";
                    }

                    continue;
                }

                if (Ctr is PictureBox pc)
                {
                    if (dt.Rows.Count > 0)
                        Funcoes.PegarFotoBanco(pc, dt.Rows[0][Coluna]);
                    else
                        Funcoes.PegarFotoBanco(pc, null);

                    continue;
                }

                if (Ctr is CheckBox ck)
                {
                    if (dt.Rows[0][Coluna].ToString() == "S")
                    {
                        ck.Checked = true;
                        continue;
                    }

                    if (dt.Rows[0][Coluna].ToString() == "N")
                    {
                        ck.Checked = false;
                        continue;
                    }

                    if (dt.Rows[0][Coluna].ToString() == PegarTag(ck, "valor"))
                    {
                        ck.Checked = true;
                        continue;
                    }

                    if (dt.Rows[0][Coluna].ToString() == PegarTag(ck, "valorF"))
                    {
                        ck.Checked = false;
                        continue;
                    }
                }

                if (Ctr is RadioButton rb)
                {
                    if (dt.Rows[0][Coluna].ToString() == rb.Text)
                    {
                        rb.Checked = true;
                        continue;
                    }

                    if (rb.Tag.ToString().ToLower().Contains("|valor"))
                    {
                        if (PegarTag(rb, "valor") == dt.Rows[0][Coluna].ToString())
                        {
                            rb.Checked = true;
                            continue;
                        }
                    }

                    rb.Checked = false;
                    continue;
                }



                if (Ctr is ComboBox cb)
                {
                    if (string.IsNullOrEmpty(cb.ValueMember) == false)
                    {
                        cb.SelectedValue = dt.Rows[0][Coluna];
                        continue;
                    }
                }

                if (Ctr.Tag.ToString().ToLower().Contains("|data"))
                {
                    //Ctr.Text = dt.Rows[0][Coluna].ToString().Replace(" 00:00:00", "");

                    try
                    {
                        string Formato = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                        Ctr.Text = Convert.ToDateTime(dt.Rows[0][Coluna]).ToString(Formato);
                    }
                    catch (Exception)
                    {
                        Ctr.Text = "";
                    }
                    continue;
                }

                if (Ctr.Tag.ToString().ToLower().Contains("|moeda"))
                {
                    if (dt.Rows[0][Coluna] == null || dt.Rows[0][Coluna] == DBNull.Value)
                        Ctr.Text = "";
                    else
                        Ctr.Text = Convert.ToDouble(dt.Rows[0][Coluna]).ToString("C");

                    continue;
                }

                Ctr.Text = dt.Rows[0][Coluna].ToString();

            }

            if (CtrId != null)
                CtrId.Text = dt.Rows[0]["id"].ToString();

            return dt;

        }

        private static bool ValidacoesGerais(string Tabela, Control CtrId)
        {
            bool DeuErro = false;

            foreach (Control Ctr in Controles)
            {

                Funcoes.RemoverLabel(Ctr);

                string tag = Ctr.Tag.ToString().ToLower();



                if (Ctr is ComboBox cb)
                {
                    if (cb.Text != string.Empty)
                    {
                        if (string.IsNullOrEmpty(cb.ValueMember) == false)
                        {
                            if (cb.SelectedIndex == -1)
                            {
                                Funcoes.CriarLabel(Ctr, "Item inválido",
                                                  descricao: "Por favor selecione apenas itens da lista.");
                                DeuErro = true;
                                continue;
                            }
                        }
                    }
                }

                //Campos obrigatórios
                if (tag.Contains("|obgt"))
                {
                    if (Ctr is ECTurbo_MaskedTextBox mk)
                    {
                        if (mk.SalvarMascara == true)
                        {
                            mk.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                            if (mk.Text == string.Empty)
                            {
                                Funcoes.CriarLabel(mk, "Obrigatório", descricao: "Por favor preencha este campo para prosseguir.");
                                DeuErro = true;
                                continue;
                            }

                            mk.TextMaskFormat = MaskFormat.IncludeLiterals;

                        }
                    }
                    else if (Ctr is ECTurbo_Imagem img)
                    {
                        string resource = PegarTag(img, "padrao");

                        Image ImgPadrao = (Image)Resources.ResourceManager.GetObject(resource);

                        if (Funcoes.CompararImagens(img.Image, ImgPadrao) == true)
                        {
                            Funcoes.CriarLabel(img, "Obrigatório", descricao: "Por favor selecione uma imagem.");
                            DeuErro = true;
                            continue;
                        }
                    }
                    else if (Ctr.Text == string.Empty)
                    {
                        Funcoes.CriarLabel(Ctr, "Obrigatório", descricao: "Por favor preencha este campo para prosseguir.");
                        DeuErro = true;
                        continue;
                    }

                }

                //Para campos numéricos
                if (tag.Contains("|numero"))
                {
                    if (Ctr.Text == string.Empty)
                        continue;

                    string v = Funcoes.NormalizarNumero(Ctr.Text);

                    try
                    {
                        double valor = Convert.ToDouble(v);

                        if (valor == 0 && tag.Contains("|zerado") == false)
                        {
                            Funcoes.CriarLabel(Ctr, "Valor Inválido", descricao: "Este campo não pode permanecer zerado.");
                            DeuErro = true;
                            continue;
                        }


                        if (valor < 0 && tag.Contains("|negativo") == false)
                        {
                            Funcoes.CriarLabel(Ctr, "Valor Inválido", descricao: "Este campo não pode permanecer negativo.");
                            DeuErro = true;
                            continue;
                        }
                    }
                    catch
                    {
                        Funcoes.CriarLabel(Ctr, "Valor Inválido", descricao: "Este valor não é um número válido.");
                        DeuErro = true;
                        continue;
                    }

                }

                //Para campos Datas
                if (tag.Contains("|data"))
                {
                    if (Ctr.Text == string.Empty)
                        continue;

                    try
                    {
                        Convert.ToDateTime(Ctr.Text);
                    }
                    catch
                    {
                        Funcoes.CriarLabel(Ctr, "Data Inválida", descricao: "Esta data não é válida.");
                        DeuErro = true;
                        continue;
                    }
                }

                //Para valores duplicados
                if (tag.Contains("|unico"))
                {
                    if (Duplicidade(Tabela, Ctr, CtrId) == true)
                        DeuErro = true;
                }

            }


            Dictionary<string, bool> rbGrupos = new Dictionary<string, bool>();
            Dictionary<string, RadioButton> rbMostrarErro = new Dictionary<string, RadioButton>();

            foreach (var controle in Controles)
            {
                if (controle is RadioButton radioButton && radioButton.Tag != null)
                {
                    if (controle.Tag.ToString().ToLower().Contains("obgt"))
                    {
                        string grupo = PegarTag(controle, "col");

                        if (!rbGrupos.ContainsKey(grupo))
                        {
                            rbGrupos[grupo] = false;
                        }

                        if (radioButton.Checked)
                        {
                            rbGrupos[grupo] = true;
                        }

                        if (controle.Tag.ToString().ToLower().Contains("|x"))
                        {
                            rbMostrarErro[grupo] = radioButton;
                        }
                    }
                }
            }

            foreach (var item in rbMostrarErro)
            {
                string grupo = PegarTag(rbMostrarErro[item.Key], "col");

                if (rbGrupos[grupo] == false)
                {
                    Funcoes.CriarLabel(rbMostrarErro[item.Key], "Marque uma Opção.", descricao: "É necessário selecionar uma das opções acima");
                    DeuErro = true;
                }
                else
                    Funcoes.RemoverLabel(rbMostrarErro[item.Key]);

            }


            return DeuErro;
        }


        private static bool Duplicidade(string Tabela, Control Ctr, Control CtrId)
        {
            if (Ctr.Text == string.Empty)
                return false;

            string coluna = PegarTag(Ctr, "col");
            string msg = PegarTag(Ctr, "unico");

            if (CtrId.Text == string.Empty)
                Sql = $"SELECT id FROM {Tabela} WHERE {coluna} = @{coluna}";
            else
                Sql = $"SELECT id FROM {Tabela} WHERE {coluna} = @{coluna} AND id <> {CtrId.Text}";

            Param.Add(coluna, Ctr.Text);

            DataTable dt = BuscaSQL();

            if (dt.Rows.Count > 0)
            {
                Funcoes.CriarLabel(Ctr, msg);
                return true;
            }

            return false;
        }


        public static bool Excluir(string Tabela, string Id, string Msg = "", bool Confirmar = true)
        {
            if (Confirmar == true)
            {
                if (Msg == "")
                    Msg = "Deseja excluir este registro?\r\n\r\n* " +
                                     "Esta ação não poderá ser desfeita.";

                if (Funcoes.Pergunta(Msg) == false)
                    return false;
            }

            try
            {
                using (MySqlConnection con = Conectar())
                {
                    con.Open();

                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = $"DELETE FROM {Tabela} WHERE id = @id";

                        cmd.Parameters.AddWithValue("@id", Id);

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static object ExecutarSQL()
        {
            object resultado = null;

            using (MySqlConnection con = Conectar())
            {
                con.Open();

                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = Sql;

                    foreach (var p in Param)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }


                    if (Sql.Trim().ToUpper().StartsWith("INSERT INTO"))
                    {
                        cmd.CommandText += "; SELECT @@IDENTITY";
                        resultado = cmd.ExecuteScalar().ToString();
                    }
                    else
                    {
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (Sql.Trim().ToUpper().StartsWith("UPDATE ") || Sql.Trim().ToUpper().StartsWith("DELETE "))
                        {
                            resultado = linhasAfetadas > 0;
                        }
                    }
                }
            }

            Param.Clear();

            return resultado;
        }

        //public static string ExecutarSQL()
        //{
        //    string id = "";

        //    using (MySqlConnection con = Conectar())
        //    {
        //        con.Open();

        //        using (MySqlCommand cmd = con.CreateCommand())
        //        {
        //            cmd.CommandText = Sql;

        //            foreach (var p in Param)
        //            {
        //                cmd.Parameters.AddWithValue(p.Key, p.Value);
        //            }

        //            if (Sql.ToUpper().Contains("INSERT INTO"))
        //            {
        //                cmd.CommandText += "; SELECT @@IDENTITY";
        //                id = cmd.ExecuteScalar().ToString();
        //            }
        //            else
        //                cmd.ExecuteNonQuery();
        //        }
        //    }

        //    Param.Clear();

        //    return id;
        //}

        public static DataTable BuscaSQL(Control Exibir = null)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = Conectar())
            {
                con.Open();

                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = Sql;

                    foreach (var p in Param)
                    {
                        cmd.Parameters.AddWithValue("@" + p.Key, p.Value);
                    }
                    try
                    {
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception ex) { Funcoes.MsgErro(ex.Message); }

                }

            }

            Param.Clear();

            if (Exibir != null)
            {
                if (Exibir is ComboBox cb)
                {
                    cb.DataSource = dt;
                    cb.SelectedIndex = -1;
                }
                else if (Exibir is DataGridView ds)
                {
                    ds.DataSource = dt;
                    ds.ClearSelection();
                }
            }

            return dt;
        }

        public static void CarregarComboBox(string Tabela, string Campo, ComboBox cb, bool CarregarId = false, string Condicao = "")
        {

            if (cb is ECTurbo_ComboBox cb2)
            {
                if (cb2.SalvarId)
                {
                    CarregarId = true;
                    cb2.ApenasItensLista = true;
                }
            }

            cb.DisplayMember = Campo;

            if (Condicao != string.Empty)
                Condicao += " AND ";

            Sql = $"SELECT DISTINCT {Campo} FROM {Tabela} WHERE {Condicao} {Campo} <> '' AND {Campo} IS NOT NULL ORDER BY {Campo}";

            if (CarregarId == true)
            {
                Sql = $"SELECT DISTINCT id, {Campo} FROM {Tabela} WHERE {Condicao} {Campo} <> '' AND {Campo} IS NOT NULL ORDER BY {Campo}";
                cb.ValueMember = "id";
            }

            BuscaSQL(cb);
        }
        public static void CarregarDadosReport(DataTable dt)
        {
            DataTable dt1 = BuscaSQL();

            int a = dt1.Columns.Count;

            for (int x = 0; x < dt.Columns.Count; x++)
            {
                for (int i = 0; i < dt1.Columns.Count; i++)
                {
                    if (dt1.Columns[i].ColumnName == dt.Columns[x].ColumnName)
                    {
                        dt.Columns[x].DataType = dt1.Columns[i].DataType;
                        a--;
                        continue;
                    }
                }

            }

            dt.Merge(dt1);
        }

    }
}
