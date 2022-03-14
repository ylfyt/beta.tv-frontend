using Android.App;
using Android.Content;
using Android.Net;
using BuletinKlp01FE.Data;
using BuletinKlp01FE.Droid.Data;

[assembly: Xamarin.Forms.Dependency(typeof(INetworkConnection))]
namespace TestProjectXamarin.Droid.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

        [System.Obsolete]
        public void CheckNetworkConnection()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);

            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;

            if (networkInfo != null && networkInfo.IsConnectedOrConnecting)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }
    }
}