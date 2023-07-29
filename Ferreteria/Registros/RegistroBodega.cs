using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tienda.Registros
{
    public partial class RegistroBodega : Form
    {
        public RegistroBodega()
        {
            InitializeComponent();
        }

        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void RegistroBodega_Load(object sender, EventArgs e)
        {
            string tipoDoc = "select * from Producto";
            con.Open();
            SqlDataAdapter adapterD = new SqlDataAdapter(tipoDoc, con);
            DataTable dataD = new DataTable();
            adapterD.Fill(dataD);
            this.cBNombre.DisplayMember = "Nombre_Prod";
            this.cBNombre.ValueMember = "Id_Prod";
            this.cBNombre.DataSource = dataD;
            con.Close();

            if (this.rBId.Checked == true)
            {
                this.idProdRegis.Focus();
                this.cBNombre.Visible = false;
            }
            if(this.rBNombre.Checked == true)
            {
                this.cBNombre.Focus();
                this.idProdRegis.Visible=false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InsertarBodega();
                ConteoProd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertarBodega()
        {
            try
            {
                string queryConsult = ""; 
                if (this.rBId.Checked == true)
                {
                    queryConsult = "select Id_Prod from Producto where Id_Prod = " + this.idProdRegis.Text;
                }
                if (this.rBNombre.Checked == true)
                {
                    queryConsult = "select Id_Prod from Producto where Id_Prod = " + this.cBNombre.SelectedValue;
                }
                //
                con.Open();
                SqlCommand cmdConsul = new SqlCommand(queryConsult, con);
                SqlDataReader dr = cmdConsul.ExecuteReader();
                var cantidadProd = this.cantidadProd.Text.Equals("") ? "0" : this.cantidadProd.Text;
                int sum = 0;
                if (dr.Read())
                {
                    con.Close();
                    //
                    SqlDataAdapter adapter = new SqlDataAdapter(queryConsult, con);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    this.idProdRegis.Text = dataTable.Rows[0].ItemArray[0].ToString();
                    //
                    string queryVal = "Select * from Bodega";
                    con.Open();
                    SqlDataAdapter ad = new SqlDataAdapter(queryVal, con);
                    DataTable data = new DataTable();
                    ad.Fill(data);
                    //

                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        var idProd = data.Rows[i].ItemArray[1].ToString();

                        if (idProd == this.idProdRegis.Text)
                        {
                            var cantidad = Convert.ToDouble(data.Rows[i].ItemArray[2].ToString());
                            var total = Convert.ToInt32(cantidadProd) + cantidad;
                            string queryInsert = "update Bodega set cantidad = " + total + " where Id_Prod = " + this.idProdRegis.Text;
                            SqlCommand cmdInsert = new SqlCommand(queryInsert, con);
                            cmdInsert.ExecuteNonQuery();
                            con.Close();
                            //MessageBox.Show("Se ha ingresado el producto correctamente");
                            
                            return;
                        }
                        sum = 1 + sum;
                    }
                    if (sum == data.Rows.Count)
                    {
                        string queryInsert = "insert into Bodega values (" + this.idProdRegis.Text + ", '" + cantidadProd + "')";
                        //con.Open();
                        SqlCommand cmdInsert = new SqlCommand(queryInsert, con);
                        cmdInsert.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    con.Close();
                    MessageBox.Show("No existe este ID (" + this.idProdRegis.Text + ")" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                con.Close();
                MessageBox.Show("Numero ID_producto no valido (" + this.idProdRegis.Text + ")","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ConteoProd()
        {
            try
            {
                string queryCantidad = "SELECT cantidad from Bodega where Id_Prod = " + this.idProdRegis.Text + "";
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(queryCantidad, con);
                ad.Fill(dt);
                con.Close();
                this.cantProdBod.Text = (Convert.ToInt64(dt.Rows[0].ItemArray[0])).ToString();
                Clear();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error al contar productos" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clear()
        {
            this.idProdRegis.Clear();
            this.cantidadProd.Clear();
        }

        private void idProdRegis_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                InsertarBodega();
                ConteoProd();
            }
        }

        private void cantProdBod_TextChanged(object sender, EventArgs e)
        {

        }

        private void idProdRegis_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string query = "select * from Producto where Nombre_Prod like '%" + this.idProdRegis.Text+ "%'";
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand(query, con);
            //    SqlDataAdapter ad = new SqlDataAdapter(query, con);
            //    DataTable dt = new DataTable();
            //    ad.Fill(dt);

            //    AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        data.Add(dt.Rows[i].ItemArray[1].ToString());

            //    }
            //    this.idProdRegis.AutoCompleteCustomSource = data;
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    con.Close();
            //    MessageBox.Show("idProdC" + ex.Message);
            //}
        }

        private void rBId_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rBId.Checked == true)
            {
                this.idProdRegis.Focus();
                this.cBNombre.Visible = false;
                this.idProdRegis.Visible = true;
            }
            if (this.rBNombre.Checked == true)
            {
                this.cBNombre.Focus();
                this.idProdRegis.Visible = false;
                this.cBNombre.Visible = true;
            }
        }
    }
}
