using Ferreteria.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Tienda.Listas
{
    public partial class ListaVentas : Form
    {
        public ListaVentas()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaVentas_Load(object sender, EventArgs e)
        {
            CostContol_Load();
            //Delete();
        }

        private void CostContol_Load()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerFacturas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                con.Close();

                double suma = 0;
                var count = dataGridView1.Rows.Count ;
                if (count != 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var Cart = double.Parse(dt.Rows[i].ItemArray[2].ToString(), NumberStyles.Currency);
                        suma = suma + Cart;
                    }
                    this.totalCartera.Text = suma.ToString("C").Replace(",00", string.Empty);
                }
                TiposFactura();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "CostControl_Load");
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerFacturas", con);

                switch (selectCartera.Text)
                {
                    case ("Compras NIT"):
                        cmd.Parameters.AddWithValue("@TipoFactura", 2);
                        cmd.Parameters.AddWithValue("@Id_Factura", tBoxBusca.Text.Trim());
                        break;
                    case ("Venta sin factura"):
                        cmd.Parameters.AddWithValue("@TipoFactura", 3);
                        cmd.Parameters.AddWithValue("@Id_Factura", tBoxBusca.Text.Trim());
                        break;
                    case ("Fecha de venta"):
                        cmd.Parameters.AddWithValue("@TipoFactura", 4);
                        cmd.Parameters.AddWithValue("@Id_Factura", tBoxBusca.Text.Trim());
                        break;
                }
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
                MessageBox.Show(ex.Message, "buttonBuscar_Click");
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
                            string factura = dataGridView1.Rows[e.RowIndex].Cells["# Factura"].Value.ToString();

                            string queryDelete = "delete from Cartera where Factura = " + factura + " and Id_Cartera = " + TiposFacturas(dataGridView1.Rows[e.RowIndex].Cells["Factura"].Value.ToString());
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryDelete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            CostContol_Load();
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

        //private void Delete()
        //{
        //    DataGridViewButtonColumn button = new DataGridViewButtonColumn();
        //    button.HeaderText = "Eliminar";
        //    button.Name = "Eliminar";
        //    button.Text = "Eliminar";
        //    button.UseColumnTextForButtonValue = true;
        //    dataGridView1.Columns.Add(button);
        //}

        private void TiposFactura()
        {
            string departamento = "select * from Tipos_Cartera where Id_Cartera in (2,3,4)";
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(departamento, con);
            DataTable data = new DataTable();
            adapter.Fill(data);
            this.selectCartera.DisplayMember = "Descripticon";
            this.selectCartera.ValueMember = "Id_Cartera";
            con.Close();
            this.selectCartera.DataSource = data;
        }
        private int TiposFacturas(string nombreF)
        {
            string departamento = "select Id_Cartera from Tipos_Cartera where Descripticon = '" + nombreF + "'";
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(departamento, con);
            DataTable data = new DataTable();
            adapter.Fill(data);
            con.Close();
            return Convert.ToInt32(data.Rows[0].ItemArray[0].ToString());

        }

    }
}
