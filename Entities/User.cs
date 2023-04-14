using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        public string Lastname { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("photoURL")]
        public string? PhotoUrl { get; set; }

        [JsonPropertyName("followers")]
        public virtual IList<UsersFollowers>? Followers { get; set; }

        [JsonPropertyName("following")]
        public virtual IList<UsersFollowers>? Following { get; set; }

        [JsonPropertyName("posts")]
        public virtual IList<Post>? Posts { get; set; }

        [JsonPropertyName("comments")]
        public virtual IList<Comment>? Comments { get; set; }

        public User()
        {
        }

        public User(string _FirstName, string _Lastname, string _Email, string _Password, string _PhotoUrl)
        {
            FirstName = _FirstName;
            Lastname = _Lastname;
            Email = _Email;
            Password = _Password;
            PhotoUrl = _PhotoUrl;
        }

        public User(string _FirstName, string _Lastname, string _Email, string _Password, string _PhotoUrl, bool _Status)
        {
            FirstName = _FirstName;
            Lastname = _Lastname;
            Email = _Email;
            Password = _Password;
            PhotoUrl = _PhotoUrl;
        }

        public User(Guid _Id, string _FirstName, string _Lastname, string _Email, string _Password, string _PhotoUrl, bool _Status)
        {
            Id = _Id;
            FirstName = _FirstName;
            Lastname = _Lastname;
            Email = _Email;
            Password = _Password;
            PhotoUrl = _PhotoUrl;
        }
    }
}

