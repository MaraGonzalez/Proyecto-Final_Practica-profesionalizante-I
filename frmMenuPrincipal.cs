using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace pry_TrabajoIntegradorPP1
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }



        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleados Ventana = new frmEmpleados();
            Ventana.ShowDialog();
        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
            frmProveedores Ventana = new frmProveedores();
            Ventana.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmProductos Ventana = new frmProductos();
            Ventana.ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes Ventana = new frmClientes();
            Ventana.ShowDialog();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            frmVentas ventana = new frmVentas();
            ventana.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAcercaDe ventana = new frmAcercaDe();
            ventana.ShowDialog();
        }

        private void btnWsp_Click(object sender, EventArgs e)
        {
            frmNavegadorWeb navegador = new frmNavegadorWeb("https://web.whatsapp.com", "💬 WhatsApp Web");
            navegador.Show();
        }

        private void btnMusica_Click(object sender, EventArgs e)
        {
            String url = $"https://music.youtube.com/playlist?list=RDCLAK5uy_kb9VsP3UHTddsaLV_LvfUnZW7i01Il_5c&playnext=1&si=k-wgVwJS4S3xTtVW";
            frmNavegadorWeb navegador = new frmNavegadorWeb(url, "🎵 Playlist de Música");
            navegador.Show();
        }
    }
}
