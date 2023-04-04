using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PB_HORTAGRAM
{
    public class User
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string Lastname { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("photoURL")]
        public string PhotoURL { get; set; }

        [JsonPropertyName("status")]
        public bool? Status { get; set; }

        [JsonPropertyName("followers")]
        public virtual IList<UsersFollowers> Followers { get; set; }

        [JsonPropertyName("posts")]
        public virtual IList<Post> Posts { get; set; }

        [JsonPropertyName("comments")]
        public virtual IList<Comment> Comments { get; set; }

        public User()
        {
        }

        public User(string _FirstName, string _Lastname, string _Email, string _Password, string _PhotoURL)
        {
            FirstName = _FirstName;
            Lastname = _Lastname;
            Email = _Email;
            Password = _Password;
            PhotoURL = _PhotoURL;
        }

        public User(string _FirstName, string _Lastname, string _Email, string _Password, string _PhotoURL, bool _Status)
        {
            FirstName = _FirstName;
            Lastname = _Lastname;
            Email = _Email;
            Password = _Password;
            PhotoURL = _PhotoURL;
            Status = _Status;
        }

        public User(Guid _Id, string _FirstName, string _Lastname, string _Email, string _Password, string _PhotoURL, bool _Status)
        {
            Id = _Id;
            FirstName = _FirstName;
            Lastname = _Lastname;
            Email = _Email;
            Password = _Password;
            PhotoURL = _PhotoURL;
            Status = _Status;
        }
    }
}

