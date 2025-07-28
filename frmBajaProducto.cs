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
    public partial class frmBajaProducto : Form
    {
        private frmProductos formularioProducto;
        public frmBajaProducto(frmProductos productos)
        {
            InitializeComponent();
            formularioProducto = productos;
        }

        private void frmBajaProducto_Load(object sender, EventArgs e)
        {
            clsProductos p = new clsProductos();
            cmbNombre.DisplayMember = "Nombre";
            cmbNombre.ValueMember = "idProducto";
            cmbNombre.DataSource = p.ObtenerListadoProductos();
            cmbNombre.SelectedIndex = -1;
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
                    lblPrecioCompra.Text = datos["PrecioCompra"].ToString();
                    lblPrecioVenta.Text = datos["PrecioVenta"].ToString();
                    lblStock.Text = datos["Stock"].ToString();
                    lblProveedor.Text = datos["ProveedorNombre"].ToString();

                    btnBaja.Enabled = true;
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
        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblProducto.Text) || !Int32.TryParse(lblProducto.Text, out Int32 id))
            {
                MessageBox.Show("No hay producto seleccionado para eliminar.", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación de eliminación
            var confirmacion = MessageBox.Show( "¿Está seguro que desea eliminar este producto?", "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    clsProductos p = new clsProductos();
                    p.Eliminar(id);

                    MessageBox.Show("Producto eliminado correctamente.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
  
                    LimpiarControles();
                    RecargarComboProductos();
                    formularioProducto?.RecargarGrilla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo eliminar el producto: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimpiarControles()
        {
            lblProducto.Text = "";
            lblNombre.Text = "";
            lblRubro.Text = "";
            lblPrecioCompra.Text = "";
            lblPrecioVenta.Text = "";
            lblStock.Text = "";
            lblProveedor.Text = "";
            cmbNombre.SelectedIndex = -1;
        }

        private void RecargarComboProductos()
        {
            clsProductos p = new clsProductos();
            cmbNombre.DisplayMember = "Nombre";
            cmbNombre.ValueMember = "idProducto";
            cmbNombre.DataSource = p.ObtenerListadoProductos();
            cmbNombre.SelectedIndex = -1;

            btnBuscar.Enabled = false;
            btnBaja.Enabled = false;
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
    }
}
