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
    public partial class frmModificarProveedor : Form
    {
        private frmProveedores formularioProveedor;
        public frmModificarProveedor(frmProveedores proveedores)
        {
            InitializeComponent();
            formularioProveedor = proveedores;
        }

        private void frmModificarProveedor_Load(object sender, EventArgs e)
        {
            clsProveedor emp = new clsProveedor();
            cmbNombre.DisplayMember = "Nombre";
            cmbNombre.ValueMember = "idProveedor";
            cmbNombre.DataSource = emp.ObtenerListadoProveedores();
            cmbNombre.SelectedIndex = -1;

            txtContacto.Enabled = false;
            txtProvincia.Enabled = false;
            txtTeléfono.Enabled = false;

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
                    lblIdProveedor.Text = datos["idProveedor"].ToString();
                    lblNombre.Text = datos["Nombre"].ToString();
                    txtTeléfono.Text = datos["Teléfono"].ToString();
                    txtContacto.Text = datos["Contacto"].ToString();
                    txtProvincia.Text = datos["Provincia"].ToString();  // Provincia
                    txtCiudad.Text = datos["Ciudad"].ToString();        // Ciudad ✅

                    btnModificar.Enabled = true;
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            txtTeléfono.Enabled = true;
            txtContacto.Enabled = true;
            txtProvincia.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTeléfono.Text) || String.IsNullOrEmpty(txtContacto.Text) || String.IsNullOrEmpty(txtProvincia.Text) || String.IsNullOrEmpty(txtCiudad.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            // Validar que el teléfono sea un número
            if (!int.TryParse(txtTeléfono.Text, out int nuevoTelefono))
            {
                MessageBox.Show("El teléfono debe ser un número válido.");
                return;
            }

            // Obtener datos y llamar a Modificar
            Int32 id = Convert.ToInt32(lblIdProveedor.Text);
            String nombreProveedor = lblNombre.Text.Trim();  
            Int32 nuevoTeléfono = Convert.ToInt32(txtTeléfono.Text);
            String nuevoContacto = txtContacto.Text.Trim();
            String nuevaProvincia = txtProvincia.Text.Trim();
            String nuevaCiudad = txtCiudad.Text.Trim();

            clsProveedor p = new clsProveedor();
            p.Modificar(id, nuevoTelefono, nuevoContacto, nuevaProvincia, nuevaCiudad, nombreProveedor);

            formularioProveedor.RecargarGrilla();
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
