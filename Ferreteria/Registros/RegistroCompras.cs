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
                var empresa = this.cBoxEmp.SelectedValue.ToString();

                if (nFactura.Equals("") || valorFac.Equals("") || img == null)
                {
                    MessageBox.Show("Llene todos los campos");
                    return;
                }
                
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 6);
                cmd.Parameters.AddWithValue("@Factura", nFactura);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();

                if (!dt.Rows[0].ItemArray[0].ToString().Equals("0")) 
                { 
                    MessageBox.Show("Esta factura ("+nFactura+") ya existe", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;    
                }

                con.Open();
                SqlCommand cmdC = new SqlCommand("InsertarCartera", con);
                cmdC.Parameters.AddWithValue("@Id_Cartera", 6);
                cmdC.Parameters.AddWithValue("@Valor_Cartera", valorFac);
                cmdC.Parameters.AddWithValue("@Fecha_Registro", DateTime.Now.ToShortDateString());
                cmdC.Parameters.AddWithValue("@Factura", nFactura);
                cmdC.Parameters.AddWithValue("@Fecha_Fin", DateTime.Now.ToShortDateString());
                cmdC.Parameters.AddWithValue("@Documento", empresa);
                cmdC.CommandType = CommandType.StoredProcedure;
                cmdC.ExecuteReader();
                con.Close();

                var base64Img = System.Convert.ToBase64String((byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]))).ToString();
                con.Open();
                SqlCommand cmdFC = new SqlCommand("InsertarFacrurasCompras", con);
                cmdFC.Parameters.AddWithValue("@Id", nFactura);
                cmdFC.Parameters.AddWithValue("@Factura", base64Img);
                cmdFC.Parameters.AddWithValue("@Nit_Empresa", empresa);
                cmdFC.CommandType = CommandType.StoredProcedure;
                cmdFC.ExecuteReader();
                con.Close();

                con.Open();
                SqlCommand cmdA = new SqlCommand("InsertarAbonos", con);
                cmdA.Parameters.AddWithValue("@Id_Factura", nFactura);
                cmdA.Parameters.AddWithValue("@Valor_Total", valorFac);
                cmdA.Parameters.AddWithValue("@Abono", 0);
                cmdA.Parameters.AddWithValue("@Fecha", DateTime.Now.ToShortDateString());
                cmdA.Parameters.AddWithValue("@Valor_Actual", valorFac);
                cmdA.CommandType = CommandType.StoredProcedure;
                cmdA.ExecuteReader();
                con.Close();

                Clear();
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
                string empresa = "SELECT [Nit], [Nombre] FROM [Empresa]";
                con.Open();
                SqlDataAdapter adapterE = new SqlDataAdapter(empresa, con);
                DataTable dataE = new DataTable();
                adapterE.Fill(dataE);
                this.cBoxEmp.DisplayMember = "Nombre";
                this.cBoxEmp.ValueMember = "Nit";
                this.cBoxEmp.DataSource = dataE;
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Tipos");
            }
        }

        private void Clear()
        {
            this.numFact.Clear();
            this.valorFact.Clear();
        }
        
    }
}
