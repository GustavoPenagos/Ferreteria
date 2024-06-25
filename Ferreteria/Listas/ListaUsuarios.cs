using Ferreteria.Forms;
using iTextSharp.text;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tienda.Listas
{
    public partial class ListaUsuarios : Form
    {
        public ListaUsuarios()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaUsuarios_Load(object sender, EventArgs e)
        {
            ListarUsuarios();
            Delete();
            dataGridView1.Columns["Documento"].ReadOnly = true;
            dataGridView1.Columns["Tipo de documento"].ReadOnly = true;
            dataGridView1.Columns["Nombre usuario"].ReadOnly = false;
            dataGridView1.Columns["Tipo usuario"].ReadOnly = true;
            dataGridView1.Columns["Telefono"].ReadOnly = false;
            dataGridView1.Columns["Direccion"].ReadOnly = false;
            dataGridView1.Columns["Correo"].ReadOnly = false;
            dataGridView1.Columns["Empresa"].ReadOnly = true;
            dataGridView1.Columns["Actualizar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            //dataGridView1.Columns["Documento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridView1.Columns["Tipo de documento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridView1.Columns["Nombre usuario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridView1.Columns["Tipo usuario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridView1.Columns["Telefono"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridView1.Columns["Direccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridView1.Columns["Correo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridView1.Columns["Empresa"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.selecBus.Text = "Documento";
        }

        private void BuscarId_Click(object sender, EventArgs e)
        {
            BuscarUser();
        }

        private void BuscarUser()
        {
            try
            {
                var selectBusc = this.selecBus.Text;

                if (this.idBuscar.Text == "")
                {
                    MessageBox.Show("Campo de busqueda vacio");
                    return;
                }
                else
                {
                    switch (selectBusc)
                    {
                        case "Documento":
                            selectBusc = "Documento";
                            BuscarUsuario(selectBusc);
                            break;
                        case "Nombre":
                            selectBusc = "Nombre";
                            BuscarUsuario(selectBusc);
                            break;
                        case "Telefono":
                            selectBusc = "Telefono";
                            BuscarUsuario(selectBusc);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No ha ingresado un documento no esiste");
            }
        }

        private void BuscarUsuario(string selectBusc)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerUsuarios ", con);
                switch (selectBusc)
                {
                    case "Documento":
                        cmd.Parameters.AddWithValue("@Id", this.idBuscar.Text);
                        break;
                    case "Nombre":
                        cmd.Parameters.AddWithValue("@Nombre", this.idBuscar.Text);
                        break;
                    case "Telefono":
                        cmd.Parameters.AddWithValue("@Telefono", this.idBuscar.Text);
                        break;
                    default:
                        break;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.DataTable dataTable = new System.Data.DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                con.Close();

                //string query = " select * from Usuario where " + a.ToString() + " like '" + this.idBuscar.Text + "%'";
                //con.Open();
                //SqlCommand cmd = new SqlCommand(query, con);
                //SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                //DataTable data = new DataTable();
                //adapter.Fill(data);
                //dataGridView1.DataSource = data;

                if (dataGridView1.Rows.Count <= 0)
                {
                    MessageBox.Show("No existe un registro con este numero");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "BuscarUsuario");
            }

        }

        private void ListarUsuarios()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerUsuarios ", con); //spGetAccesosDirectoses el Stored procedure
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataReader dr = cmd.ExecuteReader();
                System.Data.DataTable dataTable = new System.Data.DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListarUsuarios");
            }

        }

        private void idBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this.selecBus.Text.Equals("Identificacion") || this.selecBus.Text.Equals("Telefono"))
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
                else if (this.selecBus.Text.Equals("Empresa"))
                {
                    e.Handled = false;
                }
                else
                {
                    if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    BuscarUser();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "idBuscar_KeyPress");
            }
        }

        private void selecBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.idBuscar.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //delete
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Password password = new Password("");
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
                            con.Open();
                            SqlCommand cmd = new SqlCommand("EliminarUsuario", con);
                            cmd.Parameters.AddWithValue("@Id", ID);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListarUsuarios();
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
            //update
            try
            {
                if (e.ColumnIndex == 1)
                {
                    Password password = new Password("");
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:

                            string Nombre = dataGridView1.CurrentRow.Cells["Nombre usuario"].Value.ToString();
                            string Telefono = dataGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
                            string Direccion = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
                            string Correo = dataGridView1.CurrentRow.Cells["Correo"].Value.ToString();

                            var ID = dataGridView1.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
                            
                            con.Open();
                            SqlCommand cmd = new SqlCommand("ActualizarUsuario", con);
                            cmd.Parameters.AddWithValue("@Id", ID);
                            cmd.Parameters.AddWithValue("@Nombre", Nombre);
                            cmd.Parameters.AddWithValue("@Telefono", Telefono);
                            cmd.Parameters.AddWithValue("@Direccion", Direccion);
                            cmd.Parameters.AddWithValue("@Correo", Correo);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListarUsuarios();
                            break;
                        case DialogResult.Cancel:
                            break;
                        default: 
                            break;
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
