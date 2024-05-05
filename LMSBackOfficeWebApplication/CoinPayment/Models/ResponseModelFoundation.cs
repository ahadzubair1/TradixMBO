using System.Runtime.Serialization;

namespace LMSBackOfficeWebApplication.CoinPayment
{
    [DataContract]
    public class ResponseModelFoundation<T> : ResponseModel
    {
        public string Error { get; set; }
        public T Result { get; set; }

        public bool IsSuccess { get { return Error == "ok"; } }
    }
}
