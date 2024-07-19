using POSWindowsForms.Enviroments;
using POSWindowsForms.Security;
using POSWindowsForms.Security.AES;
using POSWindowsForms.Security.DTOS;
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
    public partial class Login : Form
    {
        SecurityService securityService;
        public Login()
        {
            InitializeComponent();
            securityService = new SecurityService();
        }

        void limpiar()
        {
            this.txtEmail.Clear();
            this.txtClave.Clear();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            AuthorizationRequest request = new AuthorizationRequest()
            {
                User = txtEmail.Text,
                Password = AES.EncryptString(txtClave.Text),
            };
            var data = await securityService.Autenticar(request);

            if (data != null && data.Resultado == true)
            {
                Urls.TOKEN = data.Token;
                MenuPrincipal forma = new MenuPrincipal();
                forma.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Email o Contraseña incorrecta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
