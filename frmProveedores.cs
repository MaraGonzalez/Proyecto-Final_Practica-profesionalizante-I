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
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            clsProveedor L = new clsProveedor();
            L.Listar(dgvProveedores);
        }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog archivo = new SaveFileDialog();
            archivo.Filter = "Archivo CSV (*.csv)|*.csv|Archivo de texto (*.txt)|*.txt";
            archivo.Title = "Guardar Reporte de Proveedores";
            archivo.FileName = "ReporteProveedores.csv";

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                clsProveedor objProveedor = new clsProveedor();
                objProveedor.Reporte(archivo.FileName);
                MessageBox.Show("Se ha guardado el archivo correctamente.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            frmAltaProveedor ventana = new frmAltaProveedor(this);
            ventana.ShowDialog();
        }

        public void RecargarGrilla()
        {
            clsProveedor L = new clsProveedor();
            L.Listar(dgvProveedores);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmModificarProveedor ventana = new frmModificarProveedor(this);
            ventana.ShowDialog();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            frmBajaProveedor ventana = new frmBajaProveedor(this);
            ventana.ShowDialog();
        }
    }
}
