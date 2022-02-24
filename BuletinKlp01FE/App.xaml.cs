﻿using BuletinKlp01FE.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SignupPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
