using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BuletinKlp01FE.Models
{
    public class User
    {
        //[PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        // constructors
        public User() { }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public bool isInputValid()
        {
            return (!this.Username.Equals("") && !this.Password.Equals(""));
        }

        public bool CheckInformation()
        {
            if (string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password))
                return false;

            return true;
        }
    }

}
