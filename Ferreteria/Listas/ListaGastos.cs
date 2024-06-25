using Ferreteria.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                double totalGastos = 0;
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerGastos", con);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Parse(dateGasto.Value.ToString("dd/MM/yyyy")));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                for(int i=0; i < dt.Rows.Count; i++)
                {
                    totalGastos = totalGastos + Convert.ToDouble(dt.Rows[i].ItemArray[1].ToString());
                }
                this.textBoxTG.Text = totalGastos.ToString();
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
                var date = dateGasto.Value.ToString("dd/MM/yyyy");
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerGastos", con);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Parse(dateGasto.Value.ToString("dd/MM/yyyy")));
                cmd.CommandType = CommandType.StoredProcedure;
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
                con.Open();
                double totalGastos = 0;
                SqlCommand cmd = new SqlCommand("ObtenerGastos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                con.Close();
                dataGridView1.DataSource = dataTable;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    totalGastos = totalGastos + Convert.ToDouble(dataTable.Rows[i].ItemArray[1].ToString());
                }
                this.textBoxTG.Text = totalGastos.ToString();
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
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                            string queryDelete = "delete from Gasto where Id = " + ID;
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
