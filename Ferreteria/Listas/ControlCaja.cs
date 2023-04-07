using Ferreteria.Forms;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections;
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
using DataTable = System.Data.DataTable;

namespace Tienda.Listas
{
    public partial class ControlCaja : Form
    {
        public ControlCaja()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ControlCaja_Load(object sender, EventArgs e)
        {
            Listar();
            Delete();
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            Capital();
            DineroBase();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.ListaCapital.Visible = true;
            this.button2.Visible = false;
            this.button1.Visible = true;
            //  
            this.label2.Visible = true;
            this.label3.Visible = true;
            this.label4.Visible = true;
            this.label5.Visible = true;
            this.label6.Visible = true;
            this.label7.Visible = true;
            this.label8.Visible = true;
            this.label9.Visible = true;
            this.label10.Visible = true;
            this.label11.Visible = true;
            this.label12.Visible = true;
            //
            this.tBAbono.Visible = true;
            this.tBCapital.Visible = true;
            this.tBGasto.Visible = true;
            this.tBTotal.Visible = true;
            this.tBTotalF.Visible = true;
            this.tBVenta.Visible = true;
            this.dineroBase.Visible = true;
            this.ventasHoy.Visible = true;
            this.totalHoy.Visible = true;
            this.gastosHoy.Visible = true;
            this.abonosHoy.Visible = true;
            //
            dataGridView1.Visible = false;
            Capital();
        }

        private void ListaCapital_Click(object sender, EventArgs e)
        {
            this.label2.Visible = false;
            this.label3.Visible = false;
            this.label4.Visible = false;
            this.label5.Visible = false;
            this.label6.Visible = false;
            this.label7.Visible = false;
            this.label8.Visible = false;
            this.label9.Visible = false;
            this.label10.Visible = false;
            this.label11.Visible = false;
            this.label12.Visible = false;
            //
            this.tBAbono.Visible = false;
            this.tBCapital.Visible = false;
            this.tBGasto.Visible = false;
            this.tBTotal.Visible = false;
            this.tBTotalF.Visible = false;
            this.tBVenta.Visible = false;
            this.dineroBase.Visible = false;
            this.ventasHoy.Visible = false;
            this.totalHoy.Visible = false;
            this.gastosHoy.Visible = false;
            this.abonosHoy.Visible = false;
            //
            dataGridView1.Visible = true;
            dataGridView1.Dock = DockStyle.Fill;
            //
            this.button2.Visible = true;
            this.ListaCapital.Visible = false;
            this.button1.Visible = false;
            Listar();
        }

        private void Listar()
        {
            try
            {
                string query = "select c.Id as 'ID' ,tc.Tipo_Cartera as 'Tipo de registro'" +
                    ",FORMAT(CONVERT(decimal, c.Valor_Cartera), 'C', 'es-CO') as 'Valor ingreso' " +
                    ", c.Fecha as 'Fecha de ingreso'" +
                    "from Cartera as c " +
                    "inner join Tipo_Cartera as tc on (tc.Id_Cartera = c.Id_Cartera)" +
                    "where c.Id_Cartera = 6";
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
                MessageBox.Show(ex.Message, "Listar Control Caja");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Password password = new Password("");
                password.ShowDialog();
                switch (password.DialogResult)
                {
                    case DialogResult.OK:
                        IngresarCapital ingresar = new IngresarCapital();
                        ingresar.ShowDialog();
                        Total();
                        Capital();
                        break;
                    case DialogResult.Cancel:
                        break;
                    default: break;
                }       
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Button1");
            }
        }

        private void Total()
        {
            try
            {
                double capital = double.Parse(this.tBCapital.Text, NumberStyles.Currency);
                var ventas = double.Parse(this.tBVenta.Text, NumberStyles.Currency);
                var gastos = double.Parse(this.tBGasto.Text, NumberStyles.Currency);
                var abonos = double.Parse(this.tBAbono.Text, NumberStyles.Currency);

                var total = (capital + ventas) - (gastos + abonos);
                this.tBTotal.Text = total.ToString("C").Replace(",00", string.Empty);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Total");
            }
        }

        private void Capital()
        {
            try
            {
                string query = "select sum(convert(decimal, Valor_Cartera)) from Cartera where Id_Cartera = 6";
                con.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                this.tBCapital.Text = Convert.ToDouble(dataTable.Rows[0].ItemArray[0]).ToString("C").Replace(",00", string.Empty);
                con.Close();
                Ventas();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Capital");
            }
        }

