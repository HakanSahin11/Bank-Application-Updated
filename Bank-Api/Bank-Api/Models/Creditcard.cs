using System.Text.Json.Serialization;

namespace Bank_Api.Models
{
    public class Creditcard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long CardNo { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}
