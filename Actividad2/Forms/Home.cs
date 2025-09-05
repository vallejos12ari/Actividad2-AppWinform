using System;
using System.Windows.Forms;

namespace Actividad2.Forms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        
        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("¡Hiciste click en el botón!");
        }
    }
}