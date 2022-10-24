using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPWeb
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string Cantidad { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Cantidad = Session["CantidadCarrito"] != null ? Session["CantidadCarrito"].ToString() : "0";
        }
    }
}