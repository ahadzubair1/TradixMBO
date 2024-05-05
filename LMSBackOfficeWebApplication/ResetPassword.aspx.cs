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
    public partial class ResetPassword : System.Web.UI.Page
    {
        public string token { get; set; } = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["token"]))
                {
                    string username = Session["Username"].ToString();
                    var member = Members_DataAccess.GetMemberInfo(username);
                    token = member.MemberCode;

                }
                else
                {
                    // Its login is already in btn reset Passowrd
                }

            }

        }
        protected void btnSubmit_Reset(object sender, EventArgs e)
        {
            string password = this.password.Value;
            string confirmpassword = this.confirmpassword.Value;
            string currentDomainUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                string activationToken = "";

            if (Session["Username"] != null && !string.IsNullOrEmpty(Session["Username"].ToString()))
            {
                string username = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(username);
                activationToken = member.MemberCode;
            }
            else
            {

            activationToken=Request.QueryString["token"];
            }
            string passwordValidation = validatePassword(password, confirmpassword);
            if (passwordValidation != "Success")
            {
                ResponseMessage.InnerText = passwordValidation;
                ResponseMessage.Style.Add("display", "block");
                ResponseMessage.Style.Add("color", "#ff2600");
               Response.AddHeader("REFRESH", "2;URL=ResetPassword.aspx?token=" + activationToken);
            }
            else
            {
                if (string.IsNullOrEmpty(activationToken))
                {
                    ResponseMessage.InnerText = "Invalid Token";
                    ResponseMessage.Style.Add("display", "block");
                    ResponseMessage.Style.Add("color", "#ff2600");
                    Response.AddHeader("REFRESH", "2;URL=ResetPassword.aspx?token=" + activationToken);
                }
                else
                {
                    bool updatePassword = Members_DataAccess.UpdateMemberPassword(activationToken,password);
                    if (updatePassword)
                    {
                        ResponseMessage.InnerText = "Your Password is Successfully Reset";
                        ResponseMessage.Style.Add("display", "block");
                        ResponseMessage.Style.Add("color", "#e012ee");
                        this.password.Value = "";
                        this.confirmpassword.Value = "";
                      //  Response.AddHeader("REFRESH", "2;URL=ResetPassword.aspx?token="+activationToken);
                    }
                    else
                    {
                        ResponseMessage.InnerText = "Cannot find user";
                        ResponseMessage.Style.Add("display", "block");
                        ResponseMessage.Style.Add("color", "#ff2600");
                        Response.AddHeader("REFRESH", "2;URL=ResetPassword.aspx?token="+activationToken);
                    }
                }


            }
        }
        protected static string validatePassword(string password, string confirmpassword)
        {

            // Check for length
            if (password != confirmpassword)
            {
                return "Password and confirm passwords Dont Match";
            }
            if (password.Length < 8 || password.Length > 16)
            {
                return "Password Length should be between 8 to 16 charchaters";
            }

            // Check for at least 1 uppercase letter
            if (!password.Any(char.IsUpper))
            {
                return "Password must contain atleast 1 Uppercase";
            }

            // Check for at least 1 lowercase letter
            if (!password.Any(char.IsLower))
            {
                return "Password must contain atleast 1 Lowercase";
            }

            // Check for at least 1 digit
            if (!password.Any(char.IsDigit))
            {
                return "Password must contain atleast 1 digit";
            }

            // Check for at least 1 special character
            if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                return "Password must contain atleast one special character";
            }

            return "Success";
        }
    }
}