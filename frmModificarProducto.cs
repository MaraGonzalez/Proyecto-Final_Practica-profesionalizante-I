using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_TrabajoIntegradorPP1
{
    public partial class frmModificarProducto : Form
    {
        private frmProductos formularioProducto;
        public frmModificarProducto(frmProductos productos)
        {
            InitializeComponent();
            formularioProducto = productos;
        }

        private void frmModificarProducto_Load(object sender, EventArgs e)
        {
            clsProductos p = new clsProductos();
            cmbNombre.DisplayMember = "Nombre";
            cmbNombre.ValueMember = "idProducto";
            cmbNombre.DataSource = p.ObtenerListadoProductos();
            cmbNombre.SelectedIndex = -1;

            txtPrecioVenta.Enabled = false;
            txtStock.Enabled = false;
            txtPrecioCompra.Enabled = false;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbNombre.SelectedIndex != -1)
            {
                Int32 id = Convert.ToInt32(cmbNombre.SelectedValue);
                clsProductos p = new clsProductos();
                DataRow datos = p.BuscarPorId(id);

                if (datos != null)
                {
                    lblProducto.Text = datos["idProducto"].ToString();
                    lblNombre.Text = datos["Nombre"].ToString();
                    lblRubro.Text = datos["RubroNombre"].ToString();  
                    txtPrecioCompra.Text = datos["PrecioCompra"].ToString();
                    txtPrecioVenta.Text = datos["PrecioVenta"].ToString();
                    txtStock.Text = datos["Stock"].ToString();
                    lblProveedor.Text = datos["ProveedorNombre"].ToString();  

                    btnModificar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No se encontró el producto con el ID especificado.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un producto primero.");
            }
        }
        
        private void cmbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNombre.SelectedIndex != -1)
            {
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            txtPrecioCompra.Enabled = true;
            txtPrecioVenta.Enabled = true;
            txtStock.Enabled = true;
            btnGuardar.Enabled = true;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(lblProducto.Text);
            Decimal nuevoPreCompra = Convert.ToDecimal(txtPrecioCompra.Text);
            Decimal nuevoPreVenta = Convert.ToDecimal(txtPrecioVenta.Text);
            Int32 nuevoStock = Convert.ToInt32(txtStock.Text);

            clsProductos p = new clsProductos();
            p.Modificar(id, nuevoPreCompra, nuevoPreVenta, nuevoStock);

            // Actualizar dgvProductos
            formularioProducto.RecargarGrilla();

            MessageBox.Show("Datos modificados correctamente.");
        }
    }
}
