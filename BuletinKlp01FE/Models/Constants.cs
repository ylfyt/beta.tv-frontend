using System;
using System.Collections.Generic;
using System.Text;

namespace BuletinKlp01FE.Models
{
    public class Constants
    {
        public static bool IsDev = true;
        
        public static string LoginUrl = "http://10.0.2.2:5000/api/User/login";
        
        public static string NotInternetText = "No internet, please reconnect!";
    }
}
