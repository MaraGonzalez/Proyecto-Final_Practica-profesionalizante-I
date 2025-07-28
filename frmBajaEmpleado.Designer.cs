namespace pry_TrabajoIntegradorPP1
{
    partial class frmBajaEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBajaEmpleado));
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbNombreyApellido = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNombreApellido = new System.Windows.Forms.Label();
            this.lblSector = new System.Windows.Forms.Label();
            this.lblTurno = new System.Windows.Forms.Label();
            this.lblCuil = new System.Windows.Forms.Label();
            this.lblIdEmpleado = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnBuscar.Enabled = false;
            this.btnBuscar.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(295, 64);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(162, 32);
            this.btnBuscar.TabIndex = 31;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(18, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 19);
            this.label9.TabIndex = 30;
            this.label9.Text = "Nombre y apellido:";
            // 
            // cmbNombreyApellido
            // 
            this.cmbNombreyApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNombreyApellido.FormattingEnabled = true;
            this.cmbNombreyApellido.Location = new System.Drawing.Point(202, 21);
            this.cmbNombreyApellido.Name = "cmbNombreyApellido";
            this.cmbNombreyApellido.Size = new System.Drawing.Size(255, 32);
            this.cmbNombreyApellido.TabIndex = 29;
            this.cmbNombreyApellido.SelectedIndexChanged += new System.EventHandler(this.cmbNombreyApellido_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.lblNombreApellido);
            this.groupBox1.Controls.Add(this.lblSector);
            this.groupBox1.Controls.Add(this.lblTurno);
            this.groupBox1.Controls.Add(this.lblCuil);
            this.groupBox1.Controls.Add(this.lblIdEmpleado);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnEliminar);
            this.groupBox1.Controls.Add(this.lblbl);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label);
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 320);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del empleado";
            // 
            // lblNombreApellido
            // 
            this.lblNombreApellido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreApellido.Location = new System.Drawing.Point(207, 79);
            this.lblNombreApellido.Name = "lblNombreApellido";
            this.lblNombreApellido.Size = new System.Drawing.Size(238, 25);
            this.lblNombreApellido.TabIndex = 31;
            this.lblNombreApellido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSector
            // 
            this.lblSector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSector.Location = new System.Drawing.Point(207, 214);
            this.lblSector.Name = "lblSector";
            this.lblSector.Size = new System.Drawing.Size(238, 25);
            this.lblSector.TabIndex = 30;
            this.lblSector.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTurno
            // 
            this.lblTurno.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTurno.Location = new System.Drawing.Point(207, 169);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(238, 25);
            this.lblTurno.TabIndex = 29;
            this.lblTurno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCuil
            // 
            this.lblCuil.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCuil.Location = new System.Drawing.Point(207, 125);
            this.lblCuil.Name = "lblCuil";
            this.lblCuil.Size = new System.Drawing.Size(238, 23);
            this.lblCuil.TabIndex = 23;
            this.lblCuil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIdEmpleado
            // 
            this.lblIdEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdEmpleado.Location = new System.Drawing.Point(207, 38);
            this.lblIdEmpleado.Name = "lblIdEmpleado";
            this.lblIdEmpleado.Size = new System.Drawing.Size(238, 25);
            this.lblIdEmpleado.TabIndex = 21;
            this.lblIdEmpleado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "Sector:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "Turno:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(283, 265);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(162, 32);
            this.btnEliminar.TabIndex = 9;
            this.btnEliminar.Text = "&Baja";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblbl
            // 
            this.lblbl.AutoSize = true;
            this.lblbl.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbl.Location = new System.Drawing.Point(6, 85);
            this.lblbl.Name = "lblbl";
            this.lblbl.Size = new System.Drawing.Size(173, 19);
            this.lblbl.TabIndex = 5;
            this.lblbl.Text = "Nombre y apellido:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "IdEmpleado:";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(6, 129);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(58, 19);
            this.label.TabIndex = 1;
            this.label.Text = "CUIL:";
            // 
            // frmBajaEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(483, 450);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbNombreyApellido);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBajaEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Baja de empleado";
            this.Load += new System.EventHandler(this.frmBajaEmpleado_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbNombreyApellido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCuil;
        private System.Windows.Forms.Label lblIdEmpleado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label lblNombreApellido;
        private System.Windows.Forms.Label lblSector;
        private System.Windows.Forms.Label lblTurno;
    }
}