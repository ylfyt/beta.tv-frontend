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
    public partial class ProfileChangePassword : ContentPage
    {
        public ProfileChangePassword()
        {
            InitializeComponent();
        }

        public void SaveEditPassword(object sender, EventArgs args)
        {
            DisplayAlert("Edit password", "Edit password berhasil", "Oke");
        }
    }
}