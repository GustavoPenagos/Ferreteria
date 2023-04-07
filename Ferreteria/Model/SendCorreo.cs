using System;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Ferreteria.Model;

namespace DistribucionesArly_s
{
    internal class SendCorreo
    {
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conection"].ConnectionString);
        
        public void SendEmailCot(string rutaDatosPDF, string correos, string tipo, string nombre)
        {
            try
            {
                BodyMail bodyMail = new BodyMail();
                var body = bodyMail.CreateBodyMail(tipo, nombre);

                MailMessage correo = new MailMessage();

                correo.From = new MailAddress("f.DistribucionesArlys@gmail.com", "Distribuciones Arly's", System.Text.Encoding.UTF8);
                correo.To.Add(correos);
                correo.Subject = "Bienvenido a Distribuciones Arly's";
                correo.Body = body;
                correo.Attachments.Add(new Attachment(rutaDatosPDF));
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("f.DistribucionesArlys@gmail.com", "vgjwqgdtsipuvwqm");
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;
                smtp.Send(correo);

                correo.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SendEmailCot");
            }
        }
    }
}
