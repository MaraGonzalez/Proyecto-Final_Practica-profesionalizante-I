using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;

namespace pry_TrabajoIntegradorPP1
{
    internal class clsDetallesVentas
    {
        private readonly string CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BBDDMagnolia.mdb";

        public Int32 idVenta { get; set; }
        public Int32 idProducto { get; set; }
        public Int32 cantidad { get; set; }
        public Decimal precioUnitario { get; set; }
        public Decimal subtotal => precioUnitario * cantidad;
        public String nombreProducto { get; set; }

    }
}
