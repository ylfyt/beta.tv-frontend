using System;
using System.Collections.Generic;
using System.Text;

namespace BuletinKlp01FE.Utils
{
    public class Constants
    {
        public static string BASE_URL { get; } = "http://10.0.2.2:5000/api";
        public static string LOGIN_END_POINT { get; } = BASE_URL + "/User/login";
        public static string REGISTER_END_POINT { get; } = BASE_URL + "/User/register";
        public static string NotInternetText { get; } = "No internet, please reconnect!";
    }
}
