namespace Ferreteria.Forms
{
    partial class Vendedor
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
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.idText = new System.Windows.Forms.TextBox();
            this.msgLabel = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Maroon;
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 122);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 44);
            this.panel3.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.button1.Location = new System.Drawing.Point(141, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "Deshabilitar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.button2.Location = new System.Drawing.Point(21, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 38);
            this.button2.TabIndex = 2;
            this.button2.Text = "Habilitar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.label1.Location = new System.Drawing.Point(26, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Documento del vendedor:";
            // 
            // idText
            // 
            this.idText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.idText.Location = new System.Drawing.Point(31, 55);
            this.idText.Name = "idText";
            this.idText.Size = new System.Drawing.Size(223, 32);
            this.idText.TabIndex = 1;
            this.idText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.idText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.idText_KeyPress);
            // 
            // msgLabel
            // 
            this.msgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.msgLabel.Location = new System.Drawing.Point(0, 90);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(301, 28);
            this.msgLabel.TabIndex = 9;
            this.msgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Vendedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(301, 166);
            this.Controls.Add(this.msgLabel);
            this.Controls.Add(this.idText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Vendedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vendedor";
            this.Load += new System.EventHandler(this.Vendedor_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label msgLabel;
    }
}