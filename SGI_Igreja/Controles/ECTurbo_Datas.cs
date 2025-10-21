using System.ComponentModel;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_Datas : ECTurbo_TextBox
    {
        public ECTurbo_Datas() {
            TextAlign = HorizontalAlignment.Center;
            Tag = "|data";
        }



        private bool vDataAtual = false;
        [DisplayName("_Iniciar com Data Atual")]
        [Description("Se ativado o controle já será iniciado com a data atual")]
        [Category("_ECTurbo")]
        public bool DataAtual
        {
            get { return vDataAtual; }
            set { 
            
                vDataAtual = value;

                if (value)
                {
                    if (!Tag.ToString().Contains("|data_atual"))
                        Tag += "|data_atual";
                }
                else
                {
                    Tag = Tag.ToString().Replace("|data_atual", "");
                }
            }
        }





        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if(DataAtual == true)
            {
                Text = DateTime.Now.ToShortDateString();
            }

        }

        protected override void OnValidating(CancelEventArgs e)
        {
            Funcoes.RemoverLabel(this);

            if (Text == string.Empty)
                return;

            try
            {
                Text = Convert.ToDateTime(Text).ToShortDateString();
            }
            catch (Exception)
            {
                Funcoes.CriarLabel(this, "Data inválida");
                e.Cancel = true;
                return;
            }

            string[] q = Text.Split('/');

            if(Convert.ToInt32(q[2]) < 1582)
            {
                Funcoes.CriarLabel(this, "Ano inválido", descricao: "O ano válido deve ser acima de 1582");
                SelectionStart = Text.Length;
                e.Cancel = true;
            }

            base.OnValidating(e);

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);


            if (ReadOnly)
                return;

            // Permitir atalhos como Ctrl+C, Ctrl+V, Ctrl+X
            if ((ModifierKeys & Keys.Control) == Keys.Control)
                return;

            //Liberar backspace
            if (e.KeyChar == (char)Keys.Back)
                return;

            // Limpar o texto apenas se for um caractere digitável e tudo estiver selecionado
            if (SelectionLength == Text.Length && char.IsDigit(e.KeyChar))
                Text = string.Empty;


            int i = SelectionStart;

            int t = Text.Length;


            //Bloquear quando a data estiver completa
            if (t == 10) goto Continuar;

            string[] q = Text.Split('/');
            try
            {
                if (Convert.ToInt32(q[0]) > 31)
                    goto Continuar;

                if (Convert.ToInt32(q[1]) > 12)
                    goto Continuar;

            }
            catch (Exception) { }



            //Melhorias ao digitar a barra de forma manual
            if (e.KeyChar.ToString() == "/")
            {

                if (t == 0) goto Continuar;

                if (Text == "0") goto Continuar;

                if (t == 1)
                {
                    Text = "0" + Text + "/";
                }
                else if (t == 2 || t == 5)
                {
                    Text += "/";
                }
                else if (t == 4)
                {
                    Text = q[0] + "/0" + q[1] + "/";
                }

                SelectionStart = Text.Length;

            }


            //Bloquear caracteres nao numericos
            if (char.IsDigit(e.KeyChar) == false) goto Continuar;

            if (t == 2 || t == 5)
            {
                Text += "/";

                Text = Text.Insert(i + 1, e.KeyChar.ToString());


                SelectionStart = Text.Length;

                e.Handled = true;

                return;
            }

            //SelectionStart = Text.Length;

            Text = Text.Insert(i, e.KeyChar.ToString());

            SelectionStart = i + 1;

            e.Handled = true;

            return;
        Continuar:

            e.Handled = true;

        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Text == string.Empty)
                return;

            string[] q = Text.Split('/');


            try
            {
                Funcoes.RemoverLabel(this);

                if (Convert.ToInt32(q[0]) > 31)
                {
                    Funcoes.CriarLabel(this, "Dia inválido", "alerta", descricao:"O dia válido deve ser menor ou igual a 31");
                }
                else if (Convert.ToInt32(q[1]) > 12)
                {
                    Funcoes.CriarLabel(this, "Mês inválido", "alerta", descricao: "O mÊs válido deve ser menor ou igual a 12");
                }
            }
            catch (Exception) { }
        }

    }
}
