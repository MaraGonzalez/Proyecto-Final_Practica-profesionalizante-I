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
    public partial class frmBajaProveedor : Form
    {
        private frmProveedores formularioProveedor;
        public frmBajaProveedor(frmProveedores proveedores)
        {
            InitializeComponent();
            formularioProveedor = proveedores;
        }
        private void frmBajaProveedor_Load(object sender, EventArgs e)
        {
            clsProveedor emp = new clsProveedor();
            cmbNombre.DisplayMember = "Nombre";
            cmbNombre.ValueMember = "idProveedor";
            cmbNombre.DataSource = emp.ObtenerListadoProveedores();
            cmbNombre.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbNombre.SelectedIndex != -1)
            {
                Int32 id = Convert.ToInt32(cmbNombre.SelectedValue);
                clsProveedor p = new clsProveedor();
                DataRow datos = p.BuscarPorId(id);

                if (datos != null)
                {
                    lblProveedor.Text = datos["idProveedor"].ToString();
                    lblNombre.Text = datos["Nombre"].ToString();
                    lblTeléfono.Text = datos["Teléfono"].ToString();
                    lblContacto.Text = datos["Contacto"].ToString();
                    lblProvincia.Text = datos["Provincia"].ToString();
                    lblCiudad.Text = datos["Ciudad"].ToString();

                    btnEliminar.Enabled = true;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(lblProveedor.Text);

            var confirmacion = MessageBox.Show("¿Está seguro que desea eliminar este proveedor?", "Confirmar eliminación", MessageBoxButtons.YesNo);

            if (confirmacion == DialogResult.Yes)
            {
                clsProveedor p = new clsProveedor();
               p.Eliminar(id);

                //Recarga grilla del formulario principal
                formularioProveedor.RecargarGrilla();
                // Recargar ComboBox de este formulario
                cmbNombre.DataSource = p.ObtenerListadoProveedores();
                cmbNombre.SelectedIndex = -1;

                // Limpiar etiquetas
                lblProveedor.Text = "";
                lblNombre.Text = "";
                lblTeléfono.Text = "";
                lblContacto.Text = "";
                lblProvincia.Text = "";
                lblCiudad.Text = "";

                btnEliminar.Enabled = false;
                btnBuscar.Enabled = false;

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
    }
}
