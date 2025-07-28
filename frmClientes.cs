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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            clsClientes c = new clsClientes();
            c.Listar(dgvClientes);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog archivo = new SaveFileDialog();
            archivo.Filter = "Archivo CSV (*.csv)|*.csv|Archivo de texto (*.txt)|*.txt";
            archivo.Title = "Guardar Reporte de Clientes-Mayoristas";
            archivo.FileName = "ReporteClientesMayoristas.csv";

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                clsClientes clientes = new clsClientes();
                clientes.Reporte(archivo.FileName);
                MessageBox.Show("Se ha guardado el archivo correctamente.");
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            frmAltaCliente ventana = new frmAltaCliente(this);
            ventana.ShowDialog();
        }

        public void RecargarGrilla()
        {
            clsClientes c = new clsClientes();
            c.Listar(dgvClientes);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmModificarCliente ventana = new frmModificarCliente(this);
            ventana.ShowDialog();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            frmBajaCliente ventana = new frmBajaCliente(this);
            ventana.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
