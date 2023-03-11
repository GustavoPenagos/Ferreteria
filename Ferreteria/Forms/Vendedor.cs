using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
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
using DataTable = System.Data.DataTable;

namespace Ferreteria.Forms
{
    public partial class Vendedor : Form
    {
        public Vendedor()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        private void button2_Click(object sender, EventArgs e)
        {
            Habilitar();
        }

        public void Habilitar()
        {
            try
            {
                string ID = idText.Text;
                string consulta = "select estado from [User] where Id_User = " + ID + " and Id_Type_User = 3 ";
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                if (data.Rows.Count == 0)
                {
                    msgLabel.Text = "No existe este vendedor";
                    
                }
                else
                {
                    string queryVal = "select COUNT (*) from [User] where estado = 1";
                    SqlDataAdapter adapterV = new SqlDataAdapter(queryVal, con);
                    DataTable dataV = new DataTable();
                    adapterV.Fill(dataV);
                    int can = Convert.ToInt16(dataV.Rows[0].ItemArray[0].ToString());
                    if (can > 0)
                    {
                        msgLabel.Text = "Ya existe un vendedor habilitado";
                        
                    }
                    else
                    {
                        string estado = data.Rows[0].ItemArray[0].ToString();

                        if (estado.Equals("0"))
                        {
                            string query = "update [User] set estado = 1 where Id_User = " + ID;
                            con.Open();
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            msgLabel.Text = "El vendedor ya esta activo";
                            
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Habilitar/Desavilitar");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ID = idText.Text;
                string consulta = "select estado from [User] where Id_User = " + ID + " and Id_Type_User = 3 ";
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                if (data.Rows.Count == 0)
                {
                    msgLabel.Text = "No existe este vendedor";
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    string estado = data.Rows[0].ItemArray[0].ToString();
                    if (estado.Equals("1"))
                    {
                        string query = "update [User] set estado = 0 where Id_User = " + ID;
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        msgLabel.Text = "El vendedor ya esta desactivado";
                        DialogResult = DialogResult.Cancel;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
