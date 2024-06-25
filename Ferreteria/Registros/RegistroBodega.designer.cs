namespace Tienda.Registros
{
    partial class RegistroBodega
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.idProdRegis = new System.Windows.Forms.TextBox();
            this.cantidadProd = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cantProdBod = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rBId = new System.Windows.Forms.RadioButton();
            this.rBNombre = new System.Windows.Forms.RadioButton();
            this.cBNombre = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1103, 100);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1103, 100);
            this.label1.TabIndex = 1;
            this.label1.Text = "Registros en bodega";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(273, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cantidad:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(252, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "ID Producto:";
            // 
            // idProdRegis
            // 
            this.idProdRegis.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.idProdRegis.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.idProdRegis.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idProdRegis.Location = new System.Drawing.Point(404, 203);
            this.idProdRegis.Name = "idProdRegis";
            this.idProdRegis.Size = new System.Drawing.Size(289, 30);
            this.idProdRegis.TabIndex = 1;
            this.idProdRegis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idProdRegis.TextChanged += new System.EventHandler(this.idProdRegis_TextChanged);
            this.idProdRegis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.idProdRegis_KeyPress);
            // 
            // cantidadProd
            // 
            this.cantidadProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadProd.Location = new System.Drawing.Point(404, 275);
            this.cantidadProd.Name = "cantidadProd";
            this.cantidadProd.Size = new System.Drawing.Size(289, 30);
            this.cantidadProd.TabIndex = 2;
            this.cantidadProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cantidadProd.TextChanged += new System.EventHandler(this.cantidadProd_TextChanged);
            this.cantidadProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.idProdRegis_KeyPress);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button1.Location = new System.Drawing.Point(556, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 67);
            this.button1.TabIndex = 3;
            this.button1.Text = "Registrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cantProdBod
            // 
            this.cantProdBod.BackColor = System.Drawing.Color.White;
            this.cantProdBod.Cursor = System.Windows.Forms.Cursors.No;
            this.cantProdBod.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantProdBod.Location = new System.Drawing.Point(767, 275);
            this.cantProdBod.Name = "cantProdBod";
            this.cantProdBod.ReadOnly = true;
            this.cantProdBod.Size = new System.Drawing.Size(110, 30);
            this.cantProdBod.TabIndex = 4;
            this.cantProdBod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cantProdBod.TextChanged += new System.EventHandler(this.cantProdBod_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(762, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Cantidad actual";
            // 
            // rBId
            // 
            this.rBId.AutoSize = true;
            this.rBId.Checked = true;
            this.rBId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rBId.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.rBId.Location = new System.Drawing.Point(77, 132);
            this.rBId.Name = "rBId";
            this.rBId.Size = new System.Drawing.Size(49, 29);
            this.rBId.TabIndex = 12;
            this.rBId.TabStop = true;
            this.rBId.Text = "ID";
            this.rBId.UseVisualStyleBackColor = true;
            this.rBId.CheckedChanged += new System.EventHandler(this.rBId_CheckedChanged);
            // 
            // rBNombre
            // 
            this.rBNombre.AutoSize = true;
            this.rBNombre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rBNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.rBNombre.Location = new System.Drawing.Point(70, 176);
            this.rBNombre.Name = "rBNombre";
            this.rBNombre.Size = new System.Drawing.Size(99, 29);
            this.rBNombre.TabIndex = 13;
            this.rBNombre.Text = "Nombre";
            this.rBNombre.UseVisualStyleBackColor = true;
            // 
            // cBNombre
            // 
            this.cBNombre.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cBNombre.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cBNombre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cBNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cBNombre.FormattingEnabled = true;
            this.cBNombre.Location = new System.Drawing.Point(404, 203);
            this.cBNombre.Name = "cBNombre";
            this.cBNombre.Size = new System.Drawing.Size(289, 33);
            this.cBNombre.TabIndex = 14;
            // 
            // RegistroBodega
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1103, 549);
            this.Controls.Add(this.cBNombre);
            this.Controls.Add(this.rBNombre);
            this.Controls.Add(this.rBId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cantProdBod);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cantidadProd);
            this.Controls.Add(this.idProdRegis);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistroBodega";
            this.Text = "RegistroBodega";
            this.Load += new System.EventHandler(this.RegistroBodega_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idProdRegis;
        private System.Windows.Forms.TextBox cantidadProd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox cantProdBod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rBId;
        private System.Windows.Forms.RadioButton rBNombre;
        private System.Windows.Forms.ComboBox cBNombre;
    }
}