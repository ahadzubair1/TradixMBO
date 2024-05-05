using System.Runtime.Serialization;

namespace LMSBackOfficeWebApplication.CoinPayment
{
    public class CreateWithdrawalResponse : ResponseModelFoundation<CreateWithdrawalResponse.Item>
    {
        [DataContract]
        public class Item
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }
            [DataMember(Name = "status")]
            public int Status { get; set; }
            [DataMember(Name = "amount")]
            public decimal Amount { get; set; }
        }
    }
}
