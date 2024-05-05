using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using LMSBackOfficeDAL;
using LMSBackOfficeWebApplication;
using LMSBackOfficeWebApplication.Ipns;
using LMSBackOfficeWebApplication.Utitlity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.PeerToPeer;
using System.Web;

namespace Coinpayments.Example
{
    /// <summary>
    /// IPN Handler for Coinpayments, see https://www.coinpayments.net/merchant-tools-ipn
    /// </summary>
    public class IpnHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var req = IpnBase.Parse<IpnApi>(context.Request.Form);

                var requestString = JsonConvert.SerializeObject(req);
                WriteLog.LogInfo(requestString);

                var hmac = context.Request.Headers["HMAC"];
                WriteLog.LogInfo($"Hmac is {hmac}");
                //if (hmac == null || !req.SigIsValid(hmac))
                //{
                //    response(context, HttpStatusCode.BadRequest, "Invalid HMAC / MerchantId");
                //    return;
                //}

                WriteLog.LogInfo($"Status is {req.SuccessStatusLax()}");
                if (req.Status >= 100 || req.Status == 2 || req.Status == 1)
                {

                    WriteLog.LogInfo($"Transaction ID : {req.TxnId}");
                    var memberInfo = GetMemberOrderInfo(req.Custom);
                    if (memberInfo != null)
                    {
                        WriteLog.LogInfo($"Credit {req.Amount1} to {memberInfo.MemberId} wallet");

                        MemberWallets_DataAcsess.UpdateMemberWallet(memberInfo.MemberId, Convert.ToDecimal(req.Amount1), 1,memberInfo.OrderId);

                        WriteLog.LogInfo($"Updating transaction to complete for member {memberInfo.MemberId} of Order no  : {memberInfo.OrderId}");
                        //Update Transaction on success
                        var transactionCode = Transactions_DataAcsess.UpdateTransaction(memberInfo.MemberId, memberInfo.OrderId, req.Fee, CoinPaymentStatus.Complete.ToString());
                        
                        WriteLog.LogInfo($"Updating coinPaymentTransaction to complete for orderno  : {memberInfo.OrderId}");
                        CoinPaymentTransactions_DataAcsess.UpdateCoinPaymentTransaction(req.TxnId, memberInfo.OrderId, req.SendTx, req.Status, req.StatusText);

                        WriteLog.LogInfo($"Transaction {req.TxnId} completed successfully");
                    }
                }

                if (req.Status == -1)//When transaction is timeout or cancelled
                {
                    var memberInfo = GetMemberOrderInfo(req.Custom);
                    if(memberInfo != null)
                    {
                        //deduct amount
                        MemberWallets_DataAcsess.UpdateMemberWallet(memberInfo.MemberId, Convert.ToDecimal(req.Amount1), 0,memberInfo.OrderId);

                        var transactionCode = Transactions_DataAcsess.UpdateTransaction(memberInfo.MemberId, memberInfo.OrderId, req.Fee, req.StatusText);
                        WriteLog.LogInfo($"Transaction Code : {transactionCode}");
                        CoinPaymentTransactions_DataAcsess.UpdateCoinPaymentTransaction(req.TxnId, memberInfo.OrderId, req.SendTx, req.Status, req.StatusText);
                    }
                    
                }

                response(context, HttpStatusCode.OK, "Transaction completed successfully");
            }
            catch (Exception ex)
            {
                WriteLog.LogError(ex);
            }
        }

        public MemberOrder GetMemberOrderInfo(string customString)
        {
            MemberOrder memberOrder = null;
            if (!string.IsNullOrEmpty(customString))
            {
                var custom = customString.Split('|');
                if (custom.Length > 1)
                {
                    memberOrder = new MemberOrder();
                    memberOrder.OrderId = custom[0];
                    memberOrder.MemberId = custom[1];
                }
            }

            return memberOrder;
        }


        private void response(HttpContext context, HttpStatusCode statusCode, string text)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "text/plain";
            context.Response.Write(text);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class MemberOrder
    {
        public string MemberId { get; set; }
        public string OrderId { get; set; }
    }
}