using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LMSBackOfficeWebApplication.Utitlity
{
    public class CryptoUtil
    {
        public static string CalcSignature(string input, string key = null)
        {
            // Use input string to calculate MD5 hash
            using (var md5 = getHasher(key))
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

        private static HashAlgorithm getHasher(string key)
        {
            if (key == null)
            {
                return HMACSHA512.Create();
            }
            else
            {
                byte[] keyBytes = Encoding.ASCII.GetBytes(key);
                return new HMACSHA512(keyBytes);
            }
        }

        public static string DictionaryToFormData(Dictionary<string, string> dict)
        {
            var query = string.Join("&", dict.Keys.Select(key => string.Format("{0}={1}",
                key, HttpUtility.UrlEncode(dict[key]))));

            return query;
        }

        public static CoinPaymentResponseBody ConvertQueryStringToObject(string responseString)
        {

            var dict = HttpUtility.ParseQueryString(responseString);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
            CoinPaymentResponseBody respObj = JsonConvert.DeserializeObject<CoinPaymentResponseBody>(json);

            return respObj;
        }
    }

    public class CoinPaymentResponseBody
    {
        public string cmd { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string address { get; set; }
        public string note { get; set; }
        public int version { get; set; }
        public string key { get; set; }

    }
}