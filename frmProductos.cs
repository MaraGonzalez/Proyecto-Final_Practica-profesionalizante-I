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
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            clsProductos P = new clsProductos();
            P.Listar(dgvProductos);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog archivo = new SaveFileDialog();
            archivo.Filter = "Archivo CSV (*.csv)|*.csv|Archivo de texto (*.txt)|*.txt";
            archivo.Title = "Guardar Reporte de Productos";
            archivo.FileName = "ReporteProductos.csv";

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                clsProductos p = new clsProductos();
                p.Reporte(archivo.FileName);
                MessageBox.Show("Se ha guardado el archivo correctamente.");
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            frmAltaProducto ventana = new frmAltaProducto(this);
            ventana.ShowDialog();
        }

        public void RecargarGrilla()
        {
            clsProductos p = new clsProductos();
            p.Listar(dgvProductos);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmModificarProducto ventana = new frmModificarProducto(this);
            ventana.ShowDialog();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            frmBajaProducto ventana = new frmBajaProducto(this);
            ventana.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
