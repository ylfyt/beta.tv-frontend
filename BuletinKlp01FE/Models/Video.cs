﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BuletinKlp01FE.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string YoutubeVideoId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string ChannelThumbnailUrl { get; set; } = string.Empty;
        public string ChannelId { get; set; } = string.Empty;
        public string ChannelName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public List<string> Categories { get; set; } = new List<string>();
        public string Description { get; set; } = string.Empty;
        public string CreateAt { get; set; } = DateTime.Today.ToString();
        public string AuthorDescription { get; set; } = string.Empty;
        public string AuthorTitle { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string VideoInfo { get; set; } = string.Empty;
        public ImageSource ThumbnailSource { get; set; } = null;
    }
}
