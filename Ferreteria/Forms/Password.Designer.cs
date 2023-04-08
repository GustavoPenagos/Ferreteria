namespace Ferreteria.Forms
{
    partial class Password
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.passwaord = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Maroon;
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 117);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 47);
            this.panel3.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.button2.Location = new System.Drawing.Point(31, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 36);
            this.button2.TabIndex = 2;
            this.button2.Text = "Aceptar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.button1.Location = new System.Drawing.Point(156, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 10);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(75, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Contraseña:";
            // 
            // passwaord
            // 
            this.passwaord.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.passwaord.Location = new System.Drawing.Point(59, 66);
            this.passwaord.Name = "passwaord";
            this.passwaord.Size = new System.Drawing.Size(168, 32);
            this.passwaord.TabIndex = 1;
            this.passwaord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.passwaord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwaord_KeyPress);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label.Location = new System.Drawing.Point(12, 13);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(28, 26);
            this.label.TabIndex = 8;
            this.label.Text = "C";
            this.label.Visible = false;
            // 
            // Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(301, 164);
            this.Controls.Add(this.label);
            this.Controls.Add(this.passwaord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password";
            this.Load += new System.EventHandler(this.Password_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwaord;
        private System.Windows.Forms.Label label;
    }
}