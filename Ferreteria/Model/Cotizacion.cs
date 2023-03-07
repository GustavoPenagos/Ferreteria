using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Model
{
    public class Cotizacion
    {
        private string codigo = "";
        private string nombre = "";
        private string cant = "";
        private string unidad = "";
        private string vUnidad = "";
        private string subTotal = "";

        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Cant { get => cant; set => cant = value; }
        public string Unidad { get => unidad; set => unidad = value; }
        public string VUnidad { get => vUnidad; set => vUnidad = value; }
        public string SubTotal { get => subTotal; set => subTotal = value; }
        //
        //private string codigo = "";
        //private string nombre = "";
        //private string cant = "";
        //private string unidad = "";
        //private double vUnidad = 0;
        //private double subTotal = 0;

        //public string Codigo { get => codigo; set => codigo = value; }
        //public string Nombre { get => nombre; set => nombre = value; }
        //public string Cant { get => cant; set => cant = value; }
        //public string Unidad { get => unidad; set => unidad = value; }
        //public double VUnidad { get => vUnidad; set => vUnidad = value; }
        //public double SubTotal { get => subTotal; set => subTotal = value; }

    }
}
