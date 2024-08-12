using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ferreteria.Listas
{
    public partial class BalanceCompra : Form
    {
        public BalanceCompra()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private DataTable dataTable;
        private void BalanceCompra_Load(object sender, EventArgs e)
        {
            Listar();
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["Producto"].ReadOnly = true;
            dataGridView1.Columns["Marca"].ReadOnly = true;
            dataGridView1.Columns["Precio"].ReadOnly = true;
            dataGridView1.Columns["Cantidad"].ReadOnly = true;
            dataGridView1.Columns["Total"].ReadOnly = true;

            dataGridView1.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Marca"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Precio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            dataGridView1.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.selecBus.Text = "ID";
        }
        private void Listar()
        {
            try
            {
                dataTable = DataTable();
                dataGridView1.DataSource = dataTable;
                DataGridViewColumn column = dataGridView1.Columns["Cantidad"];
                dataGridView1.Sort(column, ListSortDirection.Ascending);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorr listar", ex.Message);
            }
        }
        private DataTable DataTable()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerBalanceCompra", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                return dt;

            }catch(Exception ex)
            {
                con.Close();
                MessageBox.Show("Error datatable", ex.Message);
                return null;
            }
        }
        private void buscar_Click(object sender, EventArgs e)
        {
            try
            {
                var selectBusc = this.selecBus.Text;

                if (this.producto.Text == "")
                {
                    MessageBox.Show("Campo de busqueda vacio");
                    return;
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ObtenerBalanceCompra", con);
                    switch (selectBusc)
                    {
                        case "ID":
                            cmd.Parameters.AddWithValue("@Id", this.producto.Text);
                            break;
                        case "Nombre":
                            cmd.Parameters.AddWithValue("@Nombre", this.producto.Text);
                            break;
                        case "Marca":
                            cmd.Parameters.AddWithValue("@Marca", this.producto.Text);
                            break;
                        default:
                            break;
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No existe un producto con codigo de barras: (" + this.producto.Text + ")");
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
        } 
    }
}
