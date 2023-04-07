using System;
using System.Windows.Forms;

namespace Ferreteria.Forms
{
    public partial class MsgUsrEmp : Form
    {
        public MsgUsrEmp()
        {
            InitializeComponent();
        }

        private void MsgUsrEmp_Load(object sender, EventArgs e)
        {
            this.button1.Focus();
        }
        //Empresa
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        //Persona
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.P) || e.KeyChar == Convert.ToChar(112))
            {
                DialogResult = DialogResult.OK;
            }
            if(e.KeyChar == Convert.ToChar(Keys.E) || e.KeyChar == Convert.ToChar(101))
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
