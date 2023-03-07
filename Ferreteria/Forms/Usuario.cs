using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tienda.Registros
{
    public partial class Usuario : Form
    {
        public Usuario()
        {
            InitializeComponent();
            OpenFrom(new Listas.ListaUsuarios());
        }
        private void OpenFrom(object fromhijo)
        {
            if (this.panel5.Controls.Count > 0)
            {
                this.panel5.Controls.RemoveAt(0);
            }
            Form fh = fromhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel5.Controls.Add(fh);
            this.panel5.Tag = fh;
            fh.Show();
        }
        private void registroProveedores_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroUsuarios());
        }

        private void registroProveedor_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.RegistroUsuarios());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaUsuarios());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Listas.ListaUsuarios());
        }
    }
}
