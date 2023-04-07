using System;
using System.Windows.Forms;

namespace Tienda.Forms
{
    public partial class Empresa : Form
    {
        public Empresa()
        {
            InitializeComponent();
            OpenFrom(new Listas.ListaFacturas());
        }
        private void OpenFrom(object fromhijo)
        {
            if (this.panel9.Controls.Count > 0)
            {
                this.panel9.Controls.RemoveAt(0);
            }
            Form fh = fromhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel9.Controls.Add(fh);
            this.panel9.Tag = fh;
            fh.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistraEmpresa(""));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistraEmpresa(""));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaEmpresa());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaEmpresa());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroCompras());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroCompras());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistoAbonos());
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistoAbonos());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaAbonos());
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaAbonos());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaFacturas());
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaFacturas());
        }
    }
}
