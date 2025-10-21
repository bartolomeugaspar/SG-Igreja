using System.Data;
using System.Reflection;

using ECTurbo.Controles;

namespace ECTurbo.Codigos
{
    public class GRID
    {
        private static Color CorMovimento = Color.Lavender;

        public static int CalcularTotalPaginas(DataTable Dt, int QtdLinhasGrid)
        {
            return (int)Math.Ceiling((decimal)Dt.Rows.Count / QtdLinhasGrid);
        }
        public static void MontarGrid(FlowLayoutPanel AreaGrid, 
                                      UserControl LinModelo, 
                                      int Qtd,
                                      Color CorPrincipal = default,
                                      Color CorAlternativa = default,
                                      Color CorMov = default,
                                      bool Ver = true)
        {

            if (CorPrincipal == default)
                CorPrincipal = Color.White;

            if (CorAlternativa == default)
                CorAlternativa = Color.AliceBlue;

            if (CorMov != default)
                CorMovimento = CorMov;

            AreaGrid.AutoScroll = true;
            AreaGrid.Controls.Clear();

            for (int i = 1; i <= Qtd; i++)
            {
                UserControl lst = (UserControl)Activator.CreateInstance(LinModelo.GetType());

                foreach (Control Ctr in lst.Controls)
                {
                    if(Ctr is ECTurbo_Label lb)
                    {
                        lb.Tag += "|cor1=" + lb.Cor1.ToArgb().ToString();
                        lb.Tag += "|cor2=" + lb.Cor2.ToArgb().ToString();
                    }
                    else
                    {
                        Ctr.Tag += "|cor1=" + Ctr.ForeColor.ToString();
                    }
                }

                lst.Name = "lst" + i;

                if (i % 2 == 0)
                    lst.BackColor = CorAlternativa;
                else
                    lst.BackColor = CorPrincipal;

                lst.Tag = lst.BackColor;

                lst.MouseEnter += Lst_MouseEnter;
                lst.MouseLeave += Lst_MouseLeave;

                lst.Visible = Ver;

                AreaGrid.Controls.Add(lst);
            }
        }

        private static void Lst_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is UserControl lst)
            {
                Point mouseP = lst.PointToClient(Control.MousePosition);

                if(lst.ClientRectangle.Contains(mouseP))
                    return;

                lst.BackColor = (Color)lst.Tag;

            }
        }

        private static void Lst_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is UserControl lst)
            {
                foreach (UserControl lin in lst.Parent.Controls)
                {
                    lin.BackColor = (Color)lin.Tag;
                }

                lst.BackColor = CorMovimento;
            }
        }

        public static void Colorir(UserControl LinModelo, Color Cor = default, Color Cor2 = default)
        {
            if (Cor == default)
            {
                foreach (Control Ctr in LinModelo.Controls)
                {
                    if(Ctr is ECTurbo_Label lb)
                    {
                        lb.Cor1 = Color.FromArgb(int.Parse(Funcoes.PegarTag(lb, "cor1")));
                        lb.Cor2 = Color.FromArgb(int.Parse(Funcoes.PegarTag(lb, "cor2")));
                    }
                    else
                    {
                        string corStr = Funcoes.PegarTag(Ctr, "cor1");
                        string nomeCor = corStr.Replace("Color [", "").Replace("]", "");
                        Color cor = Color.FromName(nomeCor);
                        int valorArgb = cor.ToArgb();
                        Ctr.ForeColor = Color.FromArgb(valorArgb);
                    }
                }
            }
            else
            {
                foreach (Control Ctr in LinModelo.Controls)
                {
                    if (Ctr.Tag == null)
                    {
                        Ctr.ForeColor = Cor;

                        if (Ctr is ECTurbo_Label lb)
                        {
                            if (Cor2 != default)
                                lb.Cor2 = Cor2;
                            else
                                lb.Cor2 = Cor;
                        }
                    }
                    else
                    {
                        if (Ctr.Tag.ToString().Contains("nao_colorir") == false)
                        {
                            Ctr.ForeColor = Cor;

                            if (Ctr is ECTurbo_Label lb)
                            {
                                lb.Cor1 = Cor;

                                if (Cor2 != default)
                                    lb.Cor2 = Cor2;
                                else
                                    lb.Cor2 = Cor;
                            }
                        }
                    }
                }
            }
        }

        public static void ExecutarFuncao(Control ctr, string nomeFuncao, object[]? argumentos = null)
        {
            Control? controleAtual = ctr;

            if (argumentos == null)
            {
                argumentos = [0];
                argumentos[0] = ctr;
            }

            while (controleAtual != null && !(controleAtual is Form))
            {
                controleAtual = controleAtual.Parent;
            }

            if (controleAtual is Form formularioAtual)
            {
                MethodInfo? metodo = formularioAtual.GetType().GetMethod(nomeFuncao, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                metodo?.Invoke(formularioAtual, argumentos);
            }
        }

    }
}
