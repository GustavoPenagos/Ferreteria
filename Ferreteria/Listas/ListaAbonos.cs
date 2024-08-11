using Aspose.Pdf;
using Ferreteria.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Tienda.Listas
{
    public partial class ListaAbonos : Form
    {
        public ListaAbonos()
        {
            InitializeComponent();
        }

        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaAbonos_Load(object sender, EventArgs e)
        {
            ListasAbonos();
            Delete();
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }
        private void buscarFactura_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        private void Buscar()
        {
            try
            {
                var nFactura = this.numFactura.Text;
                string queryAbonos = "select * from Listado_Abono where [N° factura] = " + this.numFactura.Text;
                con.Open();
                SqlCommand cmd = new SqlCommand(queryAbonos, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                con.Close();
                if (dt.Rows.Count != 0)
                {
                    string send = "where Id_Factura = " + nFactura;
                    MostrarTotal(send);
                }
                else
                {
                    MessageBox.Show("No existe este numero de factura: " + nFactura);
                }

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Buscar");
            }
        }
        private void ListasAbonos()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerAbonos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Los abonos esta vacio!", "", MessageBoxButtons.OK);
                    return;
                }
                dataGridView1.DataSource = dt;
             
                MostrarTotal("empty");
             

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListaAbonos");
            }
        }

        private void MostrarTotal(string send)
        {
            try
            {
                this.label3.Visible = true;
                this.label4.Visible = true;
                this.total.Visible = true;
                this.sPendiente.Visible = true;
                //TOTAL ABONOS
                var queryAbonos = "select sum(convert(decimal, abono)) from Abono" ;
                con.Open();
                SqlCommand cmd = new SqlCommand(queryAbonos, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                var totales = dt.Rows[0].ItemArray[0].ToString();
                this.total.Text = totales;
                //SALDO PENDIENTE
                string queryDistinct = "select distinct Id_Factura from Abono";
                SqlCommand cmdD = new SqlCommand(queryDistinct, con);
                SqlDataReader drD = cmdD.ExecuteReader();
                DataTable dtD = new DataTable();
                dtD.Load(drD);
                
                string queryPendiente = "";
                double pendiente = 0.0;
                foreach (DataRow item in dtD.Rows)
                {
                    string numeroFacturas = item[0].ToString();
                    queryPendiente = "select  top (1) convert(decimal, Valor_Total) from Abono where Id_Factura = " + numeroFacturas;
                    SqlCommand cmdP = new SqlCommand(queryPendiente, con);
                    SqlDataReader drP = cmdP.ExecuteReader();
                    DataTable dtP = new DataTable();
                    dtP.Load(drP);
                    pendiente = pendiente + Convert.ToDouble(dtP.Rows[0].ItemArray[0].ToString());
                }
                
                var result = pendiente - Convert.ToDouble(totales);
                this.sPendiente.Text = result.ToString();
                con.Close();


            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "MostrarTotal");
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
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["N° factura"].Value.ToString();
                            string queryDelete = "delete from Abono  where Id_Factura = " + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryDelete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListasAbonos();
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

        private void numFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Buscar();
            }
        }
    }
}
