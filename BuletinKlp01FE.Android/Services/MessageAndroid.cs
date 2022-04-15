using Android.App;
using Android.Widget;
using BuletinKlp01FE.Droid.Services;
using BuletinKlp01FE.Services;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace BuletinKlp01FE.Droid.Services
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}