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
    public partial class frmModificarCliente : Form
    {
        private frmClientes formularioCliente;
        public frmModificarCliente(frmClientes clientes)
        {
            InitializeComponent();
            formularioCliente = clientes;
        }

        private void frmModificarCliente_Load(object sender, EventArgs e)
        {
            clsClientes c = new clsClientes();
            cmbNombre.DisplayMember = "Nombre";
            cmbNombre.ValueMember = "idCliente";
            cmbNombre.DataSource = c.ObtenerListadoClientes();
            cmbNombre.SelectedIndex = -1;

            txtContacto.Enabled = false;
            txtProvincia.Enabled = false;
            txtCiudad.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbNombre.SelectedIndex != -1)
            {
                Int32 id = Convert.ToInt32(cmbNombre.SelectedValue);
                clsClientes c = new clsClientes();
                DataRow datos = c.BuscarPorId(id);

                if (datos != null)
                {
                    lblidCliente.Text = datos["idCliente"].ToString();
                    lblNombre.Text = datos["Nombre"].ToString();
                    txtContacto.Text = datos["Contacto"].ToString();
                    txtProvincia.Text = datos["Provincia"].ToString();
                    txtCiudad.Text = datos["Ciudad"].ToString();

                    txtContacto.Enabled = false;
                    txtProvincia.Enabled = false;
                    txtCiudad.Enabled = false;

                    btnModificar.Enabled = true;
                    btnGuardar.Enabled = false;
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            txtContacto.Enabled = true;
            txtProvincia.Enabled = true;
            txtCiudad.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(lblidCliente.Text, out int id))
            {
                String nuevoContacto = txtContacto.Text;
                String nuevaProvincia = txtProvincia.Text;
                String nuevaCiudad = txtCiudad.Text;

                clsClientes c = new clsClientes();
                c.Modificar(id, nuevoContacto, nuevaProvincia, nuevaCiudad);

                formularioCliente.RecargarGrilla();

                MessageBox.Show("Datos modificados correctamente.");

                btnGuardar.Enabled = false;
                btnModificar.Enabled = true;

                txtContacto.Enabled = false;
                txtProvincia.Enabled = false;
                txtCiudad.Enabled = false;
            }
            else
            {
                MessageBox.Show("ID de cliente inválido.");
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
