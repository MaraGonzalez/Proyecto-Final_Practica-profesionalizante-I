using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace pry_TrabajoIntegradorPP1
{
    internal class clsProductos
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BBDDMagnolia.mdb";
        private String Tabla = "Producto";

        public Int32 idProducto;
        public String nombProductos;
        public String rubro;
        public Decimal precioCompra;
        public Decimal precioVenta;
        public Int32 stock;
        public Int32 idProveedor;
        public String nombreProveedor;

        public void Listar(DataGridView Grilla)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"
                                    SELECT 
                                        P.idProducto, 
                                        P.Nombre, 
                                        R.Nombre AS Rubro, 
                                        P.PrecioCompra, 
                                        P.PrecioVenta, 
                                        P.Stock, 
                                        Pr.Nombre AS Proveedor
                                    FROM 
                                        ((Producto AS P 
                                        INNER JOIN Rubro AS R ON P.idRubro = R.idRubro)
                                        INNER JOIN Proveedor AS Pr ON P.idProveedor = Pr.idProveedor)";

                OleDbDataReader DR = comando.ExecuteReader();

                Grilla.Rows.Clear();
                while (DR.Read())
                {
                    Grilla.Rows.Add(
                        DR.GetInt32(0),               // idProductos
                        DR.GetString(1),              // Nombre
                        DR.GetString(2),              // Rubro (nombre)
                        DR.GetDecimal(3).ToString("C"), // PreCompra
                        DR.GetDecimal(4).ToString("C"), // PreVenta
                        DR.GetInt32(5),               // Stock
                        DR.GetString(6)               // Proveedor (nombre)
                    );
                }


                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Reporte(String rutaArchivo)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"
                                    SELECT 
                                        P.idProducto, 
                                        P.Nombre, 
                                        R.Nombre AS Rubro, 
                                        P.PrecioCompra, 
                                        P.PrecioVenta, 
                                        P.Stock, 
                                        Pr.Nombre AS Proveedor
                                    FROM 
                                        ((Producto AS P 
                                        INNER JOIN Rubro AS R ON P.idRubro = R.idRubro)
                                        INNER JOIN Proveedor AS Pr ON P.idProveedor = Pr.idProveedor)";

                OleDbDataReader DR = comando.ExecuteReader();

                StreamWriter AD = new StreamWriter(rutaArchivo, false, Encoding.UTF8);
                AD.WriteLine("Listado de Productos");
                AD.WriteLine("idProducto;Nombre;Rubro;PrecioCompra;PrecioVenta;Stock;Proveedor");

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        AD.Write(DR.GetInt32(0)); AD.Write(";");
                        AD.Write(DR.GetString(1)); AD.Write(";");
                        AD.Write(DR.GetString(2)); AD.Write(";");
                        AD.Write(DR.GetDecimal(3).ToString("C")); AD.Write(";");
                        AD.Write(DR.GetDecimal(4).ToString("C")); AD.Write(";");
                        AD.Write(DR.GetInt32(5)); AD.Write(";");
                        AD.WriteLine(DR.GetString(6));
                    }
                }

                AD.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void CargarRubrosEnComboBox(ComboBox combo)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                OleDbDataAdapter adaptador = new OleDbDataAdapter("SELECT idRubro, Nombre FROM Rubro", conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                combo.DataSource = tabla;
                combo.DisplayMember = "Nombre";     // Muestra el nombre
                combo.ValueMember = "idRubro";      // Guarda el id

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar rubros: " + ex.Message);
            }
        }

        public void Agregar(String nombreProducto, Int32 idRubro, Int32 idProveedor, Decimal precioCompra, Decimal precioVenta, Int32 stock)
        {
            try
            {
                using (var conexion = new OleDbConnection(CadenaConexion))
                {
                    conexion.Open();

                    // 1. Validar que el proveedor exista
                    using (var cmdCheckProv = new OleDbCommand("SELECT COUNT(*) FROM Proveedor WHERE idProveedor = ?", conexion))
                    {
                        cmdCheckProv.Parameters.AddWithValue("?", idProveedor);
                        int existeProveedor = (int)cmdCheckProv.ExecuteScalar();

                        if (existeProveedor == 0)
                        {
                            MessageBox.Show("El proveedor seleccionado no existe en la base de datos.");
                            return;
                        }
                    }

                    // 2. Generar ID aleatorio para el producto (1-100)
                    int nuevoIdProd;
                    Random rnd = new Random();

                    do
                    {
                        nuevoIdProd = rnd.Next(1, 101);
                        using (var checkCmd = new OleDbCommand("SELECT COUNT(*) FROM Producto WHERE idProducto = ?", conexion))
                        {
                            checkCmd.Parameters.AddWithValue("?", nuevoIdProd);
                            if ((int)checkCmd.ExecuteScalar() == 0)
                                break;
                        }
                    } while (true);

                    // 3. Insertar el producto
                    using (var insertarProd = new OleDbCommand(
                        "INSERT INTO Producto (idProducto, Nombre, idRubro, PrecioCompra, PrecioVenta, Stock, idProveedor) " +
                        "VALUES (?, ?, ?, ?, ?, ?, ?)", conexion))
                    {
                        insertarProd.Parameters.AddWithValue("?", nuevoIdProd);
                        insertarProd.Parameters.AddWithValue("?", nombreProducto.ToUpper());
                        insertarProd.Parameters.AddWithValue("?", idRubro);
                        insertarProd.Parameters.AddWithValue("?", precioCompra);
                        insertarProd.Parameters.AddWithValue("?", precioVenta);
                        insertarProd.Parameters.AddWithValue("?", stock);
                        insertarProd.Parameters.AddWithValue("?", idProveedor);

                        insertarProd.ExecuteNonQuery();
                        MessageBox.Show("Producto agregado correctamente.");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al agregar producto: " + e.Message);
            }
        }

        public void CargarProveedores(ComboBox cmbProveedor)
        {
            using (var conexion = new OleDbConnection(CadenaConexion))
            {
                conexion.Open();
                var cmd = new OleDbCommand("SELECT idProveedor, Nombre FROM Proveedor", conexion);
                var reader = cmd.ExecuteReader();

                var proveedores = new List<KeyValuePair<int, string>>();
                while (reader.Read())
                {
                    proveedores.Add(new KeyValuePair<int, string>(
                        reader.GetInt32(0),   // idProveedor
                        reader.GetString(1)   // Nombre
                    ));
                }

                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DisplayMember = "Value";  // Muestra el nombre
                cmbProveedor.ValueMember = "Key";      // Guarda el idProveedor
            }
        }


        public DataTable ObtenerListadoProductos()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idProducto, Nombre, PrecioVenta FROM Producto";

                OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
                adaptador.Fill(tabla);

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return tabla;
        }

        public DataRow BuscarPorId(Int32 id)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                String consulta = @"
                                    SELECT 
                                        p.idProducto,
                                        p.Nombre,
                                        r.Nombre AS RubroNombre,
                                        pr.Nombre AS ProveedorNombre,
                                        p.PrecioCompra,
                                        p.PrecioVenta,
                                        p.Stock,
                                        p.idProveedor,
                                        p.idRubro
                                    FROM 
                                        (Producto p
                                        LEFT JOIN Rubro r ON p.idRubro = r.idRubro)
                                        LEFT JOIN Proveedor pr ON p.idProveedor = pr.idProveedor
                                    WHERE 
                                        p.idProducto = ?";

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("?", id);

                OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
                adaptador.Fill(tabla);

                conexion.Close();

                if (tabla.Rows.Count > 0)
                {
                    return tabla.Rows[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar producto: " + ex.Message);
            }

            return null;
        }

        public void Modificar(Int32 idProductos, Decimal preCompra, Decimal preVenta, Int32 stock)
        {
            try
            {
                String sql = $"UPDATE Producto SET PrecioCompra = '{preCompra}', PrecioVenta = '{preVenta}', Stock = '{stock}' WHERE idProducto = {idProductos}";
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;

                comando.ExecuteNonQuery();//Se ejecuta el comando previo

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Eliminar(Int32 idProducto)
        {
            try
            {
                using (var conexion = new OleDbConnection(CadenaConexion))
                {
                    conexion.Open();

                    String sql = "DELETE FROM Producto WHERE idProducto = ?";

                    using (var comando = new OleDbCommand(sql, conexion))
                    {
                        comando.Parameters.AddWithValue("?", idProducto);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw; // Relanzamos la excepción para manejo superior
            }
        }








        //Para buscar en ventas
        public AutoCompleteStringCollection ObtenerAutoCompletarProductos()
        {
            AutoCompleteStringCollection productos = new AutoCompleteStringCollection();

            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idProducto, Nombre FROM Producto";

                OleDbDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    productos.Add(lector["idProducto"].ToString());
                    productos.Add(lector["Nombre"].ToString());
                }

                lector.Close();
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener productos: " + ex.Message);
            }

            return productos;
        }
        

        //ActualizarStock
        public void ActualizarStock(int idProducto, int cantidadVendida)
        {
            try
            {
                conexion.Open();
                string query = "UPDATE Producto SET Stock = Stock - @Cantidad WHERE idProducto = @idProducto";
                
                OleDbCommand comando = new OleDbCommand(query, conexion);
                comando.Parameters.AddWithValue("@Cantidad", cantidadVendida);
                comando.Parameters.AddWithValue("@idProducto", idProducto);

                comando.ExecuteNonQuery();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar stock: " + ex.Message);
            }
        }
    }
}
