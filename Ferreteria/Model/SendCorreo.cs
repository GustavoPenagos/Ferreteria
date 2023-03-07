using System;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DistribucionesArly_s
{
    internal class SendCorreo
    {
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        //pendiente cambiar ruta de FACTURAS
        public void SendMail(string correos)
        {
            try
            {
                string pathTxt = ConfigurationManager.AppSettings["pathTxt"];
                string pathPDF = ConfigurationManager.AppSettings["pathTXTPDF"];
                string mail = "";
                DialogResult de = MessageBox.Show("¿Enviar correo?", "Seleccionar", MessageBoxButtons.YesNo);
                switch (de)
                {
                    case DialogResult.Yes:
                        string id = correos.Equals("") ? Microsoft.VisualBasic.Interaction.InputBox("Correo electronico", "Datos de envio factura al correo") : correos;
                        mail = id;
                        //extract factura from data base
                        string queryBusca = "select top (1) * from Factura order by Id_Factura desc";
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(queryBusca, con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        var codeB64 = dt.Rows[0].ItemArray[1].ToString();
                        var B64ToByte = Convert.FromBase64String(codeB64);
                        var a = System.Text.Encoding.UTF8.GetString(B64ToByte);
                        con.Close();
                        File.WriteAllText(pathTxt, a);
                        //
                        Document document = new Document();
                        PdfWriter.GetInstance(document, new FileStream(pathPDF, FileMode.OpenOrCreate));
                        document.Open();
                        document.Add(new Paragraph(File.ReadAllText(pathTxt)));
                        document.Close();
                        //

                        MailMessage correo = new MailMessage();

                        correo.From = new MailAddress("f.DistribucionesArlys@gmail.com", "Distribuciones Arly's", System.Text.Encoding.UTF8);//Correo de salida
                        correo.To.Add(mail); //Correo destino
                        correo.Subject = "Gracias por su compra Distribuciones Arly's"; //Asunto
                        correo.Body = "El día de hoy se realiza el envio de su factura,\n Gracias por su compra"; //Mensaje del correo
                        correo.Attachments.Add(new Attachment(pathPDF));
                        correo.IsBodyHtml = true;
                        correo.Priority = MailPriority.Normal;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("f.DistribucionesArlys@gmail.com", "vgjwqgdtsipuvwqm");//Cuenta de correo
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                        smtp.Send(correo);
                        //
                        correo.Dispose();
                        //
                        break;
                    case DialogResult.No:
                        return;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SendMail" + ex);
            }
        }

        public void SendEmailCot(string rutaDatosPDF, string correos)
        {
            try
            {
                MailMessage correo = new MailMessage();

                correo.From = new MailAddress("f.DistribucionesArlys@gmail.com", "Distribuciones Arly's", System.Text.Encoding.UTF8);//Correo de salida
                correo.To.Add(correos); //Correo destino
                correo.Subject = "Gracias por su compra Distribuciones Arly's"; //Asunto
                correo.Body = "El día de hoy se realiza el envio de su cotizacion,\n Gracias por su compra"; //Mensaje del correo
                correo.Attachments.Add(new Attachment(rutaDatosPDF));
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("f.DistribucionesArlys@gmail.com", "vgjwqgdtsipuvwqm");//Cuenta de correo
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                smtp.Send(correo);
                //
                correo.Dispose();
                //


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SendEmailCot");
            }
        }
    }
}
