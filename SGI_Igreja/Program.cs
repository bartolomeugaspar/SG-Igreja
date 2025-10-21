using ECTurbo_CRUD;
using ECTurbo.Codigos;

namespace SGI_Igreja
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if (MYSQL.TestarConexao() == string.Empty)
            {
                Funcoes.Modal(new Formularios.FrmLogin(), 80);

                if (Funcoes.DtLogin.Rows.Count > 0)
                    Application.Run(new Formularios.FrmPrincipal());
                else
                    Application.Exit();
            }
            else
                Funcoes.Modal(new Formularios.FrmConexaoMySQL());
            
        }
    }
}