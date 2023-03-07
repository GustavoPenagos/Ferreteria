using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Tienda.Registros;

namespace Tienda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            //objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            //ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
            InitializeComponent();
        }
        private void OpenFrom(object fromhijo)
        {
            try
            {
                if (this.panel10.Controls.Count > 0)
                {
                    this.panel10.Controls.RemoveAt(0);
                }
                Form fh = fromhijo as Form;
                fh.TopLevel = false;
                fh.Dock = DockStyle.Fill;
                this.panel10.Controls.Add(fh);
                this.panel10.Tag = fh;
                fh.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "OpenForm");
            }

        }
        private void carroCompra_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.Compras());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFrom(new Registros.Compras());
        }

        private void bodega_Click(object sender, EventArgs e)
        {
            OpenFrom(new Forms.Bodega());
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFrom(new Forms.Bodega());
        }

        private void proveedor_Click(object sender, EventArgs e)
        {
            OpenFrom(new Usuario());
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            OpenFrom(new Usuario());
        }

        private void gasto_Click(object sender, EventArgs e)
        {
            OpenFrom(new Forms.Gastos());
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            OpenFrom(new Forms.Gastos());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Forms.Empresa());
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            OpenFrom(new Forms.Empresa());
        }

        private void control_Click(object sender, EventArgs e)
        {
            OpenFrom(new Forms.Control());
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OpenFrom(new Forms.Control());
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //OpenFrom(new Validacion());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFrom(new Inicio());
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                MessageBox.Show("Esta ventana no se puede cerrar");
            }
        }

        private void uOut_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
            try
            {
                string ID = "";
                ID = Microsoft.VisualBasic.Interaction.InputBox("Numero de identificacion", "Datos para ventas");
                string consulta = "select estado from [User] where Id_User = " + ID + " and Id_Type_User = 3 ";
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                if(data.Rows.Count == 0)
                {
                    MessageBox.Show("No existe este vendedor");
                    return;
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
                        //MessageBox.Show("Vendedor (" + ID + ") Desactivado");
                        OpenFrom(new Registros.Compras());
                    }
                    else
                    {
                        MessageBox.Show("El vendedor ya esta desactivado");
                    }
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show("OUT " + ex.Message);
            }
        }

        private void uIn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
            try
            {
                string ID = "";
                ID = Microsoft.VisualBasic.Interaction.InputBox("Numero de identificacion", "Datos para ventas");
                string consulta = "select estado from [User] where Id_User = " + ID + " and Id_Type_User = 3 ";
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("No existe este vendedor");
                    return ;
                } 
                else
                {
                    //
                    string queryVal = "select COUNT (*) from [User] where estado = 1";
                    SqlDataAdapter adapterV = new SqlDataAdapter(queryVal, con);
                    DataTable dataV = new DataTable();
                    adapterV.Fill(dataV);
                    int can = Convert.ToInt16(dataV.Rows[0].ItemArray[0].ToString());
                    if (can > 0)
                    {
                        MessageBox.Show("Ya existe un vendedor habilitado\ndesactive el habilitado primero");
                        return;
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
                            //MessageBox.Show("Vendedor (" + ID + ") Activo");
                            OpenFrom(new Registros.Compras());
                        }
                        else
                        {
                            MessageBox.Show("El vendedor ya esta activo");
                            return;
                        }
                    }
                    //
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("IN " + ex.Message);
            }
        }
        //
        //[StructLayout(LayoutKind.Sequential)]
        //private struct KBDLLHOOKSTRUCT
        //{
        //    public Keys key;
        //    public int scanCode;
        //    public int flags;
        //    public int time;
        //    public IntPtr extra;
        //}
        ////System level functions to be used for hook and unhook keyboard input  
        //private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern bool UnhookWindowsHookEx(IntPtr hook);
        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr GetModuleHandle(string name);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern short GetAsyncKeyState(Keys key);
        ////Declaring Global objects     
        //private IntPtr ptrHook;
        //private LowLevelKeyboardProc objKeyboardProcess;

        //private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        //{
        //    if (nCode >= 0)
        //    {
        //        KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

        //        // Disabling Windows keys 

        //        if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin || objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags) || objKeyInfo.key == Keys.Escape && (ModifierKeys & Keys.Control) == Keys.Control)
        //        {
        //            return (IntPtr)1; // if 0 is returned then All the above keys will be enabled
        //        }
        //    }
        //    return CallNextHookEx(ptrHook, nCode, wp, lp);
        //}

        //bool HasAltModifier(int flags)
        //{
        //    return (flags & 0x20) == 0x20;
        //}
    }
}
