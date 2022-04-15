using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.bookmark;
using BuletinKlp01FE.Dtos.comment;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.ViewModels;
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
        private VideoCommentsViewModel commentsViewModel;
        private bool isBookmarked = false;

        public VideoPlayer(Video video)
        {
            InitializeComponent();
            _video = video;
            LoadVideo();
            commentsViewModel = new VideoCommentsViewModel();
            BindingContext = commentsViewModel;
            checkIsBookmarked();
        }

        public async void checkIsBookmarked()
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

                string weburl = Constants.IS_BOOKMARKED_END_POINT;

                var httpResponseMessage = await client.GetAsync(weburl);

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDto<bool>>(responseBody);

                if (responseDto != null)
                {
                    isBookmarked = responseDto.Data;
                    DisplayAlert("asai", "aduh", "ok");
                    setBookmarkUI();
                    return;
                }
                else
                {
                    DisplayAlert("asai", "response null", "ok");
                }
                isBookmarked = false;
                setBookmarkUI();
                return;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }
        public async void BookmarkButtonClicked(object sender, EventArgs args)
        {
            if (!isBookmarked) // means this video will be added to bookmark
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

                    string weburl = Constants.BOOKMARK_END_POINT;
                    var content = new StringContent(JsonConvert.SerializeObject(new { videoId = _video.Id }), Encoding.UTF8, "application/json");

                    DependencyService.Get<IMessage>().ShortAlert("Loading...");

                    var httpResponseMessage = await client.PostAsync(weburl, content);

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Failed to add to bookmark");
                        return;
                    }

                    string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                    var responseDto = JsonConvert.DeserializeObject<ResponseDto<DataBookmark>>(responseBody);

                    if (responseDto != null && responseDto.Success)
                    {
                        //await Application.Current.MainPage.DisplayAlert("Ganti data berhasil", "Selamat, data Anda berhasil diupdate", "Ok");
                        DependencyService.Get<IMessage>().ShortAlert("Successfully added to bookmark");
                        isBookmarked = true;
                        setBookmarkUI();
                        return;
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Failed to add to bookmark");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    Console.WriteLine(ex.Message);
                }
            }
            else  // this video will be deleted from bookmark
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

                    string weburl = Constants.BOOKMARK_END_POINT + "/" + _video.Id.ToString();

                    var httpResponseMessage = await client.DeleteAsync(weburl);

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Failed to delete from bookmark");
                        return;
                    }

                    string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                    var responseDto = JsonConvert.DeserializeObject<ResponseDto<DataBookmark>>(responseBody);

                    if (responseDto != null && responseDto.Success)
                    {
                        //await Application.Current.MainPage.DisplayAlert("Ganti data berhasil", "Selamat, data Anda berhasil diupdate", "Ok");
                        DependencyService.Get<IMessage>().ShortAlert("Successfully deleted from bookmark");
                        isBookmarked = false;
                        setBookmarkUI();
                        return;
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert(responseDto.Message);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    Console.WriteLine(ex.Message);
                }
            }
            
        }

        private void setBookmarkUI()
        {
            var btn = this.FindByName<ImageButton>("bookmarkBtn");
            var txt = this.FindByName<Label>("bookmarkText");

            if (isBookmarked)
            {
                btn.Source = "bookmark_blue";
                txt.Text = "Added to bookmark";
            }
            else
            {
                btn.Source = "bookmark_white";
                txt.Text = "Add to bookmark";
            }
        }

        public void SwitchToCommentSection(object sender, EventArgs args)
        {
            DescriptionSection.IsVisible = false;
            CommentSection.IsVisible = true;
            FetchVideoComments();
        }

        public async void LikeButtonClicked(object sender, EventArgs args)
        {
            var btn = sender as ImageButton;
            var comment = btn!.CommandParameter as Comment;
            
            try
            {
                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    Console.WriteLine("client null");
                    DependencyService.Get<IMessage>().ShortAlert("Client is null!");
                    return;
                }

                SetFetching(comment.Id, true);

                string weburl = Constants.COMMENT_LIKE_ENDPOINT;

                HttpResponseMessage httpResponseMessage;
                bool adding = true;

                if (comment!.IsLiked)
                {
                    adding = false;
                    httpResponseMessage = await client.DeleteAsync(weburl + $"?commentId={comment.Id}");
                }
                else
                {
                    var content = new StringContent(JsonConvert.SerializeObject(new { commentId = comment.Id }), Encoding.UTF8, "application/json");
                    httpResponseMessage = await client.PostAsync(weburl, content);
                }


                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("==========================================================");
                    Console.WriteLine(await httpResponseMessage.Content.ReadAsStringAsync() + " | " + comment!.Id.ToString());
                    DependencyService.Get<IMessage>().ShortAlert("Failed to send like");
                    SetFetching(comment.Id, false);
                    return;
                }
                UpdateCommentLike(comment.Id, adding);
                SetFetching(comment.Id, false);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
                SetFetching(comment.Id, false);
            }

        }

        public void UpdateCommentLike(int id, bool adding)
        {
            for (int i = 0; i < commentsViewModel.Comments.Count; i++)
            {
                if (commentsViewModel.Comments[i].Id == id)
                {
                    var temp = commentsViewModel.Comments[i];
                    temp.IsFetching = false;
                    temp.IsLiked = adding;
                    temp.CountLikes += adding ? 1 : -1;
                    commentsViewModel.Comments[i] = temp;
                }
            }
        }

        public void SetFetching(int id, bool val)
        {
            for (int i = 0; i < commentsViewModel.Comments.Count; i++)
            {
                if (commentsViewModel.Comments[i].Id == id)
                {
                    var temp = commentsViewModel.Comments[i];
                    temp.IsFetching = val;
                    commentsViewModel.Comments[i] = temp;
                }
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

                string weburl = Constants.COMMENT_ENDPOINT;
                weburl += $"?videoId={_video.Id}";

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

                commentsViewModel.Comments.Clear();
                responseVideo.Data?.Comments.ForEach(c => commentsViewModel.Comments.Add(c));
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
                _video.Categories.ForEach(c =>
                {
                    temp += $"#{c.Label}  ";
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
                if (CommentField.Text == null || CommentField.Text == "")
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
                commentsViewModel.Comments.Insert(0, responseVideo.Data?.Comment!);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}