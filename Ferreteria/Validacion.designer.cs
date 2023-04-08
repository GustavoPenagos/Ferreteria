namespace Tienda
{
    partial class Validacion
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
            this.validarPass = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // validarPass
            // 
            this.validarPass.AutoSize = true;
            this.validarPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.validarPass.Location = new System.Drawing.Point(464, 102);
            this.validarPass.Name = "validarPass";
            this.validarPass.Size = new System.Drawing.Size(140, 60);
            this.validarPass.TabIndex = 5;
            this.validarPass.Text = "Validar";
            this.validarPass.UseVisualStyleBackColor = true;
            this.validarPass.Click += new System.EventHandler(this.validarPass_Click);
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.password.Location = new System.Drawing.Point(176, 117);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(242, 30);
            this.password.TabIndex = 4;
            this.password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.password_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(67, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Conseña";
            // 
            // Validacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.validarPass);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Validacion";
            this.Text = "Validacion";
            this.Load += new System.EventHandler(this.Validacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button validarPass;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
    }
}