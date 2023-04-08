using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tienda.Registros
{
    public partial class RegistroUsuarios : Form
    {
        public RegistroUsuarios(string cc)
        {
            InitializeComponent();
            Tipos();
            this.numeroDocumento.Text = cc;
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void RegistroUsuarios_Load(object sender, EventArgs e)
        {
            this.numeroDocumento.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertUser();
        }

        private void InsertUser()
        {
            try
            {
                var ID = this.numeroDocumento.Text;
                var nombre = this.nombreUsuario.Text;
                var telefono = this.telefonoUsuario.Text;
                var direccion = this.direcUsuario.Text;
                var nit = this.nitEmpresa.SelectedValue;
                var typeUser = this.tipoUsuario.SelectedValue;
                var typeDoc = this.tipo_Documento.SelectedValue;
                var pass = this.contrUser.Enabled == false ? "" : this.contrUser.Text;
                var email = this.eMail.Text;
                //
                string query = "INSERT INTO [dbo].[User] VALUES (" + ID + ",'" + nombre + "','" +telefono +"','" + direccion + "'," + nit + "," + typeUser + "," + typeDoc + ",'" + pass + "','" + email + "', 0, 0)";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                DialogResult = DialogResult.OK;
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error Base de datos no guardada", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numeroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void eMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                InsertUser();
            }
            
        }

        private void tipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tipoUsuario.Text == "Cliente" || tipoUsuario.Text == "Proveedor")
            {
                this.contrUser.Enabled = false;
                this.contrUser.Clear();
            }
            else
            {
                this.contrUser.Enabled = true;
            }
        }
        private void Tipos()
        {
            try
            {
                //
                string tipoDoc = "select * from Tipo_Documento";
                con.Open();
                SqlDataAdapter adapterD = new SqlDataAdapter(tipoDoc, con);
                DataTable dataD = new DataTable();
                adapterD.Fill(dataD);
                this.tipo_Documento.DisplayMember = "documento";
                this.tipo_Documento.ValueMember = "id";
                this.tipo_Documento.DataSource = dataD;
                con.Close();
                //
                string empresa = "select [Nit_Company], [Name_Company] from Company";
                con.Open();
                SqlDataAdapter adapterE = new SqlDataAdapter(empresa, con);
                DataTable dataE = new DataTable();
                adapterE.Fill(dataE);
                this.nitEmpresa.DisplayMember = "Name_Company";
                this.nitEmpresa.ValueMember = "Nit_Company";
                this.nitEmpresa.DataSource = dataE;
                con.Close();
                //
                string tUsuario = "select * from Tipo_Usuario";
                con.Open();
                SqlDataAdapter adapterU = new SqlDataAdapter(tUsuario, con);
                DataTable dataU = new DataTable();
                adapterU.Fill(dataU);
                this.tipoUsuario.DisplayMember = "Tipo_Usuario";
                this.tipoUsuario.ValueMember = "Id_Type_User";
                this.tipoUsuario.DataSource = dataU;
                con.Close();

            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show("Error en carga de ComboBox"+ ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clear()
        {
            this.numeroDocumento.Clear();
            this.nombreUsuario.Clear();
            this.telefonoUsuario.Clear();
            this.direcUsuario.Clear();
            this.contrUser.Clear();
            this.eMail.Clear();
        }

        
    }
}
