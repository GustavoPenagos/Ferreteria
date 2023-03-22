using Ferreteria.Forms;
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
    public partial class ListaEmpresa : Form
    {
        public ListaEmpresa()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaEmpresa_Load(object sender, EventArgs e)
        {
            ListEmpresa();
            Delete();
            dataGridView1.Columns["NIT"].ReadOnly = true;
            //dataGridView1.Columns["Nombre"].ReadOnly = true;
            dataGridView1.Columns["Actualizar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.empComBox.Text = "Nombre";
        }
        private void buscarEmp_Click(object sender, EventArgs e)
        {
            Validar();
            BuscarEmpresa();
        }

        private void Validar()
        {
            try
            {
                if (this.bNomEmp.Text == "")
                {
                    MessageBox.Show("Campo vacio");
                }
                else
                {
                    BuscarEmpresa();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validar");
            }

        }

        private void BuscarEmpresa()
        {
            try
            {
                var buscar = empComBox.Text;
                var id = this.bNomEmp.Text;
                switch (buscar)
                {
                    case ("Nit"):
                        buscar = "where Nit = " + id;
                        ComboBoxEmp(buscar);
                        break;
                    case ("Nombre"):
                        buscar = "where Nombre like '%" + id + "%'";
                        ComboBoxEmp(buscar);
                        break;
                    case ("Productos"):
                        buscar = "where Información like '%" + id + "%'";
                        ComboBoxEmp(buscar);
                        break;
                    case ("Ciudad"):
                        buscar = "where Ciudad like '%" + id + "%'";
                        ComboBoxEmp(buscar);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BuscarEmpresa");
            }
        }

        private void ComboBoxEmp(string buscar)
        {
            try
            {
                string query = " select * from Lista_Emp " + buscar;
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;

                if (dataGridView1.Rows.Count <= 0)
                {
                    MessageBox.Show("No existe un registro con este dato");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ComboBoxEmp");
            }
        }

        private void ListEmpresa()
        {
            try
            {
                string query = "select * from Lista_Emp";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListEmpresa");
            }

        }

        private void empComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bNomEmp.Clear();
        }

        private void bNomEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (empComBox.Text.Equals("Nombre") || empComBox.Text.Equals("Ciudad"))
            {
                if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            else if (empComBox.Text.Equals("Nit"))
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Validar();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Password password = new Password();
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["NIT"].Value.ToString();
                            string queryDelete = "delete from Company where Nit_Company = " + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryDelete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListEmpresa();
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
            //UPDATE
            try
            {
                if (e.ColumnIndex == 1)
                {
                    Password password = new Password();
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:
                            string Name_Company = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                            string Products = dataGridView1.CurrentRow.Cells["Información"].Value.ToString();
                            string Direction = dataGridView1.CurrentRow.Cells["Dirección"].Value.ToString();
                            string Phone = dataGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
                            string Correo = dataGridView1.CurrentRow.Cells["Correo"].Value.ToString();
                            string ciudad = dataGridView1.CurrentRow.Cells["Ciudad"].Value.ToString();
                            string queryCiudad = "select Id_Municipality, Id_Department from Municipality where Municipality like '%" + ciudad + "%'";
                            SqlDataAdapter adapter1 = new SqlDataAdapter(queryCiudad, con);
                            DataTable data2 = new DataTable();
                            adapter1.Fill(data2);
                            if (data2.Rows.Count == 0 || data2 == null || data2.Rows.Count > 1)
                            {
                                MessageBox.Show("Ciudad no encontrada o necesita escribir el nombre completo", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            var Id_Municipaly = data2.Rows[0].ItemArray[0].ToString();
                            var Id_Department = data2.Rows[0].ItemArray[1].ToString();
                            //
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["NIT"].Value.ToString();
                            string queryUpdate = "update Company set Name_Company = '" + Name_Company + "', Products='" + Products + "', Direction='" + Direction + "'" +
                                ", Phone='" + Phone + "', Id_Municipaly=" + Id_Municipaly + ", Id_Department=" + Id_Department + "" +
                                ", Mail = '"+Correo+"' where Nit_Company = " + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryUpdate, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ListEmpresa();
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
            //
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Eliminar";
            button.Name = "Eliminar";
            button.Text = "Eliminar";
            button.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(button);
            //
            DataGridViewButtonColumn update = new DataGridViewButtonColumn();
            update.HeaderText = "Actualizar";
            update.Name = "Actualizar";
            update.Text = "Actualizar";
            update.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(update);
        }
    }
}
