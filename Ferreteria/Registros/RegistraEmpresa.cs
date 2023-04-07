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
            Tipos();
            this.NiEmp.Text = nit;
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void RegistraEmpresa_Load(object sender, EventArgs e)
        {
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
                string query = "INSERT INTO [Company] VALUES (" + this.NiEmp.Text + ",'" + this.nomEmp.Text + "','" + this.proEmp.Text + "','" + this.dirEmp.Text + "','" + this.telEmp.Text + "'," + this.selectDepart.SelectedValue + "," + this.selectCiudad.SelectedValue + ", '" + this.mail.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Tipos()
        {
            try
            {
                string departamento = "select * from Department";
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(departamento, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                this.selectDepart.DisplayMember = "Department";
                this.selectDepart.ValueMember = "Id_Department";
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
                string query = "SELECT  Municipality, Id_Municipality " +
                    "FROM Municipality " +
                    "inner join Department on(Municipality.Id_Department = Department.Id_Department) " +
                    "where Municipality.Id_Department =" + this.selectDepart.SelectedValue;
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow dr = dt.NewRow();

                this.selectCiudad.ValueMember = "Id_Municipality";
                this.selectCiudad.DisplayMember = "Municipality";
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
