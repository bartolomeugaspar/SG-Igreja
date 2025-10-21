using System.ComponentModel;

namespace ECTurbo.Controles
{
    public class ECTurbo_CbMeses : ECTurbo_ComboBox
    {
		private bool vMesAbreviado = false;
		[DisplayName("_Mês Abreviado")]
		public bool MesAbreviado
        {
			get { return vMesAbreviado; }
			set { 
				
				vMesAbreviado = value; 
				
				Items.Clear();

				if(MesAbreviado == true)
				{
					Items.Add("Jan");
                    Items.Add("Fev");
                    Items.Add("Mar");
                    Items.Add("Abr");
                    Items.Add("Mai");
                    Items.Add("Jun");
                    Items.Add("Jul");
                    Items.Add("Ago");
                    Items.Add("Set");
                    Items.Add("Out");
                    Items.Add("Nov");
                    Items.Add("Dez");
                }
				else
				{
                    Items.Add("Janeiro");
                    Items.Add("Fevereiro");
                    Items.Add("Março");
                    Items.Add("Abril");
                    Items.Add("Maio");
                    Items.Add("Junho");
                    Items.Add("Julho");
                    Items.Add("Agosto");
                    Items.Add("Setembro");
                    Items.Add("Outubro");
                    Items.Add("Novembro");
                    Items.Add("Dezembro");
                }
			
			}
		}

	}
}
