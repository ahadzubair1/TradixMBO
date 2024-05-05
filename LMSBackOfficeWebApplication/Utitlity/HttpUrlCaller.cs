using LMSBackOfficeWebApplication.CoinPayment;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMSBackOfficeWebApplication.Utitlity
{
    public class HttpUrlCaller
    {
        public async static Task<HttpUrlResponse> GetResponse(HttpUrlRequest request)
        {
            var absoluteUri = request.RequestUrl;

            var body = request.GetQueryString();
            var method = request.Method;

            var privateKey = request.PrivateKey;

            var signature = CryptoUtil.CalcSignature(body, privateKey);

            using (var httpClient = new System.Net.Http.HttpClient())
            {
                HttpResponseMessage response;

                httpClient.DefaultRequestHeaders.Add("HMAC", signature);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                switch (method)
                {
                    case "GET":
                        response = await httpClient.GetAsync(absoluteUri);
                        break;
                    case "POST":
                        var requestBody = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
                        //var requestBody = new StringContent(body);
                        response = httpClient.PostAsync(absoluteUri, requestBody).Result;
                        break;
                    default:
                        throw new NotImplementedException("The supplied HTTP method is not supported: " + method ?? "(null)");
                }

                var contentBody = await response.Content.ReadAsStringAsync();
                var headers = response.Headers.AsEnumerable();
                var statusCode = response.StatusCode;
                var isSuccess = response.IsSuccessStatusCode;

                var genericExchangeResponse = new HttpUrlResponse(statusCode, isSuccess, headers, contentBody, absoluteUri, body);
                return genericExchangeResponse;

            }
        }

    }
}