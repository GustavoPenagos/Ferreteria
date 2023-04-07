using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Tienda.Registros
{
    public partial class RegistroCompras : Form
    {
        public RegistroCompras()
        {
            InitializeComponent();
            Tipos();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void RegistroCompras_Load(object sender, EventArgs e)
        {
            this.numFact.Focus();
        }

        private void CargarImg_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog abrirImg = new OpenFileDialog();

                if (abrirImg.ShowDialog() == DialogResult.OK)
                {
                    ImgFact.ImageLocation = abrirImg.FileName;
                    ImgFact.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.GuardarFact.Visible = true;
                }
                else
                {
                    this.GuardarFact.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en carga de imagen" + ex.Message,"",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarFact_Click_1(object sender, EventArgs e)
        {
            try
            {
                var nFactura = this.numFact.Text;
                var valorFac = this.valorFact.Text;
                var img = this.ImgFact.Image;
                var date = this.dateFact.Value.ToString("d/MM/yyyy");
                var dateLimi = this.dateLimite.Value.ToString("d/MM/yyyy");
                var empresa = this.cBoxEmp.SelectedValue.ToString();

                if (nFactura.Equals("") || valorFac.Equals("") || img == null)
                {
                    MessageBox.Show("Llene todos los campos");
                    return;
                }
                //
                string queryVal = "select [N° Factura] from Lista_Cartera";
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(queryVal, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                for(int i = 0; i< data.Rows.Count; i++)
                {
                    var factura = data.Rows[i].ItemArray[0].ToString();
                    if (nFactura.Equals(factura))
                    {
                        MessageBox.Show("Esta factura ("+nFactura+") ya existe", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                
                con.Close();
                //
                byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
                var base64Img = System.Convert.ToBase64String(bytes).ToString();
                string queryImgCompra = "INSERT INTO Cartera VALUES(4,'" + valorFac + "','" + date + "','" + nFactura + "', '" + dateLimi + "', '"+empresa+"')";
                string queryInsertImg = "INSERT INTO FacturaCompras VALUES (" + nFactura + ",'" + base64Img + "', '"+empresa+"')";
                con.Open();
                SqlCommand cmdImgCompra = new SqlCommand(queryImgCompra, con);
                cmdImgCompra.ExecuteNonQuery();
                SqlCommand cmdInsertImg = new SqlCommand(queryInsertImg, con);
                cmdInsertImg.ExecuteNonQuery();
                con.Close();
                string queryAbonoP = "INSERT INTO Abono VALUES (" + nFactura + ", '" + valorFac + "', '0', " + date + ",'" + valorFac + "')";
                con.Open();
                SqlCommand cmdAP = new SqlCommand(queryAbonoP, con);
                cmdAP.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error en guardar registro" + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void numFact_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Tipos()
        {
            try
            {
                string empresa = "select [Nit_Company], [Name_Company] from Company";
                con.Open();
                SqlDataAdapter adapterE = new SqlDataAdapter(empresa, con);
                DataTable dataE = new DataTable();
                adapterE.Fill(dataE);
                this.cBoxEmp.DisplayMember = "Name_Company";
                this.cBoxEmp.ValueMember = "Nit_Company";
                this.cBoxEmp.DataSource = dataE;
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Tipos");
            }
        }

        
    }
}
