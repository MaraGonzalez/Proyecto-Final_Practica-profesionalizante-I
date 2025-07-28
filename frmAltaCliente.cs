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
    public partial class frmAltaCliente : Form
    {
        private frmClientes formularioCliente;
        public frmAltaCliente(frmClientes clientes)
        {
            InitializeComponent();
            formularioCliente = clientes;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtContacto.Text != "" && txtProvincia.Text != "" && txtCiudad.Text != "")
            {
                try
                {
                    clsClientes nuevo = new clsClientes();
                    nuevo.Agregar(txtNombre, txtContacto, txtProvincia, txtCiudad);
                    MessageBox.Show("Cliente mayorista agregado correctamente.");

                    txtNombre.Clear();
                    txtContacto.Clear();
                    txtProvincia.Clear();
                    txtCiudad.Clear();
                    txtNombre.Focus();

                    formularioCliente.RecargarGrilla();
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
            if (txtNombre.Text.Trim() != "" && txtContacto.Text.Trim() != "" && txtProvincia.Text.Trim() != "" && txtCiudad.Text.Trim() != "")
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
