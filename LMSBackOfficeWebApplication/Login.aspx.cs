using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LMSBackOfficeDAL;
using Microsoft.Ajax.Utilities;
using LMSBackOfficeWebApplication.Utitlity;
using log4net;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;
using System.Net;


namespace LMSBackOfficeWebApplication
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear session variables only on the initial load of the page
                Session.Clear();
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = this.username.Value;
            string password = this.password.Value;
            ILog logger = log4net.LogManager.GetLogger("InfoLog");

            // Process the form data (e.g., save to database, send email, etc.)
            //if (ccLink != null)
            //{
            //    ccLink.ValidateCaptcha(txtCaptcha.Text.Trim());
            //}
            // ccLink.ValidateCaptcha(txtCaptcha.Text.Trim());
            try
            {
                bool isMembershipExpired = false;
                if (IsReCaptchValid())
                {
                    bool loginSuccess = Login_DataAccess.CheckLogin(username, password);
                    if (loginSuccess)
                    {
                        var memberInfo = Members_DataAccess.GetMemberInfo(username);
                        var isMembershipValid = Members_DataAccess.IsMembershipExpired(memberInfo.Id);
                        //if (!isMembershipValid)
                        //{
                        //    isMembershipExpired = true;
                        //}

                        this.successMessage.Value = "true";
                        Session["LoggedIn"] = true;
                        Session["Username"] = username;
                        WriteLog.LogInfo($"logged in as {username}");
                        logger.Info($"logged in as {username}");
                        Session["MembershipExpired"] = isMembershipExpired;
                        Login_DataAccess.AddLogin(username, password);
                        Response.Redirect("~/Dashboard.aspx");
                    }
                    else
                    {
                        this.successMessage.Value = "false";
                        Response.AddHeader("REFRESH", "3;URL=Login.aspx");
                        ResponseMessage.InnerText = "Login Failed: Invalid Credentials";
                        ResponseMessage.Style.Add("display", "block");
                        ResponseMessage.Style.Add("color", "#ff2600");
                    }
               }
                else
                {
                    this.successMessage.Value = "false";
                    Response.AddHeader("REFRESH", "1;URL=Login.aspx");
                    ResponseMessage.InnerText = "Login Failed: Wrong Captcha";
                    ResponseMessage.Style.Add("display", "block");
                    ResponseMessage.Style.Add("color", "#ff2600");

                }
            }
            catch (Exception ex)
            {
                WriteLog.LogError(ex);
                this.successMessage.Value = "false";
                ResponseMessage.InnerText = "Error Occurred:" + Convert.ToString(ex.Message);
                ResponseMessage.Style.Add("display", "block");
                ResponseMessage.Style.Add("color", "#ff2600");
                Response.AddHeader("REFRESH", "1;URL=Login.aspx");
            }

        }

        public bool IsReCaptchValid()
        {
            var result = false;
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = ConfigurationManager.AppSettings["CaptchaSecretKey"];
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(requestUri);

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        JObject jResponse = JObject.Parse(stream.ReadToEnd());
                        var isSuccess = jResponse.Value<bool>("success");
                        result = (isSuccess) ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.LogError(ex);
            }
           
            return result;
        }

        /* protected void Page_Load(object sender, EventArgs e)
        {
                        
            if (!IsPostBack)
            {
                string strResponse = Login_DataAccess.GetVisitorInfo();
                lblVisitorResponse.Text = "";

                if (strResponse=="Success")
                {
                    lblVisitorResponse.Text = "Visitor Response has been Successfully Saved";
                }
                else
                {
                    lblVisitorResponse.Text = "Error Occurred while Saving the Visitor Response";

                }
            }

        }*/
    }
}