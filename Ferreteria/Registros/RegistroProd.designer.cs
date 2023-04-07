namespace Tienda.Registros
{
    partial class RegistroProd
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.idProd = new System.Windows.Forms.TextBox();
            this.nomProd = new System.Windows.Forms.TextBox();
            this.precioProd = new System.Windows.Forms.TextBox();
            this.marcaProd = new System.Windows.Forms.TextBox();
            this.utilidad = new System.Windows.Forms.TextBox();
            this.unidProd = new System.Windows.Forms.ComboBox();
            this.guardarProd = new System.Windows.Forms.Button();
            this.precioFinal = new System.Windows.Forms.TextBox();
            this.txbCantidad = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label7.Location = new System.Drawing.Point(374, 430);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 25);
            this.label7.TabIndex = 20;
            this.label7.Text = "Utilidad:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label6.Location = new System.Drawing.Point(383, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 25);
            this.label6.TabIndex = 19;
            this.label6.Text = "Marca:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label4.Location = new System.Drawing.Point(293, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 25);
            this.label4.TabIndex = 18;
            this.label4.Text = "Unidad Producto:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(273, 331);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = "Precio de Producto:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(259, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "Nombre de Producto:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(339, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Id Producto:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1244, 100);
            this.panel1.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1244, 100);
            this.label5.TabIndex = 2;
            this.label5.Text = "Registro de producto";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // idProd
            // 
            this.idProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idProd.Location = new System.Drawing.Point(482, 164);
            this.idProd.Name = "idProd";
            this.idProd.Size = new System.Drawing.Size(329, 30);
            this.idProd.TabIndex = 1;
            this.idProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.idProd_KeyPress_1);
            // 
            // nomProd
            // 
            this.nomProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomProd.Location = new System.Drawing.Point(482, 217);
            this.nomProd.MaxLength = 50;
            this.nomProd.Name = "nomProd";
            this.nomProd.Size = new System.Drawing.Size(329, 30);
            this.nomProd.TabIndex = 2;
            this.nomProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // precioProd
            // 
            this.precioProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.precioProd.Location = new System.Drawing.Point(482, 326);
            this.precioProd.Name = "precioProd";
            this.precioProd.Size = new System.Drawing.Size(329, 30);
            this.precioProd.TabIndex = 4;
            this.precioProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.precioProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.idProd_KeyPress_1);
            // 
            // marcaProd
            // 
            this.marcaProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marcaProd.Location = new System.Drawing.Point(482, 375);
            this.marcaProd.Name = "marcaProd";
            this.marcaProd.Size = new System.Drawing.Size(329, 30);
            this.marcaProd.TabIndex = 5;
            this.marcaProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // utilidad
            // 
            this.utilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.utilidad.Location = new System.Drawing.Point(482, 425);
            this.utilidad.Name = "utilidad";
            this.utilidad.Size = new System.Drawing.Size(329, 30);
            this.utilidad.TabIndex = 6;
            this.utilidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.utilidad.TextChanged += new System.EventHandler(this.utilidad_TextChanged);
            this.utilidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.idProd_KeyPress);
            // 
            // unidProd
            // 
            this.unidProd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unidProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unidProd.FormattingEnabled = true;
            this.unidProd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.unidProd.Location = new System.Drawing.Point(482, 271);
            this.unidProd.Name = "unidProd";
            this.unidProd.Size = new System.Drawing.Size(329, 33);
            this.unidProd.TabIndex = 3;
            // 
            // guardarProd
            // 
            this.guardarProd.AutoSize = true;
            this.guardarProd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guardarProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardarProd.Location = new System.Drawing.Point(674, 538);
            this.guardarProd.Name = "guardarProd";
            this.guardarProd.Size = new System.Drawing.Size(137, 67);
            this.guardarProd.TabIndex = 8;
            this.guardarProd.Text = "Guardar";
            this.guardarProd.UseVisualStyleBackColor = true;
            this.guardarProd.Click += new System.EventHandler(this.guardarProd_Click);
            // 
            // precioFinal
            // 
            this.precioFinal.Cursor = System.Windows.Forms.Cursors.No;
            this.precioFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.precioFinal.Location = new System.Drawing.Point(833, 427);
            this.precioFinal.Name = "precioFinal";
            this.precioFinal.ReadOnly = true;
            this.precioFinal.Size = new System.Drawing.Size(185, 30);
            this.precioFinal.TabIndex = 22;
            this.precioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txbCantidad
            // 
            this.txbCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbCantidad.Location = new System.Drawing.Point(482, 471);
            this.txbCantidad.Name = "txbCantidad";
            this.txbCantidad.Size = new System.Drawing.Size(329, 30);
            this.txbCantidad.TabIndex = 7;
            this.txbCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txbCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.idProd_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label8.Location = new System.Drawing.Point(359, 476);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 25);
            this.label8.TabIndex = 24;
            this.label8.Text = "Cantidad:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Cursor = System.Windows.Forms.Cursors.No;
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.Location = new System.Drawing.Point(833, 473);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.ReadOnly = true;
            this.txtCantidad.Size = new System.Drawing.Size(185, 30);
            this.txtCantidad.TabIndex = 25;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RegistroProd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 737);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.txbCantidad);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.precioFinal);
            this.Controls.Add(this.guardarProd);
            this.Controls.Add(this.unidProd);
            this.Controls.Add(this.utilidad);
            this.Controls.Add(this.marcaProd);
            this.Controls.Add(this.precioProd);
            this.Controls.Add(this.nomProd);
            this.Controls.Add(this.idProd);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistroProd";
            this.Text = "RegistroProd";
            this.Load += new System.EventHandler(this.RegistroProd_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox idProd;
        private System.Windows.Forms.TextBox nomProd;
        private System.Windows.Forms.TextBox precioProd;
        private System.Windows.Forms.TextBox marcaProd;
        private System.Windows.Forms.TextBox utilidad;
        private System.Windows.Forms.ComboBox unidProd;
        private System.Windows.Forms.Button guardarProd;
        private System.Windows.Forms.TextBox precioFinal;
        private System.Windows.Forms.TextBox txbCantidad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCantidad;
    }
}