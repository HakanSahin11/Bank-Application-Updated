using System.Text.Json.Serialization;

namespace Bank_Api.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long AccountNumber { get; set; }
        public double Money { get; set; }
        [JsonIgnore]
        public virtual List<Creditcard> CreditCards { get; set;}
        [JsonIgnore]
        public virtual UserInfo UserInfo { get; set; }
    }
}
