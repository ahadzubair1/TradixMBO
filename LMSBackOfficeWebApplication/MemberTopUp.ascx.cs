using LMSBackofficeDAL;
using LMSBackOfficeDAL;
using LMSBackOfficeDAL.Model;
using LMSBackOfficeWebApplication.Utitlity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace LMSBackOfficeWebApplication
{
    public partial class MemberTopUp : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string strResponse = Login_DataAccess.GetVisitorInfo();
        }

        protected void TotpUp_Click1(object sender, EventArgs e)
        {
            decimal amount = 0;
            if (txtAmount.Text != "")
            {
                amount = Convert.ToDecimal(txtAmount.Text);
            }
            try
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                string username = Session["Username"].ToString();
                var member = Members_DataAccess.GetMemberInfo(username);

                var api = new CoinPayment.CoinPaymentService();
                var orderId = Guid.NewGuid().ToString();
                string storeLocation = HttpContext.Current.Request.Url.AbsolutePath;
                string ipn_url = $"{storeLocation}/IPNHandler.ascx";
                var response = api.CreateTransaction(amount, "USD", Configurations.ToCurrency, Configurations.CompanyCryptoWallet, member.MemberFullName,
                                                    member.Email, ipn_url, null, orderId).GetAwaiter().GetResult();
                if (response.IsSuccess)
                {
                    var responseBody = CryptoUtil.ConvertQueryStringToObject(response.HttpResponse.RequestBody);
                    var StatusCode = (int)response.HttpResponse.StatusCode;

                    //Update Balance in UserWallet table 
                    MemberWallets_DataAcsess.UpdateMemberWallet(member.Id, Convert.ToDecimal(amount), 0,orderId);

                    //Add Transaction and Coin PaymentTransaction
                    Transactions_DataAcsess.AddTransactions(member.Id, orderId, "Topup", member.MemberCurrency, Configurations.ToCurrency,
                                                            member.MemberAddress, Configurations.CompanyCryptoWallet, CoinPaymentStatus.Pending.ToString(),
                                                            Convert.ToDecimal(amount));

                    string message = $"Your topup request sent successfully ";
                    //lblMessage.Text = message;
                    //lblMessage.ForeColor = Color.Green;
                    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "ClosePopup('" + message + "')", true);
                }
                else
                {
                    //lblMessage.Text = response.Error.ToString();
                    // lblMessage.ForeColor = Color.Red;
                    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "ClosePopup('" + response.Error + "')", true);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void TotpUp_Click(object sender, EventArgs e)
        {
            decimal amount = 0;
            if (txtAmount.Text != "")
            {
                amount = Convert.ToDecimal(txtAmount.Text);
            }
            string username = Session["Username"].ToString();
            var member = Members_DataAccess.GetMemberInfo(username);
            var orderId = Guid.NewGuid().ToString();
            var checkout = new CheckoutModel
            {
                MemberFullName = member.MemberFullName,
                MemberCode = member.MemberCode,
                Email = member.Email,
                TotalAmount = amount,
                Currency = Configurations.ToCurrency,
                ToWalletAddress = Configurations.CompanyCryptoWallet,
                OrderId = orderId
            };

            Session["Checkout"] = checkout;           

            Response.Redirect("Checkout.aspx");
        }        
    }
}