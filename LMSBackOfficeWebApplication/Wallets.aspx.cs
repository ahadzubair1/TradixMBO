using LMSBackOfficeDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMSBackOfficeWebApplication
{
    public partial class Wallets : System.Web.UI.Page
    {
        protected DataTable dt { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                string userName = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(userName);
                dt = MemberWallets_DataAcsess.GetMemberCreditWallets(member.Id);

            }

        }
    }
}