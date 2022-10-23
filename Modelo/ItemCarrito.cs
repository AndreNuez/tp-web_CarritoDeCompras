using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ItemCarrito
    {
        public int IDItem { get; set; }
        public string NombreItem { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
