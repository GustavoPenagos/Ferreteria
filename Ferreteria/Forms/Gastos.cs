using System;
using System.Windows.Forms;

namespace Tienda.Forms
{
    public partial class Gastos : Form
    {
        public Gastos()
        {
            InitializeComponent();
            OpenFrom(new Listas.ListaGastos());
        }
        private void OpenFrom(object fromhijo)
        {
            if (this.panel4.Controls.Count > 0)
            {
                this.panel4.Controls.RemoveAt(0);
            }
            Form fh = fromhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel4.Controls.Add(fh);
            this.panel4.Tag = fh;
            fh.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroGastos());
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroGastos());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaGastos());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaGastos());
        }
    }
}
