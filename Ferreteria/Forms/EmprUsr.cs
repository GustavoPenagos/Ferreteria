using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
