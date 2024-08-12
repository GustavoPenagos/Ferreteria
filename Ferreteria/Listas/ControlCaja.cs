using Ferreteria.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Tienda.Listas
{
    public partial class ControlCaja : Form
    {
        double sum = 0.0;
        public ControlCaja()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ControlCaja_Load(object sender, EventArgs e)
        {
            //Listar();
            Delete();
            Capital();
            DineroBase();
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.panel8.Visible = true;
            this.panel4.Visible = true;
            this.panel5.Visible = true;
            this.button3.Visible = true;
            this.button4.Visible = true;
            this.button5.Visible = true;
            this.pictureBox8.Visible = true;
            //
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
            this.panel8.Visible = false;
            this.panel4.Visible = false;
            this.panel5.Visible = false;
            this.button3.Visible = false;
            this.button4.Visible = false;
            this.button5.Visible = false;
            this.pictureBox8.Visible = false;
            //
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
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 5);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
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
                var total = (double.Parse(this.tBCapital.Text, NumberStyles.Currency) + double.Parse(this.tBVenta.Text, NumberStyles.Currency)) - (double.Parse(this.tBGasto.Text, NumberStyles.Currency) + double.Parse(this.tBAbono.Text, NumberStyles.Currency));
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
                sum = 0.0;
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 5);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                foreach(DataRow item in dt.Rows)
                {
                    sum += double.Parse(item[1].ToString(), NumberStyles.Currency); 
                }

                this.tBCapital.Text = sum.ToString("C").Replace(",00", string.Empty);
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
                sum = 0.0;
                for (int i = 2; i < 5 ; i++)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                    cmd.Parameters.AddWithValue("@id_Cartera", i);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    con.Close();
                    foreach(DataRow item in dt.Rows)
                    {
                        sum += double.Parse(item[1].ToString(), NumberStyles.Currency);
                    }
                }
                this.tBVenta.Text = sum.ToString("C").Replace(",00", string.Empty);

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
                sum = 0.0;
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 7);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();

                foreach (DataRow item in dt.Rows)
                {
                    sum += double.Parse(item[1].ToString(), NumberStyles.Currency);
                }
                this.tBGasto.Text = sum.ToString("C").Replace(",00", string.Empty);
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
                sum = 0.0;
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 6);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();

                foreach (DataRow item in dt.Rows)
                {
                    sum += double.Parse(item[1].ToString(), NumberStyles.Currency);
                }

                this.tBTotalF.Text = sum.ToString("C").Replace(",00", string.Empty);
                
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
                sum = 0.0;
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 8);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();

                foreach (DataRow item in dt.Rows)
                {
                    sum += double.Parse(item[1].ToString(), NumberStyles.Currency);
                }

                this.tBAbono.Text = sum.ToString("C").Replace(",00", string.Empty);
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
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerDienroBase", con);
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();

                double dinero = dt.Rows.Count == 0 ? double.Parse("0") : double.Parse(dt.Rows[0].ItemArray[1].ToString());
                this.dineroBase.Text = dinero.ToString("C").Replace(",00", string.Empty);

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
                sum = 0.0;
                DataTable dt = new DataTable();
                for (int i = 1; i < 5; i++)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                    cmd.Parameters.AddWithValue("@id_Cartera", i);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    con.Close();
                    foreach (DataRow item in dt.Rows)
                    {
                        if (item[3].ToString().Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                        {
                            sum += Convert.ToDouble(item[2].ToString());
                        }
                        
                    }
                }

                this.ventasHoy.Text = sum.ToString("C").Replace(",00", string.Empty);
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
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCartera", con);
                cmd.Parameters.AddWithValue("@id_Cartera", 7);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                foreach (DataRow item in dt.Rows)
                {
                    if (item[3].ToString().Equals(DateTime.Now.Date.ToString()))
                    {
                        sum += Convert.ToDouble(item[1].ToString());
                    }
                }

                this.gastosHoy.Text = sum.ToString("C").Replace(",00", string.Empty);
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
                sum = 0.0;
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerAbonos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                foreach (DataRow item in dt.Rows)
                {
                    if (item[4].ToString().Equals(DateTime.Now.Date.ToString()))
                    {
                        sum += Convert.ToDouble(item[3].ToString());
                    }
                }

                this.abonosHoy.Text = sum.ToString("C").Replace(",00", string.Empty);
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
                double sumar = (double.Parse(this.dineroBase.Text, NumberStyles.Currency) + double.Parse(this.ventasHoy.Text, NumberStyles.Currency)) - (double.Parse(this.gastosHoy.Text, NumberStyles.Currency) + double.Parse(this.abonosHoy.Text, NumberStyles.Currency));
                this.totalHoy.Text = sumar.ToString("C").Replace(",00", string.Empty);

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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                sum = 0.0;
                con.Open();
                SqlCommand cmd = new SqlCommand("PrecioXBodega", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                
                foreach(DataRow item in dt.Rows)
                {
                    sum += Convert.ToDouble(item[0].ToString());
                }
                
                DineroTotal dinero = new DineroTotal(sum.ToString());
                dinero.ShowDialog();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Suma de bodega");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                sum = 0.0;
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerUtilidadXProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                foreach(DataRow item in dt.Rows)
                {
                    sum += Convert.ToDouble(item[0].ToString());
                }

                DineroTotal dinero = new DineroTotal(sum.ToString());
                dinero.ShowDialog();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Suma de bodega");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                sum = 0.0;
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCantidadXPrecio", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();

                foreach(DataRow item in dt.Rows)
                {
                    sum += Convert.ToDouble(item[0].ToString());
                }

                DineroTotal dinero = new DineroTotal(sum.ToString());
                dinero.ShowDialog();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Suma de bodega");
            }
        }
    }
}
