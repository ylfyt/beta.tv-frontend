using System;
using System.Threading;
using System.Threading.Tasks;
using BuletinKlp01FE.Data;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Utils;
using BuletinKlp01FE.Views;
using Xamarin.Forms;

namespace BuletinKlp01FE
{
    public partial class App : Application
    {
        private static TokenDatabaseController tokenDatabase;
        private static UserDatabaseController userDatabase;
        private static RestService restService;

        private static Label labelScreen;
        private static bool hasInternet;
        private static Page currentPage;
        private static Timer timer;
        private static bool noInterShow;

        public App()
        {
            InitializeComponent();

            var video = new Video
            {
                Id = "_tEw-Him7ho",
                Url = "https://www.youtube.com/embed/_tEw-Him7ho",
                Title = "Tutorial xamarin 2 jam jadi professional",
                ChannelName = "CNN Indonesia",
                Category = "Tutorial",
                NewsAuthorName = "Budi Santoso",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras sit amet augue ultricies sapien faucibus commodo. Duis ultricies, mauris et rhoncus scelerisque, enim augue varius velit, at sollicitudin arcu diam at lectus. \n\nCurabitur orci lacus, elementum ut maximus at, mollis ut felis. Vestibulum placerat velit placerat dui rhoncus imperdiet. Morbi a dui sit amet massa ultrices condimentum quis a dolor. Ut vel ex sem. Cras laoreet leo vitae finibus convallis. Suspendisse nec tortor in nisl ullamcorper porttitor sed id nunc. \n\nUt vitae pellentesque felis, a accumsan turpis. Pellentesque sit amet sem tincidunt, viverra neque sit amet, consequat arcu. Etiam elementum felis sed lorem sollicitudin, quis luctus orci semper. Nam maximus convallis tortor ac dapibus. Donec molestie erat a semper suscipit."
            };

            MainPage = new NavigationPage(new VideoPlayer(video));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }

                return userDatabase;
            }
        }

        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (tokenDatabase == null)
                {
                    tokenDatabase = new TokenDatabaseController();
                }

                return tokenDatabase;
            }
        }

        public static RestService RestService
        {
            get
            {
                if (restService == null)
                {
                    restService = new RestService();
                }

                return restService;
            }
        }

        //------------Internet connection-------------//

        public static void StartCheckIfInternet(Label label, Page page)
        {
            labelScreen = label;
            label.Text = Constants.NotInternetText;
            label.IsVisible = false;

            hasInternet = true;
            currentPage = page;

            if (timer == null)
            {
                timer = new Timer((e) =>
                {
                    CheckIfInternetOverTime();
                }, null, 10, (int)TimeSpan.FromSeconds(3).TotalMilliseconds);
            }
        }

        private static void CheckIfInternetOverTime()
        {
            INetworkConnection networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if (!networkConnection.IsConnected)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (hasInternet)
                    {
                        if (!noInterShow)
                        {
                            hasInternet = false;
                            labelScreen.IsVisible = true;
                            await ShowDisplayAlert();
                        }
                    }
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    hasInternet = true;
                    labelScreen.IsVisible = false;
                });
            }
        }

        public static async Task<bool> CheckIfInternet()
        {
            INetworkConnection networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();

            return networkConnection.IsConnected;
        }

        public static async Task<bool> CheckIfInternetAlert()
        {
            INetworkConnection networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();

            if (!networkConnection.IsConnected)
            {
                if (!noInterShow)
                {
                    await ShowDisplayAlert();
                }

                return false;
            }

            return true;
        }

        private static async Task ShowDisplayAlert()
        {
            noInterShow = false;
            await currentPage.DisplayAlert("Internet", "Device has no internet, please reconnect", "Ok");
            noInterShow = false;
        }
    }
}
