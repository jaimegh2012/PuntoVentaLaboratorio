using Newtonsoft.Json;
using POSWindowsForms.Datos;
using POSWindowsForms.Datos.DTOS;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSWindowsForms.Forms
{
    public partial class Productos : Form
    {
        ProductosService productosService;
        ClientesService clientesService;
        CategoriasService categoriasService;

        string CodProducto;

        public Productos()
        {
            productosService = new ProductosService();
            categoriasService = new CategoriasService();
            clientesService = new ClientesService();
            CodProducto = "";
            InitializeComponent();
        }

        void limpiar()
        {
            this.txtNombre.Clear();
            this.txtPrecio.Clear();
            this.chkActivo.Checked = false;

            this.txtNombre.Focus();
            CodProducto = "";
        }

        private async void recargarDataGrid()
        {
            dataGridView1.DataSource = await productosService.ObtenerProductos();
        }

        private async void Productos_Load(object sender, EventArgs e)
        {
            this.cboCategoria.DisplayMember = "NombreCategoria";
            this.cboCategoria.ValueMember = "CodCategoria";
            this.cboCategoria.DataSource = await categoriasService.ObtenerCategorias();

            dataGridView1.DataSource = await productosService.ObtenerProductos();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var row = (sender as DataGridView).CurrentRow;
            CodProducto = row.Cells[0].Value.ToString();

            this.txtNombre.Text = row.Cells[1].Value.ToString();
            this.txtPrecio.Text = row.Cells[2].Value.ToString();
            this.cboCategoria.SelectedValue = Convert.ToInt32(row.Cells[3].Value.ToString());

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
                if (this.txtNombre.Text != "" && this.txtPrecio.Text != "" && this.cboCategoria.Text != "")
                {
                    ProductoDTO data = new ProductoDTO()
                    {
                        NombreProducto = this.txtNombre.Text,
                        CodCategoria = (int)cboCategoria.SelectedValue,
                        Precio = Convert.ToDecimal(this.txtPrecio.Text),
                        Activo = chkActivo.Checked,
                    };

                    if (CodProducto.Length > 0)
                    {
                        data.CodProducto = Convert.ToInt32(CodProducto);
                        productosService.Actualizar(data);
                    }
                    else
                    {
                        productosService.Crear(data);
                    }


                    this.dataGridView1.DataSource = null;

                    limpiar();

                    recargarDataGrid();

                    MessageBox.Show("Producto Registrado Correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            this.Hide();
        }
    }
}
