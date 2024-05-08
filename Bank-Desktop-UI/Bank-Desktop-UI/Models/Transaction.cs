using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Desktop_UI.Models
{
    public class Transaction
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
