using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Shared_Classes.Models
{
    public class TransactionRequest()
    {
        [Required(ErrorMessage = "Amount to transfer is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "Must select an account to transfer funds from")]
        public long FromAccountId { get; set; }
        [Required(ErrorMessage = "Must select an account to transfer funds to")]
        public long ToAccountId { get; set; }
        public string Note { get; set; } = "";
    }
}
