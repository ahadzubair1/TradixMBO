using LMSBackofficeDAL;
using LMSBackOfficeDAL;
using LMSBackOfficeWebApplication.Models;
using Microsoft.Ajax.Utilities;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.PeerToPeer;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMSBackOfficeWebApplication
{
    public partial class Memberships : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        [WebMethod]
        public static string PurchaseMemberShip(MemberShipModel model)
        {
            string response = string.Empty;
            try
            {
                var username = HttpContext.Current.Session["Username"].ToString();
                DataTable membershipResult = Memberships_DataAccess.GetMembershipDetails(model.MemberShipCode);
                if (membershipResult != null && membershipResult.Rows.Count > 0)
                {
                    DataRow row = membershipResult.Rows[0];
                    string MembershipId = row["Membership_ID"].ToString();
                    string MembershipName = row["Membership_Name"].ToString();
                    double MembershipAmount = Convert.ToDouble(row["Membership_Amount"]);
                    double ActivationFees = Convert.ToDouble(row["Membership_ActivationFees"]);
                    double TotalAmount = MembershipAmount + ActivationFees;
                    DataTable resultTable = MemberWallets_DataAcsess.GetMemberWalletBalance(username);
                    if (resultTable != null && resultTable.Rows.Count > 0)
                    {
                        DataRow memberrow = resultTable.Rows[0];
                        string MemberId = memberrow["Member_ID"].ToString();
                        string Member_Code = memberrow["Member_Code"].ToString();
                        string WalletID = memberrow["Member_WalletID"].ToString();
                        string WalletCode = memberrow["Member_WalletCode"].ToString();
                        object walletBalance = memberrow["Wallet_Balance"];
                        double Balance = 0.0;
                        if (walletBalance != DBNull.Value)
                        {
                            Balance = Convert.ToDouble(walletBalance);
                        }

                        if (Balance >= TotalAmount)
                        {
                            double newBalance = Balance - TotalAmount;
                            var member = Members_DataAccess.GetMemberInfo(HttpContext.Current.Session["Username"].ToString());
                            var orderId = Guid.NewGuid().ToString();
                            MemberWallets_DataAcsess.UpdateMemberCreditWallet(MemberId, Convert.ToDecimal(newBalance), 0);
                            Transactions_DataAcsess.AddTransactions(MemberId, orderId, "Membership Purchase", "USD", Configurations.ToCurrency,
                                                            string.Empty, Configurations.CompanyCryptoWallet, "Complete",
                                                            Convert.ToDecimal(model.Amount));
                            var SuccessPurchase = Members_DataAccess.AddMembershipPurchase(MemberId, MembershipId, MembershipName, Convert.ToDecimal(MembershipAmount), Convert.ToDecimal(ActivationFees));
                            Network_DataAccess.AssignNetworkParent(MemberId);
                            DirectBonus_DataAccess.InsertOrUpdateDirectBonus(MemberId, MembershipAmount);
                            if (SuccessPurchase == "Success")
                            {
                                UtilMethods.SendEmailMembership(member.MemberFullName, MembershipName, DateTime.Now.ToString(), member.Country, member.Email);
                                UtilMethods.SendEmailMembershipToUser(member.Email, member.MemberFullName, MembershipName);
                                response = "success";
                            }
                        }
                        else
                        {
                            response = "fail";
                        }
                    }
                    else
                    {
                        response = "fail";
                    }
                }
                else
                {
                    response = "fail";
                }
            }
            catch (Exception ex)
            {
                WriteLog.LogError(ex);
                response = "fail";
            }

            return response;
        }
    }
}