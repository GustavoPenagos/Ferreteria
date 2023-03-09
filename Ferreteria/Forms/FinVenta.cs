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
    public partial class FinVenta : Form
    {
        public FinVenta()
        {
            InitializeComponent();
        }
        //YES
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        //NO
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.No;
        }

        private void FinVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
