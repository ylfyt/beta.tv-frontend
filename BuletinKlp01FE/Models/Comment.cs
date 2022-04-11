using System;
using System.Collections.Generic;
using System.Text;

namespace BuletinKlp01FE.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public long CreateAt { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
