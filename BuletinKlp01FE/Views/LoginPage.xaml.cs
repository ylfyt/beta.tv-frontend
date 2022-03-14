using System;
using System.Net.Http;
using System.Threading.Tasks;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Views;
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

        async void SignInProcedure(object sender, EventArgs e)
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

            Button button = sender as Button;
            button.Text = "Please wait...";

            HttpClient client = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(new {  username, password }), Encoding.UTF8, "application/json");
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
                }

            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Username or password incorrect");
            }

            button.Text = "Login";
        }
    }
}