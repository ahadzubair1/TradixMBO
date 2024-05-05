using ServiceStack;
using ServiceStack.Text;

namespace LMSBackOfficeWebApplication.CoinPayment
{
    public abstract class ResponseModel
    {
        public HttpUrlResponse HttpResponse { get; set; }

        public void ProcessJson()
        {
            if (HttpResponse.IsSuccessStatusCode)
            {
                var obj = JsonSerializer.DeserializeFromString(HttpResponse.ContentBody, this.GetType());
                this.PopulateWithNonDefaultValues(obj);
            }
        }
    }
}
