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
                var cantidad = this.txbCantidad.Text.Equals("") ? "0" : this.txbCantidad.Text;
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
                string[] partPrecio;
                decimal precioF = 0;
                string sinCentavos = "";
                double aprox = 0.00;
                if (!this.utilidad.Text.Equals(""))
                {
                    double util = Convert.ToDouble(this.utilidad.Text);
                    double precProd = Convert.ToDouble(this.precioProd.Text);
                    double precVenta = Math.Round(((util / 100) + 1) * precProd);
                    string strPrecVenta = precVenta.ToString("C");
                    if (strPrecVenta.Length > 2)
                    {
                        if (strPrecVenta.Contains("."))
                        {
                            partPrecio = strPrecVenta.Trim().Replace("$", string.Empty).Split('.');
                            sinCentavos = partPrecio[1].Replace(",00", string.Empty).ToString();
                            double conDecimal = double.Parse(sinCentavos.Substring(0, 1) + "," + sinCentavos.Substring(1, 2));
                            aprox = (double)Math.Ceiling(conDecimal);
                            if (aprox >= 10)
                            {
                                aprox = 0;
                                double partPrecioF = double.Parse(partPrecio[0]) + 1;
                                partPrecio[0] = partPrecioF.ToString();
                                precioF = decimal.Parse(partPrecio[0] + aprox.ToString() + "00", CultureInfo.GetCultureInfo("es-CO"));
                            }
                            else
                            {
                                precioF = decimal.Parse(partPrecio[0] + aprox.ToString() + "00", CultureInfo.GetCultureInfo("es-CO"));
                            }
                            this.precioFinal.Text = precioF.ToString("C").Replace(",00", string.Empty);
                        }
                        else
                        {
                            partPrecio = strPrecVenta.Trim().Replace("$", string.Empty).Split(',');
                            if (partPrecio[0].Length <= 2)
                            {
                                aprox = (double)Math.Ceiling(Double.Parse(partPrecio[0]));
                            }
                            else
                            {
                                sinCentavos = partPrecio[0].Replace(",00", string.Empty).ToString();

                                double conDecimal = double.Parse(sinCentavos.Insert(2, ","));
                                aprox = (double)Math.Ceiling(conDecimal);
                            }

                            if (aprox >= 10)
                            {
                                //Cambio de prueba temporal
                                double partPrecioF = double.Parse(partPrecio[0]) + 1;
                                partPrecio[0] = partPrecioF.ToString();
                                precioF = decimal.Parse( aprox.ToString() + "00", CultureInfo.GetCultureInfo("es-CO"));
                            }
                            else
                            {
                                precioF = decimal.Parse(aprox.ToString() + "00", CultureInfo.GetCultureInfo("es-CO"));
                            }
                            this.precioFinal.Text = precioF.ToString("C").Replace(",00", string.Empty);
                        }
                    }
                    //else if (strPrecVenta.Length <= 2)
                    //{
                    //    partPrecio = strPrecVenta.Trim().Replace("$", string.Empty).Split(',');
                    //    sinCentavos = partPrecio[0].Replace(",00", string.Empty).ToString();

                    //    double conDecimal = double.Parse(sinCentavos.Insert(2, ","));
                    //    aprox = (double)Math.Ceiling(conDecimal);
                    //    if (aprox >= 10)
                    //    {
                    //        aprox = 0;
                    //        double partPrecioF = double.Parse(partPrecio[0]) + 1;
                    //        partPrecio[0] = partPrecioF.ToString();
                    //        precioF = decimal.Parse(partPrecio[0] + aprox.ToString() + "00", CultureInfo.GetCultureInfo("es-CO"));
                    //    }
                    //    else
                    //    {
                    //        precioF = decimal.Parse(aprox.ToString() + "00", CultureInfo.GetCultureInfo("es-CO"));
                    //    }
                    //    this.precioFinal.Text = precioF.ToString("C").Replace(",00", string.Empty);
                    //}

                    //partPrecio = strPrecVenta.Trim().Replace("$",string.Empty).Split('.');
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("El campo de precios no puede estar vacio");
                MessageBox.Show(ex.Message, "Caluculo de precio");
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
