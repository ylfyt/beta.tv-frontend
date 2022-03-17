using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BuletinKlp01FE.Models;
using System.Linq;

namespace BuletinKlp01FE.Data
{
    public class RestService
    {
        private HttpClient client;

        private string grant_type = "password";

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlenconded"));
        }

        public async Task<Token> Login(string username, string password)
        {
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();

            var content = new StringContent(JsonConvert.SerializeObject(new { username, password }), Encoding.UTF8, "application/json");
            string weburl = "http://10.0.2.2:5000/api/User/login";
            HttpResponseMessage httpResponseMessage = await client.PostAsync(weburl, content);
            Token token;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string temp = "";
                foreach (var header in httpResponseMessage.Headers)
                {
                    if (header.Key.ToLower() == "authorization")
                    {
                        temp = header.Value.First();
                        break;
                    }
                }
                token = new Token() { access_token = temp };
            }
            else
            {
                token = new Token() { access_token = "" };
            }

            return token;
        }

        public async Task<T> PostResponseLogin<T>(string weburl, FormUrlEncodedContent content) where T : class
        {
            HttpResponseMessage httpResponseMessage = await client.PostAsync(weburl, content);
            //Token token = new Token() { access_token = httpResponseMessage.Headers.GetValues("authorization").ToString()};
            //token.expire_date = DateTime.Now.AddMinutes(token.expires_in);

            string jsonResult = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResult);
        }

        public async Task<T> PostResponse<T>(string weburl, string jsonstring) where T : class
        {
            Token token = App.TokenDatabase.GetToken();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Berear", token.access_token);

            try
            {
                HttpResponseMessage httpResponseMessage = await client.PostAsync(weburl, new StringContent(jsonstring, Encoding.UTF8, "application/json"));

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonResult = await httpResponseMessage.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }

        public async Task<T> GetResponse<T>(string weburl) where T : class
        {
            Token token = App.TokenDatabase.GetToken();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Berear", token.access_token);

            try
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(weburl);

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonResult = await httpResponseMessage.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }
    }
}