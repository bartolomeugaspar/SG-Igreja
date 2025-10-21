using System.ComponentModel;
using System.Text.RegularExpressions;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_Numericos : ECTurbo_TextBox
    {
        public ECTurbo_Numericos() {
            Tag = "|numero";
        }

        private int vCasasDecimais = 2;

        [DisplayName("_Casas Decimais")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public int CasasDecimais
        {
            get { return vCasasDecimais; }
            set { vCasasDecimais = value; }
        }



        private bool vMoeda = false;

        [DisplayName("_Formatar como Moeda")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public bool Moeda
        {
            get { return vMoeda; }
            set
            {
                vMoeda = value;

                if (value)
                {
                    if (!Tag.ToString().Contains("|moeda"))
                        Tag += "|moeda";
                }
                else
                {
                    Tag = Tag.ToString().Replace("|moeda", "");
                }
            }
        }





        private bool vForcarFormatacao = false;

        [DisplayName("_Forçar Formatação")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public bool ForcarFormatacao
        {
            get { return vForcarFormatacao; }
            set { vForcarFormatacao = value; }
        }


        private bool vPermitirZerado = false;

        [DisplayName("_Permitir Valores Zerados")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public bool PermitirZerado
        {
            get { return vPermitirZerado; }
            set { 

                vPermitirZerado = value;

                if (value)
                {
                    if (!Tag.ToString().Contains("|zerado"))
                        Tag += "|zerado";
                }
                else
                {
                    Tag = Tag.ToString().Replace("|zerado", "");
                }
            }
        }




        private bool vPermitirNegativo = false;

        [DisplayName("_Permitir Valores Negativos")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public bool PermitirNegativo
        {
            get { return vPermitirNegativo; }
            set { 
                
                vPermitirNegativo = value;

                if (value)
                {
                    if (!Tag.ToString().Contains("|negativo"))
                        Tag += "|negativo";
                }
                else
                {
                    Tag = Tag.ToString().Replace("|negativo", "");
                }
            }
        }


        private bool vColorirNegativo = true;

        [DisplayName("_Colorir de Vermelho Valores Negativos")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public bool ColorirNegativo
        {
            get { return vColorirNegativo; }
            set { vColorirNegativo = value; }
        }


        private bool vFormatarAuto = false;
        [DisplayName("_Formatar ao Digitar")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public bool FormatarAuto
        {
            get { return vFormatarAuto; }
            set { vFormatarAuto = value; if (value == true) Moeda = true; }
        }

        private bool vFormatarAutoRS = true;
        [DisplayName("_Formatar ao Digitar com R$")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public bool FormatarAutoRS
        {
            get { return vFormatarAutoRS; }
            set { vFormatarAutoRS = value; }
        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (FormatarAuto)
            {
                Text = string.Format("{0:#,##0.00}", 0d);

                if (FormatarAutoRS)
                    Text = "R$ " + Text;
            }
        }


        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);


            if (ReadOnly)
                return;

            if (Text.Length > 0 && SelectionLength == Text.Length && (e.KeyChar == '+' == false && e.KeyChar == '-' == false))
            {
                Text = string.Empty;
                return;
            }

            if (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals((char)Keys.Back) && e.KeyChar == ',' == false && e.KeyChar == '+' == false && e.KeyChar == '-' == false)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == ',' && Text.Contains(","))
            {
                e.Handled = true;
                return;
            }

            if (PermitirNegativo == false && e.KeyChar == '-')
            {
                e.Handled = true;
                return;
            }

            if (PermitirNegativo == true && e.KeyChar == '-')
            {
                if (Text.Contains("-"))
                    goto Continuar;

                Text = "-" + Text;
                goto Continuar;
            }

            if (e.KeyChar == '+')
            {
                Text = Text.Replace("-", "");
                SelectionStart = Text.Length;
                goto Continuar;
            }

            if (FormatarAuto == true)
            {
                if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back))
                {
                    bool Neg = false;

                    if (Text.Contains("-"))
                        Neg = true;

                    string vTxtBox = Regex.Replace(Text, "[^0-9]", string.Empty);

                    if (vTxtBox == string.Empty)
                        vTxtBox = "0,00";

                    if (e.KeyChar.Equals((char)Keys.Back))
                    {
                        vTxtBox = vTxtBox.Substring(0, vTxtBox.Length - 1);
                    }
                    else
                    {
                        vTxtBox += e.KeyChar;
                    }

                    if (vTxtBox == string.Empty)
                        vTxtBox = "0";

                    Text = string.Format("{0:#,##0.00}", Double.Parse(vTxtBox) / 100);
                    Text = Text;

                    if (FormatarAutoRS)
                        Text = "R$ " + Text;

                    if(Neg)
                        Text = "-" + Text;

                    Select(Text.Length, 0);
                }

                e.Handled = true;

            }
            else
            {
                if (e.KeyChar == (char)Keys.Back)
                    return;

                

                if (e.KeyChar == ',')
                {
                    if (Text == string.Empty)
                    {
                        Text = "0,";
                        e.Handled = true;
                        SelectionStart = Text.Length;
                        return;
                    }

                    if (Text.Contains(','))
                        goto Continuar;
                }


                if (e.KeyChar == ',')
                {
                    if (SelectionStart < Text.Length - CasasDecimais)
                    {
                        e.Handled = true;
                        return;
                    }
                }

                if (Text.Contains(","))
                {
                    string[] q = Text.Split(",");

                    if (q[1].Length == CasasDecimais && SelectionStart > Text.Length - CasasDecimais - 1)
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }

            return;

        Continuar:

            int i = SelectionStart;

            //SelectionStart = Text.Length;

            if (e.KeyChar == '+' || e.KeyChar == '-')
            {
                e.Handled = true;
                SelectionStart = Text.Length;
                return;
            }

            Text = Text.Insert(i, e.KeyChar.ToString());

            SelectionStart = i + 1;

            e.Handled = true;



        }







        protected override void OnValidating(CancelEventArgs e)
        {
            

            Funcoes.RemoverLabel(this);

            if (Text == string.Empty)
                return;

            string v = Funcoes.NormalizarNumero(Text);

            try
            {
                double valor = Convert.ToDouble(v);

                Text = valor.ToString(Moeda == true ? "C" : "N" + CasasDecimais);

                if (ForcarFormatacao == false && Moeda == false)
                {
                    Text = Text.TrimEnd('0');
                    Text = Text.TrimEnd(',');
                }


                

                if(valor.ToString("N" + CasasDecimais) == (0).ToString("N" + CasasDecimais) && PermitirZerado == false)
                {
                    Funcoes.CriarLabel(this, "Valor Inválido", descricao: "Este campo não pode permanecer com valores zerados");
                    e.Cancel = true;
                    SelectionStart = Text.Length;
                    return;
                }

            }
            catch (Exception)
            {
                goto Continuar;
            }

            base.OnValidating(e);
            return;

        Continuar:
            Funcoes.CriarLabel(this, "Valor inválido");
            e.Cancel = true;
            SelectionStart = Text.Length;

            
        }



        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (ColorirNegativo == true)
            {
                if (Text.Contains('-'))
                    ForeColor = Color.Red;
                else
                    ForeColor = Color.FromArgb(64, 64, 64);
            }

        }

    }
}
