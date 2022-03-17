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
    public partial class ProfileChangePersonalData : ContentPage
    {
        public ProfileChangePersonalData()
        {
            InitializeComponent();
        }

        public void SaveEditProfile(object sender, EventArgs args)
        {
            DisplayAlert("Edit profil", "Edit profil berhasil", "Oke");
        }
    }
}