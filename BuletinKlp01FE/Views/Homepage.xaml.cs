using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Homepage : ContentPage
    {
        readonly string color0 = "#3F72AF";
        readonly string color1 = "#112D4E";
        readonly string color5 = "#DBE2EF";

        private readonly List<Button> catButtons;

        public Homepage()
        {
            InitializeComponent();

            catButtons = new List<Button>();
            catButtons.Add(ButtonAll);
            catButtons.Add(ButtonTech);
            catButtons.Add(ButtonDesain);
            catButtons.Add(ButtonBisnis);
            _ = GetVideos();
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

        async Task GetVideos(string? category = null)
        {
            SetMessage("Please wait...");

            string endpoint = category == null ? "/video" : "/video/category/" + category;

            var response = await APIRequest.Send<DataVideos>(endpoint);

            if (!response.Success)
            {
                SetMessage("Failed to get videos");
                return;
            }

            if (response.Data!.Videos.Count == 0)
            {
                SetMessage("No videos found!");
                return;
            }

            VideosListView.ItemsSource = response.Data!.Videos;
            SetMessage();
        }

        void SetMessage(string? message=null)
        {
            if (message == null || message == "")
            {
                ErrorMessage.IsVisible = false;
                return;
            }

            ErrorMessage.IsVisible = true;
            ErrorMessage.Text = message;
        }

        async void AllVideosClicked(object sender, EventArgs args)
        {
            ChangeActiveCatButton("all");
            await GetVideos();
        }

        async void TechVideosClicked(object sender, EventArgs args)
        {
            ChangeActiveCatButton("tech");
            await GetVideos("tech");
        }

        async void DesainVideosClicked(object sender, EventArgs args)
        {
            ChangeActiveCatButton("desain");
            await GetVideos("desain");
        }

        async void BisnisVideosClicked(object sender, EventArgs args)
        {
            ChangeActiveCatButton("bisnis");
            await GetVideos("bisnis");
        }


        void ChangeActiveCatButton(string cat)
        {
            catButtons.ForEach(but => {
                but.BackgroundColor = Color.FromHex(color5);
                but.TextColor = Color.FromHex(color1);
            });

            if (cat == "tech")
            {
                ButtonTech.BackgroundColor = Color.FromHex(color0);
                ButtonTech.TextColor = Color.FromHex("#ffffff");
            }
            else if (cat == "bisnis")
            {
                ButtonBisnis.BackgroundColor = Color.FromHex(color0);
                ButtonBisnis.TextColor = Color.FromHex("#ffffff");
            }
            else if (cat == "desain")
            {
                ButtonDesain.BackgroundColor = Color.FromHex(color0);
                ButtonDesain.TextColor = Color.FromHex("#ffffff");
            }
            else
            {
                ButtonAll.BackgroundColor = Color.FromHex(color0);
                ButtonAll.TextColor = Color.FromHex("#ffffff");
            }
        }
        
    }
}