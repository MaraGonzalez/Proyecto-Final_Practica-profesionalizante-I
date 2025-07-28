using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_TrabajoIntegradorPP1
{
    internal class clsVentas
    {
        private readonly String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BBDDMagnolia.mdb";

        public Int32 idVentas { get; set; }
        public DateTime Fecha { get; set; }
        public Int32 idCliente { get; set; }
        public Int32 idEmpleado { get; set; }
        public Int32 idMetodoPago { get; set; }
        public Decimal total { get; set; }
        public Decimal totalDescuento { get; set; }

        public String Cliente { get; set; }
        public String Vendedor { get; set; }



        public List<clsDetallesVentas> Detalles { get; set; }

        public clsVentas()
        {
            Detalles = new List<clsDetallesVentas>();
        }

        public void Agregar()
        {
            using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
            {
                conexion.Open();
                OleDbTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    // Generar ID único para la venta
                    idVentas = GenerarIdUnico("Ventas", "idVentas", conexion, transaccion);

                    // Insertar venta principal
                    String sqlVenta = @"INSERT INTO Ventas 
                                    (idVentas, Fecha, idEmpleado, idCliente, idMétodoPago, Total, TotalConDescuento) 
                                    VALUES (?, ?, ?, ?, ?, ?, ?)";

                    using (OleDbCommand cmdVenta = new OleDbCommand(sqlVenta, conexion, transaccion))
                    {
                        cmdVenta.Parameters.Add("@idVentas", OleDbType.Integer).Value = idVentas;
                        cmdVenta.Parameters.Add("@Fecha", OleDbType.Date).Value = Fecha;
                        cmdVenta.Parameters.Add("@idEmpleado", OleDbType.Integer).Value = idEmpleado;
                        cmdVenta.Parameters.Add("@idCliente", OleDbType.Integer).Value = idCliente;
                        cmdVenta.Parameters.Add("@idMetodoPago", OleDbType.Integer).Value = idMetodoPago;
                        cmdVenta.Parameters.Add("@Total", OleDbType.Decimal).Value = total;
                        cmdVenta.Parameters.Add("@TotalConDescuento", OleDbType.Decimal).Value = totalDescuento;

                        cmdVenta.ExecuteNonQuery();
                    }

                    // Insertar detalles de venta
                    foreach (var detalle in Detalles)
                    {
                        detalle.idVenta = idVentas; // Asignar el idVenta al detalle

                        // Insertar detalle
                        String sqlDetalle = @"INSERT INTO DetalleVentas 
                                      (idDetalle, idVenta, idProducto, Cantidad, PrecioUnitario, SubTotal) 
                                      VALUES (?, ?, ?, ?, ?, ?)";

                        using (OleDbCommand cmdDetalle = new OleDbCommand(sqlDetalle, conexion, transaccion))
                        {
                            cmdDetalle.Parameters.AddWithValue("?", GenerarIdUnico("DetalleVentas", "idDetalle", conexion, transaccion));
                            cmdDetalle.Parameters.AddWithValue("?", detalle.idVenta);
                            cmdDetalle.Parameters.AddWithValue("?", detalle.idProducto);
                            cmdDetalle.Parameters.AddWithValue("?", detalle.cantidad);
                            cmdDetalle.Parameters.AddWithValue("?", detalle.precioUnitario);
                            cmdDetalle.Parameters.AddWithValue("?", detalle.subtotal);
                            cmdDetalle.ExecuteNonQuery();
                        }

                        // Actualizar stock
                        String sqlStock = "UPDATE Producto SET Stock = Stock - ? WHERE idProducto = ?";
                        using (OleDbCommand cmdStock = new OleDbCommand(sqlStock, conexion, transaccion))
                        {
                            cmdStock.Parameters.AddWithValue("?", detalle.cantidad);
                            cmdStock.Parameters.AddWithValue("?", detalle.idProducto);
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw new Exception("Error al registrar la venta: " + ex.Message);
                }
            }
        }

        private Int32 GenerarIdUnico(String tabla, String campoId, OleDbConnection conexion, OleDbTransaction transaccion)
        {
            Random rnd = new Random();
            Int32 nuevoId;
            Boolean idUnico = false;
            Int32 intentos = 0;

            while (!idUnico && intentos < 100)
            {
                nuevoId = rnd.Next(1, 101);

                String sqlVerificar = $"SELECT COUNT(*) FROM {tabla} WHERE {campoId} = ?";
                using (OleDbCommand cmdVerificar = new OleDbCommand(sqlVerificar, conexion, transaccion))
                {
                    cmdVerificar.Parameters.AddWithValue("?", nuevoId);
                    Int32 existe = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (existe == 0)
                    {
                        return nuevoId;
                    }
                }

                intentos++;
            }

            throw new Exception("No se pudo generar un ID único después de 100 intentos");
        }

        public String GenerarTicket()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("                  ╔════════════════════════════════════════════╗");
            sb.AppendLine("                  ║                                            ║");
            sb.AppendLine("                  ║            MAGNOLIA ECO TIENDA             ║");
            sb.AppendLine("                  ║      Productos naturales y artesanales     ║");
            sb.AppendLine("                  ║             TICKET DE COMPRA               ║");
            sb.AppendLine("                  ║                                            ║");
            sb.AppendLine("                  ╚════════════════════════════════════════════╝");
            sb.AppendLine();

            sb.AppendLine($"Fecha     : {Fecha:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Vendedor  : {Vendedor}");
            sb.AppendLine($"Cliente   : {Cliente}");
            sb.AppendLine();

            sb.AppendLine("──────────────────────────────────────────────────────────────");
            sb.AppendLine("🛍️  DETALLE DE VENTA");
            sb.AppendLine("──────────────────────────────────────────────────────────────");
            sb.AppendLine();
            sb.AppendLine("Producto                       | P.Unit.   | Cant | Subtotal");
            sb.AppendLine("-------------------------------|-----------|------|-----------");

            foreach (var item in Detalles)
            {
                String nombre = item.nombreProducto;
                if (nombre.Length > 26)
                    nombre = nombre.Substring(0, 26);
                nombre = nombre.PadRight(26);

                String precio = item.precioUnitario.ToString("C0", CultureInfo.CurrentCulture).PadLeft(9);
                String cant = item.cantidad.ToString().PadLeft(4);
                String sub = item.subtotal.ToString("C0", CultureInfo.CurrentCulture).PadLeft(9);

                sb.AppendLine($"{nombre} | {precio} | {cant} | {sub}");
            }

            sb.AppendLine("--------------------------------------------------------------");

            sb.AppendLine($"Subtotal{"".PadLeft(28)}→{total.ToString("C0", CultureInfo.CurrentCulture).PadLeft(12)}");
            sb.AppendLine($"Descuento aplicado{"".PadLeft(1)}→{(total - totalDescuento).ToString("C0", CultureInfo.CurrentCulture).PadLeft(12)}");
            sb.AppendLine($"TOTAL A PAGAR{"".PadLeft(23)}→{totalDescuento.ToString("C0", CultureInfo.CurrentCulture).PadLeft(12)}");
            sb.AppendLine("==============================================================");

            sb.AppendLine();
            sb.AppendLine("           Gracias por elegir un consumo consciente ");
            sb.AppendLine("                   “Los detalles sí importan” ✨");
            sb.AppendLine();
            sb.AppendLine("Rosario, Santa Fe");

            return sb.ToString();
        }


    }
}

       


