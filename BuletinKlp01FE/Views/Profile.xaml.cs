using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Essentials;
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

        public async void VideoSelected(object sender, ItemTappedEventArgs e)
        {
            var video = e.Item as Video;

            if (video == null)
            {
                Console.WriteLine("Something wrong!!");
                return;
            }

            await Navigation.PushAsync(new VideoPlayer(video));
        }

        public async void ChangeImageClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProfileChangeImage());
        }

        public async void UbahDataDiriClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProfileChangePersonalData());
        }

        public async void ChangePassClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProfileChangePass());
        }

        public async void LogoutClicked(object sender, EventArgs args)
        {
            var answer = await DisplayAlert("Logout", "Apa anda yakin untuk keluar?", "Ya", "Tidak");
            if (answer)
            {
                try
                {
                    var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                    if (client == null)
                    {
                        return;
                    }

                    var postData = new List<KeyValuePair<string, string>>();
                    string weburl = Constants.LOGOUT_END_POINT;

                    var response = await client.PostAsync(weburl, null);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var responseDto = JsonConvert.DeserializeObject<ResponseDto<Dtos.user.DataUser>>(responseBody);
                    if (responseDto != null && responseDto.Success)
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Berhasil logout");
                        Preferences.Set("token", "");
                        Redirects.ToLoginPage();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Ganti data gagal", responseDto.Message, "Ok");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Application.Current.MainPage.DisplayAlert("Ups", "Data Anda gagal diupdate. Pastikan Anda terhubung ke internet", "Ok");
                }
            }
        }
    }
}