using Ferreteria.Forms;
using Org.BouncyCastle.Asn1.Esf;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tienda.Listas
{
    public partial class ListaEmpresa : Form
    {
        public ListaEmpresa()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaEmpresa_Load(object sender, EventArgs e)
        {
            ListEmpresa();
            Delete();
            dataGridView1.Columns["Nit"].ReadOnly = true;
            dataGridView1.Columns["Actualizar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dataGridView1.Columns["NIT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Productos"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Telefono"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Direccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Correo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Departamento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Ciudad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            
            this.empComBox.Text = "Nit";
        }
        private void buscarEmp_Click(object sender, EventArgs e)
        {
            Validar();
            //BuscarEmpresa();
        }

        private void Validar()
        {
            try
            {
                if (this.bNomEmp.Text == "")
                {
                    MessageBox.Show("Campo vacio");
                }
                else
                {
                    BuscarEmpresa();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validar");
            }

        }

        private void BuscarEmpresa()
        {
            try
            {
                var buscar = empComBox.Text;
                var id = this.bNomEmp.Text;
                ComboBoxEmp("@"+buscar, id);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BuscarEmpresa");
            }
        }

        private void ComboBoxEmp(string buscar, string id)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerEmpresa", con);
                cmd.Parameters.AddWithValue(buscar, id);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;

                if (dataGridView1.Rows.Count <= 0)
                {
                    MessageBox.Show("No existe un registro con este dato");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ComboBoxEmp");
            }
        }

        private void ListEmpresa()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerEmpresa", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListEmpresa");
            }

        }

        private void empComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bNomEmp.Clear();
        }

        private void bNomEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (empComBox.Text.Equals("Nombre") || empComBox.Text.Equals("Ciudad"))
            {
                if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Validar();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Password password = new Password("");
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["Nit"].Value.ToString();
                            string queryDelete = "delete from Empresa where Nit = '" + ID + "'";
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryDelete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListEmpresa();
                            break;
                        case DialogResult.Cancel:
                            break;
                        default: break;
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("datagridviw" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //UPDATE
            try
            {
                if (e.ColumnIndex == 1)
                {
                    Password password = new Password("");
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:
                            string nombre = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                            string producto = dataGridView1.CurrentRow.Cells["Productos"].Value.ToString();
                            string Direccion = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
                            string telefono = dataGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
                            string ciudad = dataGridView1.CurrentRow.Cells["Ciudad"].Value.ToString();
                            string Correo = dataGridView1.CurrentRow.Cells["Correo"].Value.ToString();

                            string queryCiudad = "select id, Id_Departamento from Ciudad where Ciudad like '" + ciudad + "%'";
                            SqlDataAdapter adapter1 = new SqlDataAdapter(queryCiudad, con);
                            DataTable data2 = new DataTable();
                            adapter1.Fill(data2);
                            if (data2.Rows.Count == 0 || data2 == null || data2.Rows.Count > 1)
                            {
                                MessageBox.Show("Ciudad no encontrada o necesita escribir el nombre completo", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            var idCiudad = data2.Rows[0].ItemArray[0].ToString();
                            var idDepartamento= data2.Rows[0].ItemArray[1].ToString();
                            //
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["Nit"].Value.ToString();
                            
                            con.Open();
                            SqlCommand cmd = new SqlCommand("ActualizarEmpresa", con);
                            cmd.Parameters.AddWithValue("@Nit", ID);
                            cmd.Parameters.AddWithValue("@Nombre", nombre);
                            cmd.Parameters.AddWithValue("@Producto", producto);
                            cmd.Parameters.AddWithValue("@Telefono", telefono);
                            cmd.Parameters.AddWithValue("@Direccion", Direccion);
                            cmd.Parameters.AddWithValue("@Id_Departamento", idDepartamento);
                            cmd.Parameters.AddWithValue("@Id_Ciudad", idCiudad);
                            cmd.Parameters.AddWithValue("@Correo", Correo);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListEmpresa();
                            break;
                        case DialogResult.Cancel:
                            break;
                        default: break;
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("datagridviw - update " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete()
        {
            //
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Eliminar";
            button.Name = "Eliminar";
            button.Text = "Eliminar";
            button.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(button);
            //
            DataGridViewButtonColumn update = new DataGridViewButtonColumn();
            update.HeaderText = "Actualizar";
            update.Name = "Actualizar";
            update.Text = "Actualizar";
            update.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(update);
        }
    }
}
