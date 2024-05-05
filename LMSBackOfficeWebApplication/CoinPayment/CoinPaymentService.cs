using LMSBackOfficeDAL;
using LMSBackOfficeWebApplication.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LMSBackOfficeWebApplication.CoinPayment
{
    public class CoinPaymentService
    {
 
        public Task<CreateTransactionResponse> CreateTransaction(
           decimal amount, string currency1, string currency2, string toAddress, string buyerName, string buyerEmail,string ipn_url,
           string custom = null, string itemNumber = null)
        {
            var req = new CreateTransactionRequest
            {
                Amount = amount,
                Currency1 = currency1,
                Currency2 = currency2,
                Address = toAddress,
                BuyerName = buyerName,
                BuyerEmail = buyerEmail,
                Custom = custom,
                ItemNumber = itemNumber,
                IpnUrl = ipn_url,
            };

            return CreateTransaction(req);
        }
        public async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request)
        {
            var req = new HttpUrlRequest(request);
            return await Process<CreateTransactionResponse>(req);
        }

        public async Task<CreateWithdrawalResponse> CreateWithdrawal(CreateWithdrawalRequest request)
        {
            var req = new HttpUrlRequest(request);
            return await Process<CreateWithdrawalResponse>(req);
        }

        public async Task<GetWithdrawalInfoResponse> GetWithdrawalInfo(string txnId)
        {
            var request = new GetWithdrawalInfoRequest { Id = txnId };
            var req = new HttpUrlRequest(request);
            return await Process<GetWithdrawalInfoResponse>(req);
        }

        private async Task<T1> Process<T1>(HttpUrlRequest request)
           where T1 : ResponseModel, new()
        {
            request.PublicKey = Configurations.PublicKey;
            request.PrivateKey = Configurations.PrivateKey;
            var response = await HttpUrlCaller.GetResponse(request);

            var result = new T1();
            result.HttpResponse = response;
            result.ProcessJson();

            return result;
        }
    }
}