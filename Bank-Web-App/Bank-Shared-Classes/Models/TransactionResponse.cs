using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace Bank_Shared_Classes.Models
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Sender { get; set; }
        public double Amount { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public string SenderName { get; set; }
    }
}
