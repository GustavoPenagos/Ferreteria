using Ferreteria.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Tienda.Listas
{
    public partial class ListaProd : Form
    {
        public ListaProd()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaProd_Load(object sender, EventArgs e)
        {
            ListaProducto();
            Delete();
            this.buscaProd.Focus();
            dataGridView1.Columns["Codigo"].ReadOnly = true;
            dataGridView1.Columns["Nombre"].ReadOnly = false;
            dataGridView1.Columns["Marca"].ReadOnly = false;
            dataGridView1.Columns["Unidad"].ReadOnly = true;
            dataGridView1.Columns["Precio Unidad"].ReadOnly = false;
            dataGridView1.Columns["Actualizar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.selectBus.Text = "Codigo";
        }

        private void buscaProd_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            try
            {
                var selectBusc = this.selectBus.Text;

                if (this.buscaProd.Text == "")
                {
                    MessageBox.Show("Campo de busqueda vacio");
                    return;
                }
                else
                {
                    switch (selectBusc)
                    {
                        case "Codigo":
                            selectBusc = "Id";
                            ListaBusqueda(selectBusc);
                            break;
                        case "Nombre":
                            selectBusc = "Nombre";
                            ListaBusqueda(selectBusc);
                            break;
                        case "Marca":
                            selectBusc = "Marca";
                            ListaBusqueda(selectBusc);
                            break;
                        default: 
                            MessageBox.Show("No ha elegido un opcion valida"); 
                            break;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Buscar");
            }
        }

        public void ListaBusqueda(string selectBusc)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerProducto", con);
                cmd.Parameters.AddWithValue("@"+ selectBusc, this.buscarProd.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No existe un producto con este nombre (" + this.buscarProd.Text + ")");
                }
                else
                {
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListaBusqueda");
            }

        }

        private void ListaProducto()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListaProducto");
            }

        }

        private void buscarProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.selectBus.Text.Equals("ID"))
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Buscar();
            }
        }

        private void selectBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buscarProd.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Delete
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Password password = new Password("");
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                            string queryDelete = "delete from Producto  where Id = " + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryDelete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListaProducto();
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
                MessageBox.Show("datagridviw - delete " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Update
            try
            {
                if (e.ColumnIndex == 1)
                {
                    Password password = new Password("");
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:
                            string nombreProd = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                            string marca = dataGridView1.CurrentRow.Cells["Marca"].Value.ToString();
                            string precio = dataGridView1.CurrentRow.Cells["Precio Unidad"].Value.ToString();
                            //string unidad = dataGridView1.CurrentRow.Cells["Unidad"].Value.ToString();
                            
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                            //Query SQL
                            con.Open();
                            SqlCommand cmd = new SqlCommand("ActualizarProducto", con);
                            cmd.Parameters.AddWithValue("@Id", ID);
                            cmd.Parameters.AddWithValue("@Nombre", nombreProd);
                            cmd.Parameters.AddWithValue("@Marca", marca);
                            cmd.Parameters.AddWithValue("@Precio", precio);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListaProducto();
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
                        
            DataGridViewButtonColumn eliminar = new DataGridViewButtonColumn();
            eliminar.HeaderText = "Eliminar";
            eliminar.Name = "Eliminar";
            eliminar.Text = "Eliminar";
            eliminar.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(eliminar);
            
            DataGridViewButtonColumn update = new DataGridViewButtonColumn();
            update.HeaderText = "Actualizar";
            update.Name = "Actualizar";
            update.Text = "Actualizar";
            update.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(update);
        }

        private void buscarProd_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
