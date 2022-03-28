using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        public void BeritaSayaClicked(object sender, EventArgs args)
        {
            //DisplayAlert("aya", "ayajelek", "no");
            var btnberita = this.FindByName<Button>("btnBerita");
            var btnpengaturan = this.FindByName<Button>("btnPengaturan");
            btnberita.TextColor = Color.FromHex("3F72AF");
            btnpengaturan.TextColor = Color.FromHex("#ABABAB");

            var lineberita = this.FindByName<BoxView>("lineBerita");
            var linepengaturan = this.FindByName<BoxView>("linePengaturan");
            lineberita.BackgroundColor = Color.FromHex("3F72AF");
            linepengaturan.BackgroundColor = Color.FromHex("#ABABAB");

            var pengaturan = this.FindByName<StackLayout>("pengaturan");
            pengaturan.IsVisible = false;

            var beritasaya = this.FindByName<StackLayout>("beritasaya");
            beritasaya.IsVisible = true;
        }

        public void PengaturanClicked(object sender, EventArgs args)
        {
            //DisplayAlert("aya", "ayajelek", "no");
            var btnpengaturan = this.FindByName<Button>("btnPengaturan");
            var btnberita = this.FindByName<Button>("btnBerita");
            btnpengaturan.TextColor = Color.FromHex("3F72AF");
            btnberita.TextColor = Color.FromHex("#ABABAB");

            var lpengaturan = this.FindByName<BoxView>("linePengaturan");
            var lberita = this.FindByName<BoxView>("lineBerita");
            lpengaturan.BackgroundColor = Color.FromHex("3F72AF");
            lberita.BackgroundColor = Color.FromHex("#ABABAB");

            var beritasaya = this.FindByName<StackLayout>("beritasaya");
            beritasaya.IsVisible = false;

            var pengaturan = this.FindByName<StackLayout>("pengaturan");
            pengaturan.IsVisible = true;
        }

        public async void UbahDataDiriClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProfileChangePersonalData());
        }

        public async void EditPasswordClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProfileChangePassword());
        }
        public async void LogoutClicked(object sender, EventArgs args)
        {
            var answer = await DisplayAlert("Logout", "Apa anda yakin untuk keluar?", "Ya", "Tidak");
            if (answer)
            {
                // TODO: Logout user using httpclient
                // Redirects.ToLoginPage();
            }
        }
    }
}