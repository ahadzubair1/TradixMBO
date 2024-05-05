using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBackOfficeDAL
{
    public enum PaymentStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,

        /// <summary>
        /// Authorized
        /// </summary>
        Authorized = 20,

        /// <summary>
        /// Paid
        /// </summary>
        Paid = 30,

        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled = 50,
    }

    public enum CoinPaymentStatus
    {
        Pending = 1,
        Complete = 2,
        Cancelled = -1
    }
}
