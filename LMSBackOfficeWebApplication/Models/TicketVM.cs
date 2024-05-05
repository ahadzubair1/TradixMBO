using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSBackOfficeWebApplication.Models
{
    public class TicketVM
    {
        public string TicketTitle { get; set; }
        public string TicketCode { get; set; }
        public string TicketType { get; set; }
        public string Description { get; set; }
        public string TicketPriority { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
    }

    public class TransactionVM
    {
        public string TransactionCode { get; set; }
        public string TransactionType { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string TransactionFee { get; set; }
        public string TransactionDate { get; set; }
        public string Status { get; set; }
    }
}