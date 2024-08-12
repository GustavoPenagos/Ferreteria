using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using DistribucionesArly_s;


namespace Tienda.Listas
{
    public partial class ControlFacturas : Form
    {
        public ControlFacturas()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ControlFacturas_Load(object sender, EventArgs e)
        {
            this.selecBus.Text = "Factura de venta";
        }

        private void buscarFact_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            try
            {
                con.Open();
                webBrowser1.DocumentText = string.Empty;
                SqlCommand cmd = new SqlCommand("ObtenerImgFactura", con);
                switch (this.selecBus.Text)
                {
                    case ("Factura de venta"):
                        cmd.Parameters.AddWithValue("@Id_Factura", "Factura");
                        cmd.Parameters.AddWithValue("@Factura", this.buscaID.Text.Trim());
                        break;
                    case ("Factura Remision"):
                        cmd.Parameters.AddWithValue("@Id_Factura", "FacturaRem");
                        cmd.Parameters.AddWithValue("@Factura", this.buscaID.Text.Trim());
                        break;
                    case ("Cotizacion"):

                        cmd.Parameters.AddWithValue("@Id_Factura", "Cotizacion");
                        cmd.Parameters.AddWithValue("@Factura", this.buscaID.Text.Trim());
                        break;
                }
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                string codeB64 = dt.Rows[0].ItemArray[1].ToString();
                byte[] B64ToByte = Convert.FromBase64String(codeB64);
                string rutaDatosPDF = ConfigurationManager.AppSettings["PathPDFFactura"];
                Process[] processes = Process.GetProcessesByName("Acrobat");
                for (int i = 0; i < processes.Length - 1; i++)
                {
                    processes[i].Kill();
                }
                Thread.Sleep(1000);
                File.WriteAllBytes(rutaDatosPDF, B64ToByte);


                webBrowser1.Stop();
                webBrowser1.Navigate(rutaDatosPDF);

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Buscar");
            }
        }

        private void selecBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //this.buscaID.Clear();
                //this.multiFact.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buscaID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Buscar();
            }
        }
        
        public void Cotiza(string factura, long barras)
        {
            try
            {
                string rutaPdf = ConfigurationManager.AppSettings["PathPDF"];
                Process[] processes = Process.GetProcessesByName("Acrobat");

                for (int i = 0; i < processes.Length-1;i++)
                {
                    processes[i].Kill();
                }
                
                Thread.Sleep(100);
                string queryBusca = "SELECT * FROM " + factura.ToString() + " WHERE Id_Factura = " + barras;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(queryBusca, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count == 0)
                {
                    MessageBox.Show("No hay cotizaciones con ese numero", "Vacio", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                string codeB64 = dt.Rows[0].ItemArray[1].ToString();
                byte[] B64ToByte = Convert.FromBase64String(codeB64);
                con.Close();

                File.WriteAllBytes(rutaPdf, B64ToByte);
                webBrowser1.Stop();
                webBrowser1.Navigate(rutaPdf);
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Cotiza");
            }
        }

        private void btnCorreo_Click(object sender, EventArgs e)
        {
            try
            {

                string rutaDatosPDF = ConfigurationManager.AppSettings["PathPDFFactura"];
                string correos = Microsoft.VisualBasic.Interaction.InputBox("Correo para enviar la cotización", "Datos de factura Nit");

                SendCorreo correo = new SendCorreo();
                correo.SendEmailCot(rutaDatosPDF, correos, "Factura","");
                MessageBox.Show("Correo enviado", "Correo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "BTN Correo");
            }
        }
    }
}
