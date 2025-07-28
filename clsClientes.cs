using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace pry_TrabajoIntegradorPP1
{
    internal class clsClientes
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BBDDMagnolia.mdb";
        private String Tabla = "ClienteMayorista";

        public Int32 idCliente;
        public String nombCliente;
        public String contacto;
        public Int32 idProvincia;
        public Int32 idciudad;


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
                                        C.idCliente, 
                                        C.Nombre, 
                                        C.Contacto, 
                                        P.Nombre AS Provincia, 
                                        Ci.Nombre AS Ciudad
                                    FROM 
                                        ((ClienteMayorista C
                                        LEFT JOIN Provincias P ON C.idProvincia = P.idProvincia)
                                        LEFT JOIN Ciudades Ci ON C.idCiudad = Ci.idCiudad)";

                OleDbDataReader DR = comando.ExecuteReader();

                Grilla.Rows.Clear();
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        Grilla.Rows.Add(
                            Convert.ToInt32(DR["idCliente"]),
                            DR["Nombre"].ToString(),
                            DR["Contacto"].ToString(),
                            DR["Provincia"].ToString(),
                            DR["Ciudad"].ToString()
                        );
                    }
                }

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al listar clientes: " + e.Message);
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
                                        C.idCliente, 
                                        C.Nombre, 
                                        C.Contacto, 
                                        P.Nombre AS Provincia, 
                                        Ci.Nombre AS Ciudad
                                    FROM 
                                        ((ClienteMayorista C
                                        LEFT JOIN Provincias P ON C.idProvincia = P.idProvincia)
                                        LEFT JOIN Ciudades Ci ON C.idCiudad = Ci.idCiudad)";

                OleDbDataReader DR = comando.ExecuteReader();

                using (StreamWriter AD = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
                {
                    AD.WriteLine("Listado de Clientes Mayorista");
                    AD.WriteLine("idCliente;Nombre;Contacto;Provincia;Ciudad");

                    while (DR.Read())
                    {
                        AD.Write(Convert.ToInt32(DR["idCliente"])); AD.Write(";");
                        AD.Write(DR["Nombre"].ToString()); AD.Write(";");
                        AD.Write(DR["Contacto"].ToString()); AD.Write(";");
                        AD.Write(DR["Provincia"].ToString()); AD.Write(";");
                        AD.WriteLine(DR["Ciudad"].ToString());
                    }
                }

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al generar reporte: " + e.Message);
            }
        }

        public void Agregar(TextBox txtNombre, TextBox txtContacto, TextBox txtProvincia, TextBox txtCiudad)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                Random rnd = new Random();

                // ======== Provincia ========
                String nombreProvincia = txtProvincia.Text.Trim().ToUpper();
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

                // ========  Ciudad ========
                String nombreCiudad = txtCiudad.Text.Trim().ToUpper();
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
                    } while ((Int32)comando.ExecuteScalar() > 0);

                    comando = new OleDbCommand($"INSERT INTO Ciudades (idCiudad, Nombre, idProvincia) VALUES ({idCiudad}, '{nombreCiudad}', {idProvincia})", conexion);
                    comando.ExecuteNonQuery();
                }

                // ========  Cliente Mayorista ========
                String nombreCliente = txtNombre.Text.Trim().ToUpper();
                String contacto = txtContacto.Text.Trim().ToUpper();
                Int32 idCliente;
                do
                {
                    idCliente = rnd.Next(1, 100);
                    comando = new OleDbCommand($"SELECT COUNT(*) FROM ClienteMayorista WHERE idCliente = {idCliente}", conexion);
                } while ((Int32)comando.ExecuteScalar() > 0);

                string sql = $"INSERT INTO ClienteMayorista (idCliente, Nombre, idProvincia, Contacto, idCiudad) " +
                             $"VALUES ({idCliente}, '{nombreCliente}', {idProvincia}, '{contacto}', {idCiudad})";

                comando = new OleDbCommand(sql, conexion);
                comando.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente: " + ex.Message);
            }
        }

        public DataTable ObtenerListadoClientes()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idCliente, Nombre FROM ClienteMayorista";

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

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;

                comando.CommandText = @"
                                        SELECT 
                                            C.idCliente, 
                                            C.Nombre, 
                                            C.Contacto, 
                                            P.Nombre AS Provincia, 
                                            Ci.Nombre AS Ciudad
                                        FROM 
                                            ((ClienteMayorista C
                                            LEFT JOIN Provincias P ON C.idProvincia = P.idProvincia)
                                            LEFT JOIN Ciudades Ci ON C.idCiudad = Ci.idCiudad)
                                        WHERE 
                                            C.idCliente = ?";

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
                MessageBox.Show(ex.Message);
            }

            return null;
        }

        public void Modificar(Int32 idCliente, String contacto, String provincia, String ciudad)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                Random rnd = new Random();

                // Verificar o crear Provincia
                Int32 idProvincia;
                comando = new OleDbCommand("SELECT idProvincia FROM Provincias WHERE Nombre = ?", conexion);
                comando.Parameters.AddWithValue("?", provincia.ToUpper());
                object resProv = comando.ExecuteScalar();

                if (resProv != null)
                {
                    idProvincia = Convert.ToInt32(resProv);
                }
                else
                {                  
                    Int32 nuevoIdProv;
                    do
                    {
                        nuevoIdProv = rnd.Next(1, 101);
                        var check = new OleDbCommand("SELECT COUNT(*) FROM Provincias WHERE idProvincia = ?", conexion);
                        check.Parameters.AddWithValue("?", nuevoIdProv);
                        if ((Int32)check.ExecuteScalar() == 0) break;
                    } while (true);

                    // Insertar nueva provincia
                    var insertProv = new OleDbCommand("INSERT INTO Provincias (idProvincia, Nombre) VALUES (?, ?)", conexion);
                    insertProv.Parameters.AddWithValue("?", nuevoIdProv);
                    insertProv.Parameters.AddWithValue("?", provincia.ToUpper());
                    insertProv.ExecuteNonQuery();

                    idProvincia = nuevoIdProv;
                }

                //  Verificar o crear Ciudad
                Int32 idCiudad;
                comando = new OleDbCommand("SELECT idCiudad FROM Ciudades WHERE Nombre = ?", conexion);
                comando.Parameters.AddWithValue("?", ciudad.ToUpper());
                object resCiudad = comando.ExecuteScalar();

                if (resCiudad != null)
                {
                    idCiudad = Convert.ToInt32(resCiudad);
                }
                else
                {                    
                    Int32 nuevoIdCiudad;
                    do
                    {
                        nuevoIdCiudad = rnd.Next(1, 101);
                        var check = new OleDbCommand("SELECT COUNT(*) FROM Ciudades WHERE idCiudad = ?", conexion);
                        check.Parameters.AddWithValue("?", nuevoIdCiudad);
                        if ((Int32)check.ExecuteScalar() == 0) break;
                    } while (true);

                    // Insertar nueva ciudad
                    var insertCiudad = new OleDbCommand("INSERT INTO Ciudades (idCiudad, Nombre, idProvincia) VALUES (?, ?, ?)", conexion);
                    insertCiudad.Parameters.AddWithValue("?", nuevoIdCiudad);
                    insertCiudad.Parameters.AddWithValue("?", ciudad.ToUpper());
                    insertCiudad.Parameters.AddWithValue("?", idProvincia);
                    insertCiudad.ExecuteNonQuery();

                    idCiudad = nuevoIdCiudad;
                }

                // === Actualizar cliente ===
                comando = new OleDbCommand("UPDATE ClienteMayorista SET Contacto = ?, idProvincia = ?, idCiudad = ? WHERE idCliente = ?", conexion);
                comando.Parameters.AddWithValue("?", contacto.ToUpper());
                comando.Parameters.AddWithValue("?", idProvincia);
                comando.Parameters.AddWithValue("?", idCiudad);
                comando.Parameters.AddWithValue("?", idCliente);

                comando.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Eliminar(Int32 IdCliente)
        {
            try
            {
                String sql = "DELETE * FROM ClienteMayorista WHERE idCliente = " + IdCliente.ToString();

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();
                comando.Connection = conexion;

                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;

                comando.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
