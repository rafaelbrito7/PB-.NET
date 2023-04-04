using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageBase64 { get; set; }
        public bool Status { get; set; }

        public UserRequest()
        {
        }

        public UserRequest(string firstName, string lastname, string email, string password, string imageBase64)
        {
            FirstName = firstName;
            Lastname = lastname;
            Email = email;
            Password = password;
            ImageBase64 = imageBase64;
            Status = true;
        }
    }
}
