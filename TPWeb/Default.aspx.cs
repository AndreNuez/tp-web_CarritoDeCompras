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
        public List<Articulo> ListaArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulos = negocio.listar();
            Session.Add("ListaArticulos", ListaArticulos);

            if (!IsPostBack)
            {
                repRepetidor.DataSource = Session["ListaArticulos"];
                repRepetidor.DataBind();
            }

        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            int IDItem = int.Parse(((Button)sender).CommandArgument);

            List<ItemCarrito> ListaCarrito = (List<ItemCarrito>)Session["ListaCarrito"] != null ?
                (List<ItemCarrito>)Session["ListaCarrito"] : ListaCarrito = new List<ItemCarrito>();

            Articulo ItemAgregado = new Articulo();
            ItemAgregado = ListaArticulos.Find(x => x.IDArticulo == IDItem);

            ItemCarrito NuevoItem = new ItemCarrito();
            NuevoItem.IDItem = ItemAgregado.IDArticulo;
            NuevoItem.NombreItem = ItemAgregado.Nombre;
            NuevoItem.Cantidad = 1;
            NuevoItem.Precio = ItemAgregado.Precio;


            if ((List<ItemCarrito>)Session["ListaCarrito"] != null)
            {
                int posItem = BuscarItem(ListaCarrito, NuevoItem);
                
                if (posItem != -1)
                {
                    ListaCarrito[posItem].Cantidad++;
                    ListaCarrito[posItem].Precio += NuevoItem.Precio;
                }
                else
                {
                    ListaCarrito.Add(NuevoItem);
                }
            }
            else
            {
                ListaCarrito.Add(NuevoItem);
            }

            Session.Add("ListaCarrito", ListaCarrito);
            Response.Redirect("Carrito.aspx", false);
        }

        protected int BuscarItem(List<ItemCarrito> ListaCarrito, ItemCarrito NuevoItem)
        {
            int pos;

            foreach (ItemCarrito item in ListaCarrito)
            {
                if (item.IDItem == NuevoItem.IDItem)
                {
                   pos = ListaCarrito.IndexOf(item);
                   return pos;
                }
            }

            return -1;
        }

        protected void txtFiltrar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> Lista = (List<Articulo>)Session["ListaArticulos"];
            List<Articulo> ListaFiltrada = Lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltrar.Text.ToUpper()));
            repRepetidor.DataSource = Session["ListaFiltrada"];
            repRepetidor.DataBind();
        }
    }
}