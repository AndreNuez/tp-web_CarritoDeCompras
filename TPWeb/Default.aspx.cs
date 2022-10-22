using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Negocio;

namespace TPWeb
{
    public partial class Default : System.Web.UI.Page
    {
        public  List<Articulo> ListaArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulos = negocio.listar();
            
            repRepetidor.DataSource = ListaArticulos;
            repRepetidor.DataBind();
        }
    }
}