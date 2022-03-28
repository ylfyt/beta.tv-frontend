﻿using Android.App;
using Android.Views;
using BuletinKlp01FE.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarImplementation))]
namespace BuletinKlp01FE.Droid.Services
{
    public class StatusBarImplementation : IStatusBar
    {
        public StatusBarImplementation()
        {
        }

        WindowManagerFlags _originalFlags;

        #region IStatusBar implementation

        [System.Obsolete]
        public void HideStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= Android.Views.WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }

        [System.Obsolete]
        public void ShowStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
        }

        #endregion
    }
}