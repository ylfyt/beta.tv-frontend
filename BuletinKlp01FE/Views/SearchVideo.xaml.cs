using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        }

        public async void SearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                SearchButton.Source = "search_icon_primary.png";
                MessageText.Text = "Loading...";

                if (QueryTextInput.Text == null || QueryTextInput.Text == "")
                {
                    MessageText.Text = "Input not valid";
                    SearchButton.Source = "search_icon.png";
                    return;
                }

                HttpClient client = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(new { query = QueryTextInput.Text }), Encoding.UTF8, "application/json");
                string weburl = Constants.SEARCH_VIDEO_ENDPOINT;
                var httpResponseMessage = await client.PostAsync(weburl, content);

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                MessageText.Text = "Hasil Pencarian";
                SearchButton.Source = "search_icon.png";
            }
            catch (Exception ex)
            {
                MessageText.Text = "Something wrong";
                SearchButton.Source = "search_icon.png";
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}