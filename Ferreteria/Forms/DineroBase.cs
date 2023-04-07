using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ferreteria.Forms
{
    public partial class DineroBase : Form
    {
        public DineroBase()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void DineroBase_Load(object sender, EventArgs e)
        {
            this.dinero.Focus();

        }

        private void Dinero_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dinero.Text.Equals(""))
                {
                    dineroMoneda.Text = "";
                }
                else
                {
                    var moneda = Convert.ToInt64(dinero.Text);

                    dineroMoneda.Text = moneda.ToString("C").Replace(",00", string.Empty);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "INGRSO DE DINERO");
            }
        }

        private void dinero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                IngresarDienro();
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IngresarDienro();
        }

        public void IngresarDienro()
        {
            string date = DateTime.Now.ToShortDateString();
            string query = "";

            if(dinero.Text.Equals("") || dinero.Text.Length < 4)
            {
                MessageBox.Show("Ingresar un valor valido");
                DialogResult = DialogResult.No;
            }
            else
            {
                query = "INSERT INTO DineroBase VALUES('" + dinero.Text + "','" + date + "')";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                DialogResult = DialogResult.Yes;
            }
            
            
            
        }
    }
}
