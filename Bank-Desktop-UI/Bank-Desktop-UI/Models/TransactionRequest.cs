using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Desktop_UI.Models
{
    public class TransactionRequest
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public long FromAccountId { get; set; }
        public long ToAccountId { get; set; }
    }
}
