using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using BuletinKlp01FE.Utils;
using BuletinKlp01FE.Services;
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
            try
            {
                if (!IsValidInput())
                {
                    DependencyService.Get<IMessage>().ShortAlert("Input not valid");
                    return;
                }

                string name = SignupEntryName.Text;
                string username = SignupEntryUsername.Text;
                string email = SignupEntryEmail.Text;
                string password = SignupEntryPassword.Text;

                SetLoading(true);

                var response = await APIRequest.Send<DataUser>(
                    endpoint: Constants.ENDPOINT_USER_REGISTER, 
                    method: "POST", 
                    data: new { name, email, username, password}, 
                    token: false);

                if (!response.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert(response.Message);
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
                RegisterButton.Text = "Please wait...";
            else
                RegisterButton.Text = "Login";
        }
    }
}