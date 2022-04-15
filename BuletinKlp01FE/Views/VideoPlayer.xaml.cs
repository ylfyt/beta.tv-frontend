using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.bookmark;
using BuletinKlp01FE.Dtos.comment;
using BuletinKlp01FE.Dtos.commentLike;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.ViewModels;
using Newtonsoft.Json;
using System;
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
        private readonly VideoCommentsViewModel commentsViewModel;
        private bool isBookmarked = false;

        public VideoPlayer(Video video)
        {
            InitializeComponent();
            _video = video;
            LoadVideo();
            commentsViewModel = new VideoCommentsViewModel();
            BindingContext = commentsViewModel;
            CheckIsBookmarked();
        }

        public async void CheckIsBookmarked()
        {
            try
            {
                var endpoint = Constants.ENDPOINT_BOOKMARK_CHECK + $"/{_video.Id}";
                var response = await APIRequest.Send<DataBookmark>(
                    endpoint: endpoint,
                    method: "GET"
                    );
                if (!response.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    return;
                }
                isBookmarked = response.Data != null; 
                SetBookmarkUI();
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
                    var response = await APIRequest.Send<DataBookmark>(
                        endpoint: Constants.BOOKMARK_END_POINT,
                        method: "POST",
                        data: new { videoId = _video.Id }
                        );

                    if (!response.Success)
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Failed to add this video!");
                        return;
                    }

                    isBookmarked = true;
                    SetBookmarkUI();
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
                    var response = await APIRequest.Send<DataBookmark>(
                        endpoint: Constants.ENDPOINT_BOOKMARK + $"/video/{_video.Id}",
                        method: "DELETE"
                        );
                    if (!response.Success)
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Failed to delete this video!");
                        return;
                    }

                    isBookmarked = false;
                    SetBookmarkUI();
                }
                catch (Exception ex)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    Console.WriteLine(ex.Message);
                }
            }

        }

        private void SetBookmarkUI()
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
                bool adding = true;

                SetFetching(comment!.Id, true);
                ResponseDto<DataCommentLike> response;

                if (comment!.IsLiked)
                {
                    adding = false;
                    response = await APIRequest.Send<DataCommentLike>(
                        endpoint: Constants.ENDPOINT_COMMENT_LIKE + $"?commentId={comment.Id}",
                        method: "DELETE");
                }
                else
                {
                    response = await APIRequest.Send<DataCommentLike>(
                        endpoint: Constants.ENDPOINT_COMMENT_LIKE,
                        method: "POST",
                        data: new { commentId = comment.Id }
                        );
                }


                if (!response.Success)
                {
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
                SetFetching(comment!.Id, false);
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
                var response = await APIRequest.Send<DataComments>(Constants.ENDPOINT_COMMENT + $"?videoId={_video.Id}");

                if (!response.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    return;
                }

                if (response.Data?.Comments.Count == 0)
                {
                    DependencyService.Get<IMessage>().ShortAlert("This video has no any comment yet!");
                    return;
                }

                commentsViewModel.Comments.Clear();
                response.Data?.Comments.ForEach(c => commentsViewModel.Comments.Add(c));
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

                var response = await APIRequest.Send<DataComment>(
                    endpoint: Constants.ENDPOINT_COMMENT,
                    method: "POST",
                    data: new { videoId = _video.Id, text = CommentField.Text });

                if (!response.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to send comment");
                    return;
                }

                CommentField.Text = "";
                commentsViewModel.Comments.Insert(0, response.Data?.Comment!);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}