using Microsoft.Office.Interop.Excel;
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
using DataTable = System.Data.DataTable;

namespace Ferreteria.Forms
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void Password_Load(object sender, EventArgs e)
        {
            this.passwaord.Focus();
            this.passwaord.PasswordChar = '*';
        }
        //ACEPT
        private void button2_Click(object sender, EventArgs e)
        {
            ValidarContrasena();
        }
        //CANCEL
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public void ValidarContrasena()
        {
            try
            {
                if (passwaord.Text.Equals(""))
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    string queryPass = ConfigurationManager.AppSettings["password"];
                    SqlDataAdapter adapter = new SqlDataAdapter(queryPass, con);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    var passw = data.Rows[0].ItemArray[0].ToString();

                    if (passwaord.Text.Equals(passw))
                    {
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta", "Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
            catch(Exception ex)
            {

            }
        }

        private void passwaord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ValidarContrasena();
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
