using System;
using System.Windows.Forms;
using Actividad2.Forms;

namespace Actividad2
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DatabaseTest.ProbarConexion();
            Application.Run(new Home());
        }
    }
}