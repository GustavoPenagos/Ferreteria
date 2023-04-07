using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ferreteria.Forms
{
    public partial class CambiarAdmin : Form
    {
        public CambiarAdmin()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        //load
        private void CambiarAdmin_Load(object sender, EventArgs e)
        {

        }
        //Aceptar
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var documento = this.documento.Text;
                if (documento.Equals(""))
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    string queryConsult = "select count(*) from [User] where Id_User = " + documento;
                    SqlDataAdapter adapter = new SqlDataAdapter(queryConsult, con);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    if (data.Rows.Count != 0 || data != null)
                    {

                        string queryUpdate = "update [user] set administra = 1 where Id_User = " + documento;
                        string queryUpdateAll = "update [user] set administra = 0 where administra = 1";
                        con.Open();
                        SqlCommand cmd1 = new SqlCommand(queryUpdateAll, con);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand(queryUpdate, con);
                        cmd2.ExecuteNonQuery();
                        con.Close();

                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("No existe cliente con este documento : " + documento);
                    }
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "aceptar cambio admin");
                DialogResult = DialogResult.Cancel;
            }
        }
        //Cancelar
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "cencelar cambio admin");
            }
        }
    }
}
