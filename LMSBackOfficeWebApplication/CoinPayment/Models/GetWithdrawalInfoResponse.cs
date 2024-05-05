using System.Runtime.Serialization;

namespace LMSBackOfficeWebApplication.CoinPayment
{
    public class GetWithdrawalInfoResponse : ResponseModelFoundation<GetWithdrawalInfoResponse.Item>
    {
        [DataContract]
        public class Item
        {
            [DataMember(Name = "time_created")]
            public int TimeCreated { get; set; }
            [DataMember(Name = "status")]
            public int Status { get; set; }
            [DataMember(Name = "status_text")]
            public string StatusText { get; set; }
            [DataMember(Name = "coin")]
            public string Coin { get; set; }
            [DataMember(Name = "amount")]
            public long Amount { get; set; }
            [DataMember(Name = "amountf")]
            public decimal AmountFloat { get; set; }
            [DataMember(Name = "send_address")]
            public string SendAddress { get; set; }
            [DataMember(Name = "send_txid")]
            public string SendTxid { get; set; }
        }
    }
}
