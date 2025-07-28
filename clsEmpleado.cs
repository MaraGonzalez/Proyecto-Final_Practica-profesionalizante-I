using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_TrabajoIntegradorPP1
{
    internal class clsEmpleado
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BBDDMagnolia.mdb";
        private String Tabla = "Empleado";

        public Int32 idEmpleado;
        public String nombEmpleado;
        public String apelEmpleado;
        public String CUIL;
        public String turno;
        public Int32 idSector;
        public String nombreSector;

        public void Listar(DataGridView Grilla)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = @"SELECT 
                                        E.idEmpleado, 
                                        E.Nombre, 
                                        E.Apellido, 
                                        E.CUIL, 
                                        E.Turno, 
                                        S.Nombre 
                                    FROM Empleado AS E
                                    INNER JOIN Sector AS S ON E.idSector = S.idSector";

                OleDbDataReader DR = comando.ExecuteReader();

                Grilla.Rows.Clear();
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        Grilla.Rows.Add(
                            DR.GetInt32(0),         // idEmpleado
                            DR.GetString(1),        // Nombre
                            DR.GetString(2),        // Apellido
                            DR.GetString(3),        // CUIL
                            DR.GetString(4),        // Turno
                            DR.GetString(5)         // NoidSector sino NombreSector
                        );
                    }
                }
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void ReporteEmpleados(String rutaArchivo)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = @"SELECT 
                                        E.idEmpleado, 
                                        E.Nombre, 
                                        E.Apellido, 
                                        E.CUIL, 
                                        E.Turno, 
                                        S.Nombre 
                                    FROM Empleado AS E
                                    INNER JOIN Sector AS S ON E.idSector = S.idSector";

                OleDbDataReader DR = comando.ExecuteReader();

                StreamWriter AD = new StreamWriter(rutaArchivo, false, Encoding.UTF8);
                AD.WriteLine("Listado de Empleados\n");
                AD.WriteLine("IdEmpleado;Nombre;Apellido;CUIL;Turno;Sector");
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        AD.Write(DR.GetInt32(0));   
                        AD.Write(";");
                        AD.Write(DR.GetString(1));  
                        AD.Write(";");
                        AD.Write(DR.GetString(2));  
                        AD.Write(";");
                        AD.Write(DR.GetString(3));  
                        AD.Write(";");
                        AD.Write(DR.GetString(4));  
                        AD.Write(";");
                        AD.WriteLine(DR.GetString(5)); 
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

        public void Agregar()
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                DataTable tabla = DS.Tables[Tabla];
                DataRow fila = tabla.NewRow();

                fila["idEmpleado"] = idEmpleado;
                fila["Nombre"] = nombEmpleado;
                fila["Apellido"] = apelEmpleado;
                fila["CUIL"] = CUIL;
                fila["Turno"] = turno;
                fila["idSector"] = idSector;

                tabla.Rows.Add(fila);
                OleDbCommandBuilder ConciliaCambios = new OleDbCommandBuilder(adaptador);
                adaptador.Update(DS, Tabla);
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        public void Modificar(Int32 idEmpleado, String turno, String sector)
        {
            try
            {
                OleDbConnection conexion = new OleDbConnection(CadenaConexion);
                OleDbCommand comando = new OleDbCommand();
                
               
                conexion.Open();

                Int32 idSector;

                // Revisamos que el sector exista y obtenemos su ID
                String sqlBuscarSector = "SELECT idSector FROM Sector WHERE Nombre = ?";
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = sqlBuscarSector;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("?", sector.ToUpper());

                object resultado = comando.ExecuteScalar();

                if (resultado != null)
                {
                    // Si el sector ya existe, usamos ese ID
                    idSector = Convert.ToInt32(resultado);
                }
                else
                {
                    // Si no existe, generamos uno al azar que no esté en uso
                    Random rnd = new Random();
                    Int32 nuevoId;
                    Boolean idUnico = false;

                    do
                    {
                        nuevoId = rnd.Next(1, 100); // entre 1 y 99

                        String sqlVerificar = "SELECT COUNT(*) FROM Sector WHERE idSector = ?";
                        comando.CommandText = sqlVerificar;
                        comando.Parameters.Clear();
                        comando.Parameters.AddWithValue("?", nuevoId);

                        Int32 existe = Convert.ToInt32(comando.ExecuteScalar());
                        if (existe == 0)
                            idUnico = true;

                    } while (!idUnico);

                    idSector = nuevoId;

                    // Insertamos el nuevo sector
                    String sqlInsertarSector = "INSERT INTO Sector (idSector, Nombre) VALUES (?, ?)";
                    comando.CommandText = sqlInsertarSector;
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("?", idSector);
                    comando.Parameters.AddWithValue("?", sector.ToUpper());
                    comando.ExecuteNonQuery();
                }

                // Actualizamos al empleado
                String sqlUpdate = "UPDATE Empleado SET Turno = ?, idSector = ? WHERE idEmpleado = ?";
                comando.CommandText = sqlUpdate;
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("?", turno.ToUpper());
                comando.Parameters.AddWithValue("?", idSector);
                comando.Parameters.AddWithValue("?", idEmpleado);
                comando.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public DataTable ObtenerListadoEmpleados()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT idEmpleado, Nombre & ' ' & Apellido AS NombreCompleto FROM Empleado";

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
                comando.CommandText = $"SELECT * FROM Empleado WHERE idEmpleado = {id}";

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

        public void Eliminar(Int32 IdEmpleado)
        {
            try
            {
                String sql = "DELETE * FROM Empleado WHERE idEmpleado = " + IdEmpleado.ToString();

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

        public DataTable ObtenerTurnosUnicos()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT DISTINCT Turno FROM Empleado";

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

        public DataTable ObtenerSectores()
        {
            DataTable tablaSectores = new DataTable();

            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandText = "SELECT idSector, Nombre FROM Sector";
                comando.CommandType = CommandType.Text;

                OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
                adaptador.Fill(tablaSectores);

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los sectores: " + ex.Message);
            }

            return tablaSectores;
        }

        public String ObtenerNombreSectorPorId(int idSector)
        {
            String nombre = "";

            OleDbConnection conexion = new OleDbConnection(CadenaConexion);
            OleDbCommand comando = new OleDbCommand();

            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT Nombre FROM Sector WHERE idSector = ?";
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("?", idSector);

            object resultado = comando.ExecuteScalar();

            if (resultado != null)
                nombre = resultado.ToString();

            conexion.Close();

            return nombre;
        }

    }
}
