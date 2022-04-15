using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.category;
using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuletinKlp01FE.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

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
            createCatButton();
            _ = GetVideos();
        }

        public async void createCatButton()
        {
            catButtons.Add(ButtonAll);
            List<Category> categories = await GetCategories();
            foreach (Category category in categories)
            {
                Console.WriteLine(category.Label);
                Button catButton = new Button
                {
                    Text = category.Label,
                    BackgroundColor = Color.FromHex(color5),
                    TextColor = Color.FromHex(color1),
                    FontAttributes = FontAttributes.Bold,
                    CornerRadius = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    Padding = new Thickness(20, 0),
                    HeightRequest = 35,
                };
                catButton.Clicked += async (sender, args) => await catButtonClicked(sender, args);
                catButton.CommandParameter = category.Slug;
                catButtons.Add(catButton);
                CatButtonContainer.Children.Add(catButton);
            }
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

        async Task<List<Category>> GetCategories()
        {
            try
            {
                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    Console.WriteLine("client null");
                    DependencyService.Get<IMessage>().ShortAlert("Client is null!");
                    return new List<Category>();
                }

                string weburl = Constants.CATEGORY_ENDPOINT;

                SetMessage("Loading...");
                var httpResponseMessage = await client.GetAsync(weburl);


                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    SetMessage();
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get category!");
                    return new List<Category>();
                }

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseCategory = JsonConvert.DeserializeObject<ResponseDto<DataCategories>>(responseBody);

                if (responseCategory == null)
                {
                    SetMessage();
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get category!");
                    return new List<Category>();
                }

                if (!responseCategory.Success)
                {
                    SetMessage();
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    return new List<Category>();
                }

                if (responseCategory.Data?.Categories.Count == 0)
                {
                    SetMessage("No Categories Found!");
                    DependencyService.Get<IMessage>().ShortAlert("No Videos Found!");
                    return new List<Category>();
                }
                return responseCategory.Data!.Categories;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
                return new List<Category>();
            }

            //SetMessage();
        }

        void SetMessage(string? message = null)
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

        async Task catButtonClicked(object sender, EventArgs args)
        {
            Button? receiver = sender! as Button;
            string catName = receiver!.Text;
            string slug = receiver!.CommandParameter.ToString();
            ChangeActiveCatButton(catName);
            await GetVideos(slug);
            Console.WriteLine("Success");
        }


        void ChangeActiveCatButton(string cat)
        {
            catButtons.ForEach(but =>
            {
                if (but.Text == cat)
                {
                    but.BackgroundColor = Color.FromHex(color0);
                    but.TextColor = Color.FromHex("#ffffff");
                }
                else
                {
                    but.BackgroundColor = Color.FromHex(color5);
                    but.TextColor = Color.FromHex(color1);
                }
            });

            if (cat == "all")
            {
                ButtonAll.BackgroundColor = Color.FromHex(color0);
                ButtonAll.TextColor = Color.FromHex("#ffffff");
            }
        }

    }
}