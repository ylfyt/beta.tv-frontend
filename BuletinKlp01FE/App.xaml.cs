﻿using BuletinKlp01FE.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuletinKlp01FE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Preferences.Remove("token");
            MainPage = new NavigationPage(new Homepage());
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
    }
}
