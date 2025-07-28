using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_TrabajoIntegradorPP1
{
    internal class clsAsistencia
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BBDDMagnolia.mdb";
        private String Tabla = "Asistencia";

        public Int32 idAsistencia;
        public Int32 idEmpleado;
        public DateTime fecha;
        public String turno;
        public DateTime horaEntrada;
        public DateTime horaSalida;

        public void CargarAsistenciaPorEmpleado(int idEmpleado, string nombreEmpleado, DateTime desde, DateTime hasta, ListView lst)
        {
            lst.Items.Clear();
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
                {
                    String sql = @"SELECT Fecha, Turno, HoraEntrada, HoraSalida 
                         FROM Asistencia 
                         WHERE idEmpleado = @idEmpleado 
                         AND Fecha BETWEEN @desde AND @hasta
                         ORDER BY Fecha ASC, IIF(Turno = 'Mañana', 0, 1), HoraEntrada";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        cmd.Parameters.AddWithValue("@desde", desde);
                        cmd.Parameters.AddWithValue("@hasta", hasta);

                        conexion.Open();
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(nombreEmpleado);

                                // Fecha
                                item.SubItems.Add(reader.GetDateTime(0).ToString("dd/MM/yyyy"));

                                // Turno
                                item.SubItems.Add(reader.IsDBNull(1) ? "N/A" : reader.GetString(1));

                                // Hora Entrada
                                item.SubItems.Add(reader.IsDBNull(2) ? "N/A" :
                                                reader.GetDateTime(2).ToString("HH:mm"));

                                // Hora Salida
                                item.SubItems.Add(reader.IsDBNull(3) ? "N/A" :
                                                reader.GetDateTime(3).ToString("HH:mm"));

                                lst.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar asistencia:\n{ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
