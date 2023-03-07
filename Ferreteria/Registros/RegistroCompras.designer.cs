namespace Tienda.Registros
{
    partial class RegistroCompras
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
            this.ImgFact = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cBoxEmp = new System.Windows.Forms.ComboBox();
            this.GuardarFact = new System.Windows.Forms.Button();
            this.CargarImg = new System.Windows.Forms.Button();
            this.valorFact = new System.Windows.Forms.TextBox();
            this.numFact = new System.Windows.Forms.TextBox();
            this.dateLimite = new System.Windows.Forms.DateTimePicker();
            this.dateFact = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgFact)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 100);
            this.panel1.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(982, 100);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cartera empresas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ImgFact
            // 
            this.ImgFact.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ImgFact.Cursor = System.Windows.Forms.Cursors.No;
            this.ImgFact.Dock = System.Windows.Forms.DockStyle.Right;
            this.ImgFact.Location = new System.Drawing.Point(604, 100);
            this.ImgFact.Name = "ImgFact";
            this.ImgFact.Size = new System.Drawing.Size(378, 568);
            this.ImgFact.TabIndex = 26;
            this.ImgFact.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cBoxEmp);
            this.panel2.Controls.Add(this.GuardarFact);
            this.panel2.Controls.Add(this.CargarImg);
            this.panel2.Controls.Add(this.valorFact);
            this.panel2.Controls.Add(this.numFact);
            this.panel2.Controls.Add(this.dateLimite);
            this.panel2.Controls.Add(this.dateFact);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(604, 568);
            this.panel2.TabIndex = 27;
            // 
            // cBoxEmp
            // 
            this.cBoxEmp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxEmp.FormattingEnabled = true;
            this.cBoxEmp.Location = new System.Drawing.Point(204, 266);
            this.cBoxEmp.Name = "cBoxEmp";
            this.cBoxEmp.Size = new System.Drawing.Size(350, 33);
            this.cBoxEmp.TabIndex = 5;
            // 
            // GuardarFact
            // 
            this.GuardarFact.AutoSize = true;
            this.GuardarFact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GuardarFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GuardarFact.Location = new System.Drawing.Point(403, 424);
            this.GuardarFact.Name = "GuardarFact";
            this.GuardarFact.Size = new System.Drawing.Size(137, 67);
            this.GuardarFact.TabIndex = 7;
            this.GuardarFact.Text = "Guardar";
            this.GuardarFact.UseVisualStyleBackColor = true;
            this.GuardarFact.Visible = false;
            this.GuardarFact.Click += new System.EventHandler(this.GuardarFact_Click_1);
            // 
            // CargarImg
            // 
            this.CargarImg.AutoSize = true;
            this.CargarImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CargarImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CargarImg.Location = new System.Drawing.Point(403, 331);
            this.CargarImg.Name = "CargarImg";
            this.CargarImg.Size = new System.Drawing.Size(151, 67);
            this.CargarImg.TabIndex = 6;
            this.CargarImg.Text = "Cargar imagen";
            this.CargarImg.UseVisualStyleBackColor = true;
            this.CargarImg.Click += new System.EventHandler(this.CargarImg_Click_1);
            // 
            // valorFact
            // 
            this.valorFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valorFact.Location = new System.Drawing.Point(204, 216);
            this.valorFact.Name = "valorFact";
            this.valorFact.Size = new System.Drawing.Size(350, 30);
            this.valorFact.TabIndex = 4;
            this.valorFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.valorFact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numFact_KeyPress_1);
            // 
            // numFact
            // 
            this.numFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFact.Location = new System.Drawing.Point(204, 160);
            this.numFact.Name = "numFact";
            this.numFact.Size = new System.Drawing.Size(350, 30);
            this.numFact.TabIndex = 3;
            this.numFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numFact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numFact_KeyPress_1);
            // 
            // dateLimite
            // 
            this.dateLimite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateLimite.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLimite.Location = new System.Drawing.Point(204, 106);
            this.dateLimite.Name = "dateLimite";
            this.dateLimite.Size = new System.Drawing.Size(350, 30);
            this.dateLimite.TabIndex = 2;
            // 
            // dateFact
            // 
            this.dateFact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFact.Location = new System.Drawing.Point(204, 51);
            this.dateFact.Name = "dateFact";
            this.dateFact.Size = new System.Drawing.Size(350, 30);
            this.dateFact.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(90, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 25);
            this.label6.TabIndex = 32;
            this.label6.Text = "Empresa:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(64, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 25);
            this.label5.TabIndex = 31;
            this.label5.Text = "Fecha limite:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 25);
            this.label4.TabIndex = 30;
            this.label4.Text = "Fecha de inicio:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 25);
            this.label3.TabIndex = 29;
            this.label3.Text = "Valor factura:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(55, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 25);
            this.label2.TabIndex = 28;
            this.label2.Text = "N° de factura:";
            // 
            // RegistroCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 668);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ImgFact);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistroCompras";
            this.Text = "RegistroCompras";
            this.Load += new System.EventHandler(this.RegistroCompras_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgFact)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox ImgFact;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cBoxEmp;
        private System.Windows.Forms.Button GuardarFact;
        private System.Windows.Forms.Button CargarImg;
        private System.Windows.Forms.TextBox valorFact;
        private System.Windows.Forms.TextBox numFact;
        private System.Windows.Forms.DateTimePicker dateLimite;
        private System.Windows.Forms.DateTimePicker dateFact;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}