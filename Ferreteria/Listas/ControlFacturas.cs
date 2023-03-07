using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

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
               var seleccion = this.selecBus.Text;
                var cBarras = Convert.ToInt64(this.buscaID.Text.Trim());
                switch (seleccion)
                {
                    case ("Factura de venta"):
                        seleccion = "Factura";
                        Mostrar(seleccion, cBarras);
                        break;
                    case ("Factura Remision"):
                        seleccion = "FacturaRem";
                        Mostrar(seleccion, cBarras);
                        break;
                    case ("Cotizacion"):
                        seleccion = "Cotizacion";
                        Cotiza(seleccion, cBarras);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Buscar");
            }
        }

        private void Mostrar(string factura, long barras)
        {
            try
            {
                webBrowser1.DocumentText = string.Empty;
                string queryBusca = "SELECT * FROM " + factura.ToString() + " WHERE Id_Factura = " + barras;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(queryBusca, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var codeB64 = dt.Rows[0].ItemArray[1].ToString();
                var B64ToByte = Convert.FromBase64String(codeB64);
                var a = System.Text.Encoding.UTF8.GetString(B64ToByte);
                con.Close();
                this.multiFact.Text = a;

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Mostar");
            }
        }

        private void selecBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.buscaID.Clear();
                this.multiFact.Clear();
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
                //
                Process[] processes = Process.GetProcessesByName("Acrobat");

                // Finalizar cada proceso de Adobe Acrobat
                for (int i = 0; i < processes.Length-1;i++)
                {
                    processes[i].Kill();
                }
                //
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
                var codeB64 = dt.Rows[0].ItemArray[1].ToString();
                var B64ToByte = Convert.FromBase64String(codeB64);
                con.Close();
                // Guarda los bytes en un archivo PDF
                File.WriteAllBytes(rutaPdf, B64ToByte);
                webBrowser1.Stop();
                webBrowser1.Navigate(rutaPdf);
                //webBrowser1.Dispose();


            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Cotiza");
            }
        }

    }
}
