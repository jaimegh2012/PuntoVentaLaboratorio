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
    public partial class DireccionesCliente : Form
    {
        ClientesService clientesService;
        DireccionesClienteService direccionesClienteService;
        string CodDireccion;
        public DireccionesCliente()
        {
            InitializeComponent();
            clientesService = new ClientesService();
            direccionesClienteService = new DireccionesClienteService();
            CodDireccion = "";
        }

        void limpiar()
        {
            this.txtDireccion.Clear();
            this.chkActivo.Checked = false;

            this.txtDireccion.Focus();
            CodDireccion = "";
        }

        private async void recargarDataGrid()
        {
            if (cboCliente.SelectedValue == null)
            {
                return;
            }

            var codCliente = (int)cboCliente.SelectedValue;
            dataGridView1.DataSource = await direccionesClienteService.ObtenerDireccionesCliente(codCliente);
        }


        private async void DireccionesCliente_Load(object sender, EventArgs e)
        {
            this.cboCliente.DisplayMember = "NombreCliente";
            this.cboCliente.ValueMember = "CodCliente";
            this.cboCliente.DataSource = await clientesService.ObtenerClientes();
        }

        private void cboCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtDireccion.Text != "" && this.cboCliente.Text != "")
                {
                    DireccionClienteDTO data = new DireccionClienteDTO()
                    {
                        Direccion = txtDireccion.Text,
                        CodCliente = (int)cboCliente.SelectedValue,
                        Activo = chkActivo.Checked,
                    };

                    if (CodDireccion.Length > 0)
                    {
                        data.CodDireccion = Convert.ToInt32(CodDireccion);
                        direccionesClienteService.Actualizar(data);
                    }
                    else
                    {
                        direccionesClienteService.Crear(data);
                    }


                    this.dataGridView1.DataSource = null;

                    limpiar();

                    recargarDataGrid();

                    MessageBox.Show("Registro guardado Correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Completar Todos los Datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var row = (sender as DataGridView).CurrentRow;
            CodDireccion = row.Cells[0].Value.ToString();

            this.txtDireccion.Text = row.Cells[2].Value.ToString();

            if (row.Cells[3].Value.ToString() == "True")
            {
                this.chkActivo.Checked = true;
            }
            else
                this.chkActivo.Checked = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.recargarDataGrid();
        }
    }
}
