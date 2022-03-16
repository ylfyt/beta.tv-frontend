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
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Xamarin.Essentials;

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

        private void RedirectToHome()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            try
            { 
                User user = new User(SignupEntryName.Text, SignupEntryUsername.Text, SignupEntryEmail.Text, SignupEntryPassword.Text);

                button.Text = "Please Wait...";

                if (user.IsInputValid())    
                {   
                    var client = new HttpClient();
                    var postData = new List<KeyValuePair<string, string>>();

                    var content1 = new StringContent(JsonConvert.SerializeObject(new { name = user.Name, username = user.Username, email = user.Email, password = user.Password }), Encoding.UTF8, "application/json");

                    var content = new FormUrlEncodedContent(postData);
                    string weburl = Constants.REGISTER_END_POINT;
                    client.BaseAddress = new Uri(weburl);

                    var response = await client.PostAsync("", content1);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Gagal");
                        button.Text = "Register";
                        return;
                    }

                    string token = "";
                    foreach (var header in response.Headers)
                    {
                        if (header.Key.ToLower() == "authorization")
                        {
                            token = header.Value.First();
                            break;
                        }
                    }
                    if (token == "")
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Something wrong...");
                        return;
                    }
                    // TODO: Save token && redirect to home
                    Preferences.Set("token", token);
                    RedirectToHome();
                    return;
                }
                else
                {
                    button.Text = "Register";
                    DependencyService.Get<IMessage>().ShortAlert("Input not valid");
                }
                
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