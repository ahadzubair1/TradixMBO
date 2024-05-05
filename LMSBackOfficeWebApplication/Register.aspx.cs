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
    public partial class Register : Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {

            /*if (!IsPostBack)
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
            }*/

            if (!IsPostBack)
            {
                // Fetch referral code from URL
                string referralCode = Request.QueryString["referralcode"];

                // Populate textbox with referral code
                refcode.Value = referralCode;
                try
                {
                    List<Countries_DataAccess.Country> allCountries = Countries_DataAccess.GetAllCountries();

                    // Populate select element with countries
                    foreach (Countries_DataAccess.Country country in allCountries)
                    {
                        ListItem item = new ListItem(country.CountryName, country.CountryID);
                        countries.Items.Add(item);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }



        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = this.fullname.Value;
            string email = this.email.Value;
            string password = this.password.Value;
            string confirmpassword = this.confirmpassword.Value;
            string refcode = this.refcode.Value;
            string phone = this.phone.Value;
            string countries = this.countries.SelectedValue;
            string username = this.username.Value;
            string currentDomainUrl = Request.Url.GetLeftPart(UriPartial.Authority);

            string passwordValidation = validatePassword(password, confirmpassword);
            if (passwordValidation != "Success")
            {
                ResponseMessage.InnerText = passwordValidation;
                ResponseMessage.Style.Add("display", "block");
                ResponseMessage.Style.Add("color", "#ff2600");
            }
            else
            {
                // Process the form data (e.g., save to database, send email, etc.)
                // You can write your logic here
                bool userExists = Members_DataAccess.CheckUsernameExists(username);
                bool CheckEmailExists = Members_DataAccess.CheckEmailExists(email);
                if (CheckEmailExists)
                {
                    ResponseMessage.InnerText = "Email already Exists";
                    ResponseMessage.Style.Add("display", "block");
                    ResponseMessage.Style.Add("color", "#ff2600");
                }
                else
                {
                    if (userExists)
                    {
                        ResponseMessage.InnerText = "Username already Taken";
                        ResponseMessage.Style.Add("display", "block");
                        ResponseMessage.Style.Add("color", "#ff2600");
                    }
                    else
                    {
                        DataTable resultTable = ReferralCodes_DataAccess.CheckParentReferral(refcode);
                        if (resultTable != null && resultTable.Rows.Count > 0)
                        {
                            DataRow row = resultTable.Rows[0];
                            string referredByParentId = row["Member_ID"].ToString();
                            int Position = (int)row["NetworkPosition"];
                            string registrationSuccess = Members_DataAccess.AddMember(name, username, email, password, referredByParentId, Position, phone, countries, currentDomainUrl);
                            if (registrationSuccess == "Success")
                            {
                                // Display success message
                                this.fullname.Value = "";
                                this.email.Value = "";
                                this.password.Value = "";
                                this.confirmpassword.Value = "";
                                this.refcode.Value = "";
                                this.phone.Value = "";
                                this.username.Value = "";// Assuming you have a server-side control for the success message
                                ResponseMessage.InnerText = "Registration Successful";
                                ResponseMessage1.InnerText = "Check your email to activate";
                                ResponseMessage.Style.Add("display", "block");
                                ResponseMessage.Style.Add("color", "#e012ee");
                                Response.AddHeader("REFRESH", "6;URL=Login.aspx");
                            }
                            else
                            {
                                ResponseMessage.InnerText = "Registration Failed";
                                ResponseMessage.Style.Add("display", "block");
                                ResponseMessage.Style.Add("color", "#ff2600");
                            }
                        }
                        else
                        {
                            ResponseMessage.InnerText = "Invalid Referral Code";
                            ResponseMessage.Style.Add("display", "block");
                            ResponseMessage.Style.Add("color", "#ff2600");
                        }

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