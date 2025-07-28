using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace pry_TrabajoIntegradorPP1
{
    public partial class frmVentas : Form
    {
        public frmVentas()
        {
            InitializeComponent();
        }

        private Int32 filaSeleccionadaModificar = -1;
        private Boolean enModoModificacion = false;
        private String ticketTexto = "";

        private void frmVentas_Load(object sender, EventArgs e)
        {
            //Cargar cmb de Clientes
            clsClientes c = new clsClientes();
            cmbCliente.DisplayMember = "Nombre";
            cmbCliente.ValueMember = "idCliente";
            cmbCliente.DataSource = c.ObtenerListadoClientes();
            cmbCliente.SelectedIndex = -1;

            // Cargar ComboBox de vendedores
            clsEmpleado v = new clsEmpleado();
            cmbVendedor.DisplayMember = "NombreCompleto";
            cmbVendedor.ValueMember = "idEmpleado";
            cmbVendedor.DataSource = v.ObtenerListadoEmpleados();
            cmbVendedor.SelectedIndex = -1;

            //Buscar productos
            clsProductos p = new clsProductos();
            txtBuscarProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBuscarProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBuscarProducto.AutoCompleteCustomSource = p.ObtenerAutoCompletarProductos();
        }

        private void ValidarAgregar()
        {
            btnAgregar.Enabled = !string.IsNullOrWhiteSpace(txtBuscarProducto.Text) && nudCantidad.Value > 0;
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            ValidarAgregar();
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            ValidarAgregar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            String producto = txtBuscarProducto.Text.Trim();
            Int32 cantidad = (Int32)nudCantidad.Value;

            if (String.IsNullOrEmpty(producto) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese un producto y una cantidad válida.");
                return;
            }

            clsProductos objProd = new clsProductos();
            DataTable tabla = objProd.ObtenerListadoProductos();

            DataRow[] filas;

            // Buscar por ID o por nombre
            if (Int32.TryParse(producto, out Int32 idProducto))
            {
                filas = tabla.Select("idProducto = " + idProducto);
            }
            else
            {
                String nombreEscapado = producto.Replace("'", "''");
                filas = tabla.Select($"Nombre = '{nombreEscapado}'");
            }

            if (filas.Length == 0)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            // Agregar a la grilla
            DataRow fila = filas[0];
            Int32 idProd = Convert.ToInt32(fila["idProducto"]);
            String nombre = fila["Nombre"].ToString();
            Decimal precioUnitario = Convert.ToDecimal(fila["PrecioVenta"]);
            Decimal total = precioUnitario * cantidad;

            dgvVenta.Rows.Add(idProd, nombre, precioUnitario.ToString("C"), cantidad, total.ToString("C"));

            // Limpiar campos
            txtBuscarProducto.Clear();
            nudCantidad.Value = 0;
            btnAgregar.Enabled = false;
            btnQuitar.Enabled = true;

            CalcularTotalVenta();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvVenta.SelectedRows.Count > 0)
            {
                // Eliminar la fila seleccionada
                dgvVenta.Rows.RemoveAt(dgvVenta.SelectedRows[0].Index);

                // Deshabilitar el botón si no quedan filas
                btnQuitar.Enabled = dgvVenta.Rows.Count > 0;

                // Opcional: Recalcular el total de la venta
                CalcularTotalVenta();
            }
        }



        //Calcular TOTAL
        private Decimal CalcularTotalVenta()
        {
            Decimal totalVenta = 0;

            // Sumar todos los subtotales de las filas
            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (fila.Cells[4].Value != null) // Verificar que no sea nulo
                {
                    String valorTotal = fila.Cells[4].Value.ToString();
                    Decimal subtotal = Convert.ToDecimal(valorTotal.Replace("$", "").Trim());
                    totalVenta += subtotal;
                }
            }

            // Mostrar el total en un Label o TextBox (ejemplo)
            lblTotalSinDescuento.Text = totalVenta.ToString("C");

            // Actualizar también el total con descuento si hay un método de pago seleccionado
            if (rdbEfectivo.Checked || rdbTransferencia.Checked || rdbDebito.Checked || rdbCredito.Checked)
            {
                CalcularTotalConDescuento();
            }
            return totalVenta;
        }

        private Decimal CalcularTotalConDescuento()
        {
            if (lblTotalSinDescuento.Text == "$0,00" || string.IsNullOrEmpty(lblTotalSinDescuento.Text))
            {
                lblTotalConDescuento.Text = "$0,00";
                return 0m;
            }

            Decimal totalSinDescuento = Convert.ToDecimal(lblTotalSinDescuento.Text.Replace("$", "").Trim());
            Decimal descuento = 0;
            Decimal totalConDescuento = totalSinDescuento;

            // Verificar si es cliente mayorista (idCliente >= 2)
            Boolean esMayorista = cmbCliente.SelectedValue != null &&
                              cmbCliente.SelectedValue is Int32 idCliente &&
                              idCliente >= 2;

            // Aplicar descuentos según método de pago
            if (rdbEfectivo.Checked || rdbTransferencia.Checked)
            {
                if (esMayorista)
                {
                    descuento = totalSinDescuento * 0.40m; // 40% descuento para mayoristas
                }
                else
                {
                    descuento = totalSinDescuento * 0.20m; // 20% descuento para consumidor final
                }
            }
            // Débito y Crédito no tienen descuento
            else if (rdbDebito.Checked || rdbCredito.Checked)
            {
                descuento = 0;
            }

            totalConDescuento = totalSinDescuento - descuento;
            lblTotalConDescuento.Text = totalConDescuento.ToString("C");
            return totalConDescuento;
        }

        private void rdbEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbEfectivo.Checked)
                CalcularTotalConDescuento();
        }

        private void rdbTransferencia_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTransferencia.Checked)
                CalcularTotalConDescuento();
        }

        private void rdbDebito_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDebito.Checked)
                CalcularTotalConDescuento();
        }

        private void rdbCredito_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCredito.Checked)
                CalcularTotalConDescuento();
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCliente.SelectedValue != null &&
        (rdbEfectivo.Checked || rdbTransferencia.Checked || rdbDebito.Checked || rdbCredito.Checked))
            {
                CalcularTotalConDescuento();
            }
        }

        private void btnCargarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (cmbCliente.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbVendedor.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un vendedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvVenta.Rows.Count == 0 || dgvVenta.Rows[0].IsNewRow)
                {
                    MessageBox.Show("Debe agregar al menos un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Int32 idMetodoPago = ObtenerIdMetodoPago();
                if (idMetodoPago == 0)
                {
                    MessageBox.Show("Debe seleccionar un método de pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Decimal totalVenta, totalConDescuento;

                if (!Decimal.TryParse(lblTotalSinDescuento.Text,
                                     NumberStyles.Currency,
                                     CultureInfo.CurrentCulture,
                                     out totalVenta))
                {
                    MessageBox.Show("El formato del total es incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Decimal.TryParse(lblTotalConDescuento.Text,
                                     NumberStyles.Currency,
                                     CultureInfo.CurrentCulture,
                                     out totalConDescuento))
                {
                    MessageBox.Show("El formato del total con descuento es incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Crear objeto venta con los valores ya validados
                clsVentas venta = new clsVentas
                {
                    Fecha = DateTime.Now,
                    idEmpleado = Convert.ToInt32(cmbVendedor.SelectedValue),
                    idCliente = Convert.ToInt32(cmbCliente.SelectedValue),
                    idMetodoPago = idMetodoPago,

                    total = totalVenta,
                    totalDescuento = totalConDescuento,

                    Vendedor = cmbVendedor.Text,
                    Cliente = cmbCliente.Text
                };

                // Recorrer grilla
                foreach (DataGridViewRow fila in dgvVenta.Rows)
                {
                    if (fila.IsNewRow || fila.Cells[0].Value == null) continue;

                    Int32 idProd = Convert.ToInt32(fila.Cells[0].Value);
                    String nombre = fila.Cells[1].Value.ToString();

                    // El precio viene con formato $x.xxx,xx o similar, se parsea con la cultura local
                    Decimal precioUnitario = Decimal.Parse(fila.Cells[2].Value.ToString(),
                                                           NumberStyles.Currency,
                                                           CultureInfo.CurrentCulture);

                    Int32 cantidad = Convert.ToInt32(fila.Cells[3].Value);

                    clsDetallesVentas detalle = new clsDetallesVentas
                    {
                        idProducto = idProd,
                        nombreProducto = nombre,
                        precioUnitario = precioUnitario,
                        cantidad = cantidad
                    };

                    venta.Detalles.Add(detalle);
                }

                // Guardar venta
                venta.Agregar();

                MessageBox.Show("Venta registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Int32 ObtenerIdMetodoPago()
        {
            if (rdbEfectivo.Checked) return 1;       // Asumiendo que 1 es el id para Efectivo
            if (rdbTransferencia.Checked) return 2;  // 2 para Transferencia
            if (rdbDebito.Checked) return 3;         // 3 para Débito
            if (rdbCredito.Checked) return 4;        // 4 para Crédito

            return 0; // o lanzar excepción, o manejar caso sin selección
        }

        private void ValidarBotones()
        {
            btnCargarVenta.Enabled = dgvVenta.Rows.Count > 1;
            btnLimpiar.Enabled = dgvVenta.Rows.Count > 1;
            btnGuardar.Enabled = dgvVenta.Rows.Count > 1;
        }


        private void dgvVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvVenta_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ValidarBotones();
        }

        private void dgvVenta_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ValidarBotones();
        }




        private void LimpiarFormulario()
        {
            dgvVenta.Rows.Clear();
            cmbCliente.SelectedIndex = -1;
            cmbVendedor.SelectedIndex = -1;
            txtBuscarProducto.Clear();
            nudCantidad.Value = 0;
            lblTotalSinDescuento.Text = "$0.00";
            lblTotalConDescuento.Text = "$0.00";

            // Desmarcar todos los RadioButtons
            rdbEfectivo.Checked = false;
            rdbTransferencia.Checked = false;
            rdbDebito.Checked = false;
            rdbCredito.Checked = false;

            btnAgregar.Enabled = false;
            btnQuitar.Enabled = false;
            btnCargarVenta.Enabled = false;
        }
       
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            clsVentas v = new clsVentas();
            v.Fecha = DateTime.Now;
            v.Vendedor = cmbVendedor.Text;
            v.Cliente = cmbCliente.Text;
            v.total = CalcularTotalVenta();
            v.totalDescuento = CalcularTotalConDescuento();
            v.Detalles = ObtenerDetallesDesdeGrilla();

            ticketTexto = v.GenerarTicket(); // Genera el string con el formato de ticket

            // Configurar el documento a imprimir
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += printDocument1_PrintPage;

            PrintPreviewDialog vistaPrevia = new PrintPreviewDialog
            {
                Document = printDocument1,
                Width = 800,
                Height = 600
            };

            try
            {
                vistaPrevia.ShowDialog(); // Vista previa de impresión
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar mostrar vista previa: " + ex.Message);
            }
        }

        private List<clsDetallesVentas> ObtenerDetallesDesdeGrilla()
        {
            List<clsDetallesVentas> lista = new List<clsDetallesVentas>();
            Int32 colNombre = 1; 
            Int32 colPrecio = 2; 
            Int32 colCantidad = 3; 

            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (fila.IsNewRow || fila.Cells[colNombre].Value == null)
                    continue;

                try
                {
                    clsDetallesVentas detalle = new clsDetallesVentas
                    {
                        nombreProducto = fila.Cells[colNombre].Value.ToString(),
                        precioUnitario = Convert.ToDecimal(fila.Cells[colPrecio].Value.ToString().Replace("$", "")),
                        cantidad = Convert.ToInt32(fila.Cells[colCantidad].Value)
                    };

                    lista.Add(detalle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar fila: {ex.Message}");
                    continue;
                }
            }

            return lista;

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font fuente = new Font("Courier New", 10); // Fuente monoespaciada
            float y = 20;
            float leftMargin = e.MarginBounds.Left;

            StringReader lector = new StringReader(ticketTexto);
            string linea;

            while ((linea = lector.ReadLine()) != null)
            {
                e.Graphics.DrawString(linea, fuente, Brushes.Black, leftMargin, y);
                y += fuente.GetHeight(e.Graphics);
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            frmDetalleVentas ventana = new frmDetalleVentas();
            ventana.ShowDialog();
        }
    }
}
