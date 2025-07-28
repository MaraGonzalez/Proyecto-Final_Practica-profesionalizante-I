using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_TrabajoIntegradorPP1
{
    public partial class frmAsistencia : Form
    {
        public frmAsistencia()
        {
            InitializeComponent(); 
        }
        private readonly String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BBDDMagnolia.mdb";
        private void frmAsistencia_Load(object sender, EventArgs e)
        {
            clsEmpleado emp = new clsEmpleado();

            //Configuración del primer Cmb (Entrada/Salida)
            cmbNombreyApellido.DisplayMember = "NombreCompleto";
            cmbNombreyApellido.ValueMember = "idEmpleado";
            cmbNombreyApellido.DataSource = emp.ObtenerListadoEmpleados();
            cmbNombreyApellido.SelectedIndex = -1;

            //Configuración del cmbTurnos
            cmbTurno.DataSource = emp.ObtenerTurnosUnicos();
            cmbTurno.DisplayMember = "Turno";
            cmbTurno.ValueMember = "Turno";
            cmbTurno.SelectedIndex = -1;

            //Configuración del segundo cmb(Búsqueda)
            cmbNombreApellido2.DisplayMember = "NombreCompleto";
            cmbNombreApellido2.ValueMember = "idEmpleado";
            cmbNombreApellido2.DataSource = emp.ObtenerListadoEmpleados();
            cmbNombreApellido2.SelectedIndex = -1;

            // Configuración de fechas por defecto para búsqueda
            dptDesde.Value = DateTime.Today.AddDays(-7);
            dptHasta.Value = DateTime.Today;
        }

        private void btnEntrada_Click(object sender, EventArgs e)
        {
            if (!ValidarSeleccionEntrada()) return;

            Int32 idEmpleado = Convert.ToInt32(cmbNombreyApellido.SelectedValue);
            String turno = cmbTurno.Text;
            DateTime ahora = DateTime.Now;

            try
            {
                using (var conexion = new OleDbConnection(CadenaConexion))
                {
                    conexion.Open();

                    String sql = @"INSERT INTO Asistencia (idEmpleado, Fecha, Turno, HoraEntrada) VALUES (@idEmpleado, @fecha, @turno, @horaEntrada)";


                    using (OleDbCommand cmd = new OleDbCommand(sql, conexion))
                    {
                        // 1. idEmpleado (Integer)
                        cmd.Parameters.Add("@idEmpleado", OleDbType.Integer).Value = idEmpleado;

                        // 2. Fecha (SOLO fecha en formato ISO yyyy-MM-dd)
                        cmd.Parameters.Add("@fecha", OleDbType.DBDate).Value = ahora.Date.ToString("yyyy-MM-dd");

                        // 3. Turno (Texto, máximo 50 caracteres)
                        cmd.Parameters.Add("@turno", OleDbType.VarChar, 50).Value = turno;

                        // 4. HoraEntrada (Fecha y hora en formato ISO)
                        cmd.Parameters.Add("@horaEntrada", OleDbType.DBTimeStamp).Value = ahora.ToString("yyyy-MM-dd HH:mm:ss");

                        Int32 resultado = cmd.ExecuteNonQuery();

                        if (resultado > 0)
                        {
                            MessageBox.Show("✅ Registro exitoso\n" +
                                          $"ID: {idEmpleado}\n" +
                                          $"Fecha: {ahora.Date:dd/MM/yyyy}\n" +
                                          $"Turno: {turno}\n" +
                                          $"Hora: {ahora:HH:mm:ss}");
                        }
                    }
                }
            }
            catch (OleDbException ex)
            {
                // Mensaje de error mejorado
                string errorDetails = $"Error al insertar en Access:\n\n" +
                                    $"Código: {ex.ErrorCode}\n" +
                                    $"Mensaje: {ex.Message}\n\n" +
                                    "Solución:\n" +
                                    "1. Verificar tipos de datos en la tabla\n" +
                                    "2. Confirmar formato de fechas\n" +
                                    "3. Revisar longitud del campo Turno";

                MessageBox.Show(errorDetails, "Error de Base de Datos",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Limpiar ComboBox
            cmbNombreyApellido.SelectedIndex = -1;
            cmbTurno.SelectedIndex = -1;

            // Deshabilitar botones nuevamente
            btnEntrada.Enabled = false;
            btnSalida.Enabled = false;
        }
        private bool ValidarSeleccionEntrada()
        {
            if (cmbNombreyApellido.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un empleado", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbTurno.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un turno", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            if (!ValidarSeleccionEntrada()) return;

            Int32 idEmpleado = Convert.ToInt32(cmbNombreyApellido.SelectedValue);
            String turno = cmbTurno.Text;
            DateTime ahora = DateTime.Now;

            try
            {
                using (OleDbConnection conexion = new OleDbConnection(CadenaConexion))
                {
                    conexion.Open();

                    String sql = @"UPDATE Asistencia 
                         SET HoraSalida = @horaSalida
                         WHERE idEmpleado = @idEmpleado 
                         AND Fecha = @fecha
                         AND HoraSalida IS NULL";  // Solo actualizar si no tiene salida registrada

                    using (OleDbCommand cmd = new OleDbCommand(sql, conexion))
                    {
                        // 1. HoraSalida (DateTime completo)
                        cmd.Parameters.Add("@horaSalida", OleDbType.DBTimeStamp).Value = ahora.ToString("yyyy-MM-dd HH:mm:ss");

                        // 2. idEmpleado (Integer)
                        cmd.Parameters.Add("@idEmpleado", OleDbType.Integer).Value = idEmpleado;

                        // 3. Fecha (SOLO fecha en formato ISO)
                        cmd.Parameters.Add("@fecha", OleDbType.DBDate).Value = ahora.Date.ToString("yyyy-MM-dd");

                        Int32 filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show($"✅ Salida registrada correctamente\n" +
                                          $"Empleado: {idEmpleado}\n" +
                                          $"Fecha: {ahora.Date:dd/MM/yyyy}\n" +
                                          $"Hora: {ahora:HH:mm:ss}",
                                          "Éxito",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Mensaje más descriptivo
                            string mensaje = "No se encontró registro de entrada para:\n";
                            mensaje += $"• Empleado: {idEmpleado}\n";
                            mensaje += $"• Fecha: {ahora.Date:dd/MM/yyyy}\n\n";
                            mensaje += "Posibles causas:\n";
                            mensaje += "1. No registró entrada hoy\n";
                            mensaje += "2. Ya tiene una salida registrada";

                            MessageBox.Show(mensaje, "Registro no encontrado",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (OleDbException ex)
            {
                // Manejo específico para errores de base de datos
                string errorMsg = "Error al registrar salida:\n\n";
                errorMsg += $"Código: {ex.ErrorCode}\n";
                errorMsg += $"Mensaje: {ex.Message}\n\n";

                if (ex.Message.Contains("tipo de datos"))
                {
                    errorMsg += "SOLUCIÓN:\n";
                    errorMsg += "1. Verificar que los tipos coincidan:\n";
                    errorMsg += "   - idEmpleado: Entero\n";
                    errorMsg += "   - Fecha: Solo fecha (sin hora)\n";
                    errorMsg += "   - HoraSalida: Fecha y hora\n";
                    errorMsg += "2. Revisar formato de fechas";
                }

                MessageBox.Show(errorMsg, "Error de Base de Datos",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            // Limpiar ComboBox
            cmbNombreyApellido.SelectedIndex = -1;
            cmbTurno.SelectedIndex = -1;

            // Deshabilitar botones nuevamente
            btnEntrada.Enabled = false;
            btnSalida.Enabled = false;
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbNombreApellido2.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un empleado.");
                return;
            }

            Int32 idEmpleado = Convert.ToInt32(cmbNombreApellido2.SelectedValue);
            String nombreEmpleado = cmbNombreApellido2.Text;
            DateTime desde = dptDesde.Value.Date;
            DateTime hasta = dptHasta.Value.Date;

            // Validación de rango de fechas
            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Configurar las columnas del ListView (solo una vez)
                if (lstAsistencia.Columns.Count == 0)
                {
                    lstAsistencia.View = View.Details;
                    lstAsistencia.Columns.Add("Empleado", 150);
                    lstAsistencia.Columns.Add("Fecha", 100);
                    lstAsistencia.Columns.Add("Turno", 80);
                    lstAsistencia.Columns.Add("Entrada", 80);
                    lstAsistencia.Columns.Add("Salida", 80);
                    lstAsistencia.FullRowSelect = true;
                }

                // Limpiar lista antes de cargar nuevos datos
                lstAsistencia.Items.Clear();

                // Crear instancia y cargar datos
                clsAsistencia asistencia = new clsAsistencia();
                asistencia.CargarAsistenciaPorEmpleado(idEmpleado, nombreEmpleado, desde, hasta, lstAsistencia);

                // Mostrar feedback si no hay resultados
                if (lstAsistencia.Items.Count == 0)
                {
                    MessageBox.Show($"No se encontraron registros para:\n" +
                                  $"Empleado: {cmbNombreApellido2.Text}\n" +
                                  $"Desde: {desde:dd/MM/yyyy}\n" +
                                  $"Hasta: {hasta:dd/MM/yyyy}",
                                  "Información",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar registros: {ex.Message}");
            }
            btnLimpiar.Enabled = true;
        }

        private void VerificarHabilitarBotones()
        {
            if (cmbNombreyApellido.SelectedIndex >= 0 && cmbTurno.SelectedIndex >= 0)
            {
                btnEntrada.Enabled = true;
                btnSalida.Enabled = true;
            }
            else
            {
                btnEntrada.Enabled = false;
                btnSalida.Enabled = false;
            }
        }

        private void cmbNombreyApellido_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificarHabilitarBotones();
        }

        private void cmbNombreApellido2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNombreApellido2.SelectedIndex != -1)
            {
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }
        }

        private void cmbTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificarHabilitarBotones();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbNombreApellido2.SelectedIndex = -1;
            lstAsistencia.Items.Clear();
            btnBuscar.Enabled = false;
        }


    }
}
