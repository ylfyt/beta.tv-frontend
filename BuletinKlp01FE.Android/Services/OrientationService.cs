using Android.App;
using Android.Content.PM;
using BuletinKlp01FE.Droid.Services;
using BuletinKlp01FE.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(OrientationService))]
namespace BuletinKlp01FE.Droid.Services
{
    public class OrientationService : IOrientationService
    {
        public void Landscape()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Landscape;
        }

        public void Portrait()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Portrait;
        }
    }
}