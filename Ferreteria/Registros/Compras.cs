using DistribucionesArly_s;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Tienda.Model;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Application = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;
using Form = System.Windows.Forms.Form;
using System.IO;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Threading;
using Ferreteria.Forms;
using Ferreteria.Model;
using Aspose.Pdf.Operators;
 using Org.BouncyCastle.Utilities.Collections;
using Newtonsoft.Json;
using System.Data;
using Org.BouncyCastle.Crypto.Macs;

namespace Tienda.Registros
{
    public partial class Compras : Form
    {
        public Compras()
        {
            InitializeComponent();
        }

        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);

        private void Compras_Load(object sender, EventArgs e)
        {
            try
            {
                Disable();

                string queryE = "select Nombre from Usuario where estado = 1";
                SqlDataAdapter adapterE = new SqlDataAdapter(queryE, con);
                DataTable dataE = new DataTable();
                adapterE.Fill(dataE);
                if (dataE.Rows.Count == 0)
                {
                    MessageBox.Show("Primero habilite un vendedor");
                    return;
                }
                else
                {
                    this.vendedor.Text = dataE.Rows[0].ItemArray[0].ToString();
                }
                IngresarDinero();
                string queryConsulta = "SELECT Dinero FROM DineroBase WHERE Fecha_Registro LIKE '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                SqlDataAdapter ad = new SqlDataAdapter(queryConsulta, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    string queryP = "select * from Producto order by Nombre asc";
                    SqlDataAdapter adapterD = new SqlDataAdapter(queryP, con);
                    DataTable dataD = new DataTable();
                    adapterD.Fill(dataD);
                    this.cBNombre.DisplayMember = "Nombre";
                    this.cBNombre.ValueMember = "Id";
                    this.cBNombre.DataSource = dataD;
                    Delete();
                    
                    
                    DatosCompra();
                    ListaCompra();
                    this.idProdC.Focus();
                    NumeroCotizacion();
                    Enable();
                    dataGridView2.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    //dataGridView2.Columns["ID"].Visible = false;
                    //dataGridView2.Columns["# Venta"].Visible = false;
                    //dataGridView2.Columns["# Venta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                    //dataGridView2.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    //dataGridView2.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                    //dataGridView2.Columns["Precio Unidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                    //dataGridView2.Columns["Tipo Unidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                    
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LOAD");
            }

        }

