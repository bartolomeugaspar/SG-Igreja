using System.ComponentModel;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_Anos : ECTurbo_TextBox
    {

        public ECTurbo_Anos()
        {
            Tag = "|numero";
        }

        private int vAnoMin = 1582;
		[DisplayName("_Ano Mínimo")]
		public int AnoMin
		{
			get { return vAnoMin; }
			set {

				if (value < 1582)
					value = 1582;

				vAnoMin = value; 
			
			}
		}


		private int vAnoMax = 0;
		[DisplayName("_Ano Máximo")]
		public int AnoMax
		{
			get { return vAnoMax; }
			set { 
				
				if(value > 0 && value < AnoMin)
					value = AnoMin;

				vAnoMax = value; 			
			}
		}


        protected override void OnKeyPress(KeyPressEventArgs e)
        {
			if(!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals((char)Keys.Back))
				e.Handled = true;

			if (Text.Trim().Length == 4 && !e.KeyChar.Equals((char)Keys.Back))
				e.Handled = true;

            base.OnKeyPress(e);
        }

        protected override void OnValidating(CancelEventArgs e)
        {
			if (Text.Trim() == string.Empty)
				return;

			if(Text.Length < 4)
			{
				Funcoes.CriarLabel(this, "Digite um ano de 4 digitos");
				e.Cancel = true;
				return;
			}

			int ano = Convert.ToInt32(Text);

			if(ano < AnoMin)
            {
                Funcoes.CriarLabel(this, "Ano inválido!", descricao:$"Ano mínimo permitido: {AnoMin}");
                e.Cancel = true;
                return;
            }

            if (ano > AnoMax && AnoMax > 0)
            {
                Funcoes.CriarLabel(this, "Ano inválido!", descricao: $"Ano máximo permitido: {AnoMax}");
                e.Cancel = true;
                return;
            }

            base.OnValidating(e);
        }


    }
}
