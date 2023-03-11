using Aspose.Pdf.Operators;
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
                var num = 0.00;
                var date = DateTime.Now.ToShortDateString();
                string queryCapital = "INSERT INTO Cartera values (6, '" + capital.Trim() + "', '" + date + "', '0', '0','0')";
                con.Open();
                SqlCommand cmdCap = new SqlCommand(queryCapital, con);
                cmdCap.ExecuteNonQuery();
                con.Close();
                //
                string query = "INSERT INTO DineroBase VALUES('" + capital + "','" + date + "')";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                //
                string queryConsult = "SELECT * FROM Cartera where Id_Cartera = 6";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(queryConsult, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var count = dt.Rows.Count;

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        num = double.Parse(dt.Rows[i].ItemArray[2].ToString());
                        sum = sum + num;
                    }
                }
                con.Close();
                DialogResult = DialogResult.OK;
            }
            catch
            {

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

                    dineroMoneda.Text = moneda.ToString("C").Replace(",00", string.Empty);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "dineroMoneda");
            }
        }
    }
}
