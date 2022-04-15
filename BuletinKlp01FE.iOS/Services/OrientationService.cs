using BuletinKlp01FE.iOS.Services;
using BuletinKlp01FE.Services;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(OrientationService))]
namespace BuletinKlp01FE.iOS.Services
{
    public class OrientationService : IOrientationService
    {
        public void Landscape()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
        }

        public void Portrait()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
        }
    }
}