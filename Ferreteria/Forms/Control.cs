using Ferreteria.Listas;
using System;
using System.Windows.Forms;

namespace Tienda.Forms
{
    public partial class Control : Form
    {
        public Control()
        {
            InitializeComponent();
            OpenFrom(new Listas.ControlCaja());
        }
        private void OpenFrom(object fromhijo)
        {
            if (this.panel6.Controls.Count > 0)
            {
                this.panel6.Controls.RemoveAt(0);
            }
            Form fh = fromhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel6.Controls.Add(fh);
            this.panel6.Tag = fh;
            fh.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaVentas());
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaVentas());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ControlFacturas());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ControlFacturas());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ControlCaja());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ControlCaja());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFrom(new BalanceUtilidad());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFrom(new BalanceCompra());
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            OpenFrom(new BalanceUtilidad());
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OpenFrom(new BalanceCompra());
        }
    }
}
