using LMSBackOfficeDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace LMSBackOfficeWebApplication
{
    public partial class AIcopyTrading : System.Web.UI.Page
    {
        protected bool MembershipExist { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                // Session has expired, redirect to login page
                Response.Redirect("~/Login.aspx");
            }

            string userName = Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(userName);

            // Get membership existence and highest membership amount

            bool membershipExist = Memberships_DataAccess.CheckMembershipExist(Session["Username"].ToString());
            decimal highestMembershipAmount = Memberships_DataAccess.GetHighestMembershipAmount(member.Id);

            // Register a startup script to define the MembershipExist and HighestMembershipAmount variables in JavaScript
            ClientScript.RegisterStartupScript(this.GetType(), "membershipVariablesScript",
                "var MembershipExist = " + membershipExist.ToString().ToLower() + ";" +
                "var HighestMembershipAmount = " + highestMembershipAmount.ToString() + ";", true);



        }




    }
}