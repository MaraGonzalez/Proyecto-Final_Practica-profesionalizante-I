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
    public partial class frmBajaCliente : Form
    {
        private frmClientes formularioCliente;
        public frmBajaCliente(frmClientes clientes)
        {
            InitializeComponent();
            formularioCliente = clientes;
        }

        private void frmBajaCliente_Load(object sender, EventArgs e)
        {
            clsClientes c = new clsClientes();
            cmbNombre.DisplayMember = "Nombre";
            cmbNombre.ValueMember = "idCliente";
            cmbNombre.DataSource = c.ObtenerListadoClientes();
            cmbNombre.SelectedIndex = -1;
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
                    lblContacto.Text = datos["Contacto"].ToString();
                    lblProvincia.Text = datos["Provincia"].ToString();
                    lblCiudad.Text = datos["Ciudad"].ToString();

                    btnEliminar.Enabled = true;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(lblidCliente.Text);

            var confirmacion = MessageBox.Show("¿Está seguro que desea eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo);

            if (confirmacion == DialogResult.Yes)
            {
                clsClientes c = new clsClientes();
                c.Eliminar(id);

                // Se actualiza dgvClientes
                formularioCliente.RecargarGrilla();

                // Actualizar cmbClientes
                cmbNombre.DataSource = null;
                clsClientes nuevoListado = new clsClientes();
                cmbNombre.DataSource = nuevoListado.ObtenerListadoClientes();
                cmbNombre.DisplayMember = "Nombre";
                cmbNombre.ValueMember = "idCliente";
                cmbNombre.SelectedIndex = -1;

                // Limpiar etiquetas
                lblidCliente.Text = "";
                lblNombre.Text = "";
                lblContacto.Text = "";
                lblProvincia.Text = "";
                lblCiudad.Text = "";
                btnBuscar.Enabled = false;
                btnEliminar.Enabled = false;

                MessageBox.Show("Cliente eliminado correctamente.");
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
