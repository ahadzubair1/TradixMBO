using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LMSBackOfficeDAL;
using static ServiceStack.Script.Lisp;

namespace LMSBackOfficeWebApplication
{
    public partial class Bonuses : System.Web.UI.Page
    {
        protected DataTable dtNetworkBonusTable { get; set; }
        protected DataTable dtDirectBonusTable { get; set; }
        private string memberId; // Member ID field to store the current member ID

        protected string headerTitleDirectBonusAmount { get; set; }
        protected string headerTitleNetworkBonusAmount { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userName = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(userName);
                memberId = member.Id; // Store the member ID
                BindGridView(memberId);

            }


        }

        private void BindGridView(string memberId)
        {
            dtNetworkBonusTable = Bonus_DataAccess.GetNetworkBonusByMemberId(memberId);
            dtDirectBonusTable = Bonus_DataAccess.GetDirectBonusByMemberId(memberId);  
            

            gvNetworkBonus.DataSource = dtNetworkBonusTable;
            gvNetworkBonus.DataBind();

            if (dtNetworkBonusTable.Rows.Count > 0)
                headerTitleNetworkBonusAmount += dtNetworkBonusTable.Rows[0]["BonusAmount"].ToString();


            gvDirectBonus.DataSource = dtDirectBonusTable;
            gvDirectBonus.DataBind();

            if (dtDirectBonusTable.Rows.Count > 0)
                headerTitleDirectBonusAmount += dtDirectBonusTable.Rows[0]["Bonus_Amount"].ToString();
                

        }
        protected void gvNetworkBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNetworkBonus.PageIndex = e.NewPageIndex;
            BindGridView(memberId);
        }

        protected void gvDirectBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDirectBonus.PageIndex = e.NewPageIndex;
            BindGridView(memberId);
        }
        protected string GetDefaultDirectBonusAmountFromServer()
        {
            // Your logic to fetch the default direct bonus amount from the server
            return "1002"; // Example value
        }

        protected string GetDefaultNetworkBonusAmountFromServer()
        {
            // Your logic to fetch the default network bonus amount from the server
            return "200"; // Example value
        }
    }
}