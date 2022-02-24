using BuletinKlp01FE.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuletinKlp01FE.Data
{
    class RestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }
        /*
        public async Task Signup(User user)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("username", user.Username));
            postData.Add(new KeyValuePair<string, string>("name", user.Name));
            postData.Add(new KeyValuePair<string, string>("email", user.Email));
            postData.Add(new KeyValuePair<string, string>("password", user.Password));

            var content = new FormUrlEncodedContent(postData);
            var weburl = "http://localhost:5000/api/User/register";

            try
            {
                var response = await client.PostAsync(weburl, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
            }
            var result = await client.PostAsync(weburl, content);
            if (result.IsSuccessStatusCode)
            {
                var jsonResult = result.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<string>(jsonResult);
                var a = result.Headers.GetValues("authorization");
                return a.ToString();
            }
            return null;
        }
        */
    }
}
