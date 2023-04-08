namespace Tienda.Registros
{
    partial class RegistoAbonos
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.buscarFactura = new System.Windows.Forms.Button();
            this.bFCompra = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.insertAbono = new System.Windows.Forms.Button();
            this.totalFact = new System.Windows.Forms.TextBox();
            this.abonoFact = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.imgFactura = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgFactura)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1012, 100);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1012, 100);
            this.label1.TabIndex = 1;
            this.label1.Text = "Registro abonos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.buscarFactura);
            this.panel2.Controls.Add(this.bFCompra);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1012, 100);
            this.panel2.TabIndex = 1;
            // 
            // buscarFactura
            // 
            this.buscarFactura.AutoSize = true;
            this.buscarFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buscarFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscarFactura.Location = new System.Drawing.Point(582, 18);
            this.buscarFactura.Name = "buscarFactura";
            this.buscarFactura.Size = new System.Drawing.Size(137, 67);
            this.buscarFactura.TabIndex = 2;
            this.buscarFactura.Text = "Buscar";
            this.buscarFactura.UseVisualStyleBackColor = true;
            this.buscarFactura.Click += new System.EventHandler(this.buscarFactura_Click);
            // 
            // bFCompra
            // 
            this.bFCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bFCompra.Location = new System.Drawing.Point(253, 36);
            this.bFCompra.Name = "bFCompra";
            this.bFCompra.Size = new System.Drawing.Size(300, 30);
            this.bFCompra.TabIndex = 1;
            this.bFCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bFCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bFCompra_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(56, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Número de factura:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.insertAbono);
            this.panel3.Controls.Add(this.totalFact);
            this.panel3.Controls.Add(this.abonoFact);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(768, 200);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 588);
            this.panel3.TabIndex = 2;
            // 
            // insertAbono
            // 
            this.insertAbono.AutoSize = true;
            this.insertAbono.Cursor = System.Windows.Forms.Cursors.Hand;
            this.insertAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertAbono.Location = new System.Drawing.Point(83, 285);
            this.insertAbono.Name = "insertAbono";
            this.insertAbono.Size = new System.Drawing.Size(137, 67);
            this.insertAbono.TabIndex = 4;
            this.insertAbono.Text = "Abonar";
            this.insertAbono.UseVisualStyleBackColor = true;
            this.insertAbono.Click += new System.EventHandler(this.insertAbono_Click);
            // 
            // totalFact
            // 
            this.totalFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalFact.Location = new System.Drawing.Point(18, 89);
            this.totalFact.Name = "totalFact";
            this.totalFact.Size = new System.Drawing.Size(202, 30);
            this.totalFact.TabIndex = 4;
            this.totalFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // abonoFact
            // 
            this.abonoFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.abonoFact.Location = new System.Drawing.Point(18, 210);
            this.abonoFact.Name = "abonoFact";
            this.abonoFact.Size = new System.Drawing.Size(202, 30);
            this.abonoFact.TabIndex = 3;
            this.abonoFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.abonoFact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bFCompra_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Total:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Abonar:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.imgFactura);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 200);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(768, 588);
            this.panel4.TabIndex = 3;
            // 
            // imgFactura
            // 
            this.imgFactura.BackColor = System.Drawing.Color.White;
            this.imgFactura.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgFactura.Cursor = System.Windows.Forms.Cursors.No;
            this.imgFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgFactura.Location = new System.Drawing.Point(0, 0);
            this.imgFactura.Name = "imgFactura";
            this.imgFactura.Size = new System.Drawing.Size(768, 588);
            this.imgFactura.TabIndex = 0;
            this.imgFactura.TabStop = false;
            // 
            // RegistoAbonos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 788);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistoAbonos";
            this.Text = "RegistoAbonos";
            this.Load += new System.EventHandler(this.RegistoAbonos_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgFactura)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buscarFactura;
        private System.Windows.Forms.TextBox bFCompra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button insertAbono;
        private System.Windows.Forms.TextBox totalFact;
        private System.Windows.Forms.TextBox abonoFact;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox imgFactura;
    }
}