namespace Tienda.Listas
{
    partial class ControlFacturas
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
            this.buscarFact = new System.Windows.Forms.Button();
            this.buscaID = new System.Windows.Forms.TextBox();
            this.selecBus = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.multiFact = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1046, 100);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1046, 100);
            this.label1.TabIndex = 1;
            this.label1.Text = "Control del facturas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buscarFact);
            this.panel2.Controls.Add(this.buscaID);
            this.panel2.Controls.Add(this.selecBus);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1046, 100);
            this.panel2.TabIndex = 1;
            // 
            // buscarFact
            // 
            this.buscarFact.AutoSize = true;
            this.buscarFact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buscarFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscarFact.Location = new System.Drawing.Point(756, 15);
            this.buscarFact.Name = "buscarFact";
            this.buscarFact.Size = new System.Drawing.Size(137, 67);
            this.buscarFact.TabIndex = 2;
            this.buscarFact.Text = "Buscar";
            this.buscarFact.UseVisualStyleBackColor = true;
            this.buscarFact.Click += new System.EventHandler(this.buscarFact_Click);
            // 
            // buscaID
            // 
            this.buscaID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscaID.Location = new System.Drawing.Point(442, 36);
            this.buscaID.Name = "buscaID";
            this.buscaID.Size = new System.Drawing.Size(256, 30);
            this.buscaID.TabIndex = 1;
            this.buscaID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.buscaID_KeyPress);
            // 
            // selecBus
            // 
            this.selecBus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selecBus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selecBus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selecBus.FormattingEnabled = true;
            this.selecBus.Items.AddRange(new object[] {
            "Factura de venta",
            "Factura Remision",
            "Cotizacion"});
            this.selecBus.Location = new System.Drawing.Point(205, 33);
            this.selecBus.Name = "selecBus";
            this.selecBus.Size = new System.Drawing.Size(186, 33);
            this.selecBus.TabIndex = 0;
            this.selecBus.SelectedIndexChanged += new System.EventHandler(this.selecBus_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.webBrowser1);
            this.panel3.Controls.Add(this.multiFact);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 200);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1046, 549);
            this.panel3.TabIndex = 2;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(594, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(452, 549);
            this.webBrowser1.TabIndex = 1;
            // 
            // multiFact
            // 
            this.multiFact.Dock = System.Windows.Forms.DockStyle.Left;
            this.multiFact.Location = new System.Drawing.Point(0, 0);
            this.multiFact.Multiline = true;
            this.multiFact.Name = "multiFact";
            this.multiFact.ReadOnly = true;
            this.multiFact.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.multiFact.Size = new System.Drawing.Size(594, 549);
            this.multiFact.TabIndex = 0;
            // 
            // ControlFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 749);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ControlFacturas";
            this.Text = "ControlFacturas";
            this.Load += new System.EventHandler(this.ControlFacturas_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buscarFact;
        private System.Windows.Forms.TextBox buscaID;
        private System.Windows.Forms.ComboBox selecBus;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox multiFact;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}