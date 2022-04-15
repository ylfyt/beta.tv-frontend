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
using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.user;

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
                Redirects.ToHomePage();
                return;
            }
        }

        void RedirectToSignupPageTrigger(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupPage();
        }

        void RedirectToHomePage()
        {
            Application.Current.MainPage = new NavigationPage(new SearchVideo());
        }

        public async void SignInProcedure(object sender, EventArgs e)
        {

            Button button = (sender as Button)!;
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

                HttpClient client = HttpClientGetter.GetHttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(new { username, password }), Encoding.UTF8, "application/json");
                string weburl = Constants.LOGIN_END_POINT;

                HttpResponseMessage httpResponseMessage = await client.PostAsync(weburl, content);
                var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDto<DataUser>>(responseBody);

                if (responseDto == null || !(responseDto.Success))
                {
                    DependencyService.Get<IMessage>().ShortAlert("Username or password incorrect");
                    button.Text = "Login";
                    return;
                }

                if (responseDto.Data?.token == "")
                {
                    DependencyService.Get<IMessage>().ShortAlert("Username or password incorrect");
                    button.Text = "Login";
                    return;
                }

                Preferences.Set("token", responseDto.Data?.token);
                Redirects.ToHomePage();
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