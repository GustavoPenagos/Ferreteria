using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tienda.Registros
{
    public partial class RegistraEmpresa : Form
    {
        public RegistraEmpresa(string nit)
        {
            InitializeComponent();
            this.NiEmp.Text = nit;
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void RegistraEmpresa_Load(object sender, EventArgs e)
        {
            Tipos();
            this.NiEmp.Focus();
        }

        private void GuardarEmp_Click(object sender, EventArgs e)
        {
            GuardarEmpresa();
        }

        private void GuardarEmpresa()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertarEmpresa", con);
                cmd.Parameters.AddWithValue("@Nit", this.NiEmp.Text);
                cmd.Parameters.AddWithValue("@Nombre", this.nomEmp.Text);
                cmd.Parameters.AddWithValue("@Producto", this.proEmp.Text);
                cmd.Parameters.AddWithValue("@Telefono", this.telEmp.Text);
                cmd.Parameters.AddWithValue("@Direccion", this.dirEmp.Text);
                cmd.Parameters.AddWithValue("@IdDepartamento", this.selectDepart.SelectedValue);
                cmd.Parameters.AddWithValue("@IdCiudad", this.selectCiudad.SelectedValue);
                cmd.Parameters.AddWithValue("@Correo", this.mail.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error registrando los datos " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NiEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void Tipos()
        {
            try
            {
                string departamento = "select * from Departamento";
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(departamento, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                this.selectDepart.DisplayMember = "Departamento";
                this.selectDepart.ValueMember = "Id";
                con.Close();
                this.selectDepart.DataSource = data;
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Tipos");
            }
        }

        private void selectDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCiudadXDepartamento", con);
                cmd.Parameters.AddWithValue("@IdDepartamento", this.selectDepart.SelectedValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow dr = dt.NewRow();

                this.selectCiudad.ValueMember = "Id";
                this.selectCiudad.DisplayMember = "Ciudad";
                this.selectCiudad.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Select Municipality");
            }
        }

        private void Clear()
        {
            this.NiEmp.Clear();
            this.nomEmp.Clear();
            this.proEmp.Clear();
            this.dirEmp.Clear();
            this.telEmp.Clear();
            this.mail.Clear();
        }

        private void selectCiudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                GuardarEmpresa();
            }
        }

       
    }
}
