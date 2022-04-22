using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using BuletinKlp01FE.Dtos.user;
using System.Threading.Tasks;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public bool isBusy = false;
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
                if (user != null && user.IsConfirmed)
                {
                    Redirects.ToHomePage();
                }

                if (user != null && !user.IsConfirmed)
                {
                    await ShowNotConfirmedMessage();
                }

                SetLoading();
                return;
            }
        }

        void RedirectToSignupPageTrigger(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupPage();
        }

        async Task ShowNotConfirmedMessage()
        {
            await DisplayAlert("Login gagal!", "Akun anda belum aktif. Silahkan periksa email anda terlebih dahulu untuk melakukan aktivasi akun!", "ok");
        }

        public async void SignInProcedure(object sender, EventArgs e)
        {
            try
            {
                if (isBusy)
                    return;

                string username = Entry_Username.Text;
                string password = Entry_Password.Text;

                if (username == null || password == null || username == "" || password == "")
                {
                    DependencyService.Get<IMessage>().ShortAlert("Input not valid");
                    return;
                }

                SetLoading(true);

                var response = await APIRequest.Send<DataUser>(
                    endpoint: Constants.ENDPOINT_USER_LOGIN,
                    method: "POST",
                    data: new { username, password },
                    token: false);

                if (!response.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Username or password is wrong!");
                    SetLoading();
                    return;
                }

                if (!response.Data!.user!.IsConfirmed)
                {
                    await ShowNotConfirmedMessage();
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
            {
                isBusy = true;
                LoginButton.Text = "Please wait...";
            }
            else
            {
                isBusy = false;
                LoginButton.Text = "Login";
            }
        }
    }
}