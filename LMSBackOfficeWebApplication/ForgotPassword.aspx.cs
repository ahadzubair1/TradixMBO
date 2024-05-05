using DocumentFormat.OpenXml.Wordprocessing;
using LMSBackofficeDAL;
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
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Reset(object sender, EventArgs e)
        {
            string email = this.reg_email1.Value;
            string currentDomainUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            // Process the form data (e.g., save to database, send email, etc.)
            // You can write your logic here
            bool CheckEmailExists = Members_DataAccess.CheckEmailExists(email);
            if (!CheckEmailExists)
            {
                ResponseMessage1.InnerText = "Email does not Exists";
                ResponseMessage1.Style.Add("display", "block");
                ResponseMessage1.Style.Add("color", "#ff2600");
            }
            else
            {
                    DataTable resultTable = Members_DataAccess.GetMemberDetailsByEmail(email);
                    if (resultTable != null && resultTable.Rows.Count > 0)
                    {
                        DataRow row = resultTable.Rows[0];
                        string MemberCode = row["Member_Code"].ToString();
                        string MemberName = row["Member_FullName"].ToString();
                        UtilMethods.SendEmailForgotPassword(email, MemberName, MemberCode, currentDomainUrl);
                        ResponseMessage1.InnerText = "Please check your email";
                        ResponseMessage1.Style.Add("display", "block");
                        ResponseMessage1.Style.Add("color", "#e012ee");
                        this.reg_email1.Value = ""; 
                        Response.AddHeader("REFRESH", "2;URL=ForgotPassword.aspx");
                    }
                    else
                    {
                        ResponseMessage1.InnerText = "Cannot find user with Email";
                        ResponseMessage1.Style.Add("display", "block");
                        ResponseMessage1.Style.Add("color", "#ff2600");
                        Response.AddHeader("REFRESH", "1;URL=ForgotPassword.aspx");
                    }

            }

            
        }
    }
}