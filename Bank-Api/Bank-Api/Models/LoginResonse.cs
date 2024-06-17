using System.ComponentModel.DataAnnotations.Schema;

namespace Bank_Api.Models
{
    public class LoginResonse : UserInfo
    {
        [ForeignKey("UserAuthentication")]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Token { get; set; }

        public LoginResonse(UserInfo user, string token)
        {
            Id = user.Id;
            Firstname = user.Firstname; 
            Lastname = user.Lastname;
            Token = token;
        }
    }
}
