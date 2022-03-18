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
            try
            {
                Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
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