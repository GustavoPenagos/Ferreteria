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
                con.Close();
                DataGridViewColumn column = dataGridView1.Columns["Cantidad"];
                dataGridView1.Sort(column, ListSortDirection.Ascending);

            }
            catch (Exception ex)
            {

            }
        }
        private DataTable DataTable()
        {
            string query1 = "SELECT top(30)*  FROM Lista_BCompra";
            SqlDataAdapter adapter = new SqlDataAdapter(query1, con);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
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

            }
        }

        private void ListaBusqueda(string selectBusc)
        {
            try
            {
                string query = "";
                switch (selectBusc)
                {
                    case "ID":
                        query = "SELECT *  FROM Lista_BCompra where " + selectBusc + " = " + this.producto.Text;
                        break;
                    default:
                        query = "SELECT *  FROM Lista_BCompra where " + selectBusc + " like '%" + this.producto.Text + "%'";
                        break;
                }
                con.Open();
                SqlDataAdapter dr = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                dr.Fill(dt);
                dataGridView1.DataSource = dt;
                if (dataGridView1.Rows.Count <= 0)
                {
                    MessageBox.Show("No existe un producto con este codigo de barras (" + this.producto.Text + ")");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, " ListaBusqueda");
            }
        }        
    }
}
