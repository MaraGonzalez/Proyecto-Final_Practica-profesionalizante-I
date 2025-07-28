using System;
using System.Collections;
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
    public partial class frmAltaProducto : Form
    {
        private frmProductos formularioProducto;
        public frmAltaProducto(frmProductos productos)
        {
            InitializeComponent();
            formularioProducto = productos;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmAltaProducto_Load(object sender, EventArgs e)
        {
            clsProductos productos = new clsProductos();
            productos.CargarRubrosEnComboBox(cmbRubro);
            productos.CargarProveedores(cmbProveedor);
            cmbRubro.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto.");
                return;
            }

            if (cmbRubro.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un rubro.");
                return;
            }

            if (cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            if (!Decimal.TryParse(txtPrecioCompra.Text, out Decimal precioCompra) || precioCompra <= 0)
            {
                MessageBox.Show("Precio de compra inválido. Debe ser un número mayor a 0.");
                return;
            }

            if (!Decimal.TryParse(txtPrecioVenta.Text, out Decimal precioVenta) || precioVenta <= 0)
            {
                MessageBox.Show("Precio de venta inválido. Debe ser un número mayor a 0.");
                return;
            }

            if (!Int32.TryParse(txtStock.Text, out Int32 stock) || stock < 0)
            {
                MessageBox.Show("Stock inválido. Debe ser un número entero positivo.");
                return;
            }

            // Obtener datos de los controles
            try
            {
                String nombreProducto = txtNombre.Text.Trim();
                Int32 idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue); 
                Int32 idRubro = Convert.ToInt32(cmbRubro.SelectedValue);      

                // Insertar el producto
                clsProductos productos = new clsProductos();
                productos.Agregar(nombreProducto, idRubro, idProveedor, precioCompra, precioVenta, stock);

                // Limpiar el formulario después de la inserción
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}");
            }
            finally
            {
                //  Actualizar la grilla (si es necesario)
                if (formularioProducto != null)
                {
                    formularioProducto.RecargarGrilla();
                }
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtStock.Clear();
            cmbRubro.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            txtNombre.Focus();
        }

        private void ValidarCampos()
        {
            if (txtNombre.Text.Trim() != "" && cmbRubro.SelectedIndex != -1 && txtPrecioCompra.Text.Trim() != "" && txtPrecioVenta.Text.Trim() != "" && txtStock.Text.Trim() !="" && cmbProveedor.SelectedIndex != -1 )
            {
                btnCargar.Enabled = true;
            }
            else
            {
                btnCargar.Enabled = false;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void cmbRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void txtPrecioCompra_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }
    } 
}
