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

        protected void dgvCarrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IDItem = (int)dgvCarrito.SelectedDataKey.Value;

            int posItem = BuscarItem((List<ItemCarrito>)Session["ListaCarrito"], IDItem);

            List<ItemCarrito> Temporal = (List<ItemCarrito>)Session["ListaCarrito"];
            
            if(Temporal[posItem].Cantidad > 1)
            {
                Temporal[posItem].Precio -= Temporal[posItem].Precio / Temporal[posItem].Cantidad;
                Temporal[posItem].Cantidad--;
            }
            else
            {
                ItemCarrito Eliminado = Temporal.Find(x => x.IDItem == IDItem);
                Temporal.Remove(Eliminado);
            }

            Session.Add("ListaCarrito", Temporal);
            Response.Redirect("Carrito.aspx", false);

        }

        protected int BuscarItem(List<ItemCarrito> ListaCarrito, int IDItem)
        {
            int pos;

            foreach (ItemCarrito item in ListaCarrito)
            {
                if (item.IDItem == IDItem)
                {
                    pos = ListaCarrito.IndexOf(item);
                    return pos;
                }
            }

            return -1;
        }
    }
}