using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSBackOfficeWebApplication.Models
{
    public class TicketModel
    {
        public string TicketTitle { get; set; }
        public string TicketType { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
    }

    public class MemberShipModel
    {
        public string Amount { get; set; }
        public string MemberShipCode { get; set; }
    }
}