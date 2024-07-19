using POSWindowsForms.Datos;
using POSWindowsForms.Datos.DTOS;
using POSWindowsForms.Security.AES;
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
    public partial class Users : Form
    {
        UsuariosService usuariosService;
        string CodUsuario;

        public Users()
        {
            InitializeComponent();
            CodUsuario = "";
            usuariosService = new UsuariosService();
        }

        void limpiar()
        {
            this.txtNombre.Clear();
            this.txtEmail.Clear();
            this.txtClave.Clear();
            this.chkActivo.Checked = false;

            this.txtNombre.Focus();
            CodUsuario = "";
        }

        private async void recargarDataGrid()
        {
            dataGridView1.DataSource = await usuariosService.ObtenerUsuarios();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            recargarDataGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var row = (sender as DataGridView).CurrentRow;
            CodUsuario = row.Cells[0].Value.ToString();

            this.txtNombre.Text = row.Cells[1].Value.ToString();
            this.txtEmail.Text = row.Cells[2].Value.ToString();

            if (row.Cells[4].Value.ToString() == "True")
            {
                this.chkActivo.Checked = true;
            }
            else
                this.chkActivo.Checked = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtNombre.Text != "" && this.txtEmail.Text != "" && this.txtClave.Text != "")
                {
                    UsuarioDTO data = new UsuarioDTO()
                    {
                        NombreUsuario = this.txtNombre.Text,
                        Usuario = this.txtEmail.Text,
                        Clave = AES.EncryptString(this.txtClave.Text),
                        Activo = chkActivo.Checked,
                    };

                    if (CodUsuario.Length > 0)
                    {
                        data.CodUsuario = Convert.ToInt32(CodUsuario);
                        usuariosService.Actualizar(data);
                    }
                    else
                    {
                        usuariosService.Crear(data);
                    }


                    this.dataGridView1.DataSource = null;

                    limpiar();

                    recargarDataGrid();

                    MessageBox.Show("Registro Guardado Correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Completar Todos los Datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
