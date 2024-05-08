using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Desktop_UI.Models
{
    public class CreateUser
    {
        public CreateUser(string email, string password, string firstname, string lastname)
        {
            Email = email;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
