using Microsoft.Office.Interop.Word;
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
using DataTable = System.Data.DataTable;

namespace Tienda.Registros
{
    public partial class RegistroProd : Form
    {
        public RegistroProd()
        {
            InitializeComponent();
            Unidades();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void RegistroProd_Load(object sender, EventArgs e)
        {
            this.idProd.Focus();
        }

        private void guardarProd_Click(object sender, EventArgs e)
        {
            InsertarProd();
        }

        private void InsertarProd()
        {
            try
            {
                double util = Convert.ToDouble(this.utilidad.Text);
                double precProd = Convert.ToDouble(this.precioProd.Text);
                double precVenta = Math.Round(((util / 100) + 1) * precProd);
                var ID = this.idProd.Text;
                var producto = this.nomProd.Text;
                var unidad = this.unidProd.SelectedValue;
                var marca = this.marcaProd.Text;
                var utilidad = this.utilidad.Text;
                var cantidad = this.txbCantidad.Text;
                string query = "INSERT INTO Producto VALUES (" + ID + ",'" + producto + "'," + precProd + "," + unidad + ", '" + marca + "','" + utilidad + "','" + precVenta + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
                string queryInsertCant = "insert into Bodega values (" + ID + ", '" + cantidad + "')";
                SqlCommand cmdInsert = new SqlCommand(queryInsertCant, con);
                con.Open();
                cmdInsert.ExecuteNonQuery();
                con.Close();
                
                string queryCantidad = "SELECT cantidad from Bodega where Id_Prod = " + this.idProd.Text + "";
                SqlDataAdapter adapter = new SqlDataAdapter(queryCantidad, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                cantidad = data.Rows[0].ItemArray[0].ToString();
                Clear();
                this.txtCantidad.Text = cantidad;
                this.idProd.Focus();
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Error registro no guardado" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Unidades()
        {
            try
            {
                string unidad = "Select * from Unidad";
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(unidad, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                this.unidProd.DisplayMember = "Unidad";
                this.unidProd.ValueMember= "Id_Unidad";
                this.unidProd.DataSource = data;
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Unidades");
            }
        }

        private void idProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                InsertarProd();
            }
        }

        private void Clear()
        {
            this.utilidad.Clear();
            this.precioProd.Clear();
            this.idProd.Clear(); ;
            this.nomProd.Clear();
            this.marcaProd.Clear();
            this.utilidad.Clear();
            this.txbCantidad.Clear();
        }

        private void utilidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.utilidad.Text.Equals(""))
                {
                    double util = Convert.ToDouble(this.utilidad.Text);
                    double precProd = Convert.ToDouble(this.precioProd.Text);
                    double precVenta = Math.Round(((util / 100) + 1) * precProd);
                    this.precioFinal.Text = precVenta.ToString("C");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("El campo de precios no puede estar vacio");
                MessageBox.Show(ex.Message, "Caluculo de precio");
            }
        }

        private void idProd_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
