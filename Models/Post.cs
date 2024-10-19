﻿namespace BloggerPro.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommunityId { get; set; }
        public int UserId { get; set; }
    }
}
