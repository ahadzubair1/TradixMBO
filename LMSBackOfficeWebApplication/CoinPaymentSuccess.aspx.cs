using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMSBackOfficeWebApplication
{
    public partial class CoinPaymentSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string orderNumber = string.Empty;
            if (!String.IsNullOrEmpty(Request.QueryString["orderNumber"]))
                orderNumber = Request.QueryString["orderNumber"];
            Session["OrderNo"] = orderNumber;

        }
    }
}