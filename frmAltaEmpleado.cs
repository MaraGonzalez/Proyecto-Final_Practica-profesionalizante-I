using System;
using System.Windows.Forms;

namespace pry_TrabajoIntegradorPP1
{
    public partial class frmAltaEmpleado : Form
    {
        private frmEmpleados formularioEmpleados;
        public frmAltaEmpleado(frmEmpleados empleados)
        {
            InitializeComponent();
            formularioEmpleados = empleados;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void frmAltaEmpleado_Load(object sender, EventArgs e)
        {
            clsEmpleado empleados = new clsEmpleado();
            cmbTurno.Items.Add("Mañana".ToUpper());
            cmbTurno.Items.Add("Tarde".ToUpper());

            cmbSector.DataSource = empleados.ObtenerSectores();
            cmbSector.DisplayMember = "Nombre";
            cmbSector.ValueMember = "idSector";

            cmbTurno.SelectedIndex = -1;
            cmbSector.SelectedIndex = -1;

            this.BeginInvoke((MethodInvoker)delegate {
                txtid.Focus();
            });

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (txtid.Text != "" && txtNombre.Text != "" && txtApellido.Text != "" && txtCUIL.Text != "" && cmbTurno.SelectedIndex != -1 && cmbSector.SelectedIndex != -1)
            {
                try
                {
                    clsEmpleado nuevo = new clsEmpleado();
                    nuevo.idEmpleado = Convert.ToInt32(txtid.Text);
                    nuevo.nombEmpleado = txtNombre.Text.ToUpper();
                    nuevo.apelEmpleado = txtApellido.Text.ToUpper();
                    nuevo.CUIL = txtCUIL.Text.ToUpper();
                    nuevo.turno = cmbTurno.SelectedItem.ToString();
                    nuevo.idSector = Convert.ToInt32(cmbSector.SelectedValue);

                    nuevo.Agregar();
                    MessageBox.Show("Empleado agregado correctamente.");

                    txtid.Clear();
                    txtNombre.Clear();
                    txtApellido.Clear();
                    txtCUIL.Clear();
                    cmbTurno.SelectedIndex = -1;
                    cmbSector.SelectedIndex = -1;
                    
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

            formularioEmpleados.RecargarGrilla();
        }
        private void ValidarCampos()
        {
            if (txtNombre.Text.Trim() != "" && txtApellido.Text.Trim() != "" && txtCUIL.Text.Trim() != "" && cmbSector.SelectedIndex != -1 && cmbTurno.SelectedIndex != -1)
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

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void txtCUIL_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void cmbTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void cmbSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }
    }
}
