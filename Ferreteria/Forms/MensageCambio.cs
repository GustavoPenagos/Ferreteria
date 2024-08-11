using System;
using System.Windows.Forms;

namespace Ferreteria.Forms
{
    public partial class MensageCambio : Form
    {
        public MensageCambio(double pMessage)
        {
            InitializeComponent();
            this.cambio.Text = pMessage.ToString();
        }

        private void MensageCambio_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
