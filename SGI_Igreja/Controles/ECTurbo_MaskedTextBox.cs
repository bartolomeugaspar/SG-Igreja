using System;
using System.ComponentModel;
using System.Windows.Forms;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_MaskedTextBox : MaskedTextBox
    {
        public ECTurbo_MaskedTextBox()
        {
            Tag = "";
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

        private bool vSalvarMascara = true;

        [DisplayName("_Salvar Mascara")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public bool SalvarMascara
        {
            get { return vSalvarMascara; }
            set { vSalvarMascara = value; }
        }

        public enum CaseOption
        {
            Normal,
            Maiusculo,
            Minusculo
        }

        private CaseOption vCaseFormat = CaseOption.Normal;
        [DisplayName("_Formato de Caixa")]
        public CaseOption CaseFormat
        {
            get { return vCaseFormat; }
            set { vCaseFormat = value; }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Funcoes.RemoverLabel(this);

            base.OnTextChanged(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            //ForeColor = Config.CorPrimaria;
            //Font = Config.FontePadrao;

            if(SalvarMascara== true)
                TextMaskFormat = MaskFormat.IncludeLiterals;
            else
                TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

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

            base.OnKeyDown(e);
        }


        protected override void OnValidating(CancelEventArgs e)
        {

            Funcoes.RemoverLabel(this);

            TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (Text == string.Empty)
                goto Continuar;

            int t = 0;

            foreach (char c in Mask)
            {
                if (c == '0' || c == 'L' || c == 'A')
                    t++;
            }

            if (Text.Replace(" ", "").Length != t)
            {
                Funcoes.CriarLabel(this, "Incompleto", descricao:"Por favor digite a informação por completo");
                e.Cancel = true;
                goto Continuar;
            }

            if (SalvarMascara == true)
                TextMaskFormat = MaskFormat.IncludeLiterals;

            Continuar:
                base.OnValidating(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (Text == string.Empty)
            {
                Funcoes.RemoverLabel(this);
                goto Continuar;
            }

            int t = 0;

            foreach (char c in Mask)
            {
                if (c == '0' || c == 'L' || c == 'A')
                    t++;
            }

            if (Text.Replace(" ", "").Length == t)
            {
                Funcoes.RemoverLabel(this);
            }

            if (vCaseFormat != CaseOption.Normal && !string.IsNullOrEmpty(Text))
            {
                int cursorPos = SelectionStart; // Salva a posição do cursor

                if (vCaseFormat == CaseOption.Maiusculo)
                {
                    Text = Text.ToUpper();
                }
                else if (vCaseFormat == CaseOption.Minusculo)
                {
                    Text = Text.ToLower();
                }

                SelectionStart = cursorPos; // Restaura a posição do cursor
            }

        Continuar:
            base.OnKeyUp(e);

        }

    }
}
