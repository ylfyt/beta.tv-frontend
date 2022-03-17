using BuletinKlp01FE.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuletinKlp01FE
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            string token = Preferences.Get("token", "");
            if (token == "")
            {
                DependencyService.Get<IMessage>().ShortAlert("Token not found");
                return;
            }
            DependencyService.Get<IMessage>().ShortAlert(token);
        }
    }
}
