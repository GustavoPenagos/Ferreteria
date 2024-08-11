using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Tienda.Model;

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
                var total = double.Parse(totalFact.Text);
                if (total == 0)
                {
                    MessageBox.Show("La factura esta con saldo $0");
                    return;
                }
                var nFactura = this.bFCompra.Text.ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 6);
                cmd.Parameters.AddWithValue("@Factura", nFactura);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                var valorFactura = double.Parse(dt.Rows[0].ItemArray[2].ToString());
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
                    con.Open();
                    SqlCommand cmdA = new SqlCommand("InsertarAbonos", con);
                    cmdA.Parameters.AddWithValue("@Id_Factura", nFactura);
                    cmdA.Parameters.AddWithValue("@Valor_Total", valorFactura.ToString());
                    cmdA.Parameters.AddWithValue("@Abono", abono.ToString());
                    cmdA.Parameters.AddWithValue("@Fecha", DateTime.Now.ToShortDateString());
                    cmdA.Parameters.AddWithValue("@Valor_Actual", valorActual.ToString());
                    cmdA.CommandType = CommandType.StoredProcedure;
                    cmdA.ExecuteReader();
                    con.Close();

                    con.Open();
                    SqlCommand cmdFact = new SqlCommand("InsertarCartera", con);
                    cmdFact.Parameters.AddWithValue("@Id_Cartera", 8);
                    cmdFact.Parameters.AddWithValue("@Valor_Cartera", valorFactura.ToString());
                    cmdFact.Parameters.AddWithValue("@Fecha_Registro", DateTime.Now.ToShortDateString());
                    cmdFact.Parameters.AddWithValue("@Factura", nFactura);
                    cmdFact.Parameters.AddWithValue("@Fecha_Fin", DateTime.Now.ToShortDateString());
                    cmdFact.Parameters.AddWithValue("@Documento", "0");
                    cmdFact.CommandType = CommandType.StoredProcedure;
                    cmdFact.ExecuteNonQuery();
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
                totalFact.Text = resultado.ToString();
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
                con.Open();
                string queryImg = "select Factura from Factura_Compras where Id = " + this.bFCompra.Text;
                SqlCommand cmdImg = new SqlCommand(queryImg, con);
                SqlDataReader drImg = cmdImg.ExecuteReader();
                DataTable dtImg = new DataTable();
                dtImg.Load(drImg);
                var imgBase64 = dtImg.Rows[0].ItemArray[0].ToString();
                byte[] imageBytes = Convert.FromBase64String(imgBase64);

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
            finally { con.Close(); }
        }

        private void buscarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                var nFactura = this.bFCompra.Text;
                var resul = 0.00;
                //CARTERA
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 6);
                cmd.Parameters.AddWithValue("@Factura", nFactura);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                var valorF = Convert.ToDouble(dt.Rows[0].ItemArray[2].ToString());
                //ABONO
                con.Open();
                string queryAbono = "select sum(convert(decimal, abono)) from Abono where Id_Factura = '" + nFactura.ToString() + "'";
                SqlCommand cmdA = new SqlCommand(queryAbono, con);
                SqlDataReader drA = cmdA.ExecuteReader();
                DataTable dtA = new DataTable();
                dtA.Load(drA);
                con.Close();
                //CONTAR ABONO
                con.Open();
                string queryCAbono = "select  abono from Abono where Id_Factura = '" + nFactura.ToString() + "'";
                SqlCommand cmdCA = new SqlCommand(queryCAbono, con);
                SqlDataReader drCA = cmdCA.ExecuteReader();
                DataTable dtCA = new DataTable();
                dtCA.Load(drCA);
                con.Close();
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
                this.totalFact.Text = resul.ToString();
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