        private void Ventas()
        {
            try
            {
                string queryVenta = "select sum(convert(decimal, Valor_Cartera)) from Cartera where Id_Cartera = 1  or Id_Cartera = 2 or Id_Cartera = 5";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(queryVenta, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var s = dt.Rows[0].ItemArray[0].ToString().Equals("") ? 0.00 : double.Parse(dt.Rows[0].ItemArray[0].ToString());
                this.tBVenta.Text = s.ToString("C").Replace(",00", string.Empty);
                con.Close();
                Gasto();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Venta");
            }
        }

        private void Gasto()
        {
            try
            {
                string queryVenta = "select sum(convert(decimal, Valor_Cartera)) from Cartera  where Id_Cartera = 3";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(queryVenta, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows[0].ItemArray[0].ToString().Equals(""))
                {
                    this.tBGasto.Text = 0.ToString("C").Replace(",00", string.Empty);
                }
                else
                {
                    var s = double.Parse(dt.Rows[0].ItemArray[0].ToString());
                    this.tBGasto.Text = s.ToString("C").Replace(",00", string.Empty);
                }
                con.Close();
                Facturas();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Gasto");
            }
        }

        private void Facturas()
        {
            try
            {
                string queryVenta = "select sum(convert(decimal, Valor_Cartera)) from Cartera  where Id_Cartera = 4";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(queryVenta, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows[0].ItemArray[0].ToString().Equals(""))
                {
                    this.tBTotalF.Text = 0.ToString("C").Replace(",00", string.Empty);
                }
                else
                {
                    var s = double.Parse(dt.Rows[0].ItemArray[0].ToString());
                    this.tBTotalF.Text = s.ToString("C").Replace(",00", string.Empty);
                }
                con.Close();
                Abono();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Gasto");
            }
        }

        private void Abono()
        {
            try
            {
                string queryVenta = "select sum(CONVERT(decimal, Abono)) from Abono";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(queryVenta, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                
                var s = dt.Rows[0].ItemArray[0].ToString().Equals("") ? double.Parse("0") : double.Parse(dt.Rows[0].ItemArray[0].ToString());
                this.tBAbono.Text = s.ToString("C").Replace(",00", string.Empty);
                con.Close();
                Total();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Abono");
            }
        }

        private void DineroBase()
        {
            try
            {
                var date = DateTime.Now.ToShortDateString();
                string query = "select Dinero_Base from DineroBase where Fecha like '"+date+"'";
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                var dinero = dt.Rows.Count == 0 ? double.Parse("0") : double.Parse(dt.Rows[0].ItemArray[0].ToString());
                this.dineroBase.Text = dinero.ToString("C").Replace(",00", string.Empty);
                con.Close();
                DineroVentas();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "DienroBase");
            }
        }

        private void DineroVentas()
        {
            try
            {
                var date = DateTime.Now.ToShortDateString();
                string query = "SELECT SUM(CONVERT(decimal, c.Valor_Cartera)) AS [Valor de venta] " +
                    "FROM dbo.Cartera AS c " +
                    "WHERE  (c.Fecha like '" + date + "') and (c.Id_Cartera = 1) or (c.Id_Cartera = 2) and (c.Id_Cartera = 5)";
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                var venta = dt.Rows[0].ItemArray[0].ToString().Equals("") ? double.Parse("0") : double.Parse(dt.Rows[0].ItemArray[0].ToString());
                this.ventasHoy.Text = venta.ToString("C").Replace(",00", string.Empty);
                con.Close();
                GastosHoy();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "DineroVentas ");
            }
        }

        private void GastosHoy()
        {
            try
            {
                var date = DateTime.Now.ToShortDateString();
                string query = "select sum(convert(decimal, Valor_Cartera)) from Cartera  where Fecha like '" + date + "' and  Id_Cartera = 3";
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                var gasto = dt.Rows[0].ItemArray[0].ToString().Equals("") ? double.Parse("0") : double.Parse(dt.Rows[0].ItemArray[0].ToString());
                this.gastosHoy.Text = gasto.ToString("C").Replace(",00", string.Empty);
                con.Close();
                AbonosHoy();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "GastosHoy");
            }
        }

        private void AbonosHoy()
        {
            try
            {
                string date = DateTime.Now.ToShortDateString();
                string query = "select sum(CONVERT(decimal, Abono)) from Abono where Fecha like '" + date + "'";
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                var abono = dt.Rows[0].ItemArray[0].ToString().Equals("") ? double.Parse("0") : double.Parse(dt.Rows[0].ItemArray[0].ToString());
                this.abonosHoy.Text = abono.ToString("C").Replace(",00", string.Empty);
                con.Close();
                TotalHoy();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "AbonosHoy");
            }
        }

        private void TotalHoy()
        {
            try
            {
                double dBase = double.Parse(this.dineroBase.Text, NumberStyles.Currency);
                double vBase = double.Parse(this.ventasHoy.Text, NumberStyles.Currency);
                double gBase = double.Parse(this.gastosHoy.Text, NumberStyles.Currency);
                double aBase = double.Parse(this.abonosHoy.Text, NumberStyles.Currency);
                double sumar = (dBase + vBase) - (gBase + aBase);
                this.totalHoy.Text = sumar.ToString("C").Replace(",00",string.Empty);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "TotalHoy ");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.ColumnIndex == 0)
                {
                    Password password = new Password("Control");
                    password.ShowDialog();
                    switch (password.DialogResult)
                    {
                        case DialogResult.OK:
                            var ID = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                            string queryDelete = "delete from cartera where Id = " + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(queryDelete, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            Listar();
                            break;
                        case DialogResult.Cancel:
                            break;
                        default: break;
                    }
                }
            }catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "datagridviw"+ "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete()
        {
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Eliminar";
            button.Name = "Eliminar";
            button.Text = "Eliminar";
            button.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(button);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            CambiarAdmin admin = new CambiarAdmin();
            admin.ShowDialog();
            switch (admin.DialogResult)
            {
                case DialogResult.OK:
                    MessageBox.Show("Administrador cambiado", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case DialogResult.Cancel:

                    break;
                default: break;
            }
        }
    }
}
