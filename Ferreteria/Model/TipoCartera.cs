using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda.Registros;

namespace Ferreteria.Model
{
    public class Enum
    {
        public enum TipoCartera : byte
        {
            Compras = 1,
            Factura_Nit = 2,
            Factura_Remision = 3,
            Factura_compra = 4,
            Ingreso_Capiatl = 5,
            Compras_Ferreteria = 6,
            Gastos = 7,
            Abonos_Compras_Ferreteria = 8
        }
    }
}
