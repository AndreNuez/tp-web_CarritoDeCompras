using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Modelo;

namespace TPWeb
{

    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<ItemCarrito> ListaCarrito = (List<ItemCarrito>)Session["ListaCarrito"] != null ?
                (List<ItemCarrito>)Session["ListaCarrito"] : ListaCarrito = new List<ItemCarrito>();

            dgvCarrito.DataSource = ListaCarrito;
            dgvCarrito.DataBind();

        }
    }
}