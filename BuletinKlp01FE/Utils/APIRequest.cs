using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.user;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class APIRequest    {
    public static async Task<User?> MeQuery()
    {
        var enpoint = Constants.ENDPOINT_USER_ME;
        var response = await Send<DataUser>(enpoint);
        
        if (!response.Success)
        {
            return null;
        }

        return response.Data?.user;
    }

    public static HttpClient? GetClient(bool token)
    {
        HttpClient? client;
        if (token)
        {
            client = HttpClientGetter.GetHttpClientWithTokenHeader();
            if (client == null)
            {
                Redirects.ToLoginPage();
            }
        }
        else
        {
            client = HttpClientGetter.GetHttpClient();
        }

        return client;
    }

    public static async Task<ResponseDto<T>> Send<T> (string endpoint, string method = "GET", object? data = null, bool token = true)
    {
        var client = GetClient(token);
        if (client == null)
        {
            return new ResponseDto<T>
            {
                Message = "Failed"
            };
        }
        
        string uri = Constants.BASE_URL + endpoint;
        HttpResponseMessage response;
        
        if (method == "POST")
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, content);
        }
        else if (method == "PUT")
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            response = await client.PutAsync(uri, content);
        }
        else if (method == "DELETE")
        {
            response = await client.DeleteAsync(uri);
        }
        else
        {
            response = await client.GetAsync(uri);
        }

        string responseBody = await response.Content.ReadAsStringAsync();
        var responseData = JsonConvert.DeserializeObject<ResponseDto<T>>(responseBody);

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Redirects.ToLoginPage();
                return new ResponseDto<T>
                {
                    Message = "Not authorized!!"
                };
            }
            return new ResponseDto<T>
            {
                Message = responseData?.Message ?? "Failed"
            };
        }

        return responseData!;

    }
}

