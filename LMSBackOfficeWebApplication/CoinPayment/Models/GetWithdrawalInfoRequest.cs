using System.Runtime.Serialization;

namespace LMSBackOfficeWebApplication.CoinPayment
{
    [DataContract]
    public class GetWithdrawalInfoRequest
    {
        public GetWithdrawalInfoRequest()
        {
            Cmd = "	get_withdrawal_info";
        }

        [DataMember(Name = "cmd")]
        public string Cmd { get; private set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}
