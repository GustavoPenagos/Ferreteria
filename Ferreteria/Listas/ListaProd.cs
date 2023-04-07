using Ferreteria.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["Unidad"].ReadOnly = true;
            dataGridView1.Columns["Precio Venta"].ReadOnly = true;
            dataGridView1.Columns["Actualizar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dataGridView1.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Marca"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Precio Compra"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Utilidad (%)"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Precio Venta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Unidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            this.selectBus.Text = "ID";
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
                        case "ID":
                            selectBusc = "ID";
                            ListaBusqueda(selectBusc.ToString());
                            break;
                        case "Nombre":
                            selectBusc = "Producto";
                            ListaBusqueda(selectBusc.ToString());
                            break;
                        case "Marca":
                            selectBusc = "Marca";
                            ListaBusqueda(selectBusc.ToString());
                            break;
                        default: MessageBox.Show("No ha elegido un opcion valida"); break;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Buscar");
            }
        }

        public void ListaBusqueda(string a)
        {
            try
            {
                string query = "select * from lista_producto where " + a.ToString() + " like '" + this.buscarProd.Text + "%'";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                con.Close();
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No existe un producto con este nombre (" + this.buscaProd.Text + ")");
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
                string query1 = "SELECT *  FROM lista_producto";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query1, con);
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
            //DELETE
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Password password = new Password("");
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                            string queryDelete = "delete from Producto  where Id_Prod  = " + ID;
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
                            string Nombre_Prod = dataGridView1.CurrentRow.Cells["Producto"].Value.ToString();
                            string Marca = dataGridView1.CurrentRow.Cells["Marca"].Value.ToString();
                            string p1 = dataGridView1.CurrentRow.Cells["Precio Compra"].Value.ToString();
                            double Precio_Prod = double.Parse(p1, NumberStyles.Currency);
                            string Utilidad = dataGridView1.CurrentRow.Cells["Utilidad (%)"].Value.ToString();
                            double Prec_Venta = Math.Round(((double.Parse(Utilidad) / 100) + 1) * Precio_Prod);
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                            string queryUpdate = "UPDATE Producto SET Nombre_Prod = '" + Nombre_Prod + "'" +
                                ", Precio_Prod = '" + Precio_Prod + "', Marca = '" + Marca + "', Utilidad = '" + Utilidad + "' " +
                                ", Prec_Venta = '" + Prec_Venta + "' where Id_Prod  = " + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryUpdate, con);
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
