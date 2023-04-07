using Ferreteria.Forms;
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

namespace Tienda.Listas
{
    public partial class ListaGastos : Form
    {
        public ListaGastos()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaGastos_Load(object sender, EventArgs e)
        {
            ListaGasto();
            Delete();
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }
        private void BuscarGasto_Click(object sender, EventArgs e)
        {
            try
            {
                Validar();
                string query = "select format(sum(convert(decimal,Valor_Gasto)), 'C', 'es-co') AS Valor_Gasto from Lista_Gastos";
                con.Open();
                //SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                ad.Fill(dt);
                con.Close();
                this.textBoxTG.Text = dt.Rows[0].ItemArray[0].ToString();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "BuscarGasto_Click");
            }
            
        }

        private void Validar()
        {
            try
            {
                var date = dateGasto.Value.ToString("dd/MM/yyyy");
                if (date == "")
                {
                    MessageBox.Show("Campo vacio");
                }
                else
                {
                    BuscarGastos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validar");
            }
        }

        private void BuscarGastos()
        {
            try
            {
                var date = dateGasto.Value.ToString("d/MM/yyyy");

                string query = "SELECT * FROM Lista_Gastos where [Fecha_Gasto] like '%" + date + "%'";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                con.Close();
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No existe gastos en esta fecha (" + date + ")");
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "BuscarGastos");
            }
        }

        private void ListaGasto()
        {
            try
            {
                string query1 = "SELECT Id_Gasto AS ID, Desc_Gastos AS Descripcion, format(convert(decimal,Costo_Gasto),'C','es-CO') AS Valor_Gasto, Fecha_Gasto FROM dbo.Gastos";
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query1, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Validar");
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
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                            string queryDelete = "delete from Gastos where Id_Gasto = " + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryDelete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListaGasto();
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
        }
    }
}
