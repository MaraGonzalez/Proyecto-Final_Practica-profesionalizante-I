using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace pry_TrabajoIntegradorPP1
{
    public partial class frmDetalleVentas : Form
    {
        private String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BBDDMagnolia.mdb";

        public frmDetalleVentas()
        {
            InitializeComponent();
        }

        private void frmDetalleVentas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarVendedores();
            cmbCliente.SelectedIndex = -1;
            cmbVendedor.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lstVentas.Items.Clear();

            using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
            {
                conexion.Open();

                String query = @"
                                SELECT V.idVentas, V.Fecha, V.TotalConDescuento, 
                                       IIF(C.Nombre IS NULL, 'CONSUMIDOR FINAL', C.Nombre) AS ClienteNombre
                                FROM Ventas V
                                LEFT JOIN ClienteMayorista C ON V.idCliente = C.idCliente
                                WHERE V.Fecha BETWEEN ? AND ?
                                ORDER BY V.Fecha";
                OleDbCommand cmd = new OleDbCommand(query, conexion);

                OleDbParameter paramDesde = new OleDbParameter("?", OleDbType.Date);
                paramDesde.Value = dtpDesde.Value.Date;

                OleDbParameter paramHasta = new OleDbParameter("?", OleDbType.Date);
                paramHasta.Value = dtpHasta.Value.Date.AddDays(1).AddTicks(-1);

                cmd.Parameters.Add(paramDesde);
                cmd.Parameters.Add(paramHasta);

                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Int32 id = Convert.ToInt32(reader["idVentas"]);
                    DateTime fecha = Convert.ToDateTime(reader["Fecha"]);
                    Decimal total = Convert.ToDecimal(reader["TotalConDescuento"]);
                    String cliente = reader["ClienteNombre"].ToString();

                    lstVentas.Items.Add($"{id} - {cliente} - {fecha:dd/MM/yyyy HH:mm} - {total:C}");
                }

                reader.Close();
            }
        }

        private void lstVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVentas.SelectedItem == null) return;

            Int32 idVenta = Convert.ToInt32(lstVentas.SelectedItem.ToString().Split('-')[0].Trim());
            MostrarFacturaEnLabel(idVenta);
            btnGuardar.Enabled = true;
            btnLimpiar.Enabled = true;
        }

        private void MostrarFacturaEnLabel(Int32 idVenta)
        {
            StringBuilder sb = new StringBuilder();

            using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
            {
                conexion.Open();

                String consulta = @"
                                    SELECT V.Fecha, C.Nombre AS ClienteMayorista, 
                                           E.Nombre & ' ' & E.Apellido AS Empleado,
                                           P.Nombre AS Producto, DV.PrecioUnitario, DV.Cantidad, DV.Subtotal,
                                           V.Total, V.TotalConDescuento
                                    FROM ((((DetalleVentas DV
                                    INNER JOIN Ventas V ON DV.idVenta = V.idVentas)
                                    INNER JOIN Producto P ON DV.idProducto = P.idProducto)
                                    INNER JOIN ClienteMayorista C ON V.idCliente = C.idCliente)
                                    INNER JOIN Empleado E ON V.idEmpleado = E.idEmpleado)
                                    WHERE V.idVentas = ?";

                OleDbCommand cmd = new OleDbCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("?", idVenta);

                OleDbDataReader reader = cmd.ExecuteReader();

                String vendedor = "", cliente = "";
                DateTime fecha = DateTime.Now;
                Decimal total = 0, totalDescuento = 0;

                // ENCABEZADO
                sb.AppendLine("             ╔════════════════════════════════════════════╗");
                sb.AppendLine("             ║                                            ║");
                sb.AppendLine("             ║            MAGNOLIA ECO TIENDA             ║");
                sb.AppendLine("             ║      Productos naturales y artesanales     ║");
                sb.AppendLine("             ║             TICKET DE COMPRA               ║");
                sb.AppendLine("             ║                                            ║");
                sb.AppendLine("             ╚════════════════════════════════════════════╝");
                sb.AppendLine();

                Boolean encabezadoAgregado = false;

                // CABECERA DE DETALLE
                String cabecera = "Producto                    | P.Unit.   | Cant | Subtotal";
                String separador = new String('-', cabecera.Length);

                while (reader.Read())
                {
                    if (!encabezadoAgregado)
                    {
                        vendedor = reader["Empleado"].ToString();
                        cliente = reader["ClienteMayorista"].ToString();
                        fecha = Convert.ToDateTime(reader["Fecha"]);
                        total = Convert.ToDecimal(reader["Total"]);
                        totalDescuento = Convert.ToDecimal(reader["TotalConDescuento"]);

                        sb.AppendLine($"Fecha     : {fecha:dd/MM/yyyy HH:mm}");
                        sb.AppendLine($"Vendedor  : {vendedor}");
                        sb.AppendLine($"Cliente   : {cliente}");
                        sb.AppendLine();
                        sb.AppendLine("──────────────────────────────────────────────────────────────");
                        sb.AppendLine(" DETALLE DE VENTA");
                        sb.AppendLine("──────────────────────────────────────────────────────────────");
                        sb.AppendLine();
                        sb.AppendLine(cabecera);
                        sb.AppendLine(separador);

                        encabezadoAgregado = true;
                    }

                    String nombreProducto = reader["Producto"].ToString();
                    if (nombreProducto.Length > 26) nombreProducto = nombreProducto.Substring(0, 26);
                    nombreProducto = nombreProducto.PadRight(26);

                    String precioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]).ToString("C0").PadLeft(9);
                    String cantidad = reader["Cantidad"].ToString().PadLeft(4);
                    String subtotal = Convert.ToDecimal(reader["Subtotal"]).ToString("C0").PadLeft(9);

                    sb.AppendLine($"{nombreProducto} | {precioUnitario} | {cantidad} | {subtotal}");
                }

                sb.AppendLine(separador);
                sb.AppendLine($"Subtotal{"".PadLeft(28)}→{total.ToString("C0").PadLeft(12)}");
                sb.AppendLine($"Descuento aplicado{"".PadLeft(18)}→{(total - totalDescuento).ToString("C0").PadLeft(12)}");
                sb.AppendLine($"TOTAL A PAGAR{"".PadLeft(23)}→{totalDescuento.ToString("C0").PadLeft(12)}");
                sb.AppendLine("==============================================================");

                // FRASE FINAL
                sb.AppendLine();
                sb.AppendLine("           Gracias por elegir un consumo consciente ");
                sb.AppendLine("             “Los detalles sí importan” ✨");
                sb.AppendLine();
                sb.AppendLine(" Rosario, Santa Fe");

                reader.Close();
            }

            lblFactura.Font = new Font("Courier New", 9);
            lblFactura.Text = sb.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            printDocument1 = new PrintDocument(); // Reasignar por si ya se usó
            printDocument1.PrintPage += printDocument1_PrintPage;

            PrintPreviewDialog vistaPrevia = new PrintPreviewDialog
            {
                Document = printDocument1,
                Width = 800,
                Height = 600
            };

            try
            {
                vistaPrevia.ShowDialog(); // Mostrar vista previa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar mostrar vista previa: " + ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lstVentas.Items.Clear();
            lblFactura.Text = "";
            btnGuardar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font fuente = new Font("Courier New", 10); // Fuente monoespaciada
            float y = 20;
            float margenIzquierdo = e.MarginBounds.Left;

            StringReader lector = new StringReader(lblFactura.Text);
            String linea;

            while ((linea = lector.ReadLine()) != null)
            {
                e.Graphics.DrawString(linea, fuente, Brushes.Black, margenIzquierdo, y);
                y += fuente.GetHeight(e.Graphics);
            }
        }

        private void lblFactura_Click(object sender, EventArgs e)
        {

        }


        private void CargarVendedores()
        {
            using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
            {
                conexion.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT idEmpleado, Nombre FROM Empleado", conexion);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmbVendedor.DataSource = dt;
                cmbVendedor.DisplayMember = "Nombre";
                cmbVendedor.ValueMember = "idEmpleado";
            }
        }

        private void CargarClientes()
        {
            using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
            {
                conexion.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT idCliente, Nombre FROM ClienteMayorista", conexion);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmbCliente.DataSource = dt;
                cmbCliente.DisplayMember = "Nombre";
                cmbCliente.ValueMember = "idCliente";
            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            chtVentas.Series.Clear(); // Limpiar gráfico
            chtVentas.Titles.Clear();
            chtVentas.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM";
            chtVentas.ChartAreas[0].AxisX.Interval = 1;

            if (rdbPeriodo.Checked)
            {
                GraficarPorPeriodo(dtpDesdeInf.Value.Date, dtpHastaInf.Value.Date.AddDays(1).AddSeconds(-1));
                chtVentas.Titles.Add("Ventas por Período");
            }
            else if (rdbVendedor.Checked && cmbVendedor.SelectedIndex >= 0)
            {
                int idEmpleado = Convert.ToInt32(cmbVendedor.SelectedValue);
                GraficarPorVendedor(idEmpleado);
                chtVentas.Titles.Add($"Ventas por Vendedor: {cmbVendedor.Text}");
            }
            else if (rdbCliente.Checked && cmbCliente.SelectedIndex >= 0)
            {
                Int32 idCliente = Convert.ToInt32(cmbCliente.SelectedValue);
                GraficarPorCliente(idCliente);
                chtVentas.Titles.Add($"Ventas por Cliente: {cmbCliente.Text}");
            }
        }

        private void GraficarPorPeriodo(DateTime desde, DateTime hasta)
        {
            using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
            {
                conexion.Open();
                string consulta = "SELECT Fecha, TotalConDescuento FROM Ventas WHERE Fecha BETWEEN ? AND ? ORDER BY Fecha";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("?", desde);
                cmd.Parameters.AddWithValue("?", hasta);

                OleDbDataReader reader = cmd.ExecuteReader();

                Series serie = chtVentas.Series.Add("Ventas");
                serie.ChartType = SeriesChartType.Column;

                while (reader.Read())
                {
                    DateTime fecha = Convert.ToDateTime(reader["Fecha"]);
                    Decimal total = Convert.ToDecimal(reader["TotalConDescuento"]);
                    serie.Points.AddXY(fecha.ToString("dd/MM"), total);
                }
            }
        }

        private void GraficarPorVendedor(Int32 idEmpleado)
        {
            using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
            {
                conexion.Open();
                String consulta = "SELECT Fecha, TotalConDescuento FROM Ventas WHERE idEmpleado = ? ORDER BY Fecha";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("?", idEmpleado);

                OleDbDataReader reader = cmd.ExecuteReader();

                Series serie = chtVentas.Series.Add("Ventas por Vendedor");
                serie.ChartType = SeriesChartType.Column;

                while (reader.Read())
                {
                    DateTime fecha = Convert.ToDateTime(reader["Fecha"]);
                    Decimal total = Convert.ToDecimal(reader["TotalConDescuento"]);
                    serie.Points.AddXY(fecha.ToString("dd/MM"), total);
                }
            }
        }

        private void GraficarPorCliente(Int32 idCliente)
        {
            using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
            {
                conexion.Open();
                String consulta = "SELECT Fecha, TotalConDescuento FROM Ventas WHERE idCliente = ? ORDER BY Fecha";
                OleDbCommand cmd = new OleDbCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("?", idCliente);

                OleDbDataReader reader = cmd.ExecuteReader();

                Series serie = chtVentas.Series.Add("Ventas por Cliente");
                serie.ChartType = SeriesChartType.Column;

                while (reader.Read())
                {
                    DateTime fecha = Convert.ToDateTime(reader["Fecha"]);
                    Decimal total = Convert.ToDecimal(reader["TotalConDescuento"]);
                    serie.Points.AddXY(fecha.ToString("dd/MM"), total);
                }
            }
        }

        private void ActualizarEstadoBotonGraficar()
        {
            if (rdbPeriodo.Checked)
            {
                btnGraficar.Enabled = true;
            }
            else if (rdbVendedor.Checked && cmbVendedor.SelectedIndex >= 0)
            {
                btnGraficar.Enabled = true;
            }
            else if (rdbCliente.Checked && cmbCliente.SelectedIndex >= 0)
            {
                btnGraficar.Enabled = true;
            }
            else
            {
                btnGraficar.Enabled = false;
            }
        }

        private void rdbPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonGraficar();
        }

        private void rdbVendedor_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonGraficar();
        }

        private void cmbVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonGraficar();
        }

        private void rdbCliente_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonGraficar();
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonGraficar();
        }
    }
}

