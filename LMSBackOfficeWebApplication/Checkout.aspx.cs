using DocumentFormat.OpenXml.Spreadsheet;
using LMSBackofficeDAL;
using LMSBackOfficeDAL;
using LMSBackOfficeDAL.Model;
using LMSBackOfficeWebApplication.Utitlity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMSBackOfficeWebApplication
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string strResponse = Login_DataAccess.GetVisitorInfo();

                    if (Session["MembershipExpired"] != null)
                    {
                        var IsMembershipExpired = Convert.ToBoolean(Session["MembershipExpired"]);
                        if (IsMembershipExpired)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "HideSideBar();", true);
                        }
                    }

                    if (Session["Checkout"] != null)
                    {
                        var checkout = Session["Checkout"] as CheckoutModel;
                        var checkoutString = JsonConvert.SerializeObject(checkout);

                        WriteLog.LogInfo(checkoutString);

                        lblAmount.Text = $"{checkout.TotalAmount.ToString()} USD";
                        lblTotalAmount.Text = $"{checkout.TotalAmount.ToString()} USD";
                        txtMemberCode.Text = checkout.MemberCode;
                        txtMemberFullName.Text = checkout.MemberFullName;
                        txtEmail.Text = checkout.Email;

                        if(checkout.TotalAmount > 250)
                        {
                            lblFeeText.Text = "Activation Fees  would also apply";
                            activationfee.Visible = true;
                        }
                        else
                        {
                            lblFeeText.Text = "";
                            activationfee.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteLog.LogError(ex);
                }
            }
        }


        private IDictionary<string, string> CreateQueryParameters(CheckoutModel model, string memberId)
        {
            string storeLocation = HttpContext.Current.Request.Url.AbsoluteUri;
            string authority = HttpContext.Current.Request.Url.Authority;
            var path = storeLocation.Split('/');
            var completeurl = $"{path[0]}://{path[2]}";

            string host = HttpContext.Current.Request.Url.Host;
            string orderNo = Guid.NewGuid().ToString();
            Uri uri = new Uri(storeLocation);
            Session["OrderNo"] = orderNo;
            var queryParameters = new Dictionary<string, string>()
            {
                //ipn settings
                ["ipn_version"] = "1.0",
                ["cmd"] = "_pay_auto",
                ["ipn_type"] = "deposit",
                ["ipn_mode"] = "hmac",
                ["deposit_id"] = Guid.NewGuid().ToString(),
                ["merchant"] = Configurations.MerchantId,
                ["allow_extra"] = "0",
                ["currency"] = "USD",
                ["amountf"] = model.TotalAmount.ToString("N2"),
                ["item_name"] = "Topup",
                ["address"] = Configurations.CompanyCryptoWallet,

                //IPN, success and cancel URL
                ["success_url"] = $"{completeurl}/SuccessHandler.aspx?orderNumber={orderNo}", // order summery page
                ["ipn_url"] = $"{completeurl}/IPNHandler.ashx",
                ["cancel_url"] = $"{completeurl}/CancelTransaction.aspx",

                //order identifier                
                ["custom"] = $"{model.OrderId}|{memberId}",

                //billing info
                ["first_name"] = model.MemberFullName,
                ["email"] = model.Email,

            };
            return queryParameters;
        }

        protected void CheckoutBtn_ServerClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Session["Checkout"] != null)
                {
                    var checkout = Session["Checkout"] as CheckoutModel;
                    checkout.TotalAmount = checkout.TotalAmount;
                    // checkout.ToWalletAddress = txtWalletAddress.Text;
                    // checkout.Currency = DropDownList1.SelectedValue.ToString();

                    string username = Session["Username"].ToString();
                    var member = Members_DataAccess.GetMemberInfo(username);

                    var isTopupAlreadyCreated = Transactions_DataAcsess.CheckTopupAlreadyRequested(member.Id);
                    if (isTopupAlreadyCreated)
                    {
                        string message = $"You already have been requested a topup, please wait while its being completed.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "ShowTopupMessage('" + message + "');", true);

                        /*Added by Admin as work aroun*/
                        var queryParameters = CreateQueryParameters(checkout, member.Id);

                        var redirectUrl = UtilMethods.AddQueryString(Configurations.CoinPaymentUrl, queryParameters);


                        WriteLog.LogInfo($"Initiate transation for member {member.Id} of order no {checkout.OrderId}");
                        //Add Transaction and Coin PaymentTransaction
                        Transactions_DataAcsess.AddTransactions(member.Id, checkout.OrderId, "Topup", member.MemberCurrency, Configurations.ToCurrency,
                                                                member.MemberAddress, Configurations.CompanyCryptoWallet, CoinPaymentStatus.Pending.ToString(),
                                                                Convert.ToDecimal(checkout.TotalAmount));
                        Response.Write("<script type='text/javascript'>");
                        Response.Write("window.open('" + redirectUrl + "','_blank');");
                        Response.Write("</script>");
                        /*Till here*/

                    }
                    else
                    {
                        var queryParameters = CreateQueryParameters(checkout, member.Id);

                        var redirectUrl = UtilMethods.AddQueryString(Configurations.CoinPaymentUrl, queryParameters);

                        //Update Balance in UserWallet table 
                        // MemberWallets_DataAcsess.UpdateMemberWallet(member.Id, Convert.ToDecimal(checkout.TotalAmount), 1);

                        WriteLog.LogInfo($"Initiate transation for member {member.Id} of order no {checkout.OrderId}");
                        //Add Transaction and Coin PaymentTransaction
                        Transactions_DataAcsess.AddTransactions(member.Id, checkout.OrderId, "Topup", member.MemberCurrency, Configurations.ToCurrency,
                                                                member.MemberAddress, Configurations.CompanyCryptoWallet, CoinPaymentStatus.Pending.ToString(),
                                                                Convert.ToDecimal(checkout.TotalAmount));

                        //Response.Redirect(redirectUrl, false);
                        Response.Write("<script type='text/javascript'>");
                        Response.Write("window.open('" + redirectUrl + "','_blank');");
                        Response.Write("</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.LogError(ex);
            }
        }
    }
}