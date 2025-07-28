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
    public partial class frmBajaEmpleado : Form
    {
        private frmEmpleados formularioEmpleados;
        public frmBajaEmpleado(frmEmpleados formulario)
        {
            InitializeComponent();
            formularioEmpleados = formulario;
        }

        private void frmBajaEmpleado_Load(object sender, EventArgs e)
        {
            CargarComboEmpleados();
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

                    btnEliminar.Enabled = true;
                }
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(lblIdEmpleado.Text);

            var confirmacion = MessageBox.Show("¿Está seguro que desea eliminar este empleado?", "Confirmar eliminación", MessageBoxButtons.YesNo);

            if (confirmacion == DialogResult.Yes)
            {
                clsEmpleado emp = new clsEmpleado();
                emp.Eliminar(id);

                formularioEmpleados.RecargarGrilla();

                MessageBox.Show("Empleado eliminado correctamente.");
            }

            cmbNombreyApellido.SelectedIndex = -1;
            lblIdEmpleado.Text = "";
            lblNombreApellido.Text = "";
            lblCuil.Text = "";  
            lblTurno.Text = "";
            lblSector.Text = "";
            CargarComboEmpleados();
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


        private void CargarComboEmpleados()
        {
            clsEmpleado emp = new clsEmpleado();
            cmbNombreyApellido.DisplayMember = "NombreCompleto";
            cmbNombreyApellido.ValueMember = "idEmpleado";
            cmbNombreyApellido.DataSource = emp.ObtenerListadoEmpleados();
            cmbNombreyApellido.SelectedIndex = -1;
        }
    }
}
