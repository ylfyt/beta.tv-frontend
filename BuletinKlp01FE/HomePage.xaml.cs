using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
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
            /*if (homePicSource == homeBlack)
            {
                homePicSource = homeBlue;
                searchPicSource = searchBlack;
                subPicSource = subBlack;
                profilePicSource = profileBlack;
            }*/
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
            /*if (searchPicSource == searchBlack)
            {
                searchPicSource = searchBlue;
                homePicSource = homeBlack;
                subPicSource = subBlack;
                profilePicSource = profileBlack;
            }*/
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
            /*if (subPicSource == subBlack)
            {
                subPicSource = subBlue;
                homePicSource = homeBlack;
                searchPicSource = searchBlack;
                profilePicSource = profileBlack;
            }*/
        }
        private void ProfilePic_OnClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            /*if (profilePicSource == profileBlack)
            {
                profilePicSource = profileBlue;
                homePicSource = homeBlack;
                searchPicSource = searchBlack;
                subPicSource = subBlack;
            }*/
        }
    }
}