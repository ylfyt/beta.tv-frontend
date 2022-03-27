using BuletinKlp01FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        string homePicSource = "carbon_home_blue.png";
        string homeBlack = "carbon_home.png";
        string homeBlue = "carbon_home_blue.png";
        public string HomePicSource
        {
            get => homePicSource;
            set
            {
                if (value == homePicSource) return;
                homePicSource = value;
                OnPropertyChanged("HomePicSource");
            }
        }

        string searchPicSource = "bx_search.png";
        string searchBlack = "bx_search.png";
        string searchBlue = "bx_search_blue.png";
        public string SearchPicSource
        {
            get => searchPicSource;
            set
            {
                if (value == searchPicSource) return;
                searchPicSource = value;
                OnPropertyChanged("SearchPicSource");
            }
        }

        string subPicSource = "ic_sharp_subscriptions.png";
        string subBlack = "ic_sharp_subscriptions.png";
        string subBlue = "ic_sharp_subscriptions_blue.png";
        public string SubPicSource
        {
            get => subPicSource;
            set
            {
                if (value == subPicSource) return;
                subPicSource = value;
                OnPropertyChanged("SubPicSource");
            }
        }

        string profilePicSource = "Vector.png";
        string profileBlack = "Vector.png";
        string profileBlue = "Vector_blue.png";
        public string ProfilePicSource
        {
            get => profilePicSource;
            set
            {
                if (value == profilePicSource) return;
                profilePicSource = value;
                OnPropertyChanged("ProfilePicSource");
            }
        }
        private void HomePic_OnClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SearchPic_OnClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SubPic_OnClicked(object sender, EventArgs e)
        {
            /*
            var video = new Video
            {
                Id = "_tEw-Him7ho",
                Url = "https://www.youtube.com/watch?v=fCw2NZfR74E",
                Title = "Tutorial xamarin 2 jam jadi professional",
                ChannelName = "CNN Indonesia",
                Category = "Tutorial",
                NewsAuthorName = "Budi Santoso",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras sit amet augue ultricies sapien faucibus commodo. Duis ultricies, mauris et rhoncus scelerisque, enim augue varius velit, at sollicitudin arcu diam at lectus. \n\nCurabitur orci lacus, elementum ut maximus at, mollis ut felis. Vestibulum placerat velit placerat dui rhoncus imperdiet. Morbi a dui sit amet massa ultrices condimentum quis a dolor. Ut vel ex sem. Cras laoreet leo vitae finibus convallis. Suspendisse nec tortor in nisl ullamcorper porttitor sed id nunc. \n\nUt vitae pellentesque felis, a accumsan turpis. Pellentesque sit amet sem tincidunt, viverra neque sit amet, consequat arcu. Etiam elementum felis sed lorem sollicitudin, quis luctus orci semper. Nam maximus convallis tortor ac dapibus. Donec molestie erat a semper suscipit."
            };

            try
            {
                Navigation.PushAsync(new VideoPlayer(video));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            */
        }
        private void ProfilePic_OnClicked(object sender, EventArgs e)
        {
            try
            {
                //Navigation.PushAsync(new Profile());
                Application.Current.MainPage = new NavigationPage(new Profile())
                {
                    BarTextColor = Color.FromHex("#3F72AF"),
                    BarBackgroundColor = Color.White,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}