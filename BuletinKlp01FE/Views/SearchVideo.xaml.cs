using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchVideo : ContentPage
    {
        public SearchVideo()
        {
            InitializeComponent();

            List<Video> temp = new();
            VideosListView.ItemsSource = temp;
            QueryTextInput.Focus();
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

        public async void SearchButtonClicked(object sender, EventArgs e)
        {
            try
            {

                if (QueryTextInput.Text == null || QueryTextInput.Text == "")
                {
                    SetUI(false, "Tolong masukan kata pencarian!");
                    return;
                }

                VideosListView.ItemsSource = null;

                SetUI(true, "Loading...", false);

                var response = await APIRequest.Send<DataVideos>(
                    endpoint: Constants.ENDPOINT_VIDEO_SEARCH,
                    method: "POST",
                    data: new { query = QueryTextInput.Text }
                    );

                if (!response.Success)
                {
                    SetUI(false, "Gagal mendapatkan video!");
                    return;
                }

                if (response.Data?.Videos.Count == 0)
                {
                    SetUI(false, "Tidak ada video yang ditemukan!");
                    return;
                }

                VideosListView.ItemsSource = response.Data?.Videos;

                SetUI(false, "Hasil pencarian: ", false);
            }
            catch (Exception ex)
            {
                SetUI(false, "Gagal mendapatkan video!");
                Console.WriteLine(ex.Message);
            }

        }

        public void SetUI(bool fetching, string message, bool error = true)
        {
            MessageText.Text = message;
            if (error)
            {
                MessageText.TextColor = Color.Orange;
            }
            else
            {
                MessageText.TextColor = Color.FromHex("#3F72AF");
            }
            if (fetching)
            {
                SearchButton.Source = "search_icon_primary.png";
            }
            else
            {
                SearchButton.Source = "search_icon.png";
            }
        }
    }
}