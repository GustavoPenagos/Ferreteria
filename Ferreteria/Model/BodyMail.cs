using System;
using System.Text;


namespace Ferreteria.Model
{
    internal class BodyMail
    {
        public string CreateBodyMail(string tipo, string nombre)
        {
            tipo = tipo.Equals("Factura") ? "Compra" : "Cotizacion";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<title>Distribuciones Arly's</title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<p>Tenga un buen día " + nombre + "</p>");
            sb.AppendLine("<p></p>");
            sb.AppendLine("<p>Nuestra empresa esta comprometida en brinda informacion a sus clientes, enviado a su correo la " + tipo.ToLower() + " que a realizado" +
                "el día de hoy (" + DateTime.Now.ToShortDateString() + ") en nuestra ferreteria.</p>");
            sb.AppendLine("<p></p>");
            sb.AppendLine("<p></p>");
            sb.AppendLine("<p></p>");
            sb.AppendLine("<p>Codialmente,</p>");
            sb.AppendLine("<p>Distribuciones Arly's</p>");
            sb.AppendLine("<p>Ferreterias</p>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();

        }
    }
}
