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
using Tienda.Model;

namespace Tienda.Listas
{
    public partial class ListaFacturas : Form
    {
        public ListaFacturas()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaFacturas_Load(object sender, EventArgs e)
        {
            ListarFacturas();
            Delete();
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.boxSelect.Text = "Numero factura";
        }
        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            try
            {
                var select = this.boxSelect.Text;
                var valor = this.boxBuscar.Text;
                string complemento = "";
                switch (select)
                {
                    case "Numero factura":
                        select = "[N° Factua]";
                        complemento = "=";
                        ListarBuscar(select, valor, complemento);
                        break;
                    case "Fecha recibido":
                        select = "[Fecha recibido]";
                        complemento = "like";
                        ListarBuscar(select, valor, complemento);
                        break;
                    default: break;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ButtonBuscar");
            }
        }

        private void ListarBuscar(string select, string valor, string complemento)
        {
            try
            {
                string query = "";
                if (complemento.Equals("like"))
                {
                    query = "select * from Lista_Cartera where " + select + " like '" + valor + "'";
                }
                else
                {
                    query = "select * from Lista_Cartera where " + select + " = " + valor;
                }
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();


            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListarBuscar");
            }
        }

        private void ListarFacturas()
        {
            try
            {
                string query = "select * from Lista_Cartera";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListarFacturas");
            }
        }

        private void boxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxSelect.Text.Equals("Numero factura"))
            {
                this.boxBuscar.Visible = true;
                this.dateTimePicker1.Visible = false;
            }
            else if (boxSelect.Text.Equals("Fecha recibido"))
            {
                this.boxBuscar.Visible = false;
                this.dateTimePicker1.Visible = true;
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
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["Factura"].Value.ToString();
                            string queryDelete = "delete from Cartera where Factura = " + ID + "and Id_Cartera = 4";
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryDelete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListarFacturas();
                            break;
                        case DialogResult.Cancel:
                            break;
                        default: break;
                    }
                }
            }
            catch (Exception ex)
            {
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

        private void boxBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Buscar();
            }
        }
    }
}
