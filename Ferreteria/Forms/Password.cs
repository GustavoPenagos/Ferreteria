using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Ferreteria.Forms
{
    public partial class Password : Form
    {
        public Password(string usuario)
        {
            InitializeComponent();
            if (usuario.Equals("Control"))
            {
                label.Text = usuario;
            }
            else
            {

            }
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
            if (label.Text.Equals("Control"))
            {
                AccesoControl();
            }
            else
            {
                ValidarContrasena();
            }
        }
        //CANCEL
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        public void AccesoControl()
        {
            try
            {
                string pass = "";
                //
                string queryActivo = ConfigurationManager.AppSettings["passwordAdmin"];
                SqlDataAdapter adapterA = new SqlDataAdapter(queryActivo, con);
                DataTable dataTable = new DataTable();
                adapterA.Fill(dataTable);
                string passw = dataTable.Rows[0].ItemArray[0].ToString();

                if (passwaord.Text.Equals(passw))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta", "Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {

            }
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
                    string pass = "";
                    string passw = "";
                    string passw2 = "";
                    string queryActivo = ConfigurationManager.AppSettings["password"];
                    SqlDataAdapter adapterA = new SqlDataAdapter(queryActivo, con);
                    DataTable dataTable = new DataTable();
                    adapterA.Fill(dataTable);
                    if (dataTable.Rows.Count > 1)
                    {
                        passw = dataTable.Rows[0].ItemArray[0].ToString();
                        passw2 = dataTable.Rows[1].ItemArray[0].ToString();
                        if (passwaord.Text.Equals(passw) || passwaord.Text.Equals(passw2))
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Contraseña incorrecta", "Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if(dataTable.Rows.Count == 1)
                    {
                        passw = dataTable.Rows[0].ItemArray[0].ToString();
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
                
            }
            catch(Exception ex)
            {

            }
        }

        private void passwaord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (label.Text.Equals("Control"))
                {
                    AccesoControl();
                }
                else
                {
                    ValidarContrasena();
                }
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
