using System;
using System.Windows.Forms;

namespace Tienda.Forms
{
    public partial class Bodega : Form
    {
        public Bodega()
        {
            InitializeComponent();
            OpenFrom(new Listas.ListaBodega());
        }
        private void OpenFrom(object fromhijo)
        {
            if (this.panel7.Controls.Count > 0)
            {
                this.panel7.Controls.RemoveAt(0);
            }
            Form fh = fromhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel7.Controls.Add(fh);
            this.panel7.Tag = fh;
            fh.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroProd());
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroProd());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaProd());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaProd());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroBodega());
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroBodega());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaBodega());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaBodega());
        }
    }
}
