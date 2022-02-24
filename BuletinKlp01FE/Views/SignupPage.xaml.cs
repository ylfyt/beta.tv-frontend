using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

using BuletinKlp01FE.Models;
using BuletinKlp01FE.Data;


namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async Task Button_Clicked(object sender, EventArgs e)
        {
            User user = new User(SignupEntryName.Text, SignupEntryUsername.Text, SignupEntryEmail.Text, SignupEntryPassword.Text);

            if (!user.isInputValid())
            {
                return;
            }

            // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
            HttpClient client = new HttpClient();

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
                await DisplayAlert("Sign up", responseBody, "OK");
            }
            catch (HttpRequestException error)
            {
                await DisplayAlert("error", "gagal signup", "ok");
            }
        }
    }
}