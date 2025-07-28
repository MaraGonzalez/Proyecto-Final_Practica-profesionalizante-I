using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace pry_TrabajoIntegradorPP1
{
    internal class clsProveedor
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BBDDMagnolia.mdb";

        public Int32 idProveedor;
        public String nombProveedor;
        public String contacto;
        public Int32 idProvincia;
        public Int32 idCiudad;
        public Int32 telefono;

        public void Listar(DataGridView Grilla)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = @"SELECT 
                                    P.idProveedor, 
                                    P.Nombre,
                                    P.Contacto, 
                                    Pr.Nombre,
                                    C.Nombre, 
                                    P.Teléfono
                                FROM ((Proveedor AS P
                                INNER JOIN Provincias AS Pr ON P.idProvincia = Pr.idProvincia)
                                INNER JOIN Ciudades AS C ON P.idCiudad = C.idCiudad); ";

                OleDbDataReader DR = comando.ExecuteReader();

                Grilla.Rows.Clear();
                while (DR.Read())
                {
                    Grilla.Rows.Add(
                        DR.GetInt32(0),               // idProveedor
                        DR.GetString(1),              // Nombre
                        DR.GetInt32(5),              // Teléfono
                        DR.GetString(2),              // Contacto
                        DR.GetString(3),               // Nombre de Provincia
                        DR.GetString(4)               // Nombre de Ciudad
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
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = @"SELECT 
                                    P.idProveedor, 
                                    P.Nombre,
                                    P.Contacto, 
                                    Pr.Nombre,
                                    C.Nombre, 
                                    P.Teléfono
                                FROM ((Proveedor AS P
                                INNER JOIN Provincias AS Pr ON P.idProvincia = Pr.idProvincia)
                                INNER JOIN Ciudades AS C ON P.idCiudad = C.idCiudad); ";

                OleDbDataReader DR = comando.ExecuteReader();

                StreamWriter AD = new StreamWriter(rutaArchivo, false, Encoding.UTF8);
                AD.WriteLine("Listado de Proveedores\n");
                AD.WriteLine("idProveedor;Nombre;Teléfono;Contacto;Provincia;Ciudad");
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        AD.Write(DR.GetInt32(0)); AD.Write(";");    // idProveedor
                        AD.Write(DR.GetString(1)); AD.Write(";");   // Nombre
                        AD.Write(DR.GetInt32(5)); AD.Write(";");   // Teléfono
                        AD.Write(DR.GetString(2)); AD.Write(";");   // Contacto
                        AD.Write(DR.GetString(3)); AD.Write(";");   // Nombre de Provincia
                        AD.WriteLine(DR.GetString(4));              // Nombre de Ciudad
                    }
                }
                conexion.Close();
                AD.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public void Agregar(TextBox txtNombre, TextBox txtTelefono, TextBox txtContacto, TextBox txtProvincia, TextBox txtCiudad)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                // Generar idProveedor aleatorio sin repetir
                Random rnd = new Random();
                Int32 nuevoIdProveedor;
                do
                {
                    nuevoIdProveedor = rnd.Next(1, 100); // 1 al 99
                    comando = new OleDbCommand($"SELECT COUNT(*) FROM Proveedor WHERE idProveedor = {nuevoIdProveedor}", conexion);
                } while ((Int32)comando.ExecuteScalar() > 0);

                // Obtener o insertar Provincia
                String nombreProvincia = txtProvincia.Text.ToUpper();
                Int32 idProvincia;
                comando = new OleDbCommand($"SELECT idProvincia FROM Provincias WHERE Nombre = '{nombreProvincia}'", conexion);
                var resultProvincia = comando.ExecuteScalar();

                if (resultProvincia != null)
                {
                    idProvincia = Convert.ToInt32(resultProvincia);
                }
                else
                {
                    do
                    {
                        idProvincia = rnd.Next(1, 100);
                        comando = new OleDbCommand($"SELECT COUNT(*) FROM Provincias WHERE idProvincia = {idProvincia}", conexion);
                    } while ((Int32)comando.ExecuteScalar() > 0);

                    comando = new OleDbCommand($"INSERT INTO Provincias (idProvincia, Nombre) VALUES ({idProvincia}, '{nombreProvincia}')", conexion);
                    comando.ExecuteNonQuery();
                }

                // Obtener o insertar Ciudad
                String nombreCiudad = txtCiudad.Text.ToUpper();
                Int32 idCiudad;
                comando = new OleDbCommand($"SELECT idCiudad FROM Ciudades WHERE Nombre = '{nombreCiudad}'", conexion);
                var resultCiudad = comando.ExecuteScalar();

                if (resultCiudad != null)
                {
                    idCiudad = Convert.ToInt32(resultCiudad);
                }
                else
                {
                    do
                    {
                        idCiudad = rnd.Next(1, 100);
                        comando = new OleDbCommand($"SELECT COUNT(*) FROM Ciudades WHERE idCiudad = {idCiudad}", conexion);
                    } while ((int)comando.ExecuteScalar() > 0);

                    comando = new OleDbCommand($"INSERT INTO Ciudades (idCiudad, Nombre, idProvincia) VALUES ({idCiudad}, '{nombreCiudad}', {idProvincia})", conexion);
                    comando.ExecuteNonQuery();
                }

                // Agregar Proveedor
                String nombreProveedor = txtNombre.Text.ToUpper();
                String contacto = txtContacto.Text.ToUpper();
                String telefono = txtTelefono.Text;

                String sql = $"INSERT INTO Proveedor (idProveedor, Nombre, Contacto, idProvincia, idCiudad, Teléfono) " +
                             $"VALUES ({nuevoIdProveedor}, '{nombreProveedor}', '{contacto}', {idProvincia}, {idCiudad}, '{telefono}')";

                comando = new OleDbCommand(sql, conexion);
                comando.ExecuteNonQuery();

                conexion.Close();

                MessageBox.Show("Proveedor agregado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar proveedor: " + ex.Message);
            }
        }
        public DataTable ObtenerListadoProveedores()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idProveedor, Nombre FROM Proveedor";

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
        public void Modificar(Int32 idProveedor, Int32 telefono, String contacto, String provinciaNombre, String ciudadNombre, String nombreProveedor)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                // Validación previa
                if (String.IsNullOrEmpty(nombreProveedor) || String.IsNullOrEmpty(contacto) ||
                    String.IsNullOrEmpty(provinciaNombre) || String.IsNullOrEmpty(ciudadNombre) || telefono == 0 || idProveedor == 0)
                {
                    MessageBox.Show("Faltan datos obligatorios para modificar.");
                    conexion.Close();
                    return;
                }

                // Convertir todo a MAYÚSCULAS
                String provMayus = provinciaNombre.ToUpper();
                String ciudadMayus = ciudadNombre.ToUpper();
                String contactoMayus = contacto.ToUpper();
                String nombreProvMayus = nombreProveedor.ToUpper();

                // 1. Verificar/Insertar Provincia
                Int32 idProvincia;
                String sqlProv = "SELECT idProvincia FROM Provincias WHERE Nombre = ?";
                OleDbCommand cmdProv = new OleDbCommand(sqlProv, conexion);
                cmdProv.Parameters.AddWithValue("?", provMayus);
                object provResult = cmdProv.ExecuteScalar();

                if (provResult == null || provResult == DBNull.Value)
                {
                    Random rnd = new Random();
                    Int32 nuevoIdProv;
                    OleDbCommand checkProv;
                    do
                    {
                        nuevoIdProv = rnd.Next(1, 100);
                        checkProv = new OleDbCommand("SELECT COUNT(*) FROM Provincias WHERE idProvincia = ?", conexion);
                        checkProv.Parameters.AddWithValue("?", nuevoIdProv);
                    } while ((Int32)(checkProv.ExecuteScalar()) > 0);

                    OleDbCommand insertProv = new OleDbCommand("INSERT INTO Provincias (idProvincia, Nombre) VALUES (?, ?)", conexion);
                    insertProv.Parameters.AddWithValue("?", nuevoIdProv);
                    insertProv.Parameters.AddWithValue("?", provMayus);
                    insertProv.ExecuteNonQuery();

                    idProvincia = nuevoIdProv;
                }
                else
                {
                    idProvincia = Convert.ToInt32(provResult);
                }

                // 2. Verificar/Insertar Ciudad
                Int32 idCiudad;
                String sqlCiudad = "SELECT idCiudad FROM Ciudades WHERE Nombre = ?";
                OleDbCommand cmdCiudad = new OleDbCommand(sqlCiudad, conexion);
                cmdCiudad.Parameters.AddWithValue("?", ciudadMayus);
                object ciudadResult = cmdCiudad.ExecuteScalar();

                if (ciudadResult == null || ciudadResult == DBNull.Value)
                {
                    Random rnd = new Random();
                    Int32 nuevoIdCiudad;
                    OleDbCommand checkCiudad;
                    do
                    {
                        nuevoIdCiudad = rnd.Next(1, 100);
                        checkCiudad = new OleDbCommand("SELECT COUNT(*) FROM Ciudades WHERE idCiudad = ?", conexion);
                        checkCiudad.Parameters.AddWithValue("?", nuevoIdCiudad);
                    } while ((Int32)(checkCiudad.ExecuteScalar()) > 0);

                    OleDbCommand insertCiudad = new OleDbCommand("INSERT INTO Ciudades (idCiudad, Nombre, idProvincia) VALUES (?, ?, ?)", conexion);
                    insertCiudad.Parameters.AddWithValue("?", nuevoIdCiudad);
                    insertCiudad.Parameters.AddWithValue("?", ciudadMayus);
                    insertCiudad.Parameters.AddWithValue("?", idProvincia);
                    insertCiudad.ExecuteNonQuery();

                    idCiudad = nuevoIdCiudad;
                }
                else
                {
                    idCiudad = Convert.ToInt32(ciudadResult);
                }

                // 3. UPDATE Proveedor
                String sqlUpdate = @"
                                    UPDATE Proveedor 
                                    SET Nombre = ?, 
                                        Contacto = ?, 
                                        idProvincia = ?, 
                                        idCiudad = ?, 
                                        Teléfono = ? 
                                    WHERE idProveedor = ?";

                OleDbCommand cmdUpdate = new OleDbCommand(sqlUpdate, conexion);
                cmdUpdate.Parameters.AddWithValue("?", nombreProvMayus);     // Nombre
                cmdUpdate.Parameters.AddWithValue("?", contactoMayus);       // Contacto
                cmdUpdate.Parameters.AddWithValue("?", idProvincia);         // Provincia
                cmdUpdate.Parameters.AddWithValue("?", idCiudad);            // Ciudad
                cmdUpdate.Parameters.AddWithValue("?", telefono);            // Teléfono
                cmdUpdate.Parameters.AddWithValue("?", idProveedor);         // ID proveedor

                cmdUpdate.ExecuteNonQuery();

                MessageBox.Show("Proveedor modificado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR al modificar proveedor: " + ex.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }
        public DataRow BuscarPorId(Int32 id)
        {
            DataTable tabla = new DataTable();
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                String sql = @"
                            SELECT 
                                P.idProveedor, 
                                P.Nombre, 
                                P.Contacto, 
                                P.Teléfono,
                                Pr.Nombre AS Provincia, 
                                C.Nombre AS Ciudad
                            FROM 
                                (Proveedor AS P 
                                INNER JOIN Provincias AS Pr ON P.idProvincia = Pr.idProvincia)
                                INNER JOIN Ciudades AS C ON P.idCiudad = C.idCiudad
                            WHERE 
                                P.idProveedor = ?";


                comando = new OleDbCommand(sql, conexion);
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
                MessageBox.Show(ex.Message);
            }

            return null;
        }
        public void Eliminar(Int32 IdProveedor)
        {
            try
            {
                String sql = "DELETE FROM Proveedor WHERE idProveedor = ?";

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();
                comando = new OleDbCommand(sql, conexion);
                comando.Parameters.AddWithValue("?", IdProveedor);

                Int32 filasAfectadas = comando.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    MessageBox.Show("No se encontró el proveedor a eliminar.");
                }
                else
                {
                    MessageBox.Show("Proveedor eliminado correctamente.");
                }

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al eliminar proveedor: " + e.Message);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

    }
}
