using POSWindowsForms.Datos;
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
    public partial class Pedidos : Form
    {
        ClientesService clientesService;
        DireccionesClienteService direccionesClienteService;
        ProductosService productosService;

        DataGridViewRow productoActual;
        public Pedidos()
        {
            InitializeComponent();
            clientesService = new ClientesService();
            direccionesClienteService = new DireccionesClienteService();
            productosService = new ProductosService();
            productoActual = new DataGridViewRow();

        }

        private async void cargarDirecciones()
        {
            if (cboCliente.SelectedValue != null)
            {
                var codCliente = (int)cboCliente.SelectedValue;

                this.cboDireccion.DisplayMember = "Direccion";
                this.cboDireccion.ValueMember = "CodDireccion";
                this.cboDireccion.DataSource = await direccionesClienteService.ObtenerDireccionesCliente(codCliente);
            }
        }

        private async void cargarProductos()
        {
            dataGridView1.DataSource = await productosService.ObtenerProductos();
        }

        private async void Pedidos_Load(object sender, EventArgs e)
        {
            this.cboCliente.DisplayMember = "NombreCliente";
            this.cboCliente.ValueMember = "CodCliente";
            this.cboCliente.DataSource = await clientesService.ObtenerClientes();

            this.cargarDirecciones();

            this.cargarProductos();

        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDirecciones();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            DataGridViewRow fila = new DataGridViewRow();
            fila.CreateCells(dataGridView2);
            fila.Cells[0].Value = productoActual.Cells[0].Value.ToString();
            fila.Cells[1].Value = productoActual.Cells[1].Value.ToString();
            fila.Cells[2].Value = productoActual.Cells[2].Value.ToString();
            fila.Cells[3].Value = "1";

            var total = Convert.ToDecimal(productoActual.Cells[2].Value.ToString()) * Convert.ToDecimal(productoActual.Cells[3].Value.ToString());
            fila.Cells[4].Value = total.ToString();

            dataGridView2.Rows.Add(fila);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            productoActual = (sender as DataGridView).CurrentRow;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
