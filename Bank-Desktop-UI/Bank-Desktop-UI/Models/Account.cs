using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Desktop_UI.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long AccountNumber { get; set; }
        public double Money { get; set; }
    }
}
