using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using System.IO;
using System.Threading;

namespace pry_TrabajoIntegradorPP1
{
    public partial class frmNavegadorWeb : Form
    {
        private String url;
        public frmNavegadorWeb()
        {
            InitializeComponent();
        }
        public frmNavegadorWeb(String url, String tituloVentana = "Navegador Web")
        {
            InitializeComponent();
            this.url = url;
            this.Text = tituloVentana;
        }

        private async void frmNavegadorWeb_Load(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async(null);
            if (!string.IsNullOrEmpty(url))
            {
                webView21.Source = new Uri(url);
            }
        }
    }
}
