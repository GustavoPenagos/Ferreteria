using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Model
{
    class AgregarCompras
    {
        public string Id { get; set; }
        public string Producto {  get; set; }
        public string Precio_Unidad{ get; set; }
        public double  Cantidad { get; set; }
        public string Tipo_Unidad{ get; set;}
        public string Precio_Total{ get; set; }

    }
}
