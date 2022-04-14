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

        protected override async void OnAppearing()
        {
            string token = Preferences.Get("token", "");
            if (token != "")
            {
                SetLoading(true);
                var user = await APIRequest.MeQuery();
                if (user != null)
                {
                    Redirects.ToHomePage();
                }
                SetLoading();
                return;
            }
        }

        void RedirectToSignupPageTrigger(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupPage();
        }

        public async void SignInProcedure(object sender, EventArgs e)
        {
            try
            {
                string username = Entry_Username.Text;
                string password = Entry_Password.Text;

                if (username == null || password == null || username == "" || password == "")
                {
                    DependencyService.Get<IMessage>().ShortAlert("Input not valid");
                    return;
                }

                SetLoading(true);

                var response = await APIRequest.PostAuth<DataUser>(endpoint: "/user/login", data: new
                {
                    username, password,
                }, token: false);

                if (!response.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Username or password is wrong!");
                    SetLoading();
                    return;
                }

                Preferences.Set("token", response.Data?.token);
                Redirects.ToHomePage();
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
                SetLoading();
            }
        }

        void SetLoading(bool loading = false)
        {
            if (loading)
                LoginButton.Text = "Please wait...";
            else
                LoginButton.Text = "Login";
        }
    }
}