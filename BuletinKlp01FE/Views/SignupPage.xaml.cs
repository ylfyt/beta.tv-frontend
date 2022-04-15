using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using BuletinKlp01FE.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

using BuletinKlp01FE.Utils;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.user;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        public void RedirectToLoginPageTrigger(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }

        public bool IsValidInput()
        {
            string Name = SignupEntryName.Text;
            string Username = SignupEntryUsername.Text;
            string Email = SignupEntryEmail.Text;
            string Password = SignupEntryPassword.Text;
            if (Username == null || Name == null || Email == null || Password == null)
            {
                return false;
            }

            return (Username != "" && !Name.Equals("") && !Email.Equals("") && !Password.Equals(""));
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (sender as Button)!;
            try
            {
                button.Text = "Please Wait...";

                if (!IsValidInput())
                {
                    button.Text = "Register";
                    DependencyService.Get<IMessage>().ShortAlert("Input not valid");
                    return;
                }

                var client = HttpClientGetter.GetHttpClient();
                var postData = new List<KeyValuePair<string, string>>();

                var content1 = new StringContent(JsonConvert.SerializeObject(new { name = SignupEntryName.Text, username = SignupEntryUsername.Text, email = SignupEntryEmail.Text, password = SignupEntryPassword.Text }), Encoding.UTF8, "application/json");

                var content = new FormUrlEncodedContent(postData);
                string weburl = Constants.REGISTER_END_POINT;

                var response = await client.PostAsync(weburl, content1);
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDto<DataUser>>(responseBody);

                if (responseDto == null || !(responseDto.Success) || responseDto.Data?.token == "")
                {
                    DependencyService.Get<IMessage>().ShortAlert(responseDto.Message);
                    button.Text = "Login";
                    return;
                }

                Preferences.Set("token", responseDto.Data?.token);
                Redirects.ToHomePage();
            }
            catch (Exception ex)
            {
                button.Text = "Register";
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }

        }
    }
}