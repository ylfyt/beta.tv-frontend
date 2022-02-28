using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

using BuletinKlp01FE.Models;
using Newtonsoft.Json;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            User user = new User(SignupEntryName.Text, SignupEntryUsername.Text, SignupEntryEmail.Text, SignupEntryPassword.Text);

            if (user.isInputValid())
            {

                var client = new HttpClient();

                if (client == null)
                {
                    await DisplayAlert("null", "client null", "no");
                    return;
                }

                var postData = new List<KeyValuePair<string, string>>();
                if (postData == null)
                {
                    await DisplayAlert("null", "postdata null", "no");
                    return;
                }
                //postData.Add(new KeyValuePair<string, string>("username", user.Username));
                //postData.Add(new KeyValuePair<string, string>("name", user.Name));
                //postData.Add(new KeyValuePair<string, string>("email", user.Email));
                //postData.Add(new KeyValuePair<string, string>("password", user.Password));

                var content1 = new StringContent(JsonConvert.SerializeObject(new { name = user.Name, username = user.Username, email = user.Email, password = user.Password }), Encoding.UTF8, "application/json");

                var content = new FormUrlEncodedContent(postData);
                string weburl = "http://10.0.2.2:5000/api/User/register";
                client.BaseAddress = new Uri(weburl);

                var response = await client.PostAsync("", content1);
                if (!response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Gagal", "Sign up gagal", "Oke");
                    return;
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Sukses", "Sign up berhasil", "Oke");
                return;
            }
        }
    }
}