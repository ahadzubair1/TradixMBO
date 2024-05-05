using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LMSBackOfficeDAL;
using System.Xml.Linq;
using static LMSBackOfficeDAL.Countries_DataAccess;
using System.Web.Security;


namespace LMSBackOfficeWebApplication
{
    public partial class Activation : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string activationToken = Request.QueryString["token"];
                if (!string.IsNullOrEmpty(activationToken))
                {
                    bool activationResult = Members_DataAccess.ActivateMember(activationToken);
                    if (activationResult)
                    {
                        ActivationMessageLiteral.Text = "Your account has been successfully activated!";
                    }
                    else
                    {
                        ActivationMessageLiteral.Text = "Invalid activation token. Please contact support for assistance.";
                    }
                }
                else
                {
                    ActivationMessageLiteral.Text = "Invalid activation token. Please ensure you clicked the correct link from your email.";
                }
            }
        }



    }
}