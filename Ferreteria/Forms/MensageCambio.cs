using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Ferreteria.Forms
{
    public partial class MensageCambio : Form
    {
        public MensageCambio(double pMessage)
        {
            InitializeComponent();
            this.cambio.Text = pMessage.ToString("C").Replace(",00", string.Empty);
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
