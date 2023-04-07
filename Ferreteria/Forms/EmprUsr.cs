using System;
using System.Windows.Forms;

namespace Ferreteria.Forms
{
    public partial class EmprUsr : Form
    {
        public EmprUsr(string msg)
        {
            InitializeComponent();
            if (msg.Contains("documento"))
            {
                msgLabel.Text = msg;
            }
            if (msg.Contains("Nit"))
            {
                msgLabel.Text = msg;
            }
        }

        private void EmprUsr_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception ex) 
            {

            }
        }
    }
}
