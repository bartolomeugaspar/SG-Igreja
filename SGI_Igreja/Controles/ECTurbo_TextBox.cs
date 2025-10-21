using System;
using System.ComponentModel;
using System.Windows.Forms;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_TextBox : TextBox
    { 

        public ECTurbo_TextBox()
        {
            Tag = "";
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
                e.SuppressKeyPress = true;
            }

            base.OnKeyDown(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {

            //if (Text == string.Empty)
            Funcoes.RemoverLabel(this);

            base.OnTextChanged(e);
        }


        private string vColuna = "";
        [DisplayName("(DB.1 Coluna Tabela)")]
        public string Coluna
        {
            get { return vColuna; }
            set { 
                
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
            set { 
                
                vObgt = value;

                if (value == true)
                {
                    if (!Tag.ToString().Contains("|obgt"))
                        Tag += "|obgt";
                }
                else
                    Tag = Tag.ToString().Replace("|obgt", "");
            }
        }

        private string vUnico = "";
        [DisplayName("(DB.3 Msg para Duplicidade)")]
        public string Unico
        {
            get { return vUnico; }
            set { 
                
                vUnico = value;

                string valor = Funcoes.PegarTag(this, "unico");

                Tag = Tag.ToString().Replace("|unico=" + valor, "");

                if(string.IsNullOrEmpty(value)==false)
                    Tag += "|unico=" + value;
            
            }
        }

        private bool vLimpeza = true;
        [DisplayName("_Limpeza Automática")]
        public bool Limpeza
        {
            get { return vLimpeza; }
            set { 
                
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


    }
}