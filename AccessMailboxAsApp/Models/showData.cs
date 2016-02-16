using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccessMailboxAsApp.Models
{
    public class showData
    {
        public string ReceiveMail { get; set; }
        public string SendMail { get; set; }
        public string OutstandingMail { get; set; }
        public Product ProductData { get; set; }
    }
    public class Product
    {
        public string ReceivedCount { get; set; }
        public string SendCount { get; set; }
        public string[] timeFrame { get; set; }
        public string TotalReceive { get; set; }
        public string TotalSend { get; set; }
        public string TotalUnaction { get; set; }
        public string TotalReceiveCom { get; set; }
        public string TotalSendCom { get; set; }
        public string TotalUnactionCom { get; set; }
        public string TotalReceiveComWhole { get; set; }
        public string TotalSendComWhole { get; set; }
        public string userResponse { get; set; }

    }
}