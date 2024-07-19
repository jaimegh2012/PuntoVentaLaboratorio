using POSWindowsForms.Datos;
using POSWindowsForms.Datos.DTOS;
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
    public partial class Clientes : Form
    {
        ClientesService clientesService;
        string CodCliente;

        public Clientes()
        {
            CodCliente = "";

            clientesService = new ClientesService();
            InitializeComponent();
        }

        void limpiar()
        {
            this.txtNombre.Clear();
            this.txtTelefono.Clear();
            this.chkActivo.Checked = false;

            this.txtNombre.Focus();
            CodCliente = "";
        }

        private async void recargarDataGrid()
        {
            dataGridView1.DataSource = await clientesService.ObtenerClientes();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            recargarDataGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var row = (sender as DataGridView).CurrentRow;
            CodCliente = row.Cells[0].Value.ToString();

            this.txtNombre.Text = row.Cells[1].Value.ToString();
            this.txtTelefono.Text = row.Cells[2].Value.ToString();

            if (row.Cells[3].Value.ToString() == "True")
            {
                this.chkActivo.Checked = true;
            }
            else
                this.chkActivo.Checked = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtNombre.Text != "" && this.txtTelefono.Text != "")
                {
                    ClienteDTO data = new ClienteDTO()
                    {
                        NombreCliente = this.txtNombre.Text,
                        Telefono = this.txtTelefono.Text,
                        Activo = chkActivo.Checked,
                    };

                    if (CodCliente.Length > 0)
                    {
                        data.CodCliente = Convert.ToInt32(CodCliente);
                        clientesService.Actualizar(data);
                    }
                    else
                    {
                        clientesService.Crear(data);
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
    }
}
