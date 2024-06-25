using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tienda.Registros
{
    public partial class RegistroGastos : Form
    {
        public RegistroGastos()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void RegistroGastos_Load(object sender, EventArgs e)
        {
            this.dineroGasto.Focus();
        }

        private void guardarRegistro_Click(object sender, EventArgs e)
        {
            InsertGasto();
        }

        private void InsertGasto()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertarGastos", con);
                cmd.Parameters.AddWithValue("@Dinero", this.dineroGasto.Text);
                cmd.Parameters.AddWithValue("@Descripcion", this.descriGasto.Text);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                this.dineroGasto.Focus();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error al registrar los datos" + ex.Message,"", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void descriGasto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                InsertGasto();
            }
        }

        private void dineroGasto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Clear()
        {
            this.descriGasto.Clear();
            this.dineroGasto.Clear();
        }

        
    }
}
