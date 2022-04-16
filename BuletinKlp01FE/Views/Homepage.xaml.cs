using BuletinKlp01FE.Dtos.category;
using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.ViewModels;
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
        private readonly HomepageViewModel homepageViewModel;

        public Homepage()
        {
            InitializeComponent();
            catButtons = new List<Button>();
            CreateCatButton();
            homepageViewModel = new HomepageViewModel();
            BindingContext = homepageViewModel;
        }

        public async void CreateCatButton()
        {
            catButtons.Add(ButtonAll);
            List<Category> categories = await GetCategories();
            foreach (Category category in categories)
            {
                Console.WriteLine(category.Label);
                Button catButton = new()
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
                catButton.Clicked += (sender, args) => CatButtonClicked(sender, args);
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

        async Task<List<Category>> GetCategories()
        {
            var response = await APIRequest.Send<DataCategories>(Constants.ENDPOINT_CATEGORY);
            if (!response.Success)
            {
                return new List<Category>();
            }

            return response.Data!.Categories;
        }

        void AllVideosClicked(object sender, EventArgs args)
        {
            ChangeActiveCatButton("all");
            homepageViewModel.GetVideo();
        }

        void CatButtonClicked(object sender, EventArgs args)
        {
            Button? receiver = sender! as Button;
            string catName = receiver!.Text;
            string slug = receiver!.CommandParameter.ToString();
            ChangeActiveCatButton(catName);
            homepageViewModel.GetVideo(slug);
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