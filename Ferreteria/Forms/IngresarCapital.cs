using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Ferreteria.Forms
{
    public partial class IngresarCapital : Form
    {
        public IngresarCapital()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void IngresarCapital_Load(object sender, EventArgs e)
        {
            this.dinero.Focus();
        }
        //ACECPT
        private void button2_Click(object sender, EventArgs e)
        {
            Ingresar();
        }
        //CANCEL
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dienro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Ingresar();
            }
            if(e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        public void Ingresar()
        {
            try
            {
                string capital = this.dinero.Text;
                var sum = 0.00;
                //
                con.Open();
                SqlCommand cmdC = new SqlCommand("InsertarCartera", con);
                cmdC.Parameters.AddWithValue("@Id_Cartera", 5);
                cmdC.Parameters.AddWithValue("@Valor_Cartera", capital.Trim());
                cmdC.Parameters.AddWithValue("@Fecha_Registro", DateTime.Now.ToShortDateString());
                cmdC.Parameters.AddWithValue("@Factura", 0);
                cmdC.Parameters.AddWithValue("@Fecha_Fin", DateTime.Now.ToShortDateString());
                cmdC.Parameters.AddWithValue("@Documento", 0);
                cmdC.CommandType = CommandType.StoredProcedure;
                cmdC.ExecuteReader();
                con.Close();
                //
                con.Open();
                SqlCommand cmdD = new SqlCommand("InsertarDineroBase", con);
                cmdD.Parameters.AddWithValue("@Dinero", capital.ToString());
                cmdD.Parameters.AddWithValue("@Fecha_Registro", DateTime.Now.ToShortDateString());
                cmdD.CommandType = CommandType.StoredProcedure;
                cmdD.ExecuteReader();
                con.Close();
                //
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 5);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sum = sum + double.Parse(dt.Rows[i].ItemArray[2].ToString());
                    }
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "IngresarCapital/Ingresar");
            }
        }

        private void dienro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dinero.Text.Equals(""))
                {
                    dineroMoneda.Text = "";
                }
                else
                {
                    long moneda = Convert.ToInt64(dinero.Text);

                    dineroMoneda.Text = moneda.ToString();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "dineroMoneda");
            }
        }
    }
}
