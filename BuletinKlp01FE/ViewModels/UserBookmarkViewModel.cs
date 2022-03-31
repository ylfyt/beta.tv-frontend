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

            Beritasaya.Add(new Video { VideoInfo = "tutorial xamarin nomor 1 cepat", AuthorTitle = "ChannelAuthorTitle contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailUrl = "sampleVideo" });
            Beritasaya.Add(new Video { VideoInfo = "hihihiahiha ihaiasihdiehi csdcudg web", AuthorTitle = "ChannelAuthorTitle contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailUrl = "sampleVideo" });
            Beritasaya.Add(new Video { VideoInfo = "contoh video", AuthorTitle = "ChannelAuthorTitle contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailUrl = "sampleVideo" });
            Beritasaya.Add(new Video { VideoInfo = "contoh video", AuthorTitle = "ChannelAuthorTitle contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailUrl = "sampleVideo" });
            Beritasaya.Add(new Video { VideoInfo = "contoh video", AuthorTitle = "ChannelAuthorTitle contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailUrl = "sampleVideo" });
            Beritasaya.Add(new Video { VideoInfo = "contoh video", AuthorTitle = "ChannelAuthorTitle contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailUrl = "sampleVideo" });
            Beritasaya.Add(new Video { VideoInfo = "contoh video", AuthorTitle = "ChannelAuthorTitle contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailUrl = "sampleVideo" });

        }
    }
}
