using System;
using System.Net.Http;
using Xamarin.Essentials;

namespace BuletinKlp01FE.Services
{
    public class HttpClientGetter
    {
        private static HttpClient? instance;

        private static void Declare()
        {
            if (instance == null)
            {
                instance = new HttpClient();
                instance.Timeout = TimeSpan.FromMilliseconds(10000);
            }
        }

        public static HttpClient? GetHttpClientWithTokenHeader()
        {
            Declare();

            var token = Preferences.Get("token", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1c2VybmFtZSI6ImFkbWluIn0.WoTKCtz0wyHdS3vFW0EbjcMaP_5WnCcW1X3gXSvW5lo18xo7BDcwCl_rThQCM92yyObHVXXfTvAk3XFtsH7XLg");

            if (token.Length < 5)
            {
                Console.WriteLine("Please login first!!");
                return null;
            }

            if (!instance!.DefaultRequestHeaders.Contains("Authorization"))
            {
                instance.DefaultRequestHeaders.Add("Authorization", token);
            }

            return instance;
        }

        public static HttpClient GetHttpClient()
        {
            Declare();

            return instance!;
        }
    }
}
