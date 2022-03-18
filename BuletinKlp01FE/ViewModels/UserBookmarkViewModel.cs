using BuletinKlp01FE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace BuletinKlp01FE.ViewModels
{
    class UserBookmarkViewModel
    {
        public ObservableRangeCollection<Video> Beritasaya { get; set; }
        public UserBookmarkViewModel()
        {
            Beritasaya = new ObservableRangeCollection<Video>();

            Beritasaya.Add(new Video { Title = "tutorial xamarin nomor 1 cepat", ChannelName = "channel contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs" });
            Beritasaya.Add(new Video { Title = "hihihiahiha ihaiasihdiehi csdcudg web", ChannelName = "channel contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs" });
            Beritasaya.Add(new Video { Title = "contoh video", ChannelName = "channel contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs" });
            Beritasaya.Add(new Video { Title = "contoh video", ChannelName = "channel contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs" });
            Beritasaya.Add(new Video { Title = "contoh video", ChannelName = "channel contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs" });
            Beritasaya.Add(new Video { Title = "contoh video", ChannelName = "channel contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs" });
            Beritasaya.Add(new Video { Title = "contoh video", ChannelName = "channel contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs" });

        }
    }
}
