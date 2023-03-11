namespace Ferreteria.Forms
{
    partial class DineroBase
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dinero = new System.Windows.Forms.TextBox();
            this.dineroMoneda = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Maroon;
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 286);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(498, 69);
            this.panel3.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button1.Location = new System.Drawing.Point(151, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 62);
            this.button1.TabIndex = 3;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(498, 23);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F);
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(465, 54);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ingresar dinero base:";
            // 
            // dinero
            // 
            this.dinero.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F);
            this.dinero.Location = new System.Drawing.Point(42, 117);
            this.dinero.MaxLength = 12;
            this.dinero.Name = "dinero";
            this.dinero.Size = new System.Drawing.Size(409, 60);
            this.dinero.TabIndex = 1;
            this.dinero.TextChanged += new System.EventHandler(this.Dinero_TextChanged);
            this.dinero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dinero_KeyPress);
            // 
            // dineroMoneda
            // 
            this.dineroMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F);
            this.dineroMoneda.Location = new System.Drawing.Point(33, 195);
            this.dineroMoneda.Name = "dineroMoneda";
            this.dineroMoneda.Size = new System.Drawing.Size(418, 54);
            this.dineroMoneda.TabIndex = 7;
            this.dineroMoneda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DineroBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 355);
            this.Controls.Add(this.dineroMoneda);
            this.Controls.Add(this.dinero);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DineroBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DineroBase";
            this.Load += new System.EventHandler(this.DineroBase_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dinero;
        private System.Windows.Forms.Label dineroMoneda;
    }
}