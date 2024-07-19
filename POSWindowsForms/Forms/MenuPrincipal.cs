using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSWindowsForms.Forms
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void mantenimientoProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Productos forma = new Productos();
            forma.MdiParent = this;
            forma.Show();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
