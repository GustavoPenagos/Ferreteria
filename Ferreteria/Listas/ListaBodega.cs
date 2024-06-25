using Ferreteria.Forms;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using iTextSharp.text.xml.simpleparser.handler;
using System.Data.Odbc;

namespace Tienda.Listas
{
    public partial class ListaBodega : Form
    {
        private DataTable dataTable;
        public ListaBodega()
        {
            InitializeComponent();
            
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaBodega_Load(object sender, EventArgs e)
        {

            Listar();
            Delete();
            dataGridView1.Columns["Codigo"].ReadOnly= true;
            dataGridView1.Columns["Nombre"].ReadOnly = true;
            dataGridView1.Columns["Marca"].ReadOnly = true;
            dataGridView1.Columns["Cantidad"].ReadOnly = false;
            dataGridView1.Columns["Unidad"].ReadOnly = true;
            dataGridView1.Columns["Precio"].ReadOnly = true;
            dataGridView1.Columns["Actualizar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dataGridView1.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            //dataGridView1.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Marca"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Unidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.selecBus.Text = "Codigo";
            this.nombreProducto.Focus();
        }
        private void verProducto_Click(object sender, EventArgs e)
        {
            BuscarEnBodega();
        }

        private void BuscarEnBodega()
        {
            try
            {
                var selectBusc = this.selecBus.Text;

                if (this.nombreProducto.Text == "")
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
                            ListaBusqueda(selectBusc.ToString());
                            break;
                        case "Marca":
                            selectBusc = "Marca";
                            ListaBusqueda(selectBusc.ToString());
                            break;
                        default: MessageBox.Show("No ha elegido un opcion valida"); 
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "BuscarEnBodega");
            }
        }

        private void ListaBusqueda(string selectBusc)
        {
            try
            {
                string query = "";
                switch (selectBusc)
                {
                    case "Id":
                        query = "select p.Id [Codigo], p.Nombre, p.Marca, b.Cantidad, u.Unidad, p.Precio_Final [Precio] from Producto p inner join Bodega b on b.Id_Producto = p.Id inner join Unidades u on u.Id = p.Id_Unidad where p." + selectBusc + " = " + this.nombreProducto.Text;
                        break;
                    case "Nombre":
                        query = "select p.Id [Codigo], p.Nombre, p.Marca, b.Cantidad, u.Unidad, p.Precio_Final [Precio] from Producto p inner join Bodega b on b.Id_Producto = p.Id inner join Unidades u on u.Id = p.Id_Unidad where p." + selectBusc + " = '" + this.nombreProducto.Text + "'";
                        break;
                    case "Marca":
                        query = "select p.Id [Codigo], p.Nombre, p.Marca, b.Cantidad, u.Unidad, p.Precio_Final [Precio] from Producto p inner join Bodega b on b.Id_Producto = p.Id inner join Unidades u on u.Id = p.Id_Unidad where p." + selectBusc + " = '" + this.nombreProducto.Text + "'";
                        break;
                    default:
                        query = "select top 20 p.Id [Codigo], p.Nombre, p.Marca, b.Cantidad, u.Unidad, p.Precio_Final [Precio] from Producto p inner join Bodega b on b.Id_Producto = p.Id inner join Unidades u on u.Id = p.Id_Unidad";
                        break;
                }                    
                con.Open();
                SqlDataAdapter dr = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                dr.Fill(dt);
                dataGridView1.DataSource = dt;
                if (dataGridView1.Rows.Count <= 0)
                {
                    MessageBox.Show("No existe un producto con este codigo: (" + this.nombreProducto.Text + ")");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, " ListaBusqueda");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void Listar()
        {
            try
            {
                dataTable = DataTable();
                                
                dataGridView1.DataSource = dataTable;
                con.Close();
                DataGridViewColumn column = dataGridView1.Columns["Cantidad"];
                dataGridView1.Sort(column, ListSortDirection.Ascending);

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "");
            }

        }

        private DataTable DataTable()
        {
            string query1 = "select top 20 p.Id [Codigo], p.Nombre, p.Marca, b.Cantidad, u.Unidad, p.Precio_Final [Precio] from Producto p inner join Bodega b on b.Id_Producto = p.Id inner join Unidades u on u.Id = p.Id_Unidad";
            SqlDataAdapter adapter = new SqlDataAdapter(query1, con);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        private void selecBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.nombreProducto.Clear();
        }

        private void nombreProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.selecBus.Text.Equals("Codigo"))
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BuscarEnBodega();
            }
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
                            string queryDelete = "delete from Bodega where Id_Producto  = " + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryDelete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            Listar();
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
                MessageBox.Show("Contraseña incorrecta" , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            var cantidad = dataGridView1.CurrentRow.Cells["Cantidad"].Value.ToString();
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                            string queryUpdate = "update Bodega set cantidad = " + cantidad + " where Id_Producto =" + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryUpdate, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            Listar();
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
            //Delete
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Eliminar";
            button.Name = "Eliminar";
            button.Text = "Eliminar";
            button.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(button);
            //Update
            DataGridViewButtonColumn update = new DataGridViewButtonColumn();
            update.HeaderText = "Actualizar";
            update.Name = "Actualizar";
            update.Text = "Actualizar";
            update.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(update);
        }

    }
}
