﻿using System;
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
    public partial class ListaUsuarios : Form
    {
        public ListaUsuarios()
        {
            InitializeComponent();
        }
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void ListaUsuarios_Load(object sender, EventArgs e)
        {
            ListarUsuarios();
            Delete();
            dataGridView1.Columns["Tipo documento"].ReadOnly = true;
            dataGridView1.Columns["Identificacion"].ReadOnly = true;
            dataGridView1.Columns["Tipo de usuario"].ReadOnly = true;
            dataGridView1.Columns["Nombre de empresa"].ReadOnly = true;
            dataGridView1.Columns["Actualizar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.selecBus.Text = "Nombre";
        }

        private void BuscarId_Click(object sender, EventArgs e)
        {
            BuscarUser();
        }

        private void BuscarUser()
        {
            try
            {
                var selectBusc = this.selecBus.Text;

                if (this.idBuscar.Text == "")
                {
                    MessageBox.Show("Campo de busqueda vacio");
                    return;
                }
                else
                {
                    switch (selectBusc)
                    {
                        case "Identificacion":
                            selectBusc = "Identificacion";
                            BuscarUsuario(selectBusc.ToString());
                            break;
                        case "Nombre":
                            selectBusc = "Nombre";
                            BuscarUsuario(selectBusc.ToString());
                            break;
                        case "Empresa":
                            selectBusc = "Nombre de empresa";
                            BuscarUsuario(selectBusc.ToString());
                            break;
                        case "Telefono":
                            selectBusc = "Telefono";
                            BuscarUsuario(selectBusc.ToString());
                            break;
                        default: MessageBox.Show("No ha elegido un opcion valida"); break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No ha ingresado un documento no esiste");
            }
        }

        private void BuscarUsuario(string a)
        {
            try
            {
                string query = " select * from lista_usuario where " + a.ToString() + " like '" + this.idBuscar.Text + "%'";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;

                if (dataGridView1.Rows.Count <= 0)
                {
                    MessageBox.Show("No existe un registro con este numero");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "BuscarUsuario");
            }

        }

        private void ListarUsuarios()
        {
            try
            {
                string query1 = "SELECT *  FROM lista_usuario";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query1, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListarUsuarios");
            }

        }

        private void idBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this.selecBus.Text.Equals("Identificacion") || this.selecBus.Text.Equals("Telefono"))
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
                else if (this.selecBus.Text.Equals("Empresa"))
                {
                    e.Handled = false;
                }
                else
                {
                    if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    BuscarUser();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "idBuscar_KeyPress");
            }
        }

        private void selecBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.idBuscar.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //delete
            try
            {
                if (e.ColumnIndex == 0)
                {
                    string PassW = Microsoft.VisualBasic.Interaction.InputBox("Contraseña: ", "Datos de aprovacion para cambios");
                    if(PassW.Equals(""))
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
                        var ID = dataGridView1.Rows[e.RowIndex].Cells["Identificacion"].Value.ToString();
                        string queryDelete = "delete from [User] where Id_User = " + ID;
                        con.Open();
                        SqlCommand cmd = new SqlCommand(queryDelete, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ListarUsuarios();
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
            //update
            try
            {
                if (e.ColumnIndex == 1)
                {
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
                        //
                        string Name = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                        string Phone = dataGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
                        string Direction = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
                        string mail = dataGridView1.CurrentRow.Cells["Correo"].Value.ToString();
                        //
                        var ID = dataGridView1.Rows[e.RowIndex].Cells["Identificacion"].Value.ToString();
                        string queryUpdate = "update [User] set [Name] ='" + Name + "', Phone='" + Phone + "'" +
                            ", Direction='" + Direction + "', mail='" + mail + "' " +
                            "where Id_User = " + ID;
                        con.Open();
                        SqlCommand cmd = new SqlCommand(queryUpdate, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ListarUsuarios();
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