namespace pry_TrabajoIntegradorPP1
{
    partial class frmDetalleVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetalleVentas));
            this.lstVentas = new System.Windows.Forms.ListBox();
            this.chtVentas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFactura = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.rdbCliente = new System.Windows.Forms.RadioButton();
            this.cmbVendedor = new System.Windows.Forms.ComboBox();
            this.rdbVendedor = new System.Windows.Forms.RadioButton();
            this.dtpHastaInf = new System.Windows.Forms.DateTimePicker();
            this.dtpDesdeInf = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp = new System.Windows.Forms.Label();
            this.rdbPeriodo = new System.Windows.Forms.RadioButton();
            this.btnGraficar = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chtVentas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstVentas
            // 
            this.lstVentas.FormattingEnabled = true;
            this.lstVentas.ItemHeight = 16;
            this.lstVentas.Location = new System.Drawing.Point(10, 124);
            this.lstVentas.Name = "lstVentas";
            this.lstVentas.Size = new System.Drawing.Size(704, 68);
            this.lstVentas.TabIndex = 0;
            this.lstVentas.SelectedIndexChanged += new System.EventHandler(this.lstVentas_SelectedIndexChanged);
            // 
            // chtVentas
            // 
            chartArea1.Name = "ChartArea1";
            this.chtVentas.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chtVentas.Legends.Add(legend1);
            this.chtVentas.Location = new System.Drawing.Point(19, 30);
            this.chtVentas.Name = "chtVentas";
            this.chtVentas.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chtVentas.Series.Add(series1);
            this.chtVentas.Size = new System.Drawing.Size(698, 413);
            this.chtVentas.TabIndex = 1;
            this.chtVentas.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.dtpHasta);
            this.groupBox1.Controls.Add(this.dtpDesde);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lstVentas);
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.groupBox1.Location = new System.Drawing.Point(10, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(726, 673);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle de ventas";
            // 
            // lblFactura
            // 
            this.lblFactura.AutoSize = true;
            this.lblFactura.Font = new System.Drawing.Font("Courier New", 12F);
            this.lblFactura.Location = new System.Drawing.Point(72, 10);
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.Size = new System.Drawing.Size(0, 18);
            this.lblFactura.TabIndex = 66;
            this.lblFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFactura.Click += new System.EventHandler(this.lblFactura_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.Enabled = false;
            this.btnLimpiar.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(369, 611);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(162, 42);
            this.btnLimpiar.TabIndex = 61;
            this.btnLimpiar.Text = "&Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(542, 611);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(162, 42);
            this.btnGuardar.TabIndex = 60;
            this.btnGuardar.Text = "&Imprimir Comprobante";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnBuscar.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(552, 64);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(162, 42);
            this.btnBuscar.TabIndex = 59;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(436, 27);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(278, 23);
            this.dtpHasta.TabIndex = 44;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(81, 27);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(278, 23);
            this.dtpDesde.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(365, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 42;
            this.label1.Text = "Hasta:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 19);
            this.label9.TabIndex = 41;
            this.label9.Text = "Desde:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbCliente);
            this.groupBox2.Controls.Add(this.rdbCliente);
            this.groupBox2.Controls.Add(this.cmbVendedor);
            this.groupBox2.Controls.Add(this.rdbVendedor);
            this.groupBox2.Controls.Add(this.dtpHastaInf);
            this.groupBox2.Controls.Add(this.dtpDesdeInf);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtp);
            this.groupBox2.Controls.Add(this.rdbPeriodo);
            this.groupBox2.Controls.Add(this.btnGraficar);
            this.groupBox2.Controls.Add(this.chtVentas);
            this.groupBox2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.groupBox2.Location = new System.Drawing.Point(745, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(733, 673);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informe de ventas";
            // 
            // cmbCliente
            // 
            this.cmbCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Location = new System.Drawing.Point(443, 580);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(275, 28);
            this.cmbCliente.TabIndex = 71;
            this.cmbCliente.SelectedIndexChanged += new System.EventHandler(this.cmbCliente_SelectedIndexChanged);
            // 
            // rdbCliente
            // 
            this.rdbCliente.AutoSize = true;
            this.rdbCliente.Location = new System.Drawing.Point(453, 547);
            this.rdbCliente.Name = "rdbCliente";
            this.rdbCliente.Size = new System.Drawing.Size(77, 20);
            this.rdbCliente.TabIndex = 70;
            this.rdbCliente.TabStop = true;
            this.rdbCliente.Text = "Cliente ";
            this.rdbCliente.UseVisualStyleBackColor = true;
            this.rdbCliente.CheckedChanged += new System.EventHandler(this.rdbCliente_CheckedChanged);
            // 
            // cmbVendedor
            // 
            this.cmbVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbVendedor.FormattingEnabled = true;
            this.cmbVendedor.Location = new System.Drawing.Point(443, 495);
            this.cmbVendedor.Name = "cmbVendedor";
            this.cmbVendedor.Size = new System.Drawing.Size(275, 28);
            this.cmbVendedor.TabIndex = 69;
            this.cmbVendedor.SelectedIndexChanged += new System.EventHandler(this.cmbVendedor_SelectedIndexChanged);
            // 
            // rdbVendedor
            // 
            this.rdbVendedor.AutoSize = true;
            this.rdbVendedor.Location = new System.Drawing.Point(453, 461);
            this.rdbVendedor.Name = "rdbVendedor";
            this.rdbVendedor.Size = new System.Drawing.Size(89, 20);
            this.rdbVendedor.TabIndex = 68;
            this.rdbVendedor.TabStop = true;
            this.rdbVendedor.Text = "Vendedor";
            this.rdbVendedor.UseVisualStyleBackColor = true;
            this.rdbVendedor.CheckedChanged += new System.EventHandler(this.rdbVendedor_CheckedChanged);
            // 
            // dtpHastaInf
            // 
            this.dtpHastaInf.Location = new System.Drawing.Point(20, 585);
            this.dtpHastaInf.Name = "dtpHastaInf";
            this.dtpHastaInf.Size = new System.Drawing.Size(278, 23);
            this.dtpHastaInf.TabIndex = 67;
            // 
            // dtpDesdeInf
            // 
            this.dtpDesdeInf.Location = new System.Drawing.Point(20, 517);
            this.dtpDesdeInf.Name = "dtpDesdeInf";
            this.dtpDesdeInf.Size = new System.Drawing.Size(278, 23);
            this.dtpDesdeInf.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(20, 554);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 65;
            this.label2.Text = "Hasta:";
            // 
            // dtp
            // 
            this.dtp.AutoSize = true;
            this.dtp.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.dtp.Location = new System.Drawing.Point(16, 495);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(57, 16);
            this.dtp.TabIndex = 64;
            this.dtp.Text = "Desde:";
            // 
            // rdbPeriodo
            // 
            this.rdbPeriodo.AutoSize = true;
            this.rdbPeriodo.Location = new System.Drawing.Point(30, 461);
            this.rdbPeriodo.Name = "rdbPeriodo";
            this.rdbPeriodo.Size = new System.Drawing.Size(180, 20);
            this.rdbPeriodo.TabIndex = 63;
            this.rdbPeriodo.TabStop = true;
            this.rdbPeriodo.Text = "Fecha - Mes - Periodo";
            this.rdbPeriodo.UseVisualStyleBackColor = true;
            this.rdbPeriodo.CheckedChanged += new System.EventHandler(this.rdbPeriodo_CheckedChanged);
            // 
            // btnGraficar
            // 
            this.btnGraficar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGraficar.Enabled = false;
            this.btnGraficar.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGraficar.ForeColor = System.Drawing.Color.White;
            this.btnGraficar.Location = new System.Drawing.Point(555, 625);
            this.btnGraficar.Name = "btnGraficar";
            this.btnGraficar.Size = new System.Drawing.Size(162, 42);
            this.btnGraficar.TabIndex = 62;
            this.btnGraficar.Text = "&Graficar";
            this.btnGraficar.UseVisualStyleBackColor = false;
            this.btnGraficar.Click += new System.EventHandler(this.btnGraficar_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lblFactura);
            this.panel1.Location = new System.Drawing.Point(6, 198);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 388);
            this.panel1.TabIndex = 67;
            // 
            // frmDetalleVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1490, 703);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDetalleVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle e informe de ventas";
            this.Load += new System.EventHandler(this.frmDetalleVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chtVentas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstVentas;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtVentas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbPeriodo;
        private System.Windows.Forms.Button btnGraficar;
        private System.Windows.Forms.RadioButton rdbVendedor;
        private System.Windows.Forms.DateTimePicker dtpHastaInf;
        private System.Windows.Forms.DateTimePicker dtpDesdeInf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dtp;
        private System.Windows.Forms.RadioButton rdbCliente;
        private System.Windows.Forms.ComboBox cmbVendedor;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblFactura;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Panel panel1;
    }
}