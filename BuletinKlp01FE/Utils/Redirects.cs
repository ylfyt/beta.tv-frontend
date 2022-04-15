using BuletinKlp01FE.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuletinKlp01FE.Utils
{
    public static class Redirects
    {
        public static void ToHomePage()
        {
            Application.Current.MainPage = new MainPage();
        }

        public static void ToLoginPage()
        {
            Preferences.Remove("token");
            Application.Current.MainPage = new LoginPage();
        }
    }
}
