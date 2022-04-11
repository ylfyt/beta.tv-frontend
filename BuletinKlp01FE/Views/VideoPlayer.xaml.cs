using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.comment;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPlayer : ContentPage
    {
        private readonly Video _video;
        private List<Comment> comments;

        public VideoPlayer(Video video)
        {
            InitializeComponent();
            _video = video;
            LoadVideo();
        }

        public void SwitchToCommentSection(object sender, EventArgs args)
        {
            DescriptionSection.IsVisible = false;
            CommentSection.IsVisible = true;

            if (comments == null)
            {
                comments = new List<Comment>();
                FetchVideoComments();
            }
        }

        private async void FetchVideoComments()
        {
            try
            {
                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    Console.WriteLine("client null");
                    DependencyService.Get<IMessage>().ShortAlert("Client is null!");
                    return;
                }

                string weburl = Constants.VIDEO_ENDPOINT;
                weburl += $"/{_video.Id}/comments";

                DependencyService.Get<IMessage>().ShortAlert("Loading...");
                var httpResponseMessage = await client.GetAsync(weburl);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get comments");
                    return;
                }

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseVideo = JsonConvert.DeserializeObject<ResponseDto<DataComments>>(responseBody);

                if (responseVideo == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get comments");
                    return;
                }

                if (!responseVideo.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    return;
                }

                if (responseVideo.Data?.Comments.Count == 0)
                {
                    DependencyService.Get<IMessage>().ShortAlert("This video has no any comment yet!");
                    return;
                }

                comments.AddRange(responseVideo.Data?.Comments);
                CommentsListView.ItemsSource = comments;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }

        public void SwitchToDescriptionSection(object sender, EventArgs args)
        {
            CommentSection.IsVisible = false;
            DescriptionSection.IsVisible = true;
        }

        private void LoadVideo()
        {
            if (_video.Categories.Count == 0)
            {
                categoryText.IsVisible = false;
            }
            else
            {
                var temp = "";
                _video.Categories.ForEach(cat =>
                {
                    cat = cat.Length == 1 ? cat.ToUpper() : (char.ToUpper(cat[0]) + cat[1..]);
                    temp += $"#{cat}  ";
                });
                categoryText.Text = temp;
            }

            titleText.Text = _video.Title;
            sourceText.Text = _video.ChannelName;
            authorNameText.Text = _video.AuthorName;
            descriptionText.Text = _video.Description;
            VideoWebView.Source = _video.Url;
            this.Title = "Video";
        }

        public async void SubmitComment(object send, EventArgs args)
        {
            try
            {
                if (CommentField.Text == "")
                {
                    DependencyService.Get<IMessage>().ShortAlert("Please type something!");
                    return;
                }

                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    Console.WriteLine("client null");
                    DependencyService.Get<IMessage>().ShortAlert("Client is null!");
                    return;
                }

                string weburl = Constants.COMMENT_ENDPOINT;

                var content = new StringContent(JsonConvert.SerializeObject(new { videoId = _video.Id, text = CommentField.Text }), Encoding.UTF8, "application/json");

                DependencyService.Get<IMessage>().ShortAlert("Loading...");
                var httpResponseMessage = await client.PostAsync(weburl, content);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to send comment");
                    return;
                }

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseVideo = JsonConvert.DeserializeObject<ResponseDto<DataComment>>(responseBody);

                if (responseVideo == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to send comment");
                    return;
                }

                if (!responseVideo.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    return;
                }

                if (responseVideo.Data?.Comment == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    return;
                }
                CommentField.Text = "";
                comments.Insert(0, responseVideo.Data?.Comment!);
                CommentsListView.ItemsSource = null;
                CommentsListView.ItemsSource = comments;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}