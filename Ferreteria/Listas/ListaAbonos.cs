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
                string queryAbonos = "select * from Listado_Abono";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryAbonos, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                con.Close();
                string send = "empty";
                if (this.numFactura.Text.Equals(""))
                {
                    this.label3.Visible = false;
                    this.label4.Visible = false;
                    this.total.Visible = false;
                    this.sPendiente.Visible = false;
                }
                else
                {
                    MostrarTotal(send);
                }

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
                var queryAbonos = send.Equals("empty") ? "select format(sum(convert(decimal, abono)), 'C', 'es-CO') from Abono" : "select format(sum(convert(decimal, abono)), 'C', 'es-CO') from Abono " + send;
                con.Open();
                SqlCommand cmd = new SqlCommand(queryAbonos, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                var totales = dt.Rows[0].ItemArray[0].ToString();
                this.total.Text = totales;
                //SALDO PENDIENTE
                var queryPendiente = "select  top (1) convert(decimal, Valor_Total) from Abono " + send;
                SqlCommand cmdP = new SqlCommand(queryPendiente, con);
                SqlDataReader drP = cmdP.ExecuteReader();
                DataTable dtP = new DataTable();
                dtP.Load(drP);
                var pendiente = dtP.Rows[0].ItemArray[0].ToString();
                var total = double.Parse(totales, NumberStyles.Currency);
                var result = Convert.ToDouble(pendiente) - Convert.ToDouble(total);
                this.sPendiente.Text = result.ToString("C").Replace(",00", string.Empty);
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
