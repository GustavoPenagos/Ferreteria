using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tienda
{
    public partial class Validacion : Form
    {
        public Validacion()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void Validacion_Load(object sender, EventArgs e)
        {
            this.password.Focus();
        }
        private void validarPass_Click(object sender, EventArgs e)
        {
            Exit();
        }
        private void Exit()
        {
            try
            {
                var passw = this.password.Text;
                if (passw.Equals(""))
                {
                    return;
                }
                string query = ConfigurationManager.AppSettings["password"];
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                var pass = dt.Rows[0].ItemArray[0].ToString();
                if (pass.Equals(passw))
                {
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Contraseña invalida (" + this.password.Text + ")");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Exit();
            }
        }

        
    }
}
