using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bank_Api.Models
{
    public class UserAuthentication
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual UserInfo UserInfo { get; set; }

    }
}
