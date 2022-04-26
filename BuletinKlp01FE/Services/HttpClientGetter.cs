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

        public static void ResetHttpClient()
        {
            instance = null;
        }

        public static HttpClient? GetHttpClientWithTokenHeader()
        {
            Declare();

            var token = Preferences.Get("token", "");

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