        private void butBusComp_Click(object sender, EventArgs e)
        {
            try
            {
                CompareExistente();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "butBusComp_Click");
            }
        }

        private void CompareExistente()
        {
            try
            {
                long ID = 0;
                if (this.canProd.Text.Equals(""))
                {
                    MessageBox.Show("Campo cantidad esta vacio"); 
                    return;
                }
                SqlCommand cmdBodega = new SqlCommand("ObtenerBodega", con);
                if (this.rBId.Checked == true)
                {
                    cmdBodega.Parameters.AddWithValue("@Id", this.idProdC.Text);
                    ID = Convert.ToInt64(this.idProdC.Text);
                }
                if (this.rBNombre.Checked == true)
                {
                    if (this.canProd.Text.Equals(""))
                    {
                        MessageBox.Show("Campo cantidad esta vacio"); return;
                    }
                    
                    cmdBodega.Parameters.AddWithValue("@Id", this.cBNombre.SelectedValue);
                    ID = Convert.ToInt64(this.cBNombre.SelectedValue);

                }
                cmdBodega.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                con.Open();
                SqlDataReader dr = cmdBodega.ExecuteReader();
                dt.Load(dr);
                con.Close();
                var existe = Convert.ToDouble(dt.Rows[0].ItemArray[3].ToString());
                var cantidad = Convert.ToDouble(this.canProd.Text);
                var result = existe - cantidad;

                if (result < 0)
                {
                    MessageBox.Show("El maximo de articulos :" + existe + "", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    CantidadData(existe, cantidad, ID, result);

                    //if (result >= 0)
                    //{
                        
                    //}
                    //else
                    //{
                    //    MessageBox.Show("El numero maximo de articulos en bodega es: " + existe + "", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Producto sin registro en bodega");
            }

        }

        private void CantidadData(double existe, double cantidad, long id, double result)
        {
            try
            {
                int count = Convert.ToInt32(dataGridView2.Rows.Count);
                if (existe >= cantidad)
                {
                    if (count == 0)
                    {
                        ListaProd();
                    }
                    else
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            var k = i + 1;
                            var ids = Convert.ToInt64(dataGridView2.Rows[i].Cells[1].Value);
                            if (ids == id)
                            {
                                int lastRow = count - 1;
                                var cellCant = Convert.ToInt64(dataGridView2.Rows[i].Cells[5].Value);
                                var cellID = Convert.ToInt64(dataGridView2.Rows[i].Cells[1].Value);

                                if (cellID == id && cellCant <= existe)
                                {
                                    VaidarDataGridView(lastRow, cellCant, cellID, id);
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("La cantidad en bodega es: " + existe + " unidades", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else if (k == dataGridView2.Rows.Count)
                            {
                                ListaProd();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El maximo de articulos disponibles es: " + existe + " Unidades", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "CantidadData");
            }
        }

        private void VaidarDataGridView(int lastRow, long cellCant, long cellID, long idText)
        {
            try
            {
                //ultimo fila (index), cantidad de la fila, id de la fila, id en la caja de texto
                con.Open();
                SqlCommand cmdd = new SqlCommand("ObtenerBodega", con);
                cmdd.Parameters.AddWithValue("@Id", idText);
                cmdd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmdd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                
                var cantidad = Convert.ToInt32(dt.Rows[0].ItemArray[3].ToString());
                var idp = Convert.ToInt64(dt.Rows[0].ItemArray[0].ToString());
                con.Close();
                double result = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (Convert.ToInt64(dataGridView2.Rows[i].Cells[1].Value) == idp)
                    {
                        double cant = Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value);
                        result = result == 0 ? Convert.ToDouble(this.canProd.Text) + cant + result : cant + result;
                    }
                    
                }
                if (result <= cantidad)
                {
                    ListaProd();
                    return;
                }
                else if (result > cantidad)
                {
                    MessageBox.Show("El maximo de articulos disponibles es: " + cantidad + " Unidades", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "VaidarDataGridView ");
            }
        }

        private void DatosCompra()
        {
            try
            {
                if (rBImprimir.Checked == false)
                {
                    rBFactNit.Visible = false;
                    rBRemision.Visible = false;
                    rBNImprimir.Visible = false;
                }
                var value1 = this.ventaBut1.Checked; var value2 = this.ventaBut2.Checked;
                var value3 = this.ventaBut3.Checked; var value4 = this.ventaBut4.Checked;
                
                string v = "";
                switch (true)
                {
                    case true when value1:
                        v = "1";
                        break;
                    case true when value2:
                        v = "2";
                        break;
                    case true when value3:
                        v = "3";
                        break;
                    case true when value4:
                        v = "4";
                        break;
                    default: break;
                }

                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCompras", con);
                cmd.Parameters.AddWithValue("@Id_Compra", v);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                if(dt.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt;
                }
                else
                {
                    return;
                } 
                double total = 0 ;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total += Convert.ToDouble(dt.Rows[i].ItemArray[5].ToString());
                }
                if (dataGridView2 == null)
                {
                    this.totalVenta.Text = "";
                }
                else
                {
                    //var tv = (0.19 * st) + st;
                    this.totalVenta.Text = total.ToString();
                }
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "DatosCompra");
            }
        }

        private void ListaCompra()
        {
            this.canProd.Text = "1";
            try
            {
                var value1 = this.ventaBut1.Checked; var value2 = this.ventaBut2.Checked;
                var value3 = this.ventaBut3.Checked; var value4 = this.ventaBut4.Checked;
                //
                string v = "";
                switch (true)
                {
                    case true when value1:
                        v = "1";
                        break;
                    case true when value2:
                        v = "2";
                        break;
                    case true when value3:
                        v = "3";
                        break;
                    case true when value4:
                        v = "4";
                        break;
                    default: break;
                }

                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCompras", con);
                cmd.Parameters.AddWithValue("@Id_Compra", v);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt;
                    double totalVenta = 0;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        totalVenta += double.Parse(dataGridView2.Rows[i].Cells["Precio total"].Value.ToString());
                    }
                    this.totalVenta.Text = totalVenta.ToString();
                }
                else
                {
                    dataGridView2.DataSource = null;
                    this.totalVenta.Clear();
                }
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListaCompra");
            }

        }

        private void ListaProd()
        {
            try
            {
                long ID = 0;
                SqlCommand cmd = new SqlCommand("ObtenerBodega", con);
                if (this.rBId.Checked == true)
                {
                    if (this.canProd.Text.Equals("") || this.idProdC.Text.Equals(""))
                    {
                        MessageBox.Show("Campos vacios");
                        return;
                    }
                    ID = Convert.ToInt64(this.idProdC.Text);
                    cmd.Parameters.AddWithValue("@Id", this.idProdC.Text);
                }
                if (this.rBNombre.Checked == true)
                {
                    ID = Convert.ToInt64(this.cBNombre.SelectedValue);
                    cmd.Parameters.AddWithValue("@Id", this.cBNombre.SelectedValue);
                }
                //
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                var id_prod = dt.Rows[0].ItemArray[0].ToString();
                var nombre = dt.Rows[0].ItemArray[1].ToString();
                var precio = dt.Rows[0].ItemArray[5].ToString();
                var unidades = dt.Rows[0].ItemArray[4].ToString();
                var cantidad = Convert.ToDouble(this.canProd.Text);
                //07-012-024
                var precioTotal = cantidad * Convert.ToDouble(precio);
                //
                con.Close();
                var value1 = this.ventaBut1.Checked; var value2 = this.ventaBut2.Checked;
                var value3 = this.ventaBut3.Checked; var value4 = this.ventaBut4.Checked;
                //
                string v = "";
                switch (true)
                {
                    case true when value1:
                        v = "1";
                        break;
                    case true when value2:
                        v = "2";
                        break;
                    case true when value3:
                        v = "3";
                        break;
                    case true when value4:
                        v = "4";
                        break;
                    default: break;
                }
                con.Open();
                string QueryValida = "select * from compras where Id_Producto = " + id_prod + " and Id_Venta = " + v;
                SqlDataAdapter adV = new SqlDataAdapter(QueryValida, con);
                DataTable dtV = new DataTable();
                adV.Fill(dtV);
                
                if (dtV.Rows.Count > 0)
                {
                    SqlCommand cmdA = new SqlCommand("ActualizarCompras", con);
                    cmdA.Parameters.AddWithValue("@Id", id_prod);
                    cmdA.Parameters.AddWithValue("@Id_Ventas", v);
                    cmdA.Parameters.AddWithValue("@Cantidad", this.canProd.Text);
                    cmdA.CommandType = CommandType.StoredProcedure;
                    cmdA.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdC = new SqlCommand("InsertarCompras", con);
                    cmdC.Parameters.AddWithValue("@Id", id_prod);
                    cmdC.Parameters.AddWithValue("@Nombre", nombre);
                    cmdC.Parameters.AddWithValue("@Precio", precio);
                    cmdC.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmdC.Parameters.AddWithValue("@Unidad_Producto", unidades);
                    cmdC.Parameters.AddWithValue("@Id_Venta", v);
                    cmdC.CommandType = CommandType.StoredProcedure;
                    cmdC.ExecuteNonQuery();
                }                
                con.Close();

                ListaCompra();
                this.idProdC.Text = "";
                this.idProdC.Focus();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ListaProd_");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.canProd.Text.Equals(""))
                {
                    MessageBox.Show("Campo candidad esta vacio");
                    return;
                }
                var cantidad = Convert.ToInt64(this.canProd.Text);
                
                var value1 = this.ventaBut1.Checked; var value2 = this.ventaBut2.Checked;
                var value3 = this.ventaBut3.Checked; var value4 = this.ventaBut4.Checked;
                //
                string v = "";
                switch (true)
                {
                    case true when value1:
                        v = "1";
                        break;
                    case true when value2:
                        v = "2";
                        break;
                    case true when value3:
                        v = "3";
                        break;
                    case true when value4:
                        v = "4";
                        break;
                    default: break;
                }
                long numericValue;
                bool isNumber = long.TryParse(this.idProdC.Text, out numericValue);
                con.Open();
                SqlCommand cmdB = new SqlCommand("EliminarCompras", con);
                if (!isNumber)
                {
                    cmdB.Parameters.AddWithValue("@Id", this.cBNombre.SelectedValue);
                }
                else
                {
                    cmdB.Parameters.AddWithValue("@Id", this.idProdC.Text);
                }
                cmdB.Parameters.AddWithValue("@Id_Venta", Convert.ToInt16(v));
                cmdB.CommandType = CommandType.StoredProcedure;
                cmdB.ExecuteNonQuery();

                con.Close();
                this.idProdC.Text = "";
                this.canProd.Text = "1";
                idProdC.Focus();

                ListaCompra();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "button1_Click");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var value1 = this.ventaBut1.Checked; var value2 = this.ventaBut2.Checked;
                var value3 = this.ventaBut3.Checked; var value4 = this.ventaBut4.Checked;
                string query = "";
                //
                string v = "";
                switch (true)
                {
                    case true when value1:
                        v = "1";
                        break;
                    case true when value2:
                        v = "2";
                        break;
                    case true when value3:
                        v = "3";
                        break;
                    case true when value4:
                        v = "4";
                        break;
                    default: break;
                }
                con.Open();
                
                SqlCommand cmd = new SqlCommand("EliminarCompras", con);
                cmd.Parameters.AddWithValue("@Id_Venta", v);
                cmd.Parameters.AddWithValue("@Id", 0);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Clear();
                con.Close();
                ListaCompra();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "vaciar");
            }
        }

        private void fCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count == 0)
                {
                    AbrirCajon.AbrirCaja imp = new AbrirCajon.AbrirCaja();
                    string impresora = ConfigurationManager.AppSettings["empresora"];
                    imp.ImprimirTiket(impresora);
                }
                else
                {
                    double total = double.Parse(this.totalVenta.Text, NumberStyles.Currency);
                    double cancela = this.cancelaCon.Text.Equals("") ? total : double.Parse(this.cancelaCon.Text);
                    if (total > cancela)
                    {
                        MessageBox.Show(cancela + " es menor a la venta de " + total);
                        return;
                    }
                    FinVenta finVenta = new FinVenta();
                    finVenta.ShowDialog();
                    switch (finVenta.DialogResult)
                    {
                        case DialogResult.Yes:
                            var resultImp = this.rBImprimir.Checked;
                            switch (resultImp)
                            {
                                case (true):
                                    SeleccionImp("yes");
                                    break;
                                case (false):
                                    SeleccionImp("no");
                                    break;
                                default: break;
                            }
                            break;
                        case DialogResult.No:
                            break;
                        default: break;
                    }

                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "fCompra_Click");
            }
        }

        private void SeleccionImp(string a)
        {
            try
            {
                if (a.Equals("yes"))
                {
                    var resultImpNit = this.rBFactNit.Checked;
                    var resultImpREM = this.rBRemision.Checked;
                    var resultImpSD = this.sinDatos.Checked;
                    if (resultImpNit == true)
                    {
                        FacturacionNit();
                    }
                    else if (resultImpREM == true)
                    {
                        FacturacionRem();
                    } else if (resultImpSD == true)
                    {
                        FacturaSinDatos();
                    }
                }
                else
                {
                    con.Open();
                    SqlCommand cmdC = new SqlCommand("InsertarCartera", con);
                    cmdC.Parameters.AddWithValue("@Id_Cartera", 1);
                    cmdC.Parameters.AddWithValue("@Valor_Cartera", this.totalVenta.Text);
                    cmdC.Parameters.AddWithValue("@Fecha_Registro", DateTime.Now.ToShortDateString());
                    cmdC.Parameters.AddWithValue("@Factura", 0);
                    cmdC.Parameters.AddWithValue("@Fecha_Fin", DateTime.Now.ToShortDateString());
                    cmdC.Parameters.AddWithValue("@Documento", 0);
                    cmdC.CommandType = CommandType.StoredProcedure;
                    cmdC.ExecuteReader();
                    con.Close();
                }
                var value1 = this.ventaBut1.Checked; var value2 = this.ventaBut2.Checked;
                var value3 = this.ventaBut3.Checked; var value4 = this.ventaBut4.Checked;
                string v = "";
                switch (true)
                {
                    case true when value1:
                        v = "1";
                        break;
                    case true when value2:
                        v = "2";
                        break;
                    case true when value3:
                        v = "3";
                        break;
                    case true when value4:
                        v = "4";
                        break;
                    default: break;
                }
                
                con.Open();
                SqlCommand cmd = new SqlCommand("ObtenerCompras", con);
                cmd.Parameters.AddWithValue("@Id_Compra", v);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                
                double totalCant = 0;
                foreach(DataRow item in dt.Rows)
                {
                    //Quantity for sale and id
                    var idPro = item["Codigo"].ToString();
                    var canitdad = Convert.ToInt16(item["Cantidad"].ToString());
                    //Quantity in warehouse
                    SqlCommand cmdB = new SqlCommand("ObtenerBodega", con);
                    cmdB.Parameters.AddWithValue("@Id", idPro);
                    cmdB.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dReaderB = cmdB.ExecuteReader();
                    DataTable dataB = new DataTable();
                    dataB.Load(dReaderB);
                    var cantBodega = Convert.ToDouble(dataB.Rows[0].ItemArray[3].ToString());
                    //Calculate total
                    totalCant = cantBodega - canitdad;
                    //Update database
                    string queryDelete = "update Bodega set cantidad = " + totalCant + " where Id_Producto = " + idPro;
                    SqlCommand cmdDelete = new SqlCommand(queryDelete, con);
                    cmdDelete.ExecuteNonQuery();
                    //delete from compras
                    SqlCommand cmdDeleteCompras = new SqlCommand("EliminarCompras", con);
                    cmdDeleteCompras.Parameters.AddWithValue("@Id", idPro);
                    cmdDeleteCompras.Parameters.AddWithValue("@Id_Venta", v);
                    cmdDeleteCompras.CommandType = CommandType.StoredProcedure;
                    cmdDeleteCompras.ExecuteNonQuery();

                }
                con.Close();
                dataGridView2.DataSource = null;
                this.cambioDe.Text = "";
                this.cancelaCon.Text = "";
                this.totalVenta.Text = "";
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "SeleccionImp");
            }

        }

        private void FacturacionNit()
        {
            try
            {
                double cancela = this.cancelaCon.Text.Equals("") ? 0 : double.Parse(this.cancelaCon.Text);
                var cambio = 0.00;

                if (cancela == 0)
                {
                    cambio = cancela;
                    this.cambioDe.Text = cambio.ToString();
                }
                else
                {
                    var value = double.Parse(this.totalVenta.Text);

                    cambio = (cancela - value);
                    

                }
                AlertCambio(cambio);
                var conteo = dataGridView2.Rows.Count;
                if (conteo != 0)
                {
                    string correo = "";
                    string ID = "";
                    string eP = "";
                    //
                    var cc = ""; int i = 0;
                    var nombre = "";
                    var direc = "";
                    var tel = "";
                    var nit = "";
                    try
                    {
                        MsgUsrEmp msgUsrEmp = new MsgUsrEmp();
                        msgUsrEmp.ShowDialog();
                        switch (msgUsrEmp.DialogResult)
                        {
                            case DialogResult.Cancel:
                                while (ID.Equals(""))
                                {
                                    ID = Microsoft.VisualBasic.Interaction.InputBox("Insertar Nit de la empresa", "Datos de factura NIT");
                                    if(ID.Equals(""))
                                    {
                                        MessageBox.Show("Este Capo no puede estar vacio");
                                        return;
                                    }
                                }
                                break;
                            case DialogResult.OK:
                                while (ID.Equals(""))
                                {
                                    ID = Microsoft.VisualBasic.Interaction.InputBox("Insertar numero de documento", "Datos de factura NIT");
                                    if(ID.Equals(""))
                                    {
                                        MessageBox.Show("Este Capo no puede estar vacio");
                                    }
                                }
                                break;
                            default: break;
                        }

                        ClsFactura.CreaTicket Ticket1 = new ClsFactura.CreaTicket();

                        Ticket1.TextoCentro("Distribuciones Arly's");
                        Ticket1.TextoCentro("Regimen comuún");
                        Ticket1.TextoCentro("NIT: 40079945-0");
                        Ticket1.TextoCentro("Calle 19 sur # 45B - 34");
                        Ticket1.TextoCentro("Tel. 3162882803");
                        Ticket1.TextoCentro("Villavicencio, Meta");
                        Ticket1.TextoCentro("-------------------------------------------------------");

                        switch (msgUsrEmp.DialogResult)
                        {
                            case DialogResult.Cancel:
                                string queryNit = "select * from Company where Nit_Company= " + ID;
                                SqlDataAdapter ad = new SqlDataAdapter(queryNit, con);
                                DataTable td = new DataTable();
                                ad.Fill(td);
                                if (td.Rows.Count != 0)
                                {
                                    for (i = 0; i < td.Rows.Count; i++)
                                    {
                                        cc = td.Rows[i].ItemArray[0].ToString();
                                        if (cc == ID)
                                        {
                                            ID = td.Rows[i].ItemArray[0].ToString();
                                            nombre = td.Rows[i].ItemArray[1].ToString();
                                            direc = td.Rows[i].ItemArray[3].ToString();
                                            tel = td.Rows[i].ItemArray[4].ToString();
                                            correo = td.Rows[i].ItemArray[7].ToString();
                                            eP = !ID.Equals("") ? "NIT" : "";
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No existe esta empresa","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                    RegistraEmpresa registra=new RegistraEmpresa(ID);
                                    registra.ShowDialog();
                                    string queryN = "select Nit_Company,Name_Company,Products,Direction,Phone,Mail from Company where Nit_Company= " + ID;
                                    SqlDataAdapter adN = new SqlDataAdapter(queryN, con);
                                    DataTable tdN = new DataTable();
                                    adN.Fill(tdN);

                                    ID = tdN.Rows[0].ItemArray[0].ToString();
                                    nombre = tdN.Rows[0].ItemArray[1].ToString();
                                    direc = tdN.Rows[0].ItemArray[3].ToString();
                                    tel = tdN.Rows[0].ItemArray[4].ToString();
                                    correo = tdN.Rows[0].ItemArray[5].ToString();
                                    eP = !ID.Equals("") ? "NIT" : "";
                                }

                                break;

                            case DialogResult.OK:
                                con.Open();
                                SqlCommand cmdU = new SqlCommand("ObtenerUsuarios", con);
                                cmdU.Parameters.AddWithValue("@Id", ID);
                                cmdU.CommandType = CommandType.StoredProcedure;
                                SqlDataReader drU = cmdU.ExecuteReader();
                                DataTable dtU = new DataTable();
                                dtU.Load(drU);
                                con.Close();
                                if (dtU.Rows.Count != 0)
                                {
                                    for (i = 0; i < dtU.Rows.Count; i++)
                                    {
                                        cc = dtU.Rows[i].ItemArray[0].ToString();
                                        if (cc == ID)
                                        {
                                            ID = dtU.Rows[i].ItemArray[0].ToString();
                                            nombre = dtU.Rows[i].ItemArray[2].ToString();
                                            direc = dtU.Rows[i].ItemArray[5].ToString();
                                            tel = dtU.Rows[i].ItemArray[4].ToString();
                                            correo = dtU.Rows[i].ItemArray[6].ToString();
                                            eP = dtU.Rows[i].ItemArray[1].ToString();
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No existe este cliente");
                                    RegistroUsuarios registro = new RegistroUsuarios(ID);
                                    registro.ShowDialog();
                                    con.Open();
                                    SqlCommand cmdC = new SqlCommand("ObtenerUsuarios", con);
                                    cmdC.Parameters.AddWithValue("@Id", ID);
                                    SqlDataReader drC = cmdC.ExecuteReader();
                                    DataTable tC = new DataTable();
                                    tC.Load(drC);
                                    con.Close();
                                    ID = tC.Rows[i].ItemArray[0].ToString();
                                    nombre = tC.Rows[i].ItemArray[2].ToString();
                                    direc = tC.Rows[i].ItemArray[5].ToString();
                                    tel = tC.Rows[i].ItemArray[4].ToString();
                                    correo = tC.Rows[i].ItemArray[6].ToString();
                                    eP = tC.Rows[i].ItemArray[1].ToString();
                                }
                                break;
                            default: break;
                        }

                        Ticket1.TextoIzquierda(eP + " : " + ID);
                        Ticket1.TextoIzquierda("Nombre: " + nombre);
                        Ticket1.TextoIzquierda("Dirc: " + direc);
                        Ticket1.TextoIzquierda("Tel: " + tel);

                        Ticket1.TextoCentro("Factura de Venta"); 
                        string queryIdFac = "select MAX(Id)+1 from Factura ";
                        SqlDataAdapter da = new SqlDataAdapter(queryIdFac, con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        var idFactura = dt.Rows[0].ItemArray[0].ToString();
                        var facturaN = idFactura.Equals("") ? 0 + 1 : Convert.ToInt64(idFactura);
                        Ticket1.TextoIzquierda("No Fac:" + facturaN.ToString());
                        Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
                        ClsFactura.CreaTicket.LineasGuion();
                        ClsFactura.CreaTicket.EncabezadoVenta();
                        ClsFactura.CreaTicket.LineasGuion();

                        foreach (DataGridViewRow r in dataGridView2.Rows)
                        {
                            var articuloT = r.Cells[2].Value.ToString();
                            var precioT = double.Parse(r.Cells[3].Value.ToString());
                            var cantidadT = double.Parse(r.Cells[5].Value.ToString());
                            var totalT = double.Parse(r.Cells[6].Value.ToString());

                            //--------------------- PROD------PrECIO------CANT----TOTAL
                            Ticket1.AgregaArticulo(articuloT, precioT, cantidadT, totalT);
                        }
                        var totalComp = double.Parse(this.totalVenta.Text);
                        var ivaComp = Math.Ceiling((totalComp / 1.19) * 0.19);
                        con.Open();
                        SqlCommand cmdFact = new SqlCommand("InsertarCartera", con);
                        cmdFact.Parameters.AddWithValue("@Id_Cartera", 2);
                        cmdFact.Parameters.AddWithValue("@Valor_Cartera", totalComp);
                        cmdFact.Parameters.AddWithValue("@Fecha_Registro", DateTime.Now.ToShortDateString());
                        cmdFact.Parameters.AddWithValue("@Factura", facturaN);
                        cmdFact.Parameters.AddWithValue("@Fecha_Fin", DateTime.Now.ToShortDateString());
                        cmdFact.Parameters.AddWithValue("@Documento", nit);
                        cmdFact.CommandType = CommandType.StoredProcedure;
                        cmdFact.ExecuteNonQuery();
                        con.Close();
                        ClsFactura.CreaTicket.LineasGuion();
                        //Ticket1.AgregaTotales("Sub-Total", totalComp); 
                        //Ticket1.AgregaTotales("Menos Descuento", 0); 
                        //Ticket1.AgregaTotales("Mas IVA (19%)", 0); 
                        //Ticket1.AgregaTotales("Mas IVA (19%)", ivaComp); 
                        //Ticket1.TextoIzquierda(" ");
                        Ticket1.AgregaTotales("Total", totalComp); 
                        Ticket1.TextoIzquierda(" ");
                        var efec = this.efectivo.Checked;
                        var transf = this.transferencia.Checked;
                        //
                        switch (true)
                        {
                            case true when efec:
                                Ticket1.AgregaTotales("Efectivo Entregado:", double.Parse(cancela.ToString()));
                                Ticket1.AgregaTotales("Efectivo Devuelto:", double.Parse(cambio.ToString()));
                                break;
                            case true when transf:
                                Ticket1.AgregaTotales("Pago por transferencia:", double.Parse(this.cancelaCon.Text, NumberStyles.Currency));
                                break;
                            default: break;
                        }
                        con.Close();
                        Ticket1.TextoIzquierda(" ");
                        Ticket1.TextoIzquierda("Atendio por: " + this.vendedor.Text);
                        Ticket1.TextoCentro("------------------------------------------");
                        Ticket1.TextoCentro("---     Gracias por preferirnos    -------");
                        Ticket1.TextoCentro("------------------------------------------");
                        string impresora = ConfigurationManager.AppSettings["empresora"];
                        Ticket1.ImprimirTiket(impresora);

                        string tipoPago = "";
                        if(efec == true)
                        {
                            tipoPago = "Efectivo";
                        }
                        else
                        {
                            tipoPago = "transferencia";
                        }
                        
                        ExcelFactura(correo, nombre, direc, tel, ID, tipoPago, cancela.ToString(), cambio.ToString(), "Factura", 0);

                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message, "(1)FacturacionNit ");
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "(2)FacturacionNit ");
            }
        }

        private void FacturacionRem()
        {
            try
            {
                double cancela = this.cancelaCon.Text.Equals("") ? Convert.ToDouble(this.cancelaCon.Text = "0") : double.Parse(this.cancelaCon.Text, NumberStyles.Currency);
                var cambio = 0.00;
                if (this.cancelaCon.Text.Equals("0"))
                {
                    this.cancelaCon.Text = this.totalVenta.Text;
                    cancela = double.Parse(this.totalVenta.Text, NumberStyles.Currency);
                    this.cambioDe.Text = "0";
                    cambio = double.Parse(this.cambioDe.Text);
                }
                else if (!this.cancelaCon.Text.Equals(""))
                {

                    var value = double.Parse(this.totalVenta.Text, NumberStyles.Currency);

                    cambio = cancela - value;
                    this.cambioDe.Text = cambio.ToString("C").Replace(",00", string.Empty);
                    cambio.ToString("C").Replace(",00", string.Empty);

                }
                AlertCambio(cambio);
                //IMPRIMIR FACTURA
                var conteo = dataGridView2.Rows.Count;
                if (conteo != 0)
                {
                    try
                    {
                        FacturaRem.CreaTicket Ticket1 = new FacturaRem.CreaTicket();

                        Ticket1.TextoCentro("Distribuciones Arly's ");
                        //Ticket3.TextoCentro("NIT: 40079945-0");
                        Ticket1.TextoCentro("Calle 19 sur # 45B - 34");
                        Ticket1.TextoCentro("Tel. 3162882803");
                        Ticket1.TextoCentro("Villavicencio, Meta");
                        Ticket1.TextoCentro("-------------------------------------------------------");
                        string queryIdFac = "select MAX(Id) + 1 from Factura_Remision ";
                        SqlDataAdapter da = new SqlDataAdapter(queryIdFac, con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        var idFactura = dt.Rows[0].ItemArray[0].ToString() == "" ? "1" : dt.Rows[0].ItemArray[0].ToString();
                        var facturaN = idFactura.Equals("") ? 0 + 1 : Convert.ToInt64(idFactura);
                        Ticket1.TextoIzquierda("No Fac:" + facturaN.ToString());
                        Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
                        FacturaRem.CreaTicket.LineasGuion();

                        FacturaRem.CreaTicket.EncabezadoVenta();
                        FacturaRem.CreaTicket.LineasGuion();
                        var n = dataGridView2.RowCount;

                        foreach (DataGridViewRow r in dataGridView2.Rows)
                        {
                            var articuloT = r.Cells[2].Value.ToString();
                            var precioT = double.Parse(r.Cells[3].Value.ToString(), NumberStyles.Currency);
                            var cantidadT = double.Parse(r.Cells[5].Value.ToString());
                            var totalT = double.Parse(r.Cells[6].Value.ToString(), NumberStyles.Currency);

                            //------------------------- PROD------PrECIO------CANT----TOTAL
                            Ticket1.AgregaArticulo(articuloT, precioT, cantidadT, totalT);
                        }

                        var totalComp = double.Parse(this.totalVenta.Text, NumberStyles.Currency);
                        var ivaComp = Math.Ceiling((totalComp / 1.19) * 0.19);
                        string fecha = DateTime.Now.ToShortDateString().ToString();
                        con.Open();
                        SqlCommand cmdFact = new SqlCommand("InsertarCartera", con);
                        cmdFact.Parameters.AddWithValue("@Id_Cartera", 3);
                        cmdFact.Parameters.AddWithValue("@Valor_Cartera", totalComp);
                        cmdFact.Parameters.AddWithValue("@Fecha_Registro", DateTime.Now.ToShortDateString());
                        cmdFact.Parameters.AddWithValue("@Factura", facturaN);
                        cmdFact.Parameters.AddWithValue("@Fecha_Fin", DateTime.Now.ToShortDateString());
                        cmdFact.Parameters.AddWithValue("@Documento", "");
                        cmdFact.CommandType = CommandType.StoredProcedure;
                        cmdFact.ExecuteNonQuery();
                        con.Close();
                        FacturaRem.CreaTicket.LineasGuion();
                        //Ticket1.AgregaTotales("Sub-Total", totalComp); // imprime linea con Subtotal
                        Ticket1.AgregaTotales("Total", totalComp); // imprime linea con total
                        Ticket1.TextoIzquierda(" ");
                        var efec = this.efectivo.Checked;
                        var transf = this.transferencia.Checked;
                        //
                        switch (true)
                        {
                            case true when efec:
                                Ticket1.AgregaTotales("Efectivo Entregado:", double.Parse(this.cancelaCon.Text, NumberStyles.Currency));
                                Ticket1.AgregaTotales("Efectivo Devuelto:", double.Parse(this.cambioDe.Text, NumberStyles.Currency));
                                break;
                            case true when transf:
                                Ticket1.AgregaTotales("Pago por transferencia:", double.Parse(this.cancelaCon.Text, NumberStyles.Currency));
                                break;
                            default: break;
                        }
                        con.Close();


                        Ticket1.TextoIzquierda("Atendio por:" + this.vendedor.Text);
                        Ticket1.TextoIzquierda(" ");
                        Ticket1.TextoCentro("------------------------------------------");
                        Ticket1.TextoCentro("---     Gracias por preferirnos    -------");
                        Ticket1.TextoCentro("------------------------------------------");

                        string impresora = ConfigurationManager.AppSettings["empresora"];
                        Ticket1.ImprimirTiket(impresora);

                        ExcelFactura("", "", "", "", "", "", cancela.ToString(), cambio.ToString(), "Factura_Remision", 0);
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message, "FacturacionRem ");
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "FacturacionRem");
            }
        }

        private void FacturaSinDatos()
        {
            try
            {
                double cancela = this.cancelaCon.Text.Equals("") ? Convert.ToDouble(this.cancelaCon.Text = "0") : double.Parse(this.cancelaCon.Text, NumberStyles.Currency);
                var cambio = 0.00;
                if (this.cancelaCon.Text.Equals("0"))
                {
                    this.cancelaCon.Text = this.totalVenta.Text;
                    this.cambioDe.Text = "0";
                    cambio = double.Parse(this.cambioDe.Text);
                }
                else if (!this.cancelaCon.Text.Equals(""))
                {

                    var value = double.Parse(this.totalVenta.Text, NumberStyles.Currency);

                    cambio = cancela - value;
                    this.cambioDe.Text = cambio.ToString("C").Replace(",00", string.Empty);
                    cambio.ToString("C").Replace(",00", string.Empty);

                }
                AlertCambio(cambio);
                //IMPRIMIR FACTURA
                var conteo = dataGridView2.Rows.Count;
                if (conteo != 0)
                {
                    try
                    {
                        FacturaSD.CreaTicket Ticket3 = new FacturaSD.CreaTicket();

                        Ticket3.TextoCentro("Distribuciones Arly's ");
                        Ticket3.TextoCentro("NIT: 40079945-0");
                        Ticket3.TextoCentro("Calle 19 sur # 45B - 34");
                        Ticket3.TextoCentro("Tel. 3162882803");
                        Ticket3.TextoCentro("Villavicencio, Meta");
                        Ticket3.TextoCentro("------------------------------------------");
                        string queryIdFac = "select MAX(Id) + 1 from Factura_Remision ";
                        SqlDataAdapter da = new SqlDataAdapter(queryIdFac, con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        var idFactura = dt.Rows[0].ItemArray[0].ToString();
                        var facturaN = idFactura.Equals("") ? 0 + 1 : Convert.ToInt64(idFactura);
                        Ticket3.TextoIzquierda("N° Venta:" + facturaN.ToString());
                        Ticket3.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
                        FacturaSD.CreaTicket.LineasGuion();
                        FacturaSD.CreaTicket.EncabezadoVenta();
                        FacturaSD.CreaTicket.LineasGuion();
                        foreach (DataGridViewRow r in dataGridView2.Rows)
                        {
                            var articuloT = r.Cells[2].Value.ToString();
                            var precioT = double.Parse(r.Cells[3].Value.ToString());
                            var cantidadT = double.Parse(r.Cells[5].Value.ToString());
                            var totalT = double.Parse(r.Cells[6].Value.ToString());

                            //------------------------- PROD------PrECIO------CANT----TOTAL
                            Ticket3.AgregaArticulo(articuloT, precioT, cantidadT, totalT);
                        }

                        var totalComp = double.Parse(this.totalVenta.Text, NumberStyles.Currency);
                        var ivaComp = Math.Ceiling((totalComp / 1.19) * 0.19);
                        string fecha = DateTime.Now.ToShortDateString().ToString();
                        con.Open();
                        SqlCommand cmdFact = new SqlCommand("InsertarCartera", con);
                        cmdFact.Parameters.AddWithValue("@Id_Cartera", 4);
                        cmdFact.Parameters.AddWithValue("@Valor_Cartera", totalComp);
                        cmdFact.Parameters.AddWithValue("@Fecha_Registro", DateTime.Now.ToShortDateString());
                        cmdFact.Parameters.AddWithValue("@Factura", facturaN);
                        cmdFact.Parameters.AddWithValue("@Fecha_Fin", DateTime.Now.ToShortDateString());
                        cmdFact.Parameters.AddWithValue("@Documento", "40079945-0");
                        cmdFact.CommandType = CommandType.StoredProcedure;
                        cmdFact.ExecuteNonQuery();
                        con.Close();
                        FacturaSD.CreaTicket.LineasGuion();
                        //Ticket3.AgregaTotales("Sub-Total", totalComp); // imprime linea con Subtotal
                        Ticket3.AgregaTotales("Total", totalComp); // imprime linea con total
                        Ticket3.TextoIzquierda(" ");
                        var efec = this.efectivo.Checked;
                        var transf = this.transferencia.Checked;
                        //
                        switch (true)
                        {
                            case true when efec:
                                Ticket3.AgregaTotales("Efectivo Entregado:", double.Parse(this.cancelaCon.Text, NumberStyles.Currency));
                                Ticket3.AgregaTotales("Efectivo Devuelto:", double.Parse(this.cambioDe.Text, NumberStyles.Currency));
                                break;
                            case true when transf:
                                Ticket3.AgregaTotales("Pago por transferencia:", double.Parse(this.cancelaCon.Text, NumberStyles.Currency));
                                break;
                            default: break;
                        }
                        Ticket3.TextoIzquierda(" ");
                        Ticket3.TextoIzquierda("Atendio por:" + this.vendedor.Text);
                        Ticket3.TextoCentro("------------------------------------------");
                        Ticket3.TextoCentro("---     Gracias por preferirnos    -------");
                        Ticket3.TextoCentro("------------------------------------------");
                        string impresora = ConfigurationManager.AppSettings["empresora"];
                        Ticket3.ImprimirTiket(impresora);

                        ExcelFactura("", "", "", "", "", "", cancela.ToString(), cambio.ToString(), "Factura_Remision", 1);
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message, "FacturacionRem ");
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "FacturacionRem");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    int row = Convert.ToInt32(e.RowIndex.ToString());
                    string str = "psi";
                    Eliminar(row, str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Datagridview2");
            }
        }

        private void Eliminar(int row, string str)
        {
            try
            {
                int index = str.Equals("") ? Convert.ToInt32(dataGridView2.CurrentCell.RowIndex.ToString()) : row;
                long ID = Convert.ToInt64(dataGridView2.Rows[index].Cells["Codigo"].Value);
                var cantidad = dataGridView2.Rows[index].Cells["Cantidad"].Value.ToString();
                string query = ""; string v = "";

                var value1 = this.ventaBut1.Checked; var value2 = this.ventaBut2.Checked;
                var value3 = this.ventaBut3.Checked; var value4 = this.ventaBut4.Checked;

                switch (true)
                {
                    case true when value1:
                        v = "1";
                        break;
                    case true when value2:
                        v = "2";
                        break;
                    case true when value3:
                        v = "3";
                        break;
                    case true when value4:
                        v = "4";
                        break;
                    default: break;
                }

                query = "delete from Compras where Id_Producto = " + ID + " and Id_Venta = " + v + " and Cantidad = " + cantidad;
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                //
                this.idProdC.Text = "";
                this.canProd.Text = "1";
                idProdC.Focus();

                ListaCompra();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Elimar");
            }
        }

        private void idProdC_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string query = "select * from lista_bodega where Producto like '%" + this.idProdC.Text + "%'";
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand(query, con);
            //    SqlDataAdapter ad = new SqlDataAdapter(query, con);
            //    DataTable dt = new DataTable();
            //    ad.Fill(dt);

            //    AutoCompleteStringCollection data = new AutoCompleteStringCollection();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        data.Add(dt.Rows[i].ItemArray[1].ToString());

            //    }
            //    this.idProdC.AutoCompleteCustomSource = data;
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    con.Close();
            //    MessageBox.Show("idProdC" + ex.Message);
            //}
        }

        private void rBImprimir_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.rBImprimir.Checked == true)
                {
                    this.rBNImprimir.Visible = true;
                    this.rBImprimir.Visible = false;
                    this.sinDatos.Visible = true;
                }
                else if (this.rBImprimir.Checked == false)
                {
                    this.rBNImprimir.Visible = false;
                    this.rBImprimir.Visible = true;
                    this.sinDatos.Visible = false;
                }
                if (this.rBNImprimir.Checked == true)
                {
                    this.rBFactNit.Visible = false;
                    this.rBRemision.Visible = false;
                    this.sinDatos.Visible = false;
                }
                if (this.rBImprimir.Checked == true)
                {
                    this.rBFactNit.Visible = true;
                    this.rBRemision.Visible = true;
                    this.sinDatos.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "rBImprimir_CheckedChanged");
            }
        }

        private void ventaBut1_CheckedChanged(object sender, EventArgs e)
        {
            ListaCompra();
            rBImprimir.Checked = false;
        }

        private void canProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsPunctuation(e.KeyChar) && e.KeyChar == '.') 
                {
                    e.Handled = false;
                }
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    CompareExistente();

                    this.idProdC.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "canProd_KeyPress");
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
            dataGridView2.Columns.Add(button);
            //
        }

        private void ventaBut2_CheckedChanged(object sender, EventArgs e)
        {
            ListaCompra();
            rBImprimir.Checked = false;
        }

        private void transferencia_CheckedChanged(object sender, EventArgs e)
        {
            this.cancelaCon.Clear();
            this.cancelaCon.Enabled = false;
        }

        private void efectivo_CheckedChanged(object sender, EventArgs e)
        {
            this.cancelaCon.Enabled = true;
        }

        private void IngresarDinero()
        {
            try
            {
                DialogResult accion;
                string queryConsulta = "SELECT Dinero FROM DineroBase WHERE Fecha_Registro LIKE '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                SqlDataAdapter ad = new SqlDataAdapter(queryConsulta, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                int cantidad = dt.Rows.Count;
                if (cantidad == 0)
                {
                    accion = DialogResult.No;
                    while (accion == DialogResult.No)
                    {
                        DineroBase dineroBase = new DineroBase();
                        dineroBase.ShowDialog();
                        switch (dineroBase.DialogResult)
                        {
                            case DialogResult.Yes:
                                accion = DialogResult.Yes;
                                break;
                            case DialogResult.Cancel:
                                accion = DialogResult.Cancel;
                                break;
                            default: break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "IngresarDinero");
            }
        }

        private void Disable()
        {
            try
            {
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel11.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Disable");
            }
        }

        private void Enable()
        {
            try
            {
                panel2.Visible = true;
                panel3.Visible = true;
                panel4.Visible = true;
                panel11.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Disable");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rBId.Checked == true)
            {
                this.idProdC.Focus();
                this.cBNombre.Visible = false;
                this.idProdC.Visible = true;
            }
            if (this.rBNombre.Checked == true)
            {
                this.cBNombre.Focus();
                this.idProdC.Visible = false;
                this.idProdC.Text = "";
                this.cBNombre.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count < 1)
                {
                    MessageBox.Show("Datos de cotizacion estan vacios","", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                NuevaCotizacion();
                string rutaDatos = ConfigurationManager.AppSettings["PathExcel"];
                string rutaDatosPDF = ConfigurationManager.AppSettings["PathPDF"];

                List<Cotizacion> listCot = new List<Cotizacion>();
                Cotizacion cotizacion = new Cotizacion();

                foreach (DataGridViewRow r in dataGridView2.Rows)
                {

                    cotizacion.Codigo = r.Cells["Codigo"].Value.ToString();
                    cotizacion.Nombre = r.Cells["Producto"].Value.ToString();
                    cotizacion.Cant = r.Cells["Precio unidad"].Value.ToString();
                    cotizacion.Unidad = r.Cells["Medida"].Value.ToString();
                    cotizacion.VUnidad = r.Cells["Cantidad"].Value.ToString();
                    cotizacion.SubTotal = r.Cells["Precio total"].Value.ToString();

                    //cotizacion.Codigo = r.Cells["ID"].Value.ToString();
                    //cotizacion.Nombre = r.Cells["Producto"].Value.ToString();
                    //cotizacion.Cant = r.Cells["Cantidad"].Value.ToString();
                    //cotizacion.Unidad = r.Cells["Tipo Unidad"].Value.ToString();
                    //cotizacion.VUnidad = r.Cells["precio unidad"].Value.ToString();
                    //cotizacion.SubTotal = r.Cells["Precio total"].Value.ToString();


                    listCot.Add(cotizacion);
                }
                //
                string queryCont = "SELECT Id FROM Cotizacion order by Id desc";
                SqlDataAdapter adapter = new SqlDataAdapter(queryCont, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                var Fac = data.Rows.Count == 0 ? 0 : Convert.ToInt64(data.Rows[0].ItemArray[0].ToString());
                var nFac = Fac + 1;
                //
                string dateda = DateTime.Now.ToShortDateString();
                Application excel = new Application();
                Workbook workbook = excel.Workbooks.Open(rutaDatos);
                Worksheet worksheet = workbook.Worksheets[1];
                //Insert data from company

                Range nf = worksheet.Cells[5, 12];
                nf.Value = nFac;

                Range dia = worksheet.Cells[7, 12];
                dia.Value = dateda;
                //Insert personal data
                string nombre = Microsoft.VisualBasic.Interaction.InputBox("Nombre del cliente", "Datos de cotizacion");
                nombre = nombre.Equals("") ? "" : nombre;
                Range range0 = worksheet.Cells[9, 2];
                range0.Value = nombre;

                string direccion = Microsoft.VisualBasic.Interaction.InputBox("Direccion del cliente", "Datos de cotizacion");
                direccion = direccion.Equals("") ? "" : direccion;
                Range range01 = worksheet.Cells[10, 2];
                range01.Value = direccion;

                string telefono = Microsoft.VisualBasic.Interaction.InputBox("telefono del cliente", "Datos de cotizacion");
                telefono = telefono.Equals("") ? "" : telefono;
                Range range02 = worksheet.Cells[11, 2];
                range02.Value = telefono;

                string nit = Microsoft.VisualBasic.Interaction.InputBox("NIT/CC del cliente", "Datos de cotizacion");
                nit = nit.Equals("") ? "" : nit;
                Range range03 = worksheet.Cells[9, 9];
                range03.Value = nit;

                Range range04 = worksheet.Cells[10, 9];
                range04.Value = "Villavicencio";
                //
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    // Agregar un campo en la fila n, columna n
                    Range range = worksheet.Cells[14 + i, 1];
                    range.Value = listCot[i].Codigo.ToString();
                    //
                    Range range1 = worksheet.Cells[14 + i, 3];
                    range1.Value = listCot[i].Nombre.ToString();
                    //
                    Range range2 = worksheet.Cells[14 + i, 8];
                    range2.Value = listCot[i].Cant.ToString();
                    //
                    Range range3 = worksheet.Cells[14 + i, 9];
                    range3.Value = listCot[i].Unidad.ToString();
                    //
                    Range range4 = worksheet.Cells[14 + i, 11];
                    range4.Value = listCot[i].VUnidad.ToString().Replace(".", "").Replace(",00","");
                    //
                    Range range5 = worksheet.Cells[14 + i, 12];
                    range5.Value = listCot[i].SubTotal.ToString().Replace(".","").Replace(",00","");

                }
                Range range6 = worksheet.Cells[53, 12];
                range6.Formula = "=SUM(L14:M51)";
                //
                Range range7 = worksheet.Cells[55, 12];
                range7.Value = "$0";
                //
                Range range8 = worksheet.Cells[57, 12];
                range8.Value = "$0";
                //
                Range range9 = worksheet.Cells[59, 12];
                range9.Formula = "=L53-L55+L57";
                //
                string observacion = Microsoft.VisualBasic.Interaction.InputBox("Observaciones de cotización", "Datos de cotizacion");
                observacion = observacion.Equals("") ? "NINGUNA" : observacion;
                Range range12 = worksheet.Range["D53:F56"];
                range12.Value = observacion;

                string condicion = Microsoft.VisualBasic.Interaction.InputBox("Condiciones de cotización", "Datos de cotizacion");
                condicion = observacion.Equals("") ? "NINGUNA" : condicion;
                Range range13 = worksheet.Range["D57:F58"];
                range13.Value = condicion;

                string validez = Microsoft.VisualBasic.Interaction.InputBox("Validez de cotización", "Datos de cotizacion");
                validez = observacion.Equals("") ? "10" : validez;
                Range range14 = worksheet.Range["H55:H56"];
                range14.Value = validez;

                workbook.Save();
                workbook.Close();
                excel.Quit();
                //
                ExportToPDF(rutaDatos, rutaDatosPDF, cotizacion.Nombre.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cotizacion ");
            }
        }

        public void NuevaCotizacion()
        {
            try
            {
                string rutaDatos = ConfigurationManager.AppSettings["PathExcel"];

                Excel.Application excel = new Excel.Application();
                Excel.Workbook workbook = excel.Workbooks.Open(rutaDatos);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                //DELETE DATA FROM CLIENT
                worksheet.Range["B9:G9"].ClearContents();
                worksheet.Range["B10:G10"].ClearContents();
                worksheet.Range["B11:G11"].ClearContents();
                worksheet.Range["I9:M9"].ClearContents();
                worksheet.Range["I10:M10"].ClearContents();
                //DELETE DATA FROM COTIZACION
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["A" + n + ":" + "B" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["C" + n + ":" + "G" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["H" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["I" + n + ":" + "J" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["K" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["L" + n + ":" + "M" + n].ClearContents();
                }
                worksheet.Range["D53:F56"].ClearContents();
                worksheet.Range["D57:F58"].ClearContents();
                worksheet.Range["H55:H56"].ClearContents();

                workbook.Save();
                workbook.Close();
                excel.Quit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NuevaCotizacion");
            }
        }

        public void ExportToPDF(string rutaDatos, string rutaDatosPDF, string nombre)
        {
            try
            {
                string correo = "";
                Excel.Application excel = new Excel.Application();
                Excel.Workbook workbook = excel.Workbooks.Open(rutaDatos);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, rutaDatosPDF);
                workbook.Close();
                excel.Quit();
                
                Process[] processes = Process.GetProcessesByName("Excel");
                for (int i = 0; i < processes.Length; i++)
                {
                    processes[i].Kill();
                }
                
                Thread.Sleep(100);
                PdfReader reader = new PdfReader(rutaDatosPDF);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper stamper = new PdfStamper(reader, ms);
                    stamper.Close();
                    reader.Close();
                    byte[] bytes = ms.ToArray();
                    var base64EncodedBytes = System.Convert.ToBase64String(bytes).ToString();
                    string query = "INSERT INTO Cotizacion VALUES('" + base64EncodedBytes + "')";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                                
                DialogResult sendEmail = MessageBox.Show("¿Enviar correo?", "Seleccionar", MessageBoxButtons.YesNo);
                switch (sendEmail)
                {
                    case DialogResult.Yes:
                        int n = 0;
                        while (!correo.Contains("@") || !correo.Contains(".com") || correo.Equals(""))
                        {
                            correo = Microsoft.VisualBasic.Interaction.InputBox("Correo para enviar la cotización", "Datos de factura Nit");
                            if (correo.Equals(""))
                            {
                                MessageBox.Show("Este campo no puede estar vacio","Correo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                n++;
                            }
                            if (n >= 2)
                            {
                                MessageBox.Show("Envio de correo cancelado", "Correo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        SendCorreo correos = new SendCorreo();
                        correos.SendEmailCot(rutaDatosPDF, correo, "Cotizacion", nombre);
                        break;
                    default: break;
                }
                NumeroCotizacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ExportToPDF");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void NumeroCotizacion()
        {
            try
            {
                
                string queryCont = "select Id from Cotizacion order by Id desc";
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(queryCont, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                con.Close();
                if(data.Rows.Count > 0)
                {
                    var numeroCotizacion = data.Rows[0].ItemArray[0].ToString();
                    this.textBox1.Text = numeroCotizacion;
                }
                else
                {
                    return;
                }
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Numero de cotizacion");
            }
        }

        public void AlertCambio(double pMessage)
        {
            try
            {
                MensageCambio msg = new MensageCambio(pMessage);
                msg.ShowDialog();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "AlertCambio");
            }
        }

        public void ExcelFactura(string correo, string nombre, string direccion, string telefono, string ID, string tipoPago, string cancelaCon, string cambioDe, string ftra, int estado)
        {
            try
            {
                if (dataGridView2.Rows.Count < 1)
                {
                    MessageBox.Show("Datos de factura estan vacios", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                NuevaFactura();
                string rutaDatos = ConfigurationManager.AppSettings["PathExcelFactura"];
                string rutaDatosPDF = ConfigurationManager.AppSettings["PathPDFFactura"];

                List<Cotizacion> listCot = new List<Cotizacion>();

                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    Cotizacion cotizacion = new Cotizacion();

                    cotizacion.Codigo = r.Cells["Codigo"].Value.ToString();
                    cotizacion.Nombre = r.Cells["Producto"].Value.ToString();
                    cotizacion.Cant = r.Cells["Cantidad"].Value.ToString();
                    cotizacion.Unidad = r.Cells["Medida"].Value.ToString();
                    cotizacion.VUnidad = r.Cells["Precio unidad"].Value.ToString();
                    cotizacion.SubTotal = r.Cells["Precio total"].Value.ToString();


                    listCot.Add(cotizacion);
                }
                //
                string queryCont = "SELECT Id FROM " + ftra + " order by Id desc";
                SqlDataAdapter adapter = new SqlDataAdapter(queryCont, con);
                DataTable data = new DataTable();
                adapter.Fill(data);
                var nFac = Convert.ToInt64(data.Rows[0].ItemArray[0].ToString());
                //var nFac = Fac + 1;
                //
                string dateda = DateTime.Now.ToString("dd/MMM/yyyy").Replace(".",string.Empty);
                Application excel = new Application();
                Workbook workbook = excel.Workbooks.Open(rutaDatos);
                Worksheet worksheet = workbook.Worksheets[1];
                //Insert data from company

                Range nf = worksheet.Cells[5, 12];
                nf.Value = nFac;

                Range dia = worksheet.Cells[7, 12];
                dia.Value = dateda;
                //Insert personal data
                if(estado != 1)
                {
                    nombre = nombre.Equals("") ? Microsoft.VisualBasic.Interaction.InputBox("Nombre del cliente", "Datos de cotizacion") : nombre;
                    Range range0 = worksheet.Cells[9, 2];
                    range0.Value = nombre;

                    direccion = direccion.Equals("") ? Microsoft.VisualBasic.Interaction.InputBox("Direccion del cliente", "Datos de cotizacion") : direccion;
                    Range range01 = worksheet.Cells[10, 2];
                    range01.Value = direccion;

                    telefono = telefono.Equals("") ? Microsoft.VisualBasic.Interaction.InputBox("Telefono del cliente", "Datos de cotizacion") : telefono;
                    Range range02 = worksheet.Cells[11, 2];
                    range02.Value = telefono;

                    ID = ID.Equals("") ? Microsoft.VisualBasic.Interaction.InputBox("Documento del cliente", "Datos de cotizacion") : ID;
                    Range range03 = worksheet.Cells[9, 9];
                    range03.Value = ID;

                    Range range04 = worksheet.Cells[10, 9];
                    range04.Value = "Villavicencio";
                }
                //
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    // Agregar un campo en la fila n, columna n
                    Range range = worksheet.Cells[14 + i, 1];
                    range.Value = listCot[i].Codigo.ToString();
                    //
                    Range range1 = worksheet.Cells[14 + i, 3];
                    range1.Value = listCot[i].Nombre.ToString();
                    //
                    Range range2 = worksheet.Cells[14 + i, 8];
                    range2.Value = listCot[i].Cant.ToString();
                    //
                    Range range3 = worksheet.Cells[14 + i, 9];
                    range3.Value = listCot[i].Unidad.ToString();
                    //
                    Range range4 = worksheet.Cells[14 + i, 11];
                    range4.Value = listCot[i].VUnidad.ToString().Replace(".", "").Replace(",00", "");
                    //
                    Range range5 = worksheet.Cells[14 + i, 12];
                    range5.Value = listCot[i].SubTotal.ToString().Replace(".", "").Replace(",00", "");

                }
                Range range6 = worksheet.Cells[53, 12];
                range6.Formula = "=SUM(L14:M51)";
                
                Range range7 = worksheet.Cells[55, 12];
                range7.Value = "$0";
                
                Range range8 = worksheet.Cells[57, 12];
                range8.Value = "$0";
                
                Range range9 = worksheet.Cells[59, 12];
                range9.Formula = "=L53-L55+L57";

                Range range12 = worksheet.Range["D53:F56"];
                range12.Value = tipoPago;

                if (tipoPago.Equals("Efectivo"))
                {
                    Range range13 = worksheet.Range["D57:F58"];
                    range13.Value = cancelaCon;

                    Range range14 = worksheet.Range["D59:F60"];
                    range14.Value = cambioDe;
                }
                else
                {
                    Range range13 = worksheet.Range["D57:F58"];
                    range13.Value = cancelaCon;
                }           

                workbook.Save();
                workbook.Close();
                excel.Quit();
                
                ExportToPDFFactura(rutaDatos, rutaDatosPDF, correo, ftra, nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cotizacion ");
            }
        }

        public void NuevaFactura()
        {
            try
            {
                string rutaDatos = ConfigurationManager.AppSettings["PathExcelFactura"];

                Excel.Application excel = new Excel.Application();
                Excel.Workbook workbook = excel.Workbooks.Open(rutaDatos);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                //DELETE DATA FROM CLIENT
                worksheet.Range["B9:G9"].ClearContents();
                worksheet.Range["B10:G10"].ClearContents();
                worksheet.Range["B11:G11"].ClearContents();
                worksheet.Range["I9:M9"].ClearContents();
                worksheet.Range["I10:M10"].ClearContents();
                //DELETE DATA FROM COTIZACION
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["A" + n + ":" + "B" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["C" + n + ":" + "G" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["H" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["I" + n + ":" + "J" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["K" + n].ClearContents();
                }
                for (int i = 0; i < 38; i++)
                {
                    var n = 14 + i;
                    worksheet.Range["L" + n + ":" + "M" + n].ClearContents();
                }
                worksheet.Range["D57:F58"].ClearComments();
                worksheet.Range["D59:F60"].ClearComments();
                

                workbook.Save();
                workbook.Close();
                excel.Quit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NuevaCotizacion");
            }
        }

        public void ExportToPDFFactura(string rutaDatos, string rutaDatosPDF, string correo, string ftra, string nombre)
        {
            try
            {
                
                Excel.Application excel = new Excel.Application();
                Excel.Workbook workbook = excel.Workbooks.Open(rutaDatos);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, rutaDatosPDF);
                workbook.Close();
                excel.Quit();

                Process[] processes = Process.GetProcessesByName("Excel");
                for (int i = 0; i < processes.Length; i++)
                {
                    processes[i].Kill();
                }

                Thread.Sleep(100);
                PdfReader reader = new PdfReader(rutaDatosPDF);

                using (MemoryStream ms = new MemoryStream())
                {
                    PdfStamper stamper = new PdfStamper(reader, ms);
                    stamper.Close();
                    reader.Close();
                    byte[] bytes = ms.ToArray();
                    var base64EncodedBytes = System.Convert.ToBase64String(bytes).ToString();
                    string query = "INSERT INTO " + ftra + " VALUES('" + base64EncodedBytes + "')";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (ftra.Equals("Factura"))
                {
                    DialogResult sendEmail = MessageBox.Show("¿Enviar correo?", "Seleccionar", MessageBoxButtons.YesNo);
                    switch (sendEmail)
                    {
                        case DialogResult.Yes:
                            Process[] processes1 = Process.GetProcessesByName("Acrobat");

                            for (int i = 0; i < processes1.Length-1; i++)
                            {
                                processes1[i].Kill();
                            }
                            SendCorreo correos = new SendCorreo();
                            correos.SendEmailCot(rutaDatosPDF, correo, "Factura", nombre);
                            MessageBox.Show("Correo enviado", "Correo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case DialogResult.No:
                            MessageBox.Show("Envio de correo cancelado", "Correo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        default: break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ExportToPDF");
            }
        }

        public void Clear()
        {
            this.totalVenta.Clear();
            this.cancelaCon.Clear();
            this.cambioDe.Clear();
            this.textBox1.Clear();
        }

        private void rBFactNit_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}