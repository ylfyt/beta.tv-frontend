using Android.Views;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using BuletinKlp01FE.Droid.CustomRenderer;
using Google.Android.Material.BottomNavigation;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(MyTabsRenderer))]
namespace BuletinKlp01FE.Droid.CustomRenderer
{
    public class MyTabsRenderer : TabbedPageRenderer
    {
        [System.Obsolete]
        public MyTabsRenderer()
        {

        }

        [System.Obsolete]
        public MyTabsRenderer(Context context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (!(GetChildAt(0) is ViewGroup layout))
                return;

            if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
                return;

            var topShadow = LayoutInflater.From(Context).Inflate(Resource.Layout.view_shadow, null);

            var layoutParams =
                new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.MatchParent, 15);
            layoutParams.AddRule(LayoutRules.Above, bottomNavigationView.Id);

            layout.AddView(topShadow, 2, layoutParams);

        }
    }
}