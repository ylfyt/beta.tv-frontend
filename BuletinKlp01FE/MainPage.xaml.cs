using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Views;

namespace BuletinKlp01FE
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Title = "Beta.TV";
        }

        public async void WatchVideo(object sender, EventArgs e)
        {
            var video = new Video
            {
                Id = "_tEw-Him7ho",
                Url = "https://www.youtube.com/embed/_tEw-Him7ho",
                Title = "Tutorial xamarin 2 jam jadi professional",
                ChannelName = "CNN Indonesia",
                Category = "Tutorial",
                NewsAuthorName = "Budi Santoso",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras sit amet augue ultricies sapien faucibus commodo. Duis ultricies, mauris et rhoncus scelerisque, enim augue varius velit, at sollicitudin arcu diam at lectus. \n\nCurabitur orci lacus, elementum ut maximus at, mollis ut felis. Vestibulum placerat velit placerat dui rhoncus imperdiet. Morbi a dui sit amet massa ultrices condimentum quis a dolor. Ut vel ex sem. Cras laoreet leo vitae finibus convallis. Suspendisse nec tortor in nisl ullamcorper porttitor sed id nunc. \n\nUt vitae pellentesque felis, a accumsan turpis. Pellentesque sit amet sem tincidunt, viverra neque sit amet, consequat arcu. Etiam elementum felis sed lorem sollicitudin, quis luctus orci semper. Nam maximus convallis tortor ac dapibus. Donec molestie erat a semper suscipit."
            };
            await Navigation.PushAsync(new VideoPlayer(video));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}
