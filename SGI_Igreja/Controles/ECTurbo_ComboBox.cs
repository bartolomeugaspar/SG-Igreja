using System;
using System.ComponentModel;
using System.Windows.Forms;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_ComboBox : ComboBox
    {
        public ECTurbo_ComboBox()
        {
            Tag = "";
            AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private bool vLimpeza = true;
        [DisplayName("_Limpeza Automática")]
        public bool Limpeza
        {
            get { return vLimpeza; }
            set
            {

                vLimpeza = value;
                if (value)
                {
                    Tag = Tag.ToString().Replace("|nao_limpar", "");
                }
                else
                {
                    if (!Tag.ToString().Contains("|nao_limpar"))
                        Tag += "|nao_limpar";
                }

            }
        }

        private string vColuna = "";
        [DisplayName("(DB.1 Coluna Tabela)")]
        public string Coluna
        {
            get { return vColuna; }
            set
            {

                vColuna = value;

                string valor = Funcoes.PegarTag(this, "col");

                Tag = Tag.ToString().Replace("col=" + valor, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag = "col=" + value + Tag.ToString();

            }
        }

        private bool vObgt = false;
        [DisplayName("(DB.2 Campo Obrigatório)")]
        public bool Obgt
        {
            get { return vObgt; }
            set
            {

                vObgt = value;

                if (value == false)
                    Tag = Tag.ToString().Replace("|obgt", "");
                else
                    Tag += "|obgt";
            }
        }

        private string vUnico = "";
        [DisplayName("(DB.3 Msg para Duplicidade)")]
        public string Unico
        {
            get { return vUnico; }
            set
            {

                vUnico = value;

                string valor = Funcoes.PegarTag(this, "unico");

                Tag = Tag.ToString().Replace("|unico=" + valor, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag += "|unico=" + value;

            }
        }

        private bool vSalvarId = false;
        [DisplayName("(DB.4 Salvar ID)")]
        public bool SalvarId
        {
            get { return vSalvarId; }
            set { vSalvarId = value; }
        }

        private bool vItensLista = true;

        [DisplayName("_Apenas Itens da Lista")]
        [Category("_ECTurbo")]
        [Description("")]
        public bool ApenasItensLista
        {
            get { return vItensLista; }
            set { vItensLista = value; }
        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            //ForeColor = Config.CorPrimaria;
            //Font = Config.FontePadrao;
        }

        protected override void OnEnter(EventArgs e)
        {
            BackColor = Config.CorEntrada;
            base.OnEnter(e);
        }


        protected override void OnLeave(EventArgs e)
        {
            BackColor = Config.CorSaida;
            base.OnLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Escape)
            {
                Text = string.Empty;
                SelectedIndex = -1;
                e.SuppressKeyPress = true;
            }

            base.OnKeyDown(e);
        }


        protected override void OnValidating(CancelEventArgs e)
        {

            Funcoes.RemoverLabel(this);

            if (Text == string.Empty)
                goto Continuar;

            if (SelectedIndex == -1 && ApenasItensLista == true)
            {
                Funcoes.CriarLabel(this, "Item inválido", descricao: "Permitido apenas o uso das opções presentes na lista");
                e.Cancel = true;
            }

        Continuar:
            base.OnValidating(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {

            if (Text == string.Empty)
            {
                Funcoes.RemoverLabel(this);
                goto Continuar;
            }

            if(SelectedIndex > -1)
            {
                Funcoes.RemoverLabel(this);
                goto Continuar;
            }

        Continuar:
            base.OnTextChanged(e);
        }

    }
}
