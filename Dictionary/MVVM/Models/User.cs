using Newtonsoft.Json;
using System;

namespace Dictionary.Models
{
    public class User
    {
        private string Username { get; set; }
        private string Password { get; set; }

        public User()
        {
            Username=string.Empty;
            Password=string.Empty;
        }
        [JsonConstructor]
        public User(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or empty.", nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));
            }

            Username = username;
            Password = password;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User other = (User)obj;
            return this.Username == other.Username && this.Password == other.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username, Password);
        }
    }
}
