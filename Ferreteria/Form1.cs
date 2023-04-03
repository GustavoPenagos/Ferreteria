using Ferreteria.Forms;
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

        private void uIn_Click(object sender, EventArgs e)
        {
            
            try
            {
                Vendedor vendedor = new Vendedor();
                vendedor.ShowDialog();
                switch (vendedor.DialogResult)
                {
                    case DialogResult.Yes:
                        OpenFrom(new Registros.Compras());
                        break;
                    case DialogResult.No:
                        break;
                    default: 
                        break;
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IN");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.lbHora.Text = DateTime.Now.ToString("hh:mm:ss");
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
