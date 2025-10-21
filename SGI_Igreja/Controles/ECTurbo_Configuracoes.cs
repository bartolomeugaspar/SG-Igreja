using System.ComponentModel;
using System.Drawing;

using ECTurbo.Codigos;

namespace ECTurbo.Controles
{
    public class ECTurbo_Configuracoes : Component
    {
		private Color vCorPrimaria = Config.CorPrimaria;
        [Category("Cores do Sistema")]
		public Color CorPrimaria
        {
			get { return vCorPrimaria; }
			set { 
				vCorPrimaria = value;
				Config.CorPrimaria = CorPrimaria;
			}
		}


        private Color vCorEntrada = Config.CorEntrada;

        [Category("Cores do Sistema")]
        public Color CorEntrada
        {
            get { return vCorEntrada; }
            set
            {
                vCorEntrada = value;
                Config.CorEntrada = CorEntrada;
            }
        }

        
        private Color vCorSaida = Config.CorSaida;

        [Category("Cores do Sistema")]
        public Color CorSaida
        {
            get { return vCorSaida; }
            set
            {
                vCorSaida = value;
                Config.CorSaida = CorSaida;
            }
        }



        private Font vFontePadrao = Config.FontePadrao;

        [Category("Fontes do Sistema")]
        public Font FontePadrao
        {
            get { return vFontePadrao; }
            set
            {
                vFontePadrao = value;
                Config.FontePadrao = FontePadrao;
            }
        }

    }
}
