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
    public partial class frmEmpleados : Form
    {
        public frmEmpleados()
        {
            InitializeComponent();
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            clsEmpleado L = new clsEmpleado();
            L.Listar(dgvEmpleados);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog archivo = new SaveFileDialog();
            archivo.Filter = "Archivo CSV (*.csv)|*.csv|Archivo de texto (*.txt)|*.txt";
            archivo.Title = "Guardar Reporte de Empleados";
            archivo.FileName = "ReporteEmpleados.csv";

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                clsEmpleado objEmpleado = new clsEmpleado();
                objEmpleado.ReporteEmpleados(archivo.FileName);
                MessageBox.Show("Se ha guardado el archivo correctamente.");
            }
        }
        private void btnAltaEmpleado_Click(object sender, EventArgs e)
        {
            frmAltaEmpleado Ventana = new frmAltaEmpleado(this);
            Ventana.ShowDialog(); 
        }
        public void RecargarGrilla()
        {
            clsEmpleado L = new clsEmpleado();
            L.Listar(dgvEmpleados);
        }

        private void btnModificarEmpleado_Click(object sender, EventArgs e)
        {
            frmModificarEmpleado Ventana = new frmModificarEmpleado(this);
            Ventana.ShowDialog();
        }

        private void btnBajaEmpleado_Click(object sender, EventArgs e)
        {
            frmBajaEmpleado Ventana = new frmBajaEmpleado(this);
            Ventana.ShowDialog();
        }

        private void btnAsistencia_Click(object sender, EventArgs e)
        {
            frmAsistencia Ventana = new frmAsistencia();
            Ventana.ShowDialog();
        }

    }
}
