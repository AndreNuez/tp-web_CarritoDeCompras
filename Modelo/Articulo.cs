using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Modelo
{
    public class Articulo
    {
        [DisplayName("Id Artículo")]
        public int IDArticulo { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        [DisplayName("Categoría")]
        public Categoria Categoria { get; set; }
        public string ImagenURL { get; set; }
        [DisplayName("Precio $")]
        public decimal Precio { get; set; }
    }
}
