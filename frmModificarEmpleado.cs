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
    public partial class frmModificarEmpleado : Form
    {
        private frmEmpleados formularioEmpleados;
        public frmModificarEmpleado(frmEmpleados formulario)
        {
            InitializeComponent();
            formularioEmpleados = formulario;
        }

        private void frmModificarEmpleado_Load(object sender, EventArgs e)
        {
            clsEmpleado emp = new clsEmpleado();
            cmbNombreyApellido.DisplayMember = "NombreCompleto";
            cmbNombreyApellido.ValueMember = "idEmpleado";
            cmbNombreyApellido.DataSource = emp.ObtenerListadoEmpleados();
            cmbNombreyApellido.SelectedIndex = -1;

            lblTurno.Enabled = false;
            lblSector.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbNombreyApellido.SelectedIndex != -1)
            {
                Int32 id = Convert.ToInt32(cmbNombreyApellido.SelectedValue);            
                clsEmpleado emp = new clsEmpleado();
                DataRow datos = emp.BuscarPorId(id);
                Int32 idSector = Convert.ToInt32(datos["idSector"]);
                String nombreSector = emp.ObtenerNombreSectorPorId(idSector);

                if (datos != null)
                {
                    lblIdEmpleado.Text = datos["idEmpleado"].ToString();
                    lblNombreApellido.Text = datos["Nombre"].ToString() + " " + datos["Apellido"].ToString();
                    lblCuil.Text = datos["CUIL"].ToString();
                    lblTurno.Text = datos["Turno"].ToString();
                    lblSector.Text = nombreSector;

                    btnModificar.Enabled = true;
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            lblTurno.Enabled = true;
            lblSector.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(lblIdEmpleado.Text);
            String nuevoTurno = lblTurno.Text;
            String nuevoSector = lblSector.Text;

            clsEmpleado emp = new clsEmpleado();
            emp.Modificar(id, nuevoTurno, nuevoSector);

            // Actualizar dgvEmpleados
            formularioEmpleados.RecargarGrilla();

            MessageBox.Show("Datos modificados correctamente.");
        }

        private void cmbNombreyApellido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNombreyApellido.SelectedIndex != -1)
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
