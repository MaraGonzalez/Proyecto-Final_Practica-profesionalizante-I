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
    public partial class frmAltaProveedor : Form
    {
        private frmProveedores formularioProveedor;
        public frmAltaProveedor(frmProveedores proveedores)
        {
            InitializeComponent();
            formularioProveedor = proveedores;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtTelefono.Text != "" && txtContacto.Text != "" && txtProvincia.Text != "" && txtCiudad.Text != "")
            {
                try
                {
                    clsProveedor nuevo = new clsProveedor();               
                    nuevo.Agregar(txtNombre, txtTelefono, txtContacto, txtProvincia, txtCiudad);

                    txtNombre.Clear();
                    txtTelefono.Clear();
                    txtContacto.Clear();
                    txtProvincia.Clear();
                    txtCiudad.Clear();
                    txtNombre.Focus();

                    formularioProveedor.RecargarGrilla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, completá todos los campos.");
            }
        }

        private void ValidarCampos()
        {
            if (txtNombre.Text.Trim() != "" && txtTelefono.Text.Trim() != "" && txtContacto.Text.Trim() != "" && txtProvincia.Text.Trim() != "" && txtCiudad.Text.Trim() != "")
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

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void txtContacto_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void txtProvincia_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void txtCiudad_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }
    }
}
