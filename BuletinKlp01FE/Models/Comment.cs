using System;

namespace BuletinKlp01FE.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public long CreateAt { get; set; }
        public string Text { get; set; } = string.Empty;
        public int CountLikes { get; set; }
        public User User { get; set; }
        public bool IsLiked { get; set; }
        public string ImageButtonSource => IsLiked ? "thumb_blue.png" : "thumb_outline.png";
        public string CreatorInfo => User.Name + " · " + DateTimeOffset.FromUnixTimeSeconds(CreateAt).LocalDateTime.ToString("MMMM dd, yyyy HH:MM");
        public bool IsFetching { get; set; } = false;
        public bool NotIsFetching => !IsFetching;
        public string LastReply { get; set; } = "Click to replay!";
    }
}
