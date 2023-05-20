using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace Tienda.Registros
{
    public partial class RegistroProd : Form
    {
        public RegistroProd()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void RegistroProd_Load(object sender, EventArgs e)
        {
            Unidades();
            this.idProd.Focus();
        }

        private void guardarProd_Click(object sender, EventArgs e)
        {
            InsertarProd();
        }

        private void InsertarProd()
        {
            try
            {
                double util = Convert.ToDouble(this.utilidad.Text);
                double precProd = Convert.ToDouble(this.precioProd.Text);
                double precVenta = double.Parse(this.precioFinal.Text.Trim().Replace("$",string.Empty).Replace(".",string.Empty), NumberStyles.Number);
                var ID = this.idProd.Text;
                var producto = this.nomProd.Text;
                var unidad = this.unidProd.SelectedValue;
                var marca = this.marcaProd.Text;
                var utilidad = this.utilidad.Text;
                var cantidad = this.txbCantidad.Text;
                string query = "INSERT INTO Producto VALUES (" + ID + ",'" + producto + "'," + precProd + "," + unidad + ", '" + marca + "','" + utilidad + "','" + precVenta + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
                string queryInsertCant = "insert into Bodega values (" + ID + ", '" + cantidad + "')";
                SqlCommand cmdInsert = new SqlCommand(queryInsertCant, con);
                con.Open();
                cmdInsert.ExecuteNonQuery();
                con.Close();
                
                string queryCantidad = "SELECT cantidad from Bodega where Id_Prod = " + this.idProd.Text + "";
                SqlDataAdapter adapter = new SqlDataAdapter(queryCantidad, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                cantidad = data.Rows[0].ItemArray[0].ToString();
                Clear();
                this.txtCantidad.Text = cantidad;
                this.idProd.Focus();
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error registro no guardado" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Unidades()
        {
            try
            {
                string unidad = "Select * from Unidad";
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(unidad, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                this.unidProd.DisplayMember = "Unidad";
                this.unidProd.ValueMember= "Id_Unidad";
                this.unidProd.DataSource = data;
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Unidades");
            }
        }

        private void idProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                InsertarProd();
            }
        }

        private void Clear()
        {
            this.utilidad.Clear();
            this.precioProd.Clear();
            this.idProd.Clear(); ;
            this.nomProd.Clear();
            this.marcaProd.Clear();
            this.utilidad.Clear();
            this.txbCantidad.Clear();
        }

        private void utilidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!utilidad.Text.Equals(""))
                {
                    string[] partPrecio;
                    decimal precioF = 0;
                    double util = Convert.ToDouble(this.utilidad.Text);
                    double precProd = Convert.ToDouble(this.precioProd.Text);
                    double precVenta = Math.Round(((util / 100) + 1) * precProd);
                    string strPrecVenta = precVenta.ToString("C");
                    double aprox = 0.00;
                    string strPreVentSin = strPrecVenta.ToString().Replace("$ ", string.Empty).Replace(",00", string.Empty);
                    string separar = "";
                    double partPrecioF = 0.00;
                    int k = 0;
                    if (!this.utilidad.Text.Equals(""))
                    {
                        if (strPreVentSin.Length <= 3)
                        {
                            separar = strPreVentSin.ToString().Insert(1, ",");
                            partPrecio = separar.Split(',');
                            //
                            string sumPrec = ""; string sumP = "";
                            //
                            for (int i = 0; i < partPrecio.Length; i++)
                            {
                                sumPrec += partPrecio[i].ToString();
                            }
                            if (double.Parse(sumPrec) >= 100)
                            {
                                separar = sumPrec.Insert(1, "-");
                                partPrecio = separar.Split('-');

                                if (double.Parse(partPrecio[1]) <= 50 && double.Parse(partPrecio[1]) >= 1)
                                {
                                    partPrecio[1] = 50.ToString();
                                    //precioF = decimal.Parse(partPrecio[0] + partPrecio[1], CultureInfo.GetCultureInfo("es-CO"));
                                }
                                if(double.Parse(partPrecio[1]) >= 51)
                                {
                                    double sumar = double.Parse(partPrecio[0]) + 1;
                                    partPrecio[0] = sumar.ToString();
                                    partPrecio[1] = "00";
                                    //precioF = decimal.Parse(partPrecio[0], CultureInfo.GetCultureInfo("es-CO"));
                                }
                                
                                precioF = decimal.Parse(partPrecio[0] + partPrecio[1], CultureInfo.GetCultureInfo("es-CO"));
                            }
                            if(double.Parse(sumPrec) <= 99)
                            {
                                precioF = decimal.Parse(100.ToString(), CultureInfo.GetCultureInfo("es-CO"));
                            }
                            
                        }
                        else
                        {
                            partPrecio = strPrecVenta.Trim().Replace("$", string.Empty).Split('.');
                            string sinCentavos = partPrecio[1].Replace(",00", string.Empty).ToString();
                            double conDecimal = double.Parse(sinCentavos.Substring(0, 1) + "," + sinCentavos.Substring(1, 2));
                            aprox = (double)Math.Ceiling(conDecimal);
                            if (aprox >= 10)
                            {
                                aprox = 0;
                                partPrecioF = double.Parse(partPrecio[0]) + 1;
                                partPrecio[0] = partPrecioF.ToString();
                                string sumP = "";
                                for (int i = 0; i < partPrecio.Length; i++)
                                {
                                    sumP += partPrecio[i].ToString();
                                }
                                precioF = decimal.Parse(sumP, CultureInfo.GetCultureInfo("es-CO"));
                            }
                            else
                            {
                                string sumP = "";
                                for(int i=0; i<partPrecio.Length; i++) 
                                { 
                                    sumP += partPrecio[i].ToString();
                                }
                                precioF = decimal.Parse(sumP, CultureInfo.GetCultureInfo("es-CO"));
                            }
                        }
                    }
                    this.precioFinal.Text = precioF.ToString("C").Replace(",00", string.Empty);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El campo de precios no puede estar vacio");
            }
        }

        private void idProd_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
