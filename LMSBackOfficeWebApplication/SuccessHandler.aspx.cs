using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMSBackOfficeWebApplication
{
    public partial class SuccessHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["orderNumber"] != null)
            {
                var incoiceno = Request.QueryString["orderNumber"].ToString();
                lblConfirmation.Text = $"Your transaction against order {incoiceno} number has been completed successfully";
            }
            
        }
    }
 }