using LMSBackOfficeWebApplication.Utitlity;
using ServiceStack.Text;
using System.Collections.Generic;

namespace LMSBackOfficeWebApplication.CoinPayment
{
    public class HttpUrlRequest
    {
        public HttpUrlRequest(object param, string method = "POST", string url = "https://www.coinpayments.net/api.php")
        {
            Method = method;
            RequestUrl = url;

            if (param != null)
            {
                RequestBody = JsonSerializer.SerializeToString(param);
            }
        }

        public string Method { get; set; }
        public string RequestUrl { get; set; }

        public string RequestBody { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }

        public string GetQueryString()
        {
            var dict = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(RequestBody);

            dict.Add("version", "1");
            dict.Add("key", PublicKey);

            return CryptoUtil.DictionaryToFormData(dict);
        }
    }
}
