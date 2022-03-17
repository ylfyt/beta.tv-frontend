using System;
using System.Collections.Generic;
using System.Text;

namespace BuletinKlp01FE.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        // constructors
        public User() { }

        public User(string name, string username, string email, string pass)
        {
            this.Username = username;
            this.Name = name;
            this.Email = email;
            this.Password = pass;
        }

        public bool IsInputValid()
        {

            if (Username == null || Name == null || Email == null || Password == null)
            {
                return false;
            }

            return (Username != "" && !Name.Equals("") && !Email.Equals("") && !Password.Equals(""));
        }
    }
}
