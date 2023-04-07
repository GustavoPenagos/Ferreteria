using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Tienda.Registros
{
    public partial class RegistoAbonos : Form
    {
        public RegistoAbonos()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void RegistoAbonos_Load(object sender, EventArgs e)
        {
            this.bFCompra.Focus();
        }

        private void insertAbono_Click(object sender, EventArgs e)
        {
            InertarAbono();
        }

        private void InertarAbono()
        {
            try
            {
                var total = double.Parse(totalFact.Text, NumberStyles.Currency);
                if (total == 0)
                {
                    MessageBox.Show("La factura esta en saldo 0");
                    return;
                }
                var nFactura = this.bFCompra.Text.ToString();
                string queryCartera = "select * from Cartera where Factura like '" + nFactura + "' and Id_Cartera = 4";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryCartera, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                var valorFactura = double.Parse(dt.Rows[0].ItemArray[2].ToString());
                con.Close();
                var date = DateTime.Now.ToShortDateString().ToString();
                var abono = double.Parse(this.abonoFact.Text);

                var resultado = total - abono;
                if (resultado < 0)
                {
                    MessageBox.Show("Ese valor es mayor al valor por pagar");
                    return;
                }
                else
                {
                    var valorActual = valorFactura - abono;
                    string queryAbono = "INSERT INTO Abono VALUES(" + nFactura + ",'" + valorFactura + "','" + abono + "','" + date + "', '"+valorActual+"')";
                    con.Open();
                    SqlCommand cmdInsert = new SqlCommand(queryAbono, con);
                    cmdInsert.ExecuteNonQuery();
                    con.Close();
                    Borrar(resultado);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error registrando el abono" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Borrar(double resultado)
        {
            try
            {
                totalFact.Text = resultado.ToString("C").Replace(",00", string.Empty);
                abonoFact.Clear();
                bFCompra.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Borrar");
            }
        }

        private void MostrarImg()
        {
            try
            {
                string queryImg = "select * from FacturaCompras where Id_Factura = " + bFCompra.Text.ToString();
                //con.Open();
                SqlCommand cmdImg = new SqlCommand(queryImg, con);
                SqlDataReader drImg = cmdImg.ExecuteReader();
                DataTable dtImg = new DataTable();
                dtImg.Load(drImg);
                var imgBase64 = dtImg.Rows[0].ItemArray[1].ToString();
                //con.Close();
                byte[] imageBytes = Convert.FromBase64String(imgBase64);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    Image image = Image.FromStream(ms, true);
                    imgFactura.Image = image;
                    imgFactura.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "MostrarImg");
            }
        }

        private void buscarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                var nFactura = this.bFCompra.Text;
                var resul = 0.00;
                con.Open();
                //CARTERA
                string queryTotal = "select * from Cartera where Factura like '" + nFactura.ToString() + "' and Id_Cartera = 4";
                SqlCommand cmd = new SqlCommand(queryTotal, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                var valorF = Convert.ToDouble(dt.Rows[0].ItemArray[2].ToString());
                //ABONO
                string queryAbono = "select sum(convert(decimal, abono)) from Abono where Id_Factura = '" + nFactura.ToString() + "'";
                SqlCommand cmdA = new SqlCommand(queryAbono, con);
                SqlDataReader drA = cmdA.ExecuteReader();
                DataTable dtA = new DataTable();
                dtA.Load(drA);
                //CONTAR ABONO
                string queryCAbono = "select  abono from Abono where Id_Factura = '" + nFactura.ToString() + "'";
                SqlCommand cmdCA = new SqlCommand(queryCAbono, con);
                SqlDataReader drCA = cmdCA.ExecuteReader();
                DataTable dtCA = new DataTable();
                dtCA.Load(drCA);
                if (dtCA.Rows.Count == 0)
                {
                    resul = valorF;
                }
                else
                {
                    var valorA = Convert.ToDouble(dtA.Rows[0].ItemArray[0].ToString());
                    resul = valorF - valorA;
                }

                //
                MostrarImg();
                con.Close();
                this.totalFact.Text = resul.ToString("C").Replace(",00", string.Empty);
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "buscarFactura_Click");
            }
        }

        private void bFCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                InertarAbono();
            }
        }
    }
}
