namespace Tienda.Registros
{
    partial class RegistroGastos
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
            this.dineroGasto = new System.Windows.Forms.TextBox();
            this.descriGasto = new System.Windows.Forms.TextBox();
            this.guardarRegistro = new System.Windows.Forms.Button();
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
            this.panel1.Size = new System.Drawing.Size(967, 100);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(967, 100);
            this.label1.TabIndex = 1;
            this.label1.Text = "Registro de gastos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(229, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Costo del gasto:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(182, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Descripcion del gasto:";
            // 
            // dineroGasto
            // 
            this.dineroGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dineroGasto.Location = new System.Drawing.Point(392, 173);
            this.dineroGasto.Name = "dineroGasto";
            this.dineroGasto.Size = new System.Drawing.Size(300, 30);
            this.dineroGasto.TabIndex = 1;
            this.dineroGasto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dineroGasto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dineroGasto_KeyPress);
            // 
            // descriGasto
            // 
            this.descriGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriGasto.Location = new System.Drawing.Point(392, 269);
            this.descriGasto.Multiline = true;
            this.descriGasto.Name = "descriGasto";
            this.descriGasto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriGasto.Size = new System.Drawing.Size(433, 168);
            this.descriGasto.TabIndex = 2;
            this.descriGasto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.descriGasto_KeyPress);
            // 
            // guardarRegistro
            // 
            this.guardarRegistro.AutoSize = true;
            this.guardarRegistro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guardarRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardarRegistro.Location = new System.Drawing.Point(688, 477);
            this.guardarRegistro.Name = "guardarRegistro";
            this.guardarRegistro.Size = new System.Drawing.Size(137, 67);
            this.guardarRegistro.TabIndex = 3;
            this.guardarRegistro.Text = "Guardar";
            this.guardarRegistro.UseVisualStyleBackColor = true;
            this.guardarRegistro.Click += new System.EventHandler(this.guardarRegistro_Click);
            // 
            // RegistroGastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(967, 620);
            this.Controls.Add(this.guardarRegistro);
            this.Controls.Add(this.descriGasto);
            this.Controls.Add(this.dineroGasto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistroGastos";
            this.Text = "RegistroGastos";
            this.Load += new System.EventHandler(this.RegistroGastos_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dineroGasto;
        private System.Windows.Forms.TextBox descriGasto;
        private System.Windows.Forms.Button guardarRegistro;
    }
}