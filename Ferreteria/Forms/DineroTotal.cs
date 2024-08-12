using System;
using System.Windows.Forms;

namespace Ferreteria.Forms
{
    public partial class DineroTotal : Form
    {
        public DineroTotal(string dinero)
        {
            InitializeComponent();
            Mostar(dinero);
        }

        private void DineroTotal_Load(object sender, EventArgs e)
        {
            this.dinero.Focus();
        }

        public void Mostar(string dinero)
        {
            try
            {
                this.dinero.Text = Convert.ToDouble(dinero).ToString("C").Replace(",00", string.Empty);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Mostrar dienro");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void dinero_KeyPress(object sender, KeyPressEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
