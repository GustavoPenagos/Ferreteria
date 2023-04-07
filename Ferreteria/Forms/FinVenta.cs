using System;
using System.Windows.Forms;

namespace Ferreteria.Forms
{
    public partial class FinVenta : Form
    {
        public FinVenta()
        {
            InitializeComponent();
        }

        private void FinVenta_Load(object sender, EventArgs e)
        {
            button2.Focus();
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

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.S) || e.KeyChar == Convert.ToChar(115))
            {
                DialogResult = DialogResult.Yes;
            }
            if(e.KeyChar == Convert.ToChar(Keys.N) || e.KeyChar == Convert.ToChar(110))
            {
                DialogResult = DialogResult.No;
            }
            if(e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                DialogResult = DialogResult.No;
            }
        }

        
    }
}
