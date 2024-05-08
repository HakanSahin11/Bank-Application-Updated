using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Bank_Api.Models
{
    public class UserInfo
    {
        [ForeignKey("UserAuthentication")]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [JsonIgnore]
        public virtual UserAuthentication UserAuthentication { get; set; }
        [JsonIgnore]
        public virtual List<Account> Accounts { get; set; } 
    }
}
