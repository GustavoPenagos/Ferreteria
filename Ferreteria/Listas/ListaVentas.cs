using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tienda.Listas
{
    public partial class ListaVentas : Form
    {
        public ListaVentas()
        {
            InitializeComponent();
            Delete();
            CostContol_Load();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaVentas_Load(object sender, EventArgs e)
        {
            this.selectCartera.Text = "Factura Nit";
        }

        private void CostContol_Load()
        {
            try
            {
                con.Open();
                string query = "select * from Lista_Ventas";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                double suma = 0;
                var count = dataGridView1.Rows.Count - 1;
                if (count != 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var Cart = double.Parse(dt.Rows[i].ItemArray[2].ToString(), NumberStyles.Currency);
                        suma = suma + Cart;
                    }
                    this.totalCartera.Text = suma.ToString("C").Replace(",00", string.Empty);
                }
                con.Close();
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
                selectCartera_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message, "buttonBuscar_Click");
            }
        }

        private void selectCartera_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (selectCartera.Text.Equals("Fecha de venta") || selectCartera.Text.Equals("Venta sin factura"))
                {
                    dateCartera.Visible = true;
                    tBoxBusca.Visible = false;
                    dateFin.Visible = true;
                }
                else
                {
                    dateCartera.Visible = false;
                    tBoxBusca.Visible = true;
                    dateFin.Visible = false;
                }
                string selecBox = selectCartera.Text;
                var date = dateCartera.Value.ToString("d/MM/yyyy");
                var dateF = dateFin.Value.ToString("d/MM/yyyy");
                switch (selecBox)
                {
                    case ("Factura Nit"):
                        selecBox = "[Tipo de venta] = 'Factura con Nit' and [Factura] = " + this.tBoxBusca.Text;
                        BuscarCartera(selecBox);
                        break;
                    case ("Remision"):
                        selecBox = "[Tipo de venta] = 'Remision' and [Factura] = " + this.tBoxBusca.Text;
                        BuscarCartera(selecBox);
                        break;
                    case ("Venta sin factura"):
                        selecBox = "[Tipo de venta] = 'Venta sin factura' and [Fecha de venta] between '" + date + "' and '" + dateF + "'";
                        BuscarCartera(selecBox);
                        break;
                    case ("Fecha de venta"):
                        selecBox = "[Fecha de venta] between '" + date + "' and '" + dateF + "'";
                        BuscarCartera(selecBox);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "selectCartera_SelectedIndexChanged");
            }
        }

        private void BuscarCartera(string selecBox)
        {
            try
            {
                string querySWhere = "";
                string fecha = dateCartera.Value.ToString("d/MM/yyyy");
                if (this.tBoxBusca.Text.Equals("") && dateCartera.Visible == false)
                {
                    querySWhere = "select * from Lista_Ventas";
                }
                else
                {
                    querySWhere = "select * from Lista_Ventas WHERE " + selecBox;
                }

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(querySWhere, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                double suma = 0;
                if (dataGridView1.Rows.Count != 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        var Cart = double.Parse(dt.Rows[i].ItemArray[2].ToString(), NumberStyles.Currency);
                        var totalCart = Convert.ToDouble(Cart);
                        suma = suma + totalCart;
                    }
                    this.totalCartera.Text = suma.ToString("C").Replace(",00", string.Empty);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "BuscarCartera");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    //Validacion de accion elminar
                    string PassW = Microsoft.VisualBasic.Interaction.InputBox("Contraseña: ", "Datos de aprovacion para cambios");
                    if (PassW.Equals(""))
                    {
                        return;
                    }
                    string queryPass = ConfigurationManager.AppSettings["password"];
                    SqlDataAdapter adapter = new SqlDataAdapter(queryPass, con);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    var passw = data.Rows[0].ItemArray[0].ToString();
                    //
                    if (PassW.Equals(passw))
                    {
                        var factura = dataGridView1.Rows[e.RowIndex].Cells["Factura"].Value.ToString();
                        string queryDelete = "delete from Cartera where Factura = " + factura;
                        con.Open();
                        SqlCommand cmd = new SqlCommand(queryDelete, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        CostContol_Load();
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
