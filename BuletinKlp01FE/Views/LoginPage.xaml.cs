using System;
using System.Net.Http;
using System.Threading.Tasks;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;


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
            User user = new User(Entry_Username.Text, Entry_Password.Text);

            if (user.CheckInformation())
            {
                Token token = await App.RestService.Login(user);

                if (token.access_token != "")
                {
                   // App.UserDatabase.SaveUser(user);
                    //App.TokenDatabase.SaveToken(token);
                    
                    await DisplayAlert("Login", token.access_token, "Ok");
                    Preferences.Set("token", token.access_token);
                    //TO DO: REDIRECT TO HOMEPAGE
                    if (Device.OS == TargetPlatform.Android)
                    {
                        await DisplayAlert("Great", "You can quit now", "I might");
                    }
                    /*else if (Device.OS == TargetPlatform.iOS)
                    {
                        
                    }
                    else
                    {
                        
                    }*/
                } 
                else
                {
                    await DisplayAlert("Empty", "Login information is incorrect", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Login", "Login Not Correct, empty username or password.", "Ok");
            }
            
            /*await DisplayAlert("Jalan", "Login jalan", "Ok");
            var client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://10.0.2.2:5000/WeatherForecast");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                await DisplayAlert("Login", responseBody, "Ok");
            }
            catch (HttpRequestException er)
            {
                await DisplayAlert("Error", er.Message, "Ok");
                Console.WriteLine("\nException Caught!");
                //Console.WriteLine("Message");
            }*/
        }
    }
}