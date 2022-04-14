using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.user;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class APIRequest    {
    public static async Task<User?> MeQuery()
    {
        var enpoint = "/user/me";
        var response = await GetAuth<DataUser>(enpoint);
        
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

    public static async Task<ResponseDto<T>> GetAuth<T>(string endpoint, bool token = true)
    {
        try
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
            var response = await client.GetAsync(uri);

            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<ResponseDto<T>>(responseBody);

            if (!response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new ResponseDto<T>
            {
                Message = "Something wrong!"
            }; ;
        }
    }

    public static async Task<ResponseDto<T>> PostAuth<T>(string endpoint, object? data = null, bool token = true)
    {
        try
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
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

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
                    Message = responseData? .Message ?? "Failed"
                };
            }

            return responseData!;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new ResponseDto<T>
            {
                Message = "Something wrong!"
            }; ;
        }
    }
}

