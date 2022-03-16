using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {

            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SignInProcedure(s, e);
        }

        protected override void OnAppearing()
        {
            string token = Preferences.Get("token", "");
            if (token != "")
            {
                RedirectToHome();
                return;
            }
        }

        private void RedirectToHome()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        void RedirectToSignupPageTrigger(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupPage();
        }

        async void SignInProcedure(object sender, EventArgs e)
        {
            Button button = sender as Button;
            try
            {
                string username = Entry_Username.Text;
                string password = Entry_Password.Text;

                if (username == null || password == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Input not valid");
                    return;
                }

                if (username == "" || password == "")
                {
                    DependencyService.Get<IMessage>().ShortAlert("Input not valid");
                    return;
                }

                button.Text = "Please wait...";

                HttpClient client = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(new { username, password }), Encoding.UTF8, "application/json");
                string weburl = Constants.LOGIN_END_POINT;
                HttpResponseMessage httpResponseMessage = await client.PostAsync(weburl, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string token = "";
                    foreach (var header in httpResponseMessage.Headers)
                    {
                        if (header.Key.ToLower() == "authorization")
                        {
                            token = header.Value.First();
                            break;
                        }
                    }

                    if (token == "")
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Username or password incorrect");
                    }
                    else
                    {
                        // TODO: Save token && redirect to home
                        Preferences.Set("token", token);
                        RedirectToHome();
                    }

                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Username or password incorrect");
                }

                button.Text = "Login";
            }
            catch (Exception ex)
            {
                button.Text = "Login";
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}