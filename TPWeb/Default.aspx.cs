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

            if (!IsPostBack)
            {
                repRepetidor.DataSource = ListaArticulos;
                repRepetidor.DataBind();
            }
            
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            int IDItem = int.Parse(((Button)sender).CommandArgument);

            Articulo ItemAgregado = new Articulo();
            ItemAgregado = ListaArticulos.Find(x => x.IDArticulo == IDItem);

            
            List<ItemCarrito> ListaCarrito = (List<ItemCarrito>)Session["ListaCarrito"] != null ?
                (List<ItemCarrito>)Session["ListaCarrito"] : ListaCarrito = new List<ItemCarrito>();
            
            ItemCarrito NuevoItem = new ItemCarrito();
            NuevoItem.IDItem = ItemAgregado.IDArticulo;
            NuevoItem.NombreItem = ItemAgregado.Nombre;
            NuevoItem.Cantidad = 1;
            NuevoItem.Precio = ItemAgregado.Precio;

            

            ListaCarrito.Add(NuevoItem);
            Session.Add("ListaCarrito", ListaCarrito);

            Response.Redirect("Carrito.aspx", false);
        }
    }
}