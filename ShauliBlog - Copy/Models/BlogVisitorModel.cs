using System.Collections.Generic;


namespace ShauliBlog.Models
{
    public class BlogVisitorModel : Post
    {
        public Comment NewComment { get; set; }

        public BlogVisitorModel()
        {
        }

        public BlogVisitorModel(Post post) {
            this.Author = post.Author;
            this.AuthorWebsiteAddress = post.AuthorWebsiteAddress;
            this.Content = post.Content;
            this.Headline = post.Headline;
            this.Id = post.Id;
            this.Comments = post.Comments;
            this.Image = post.Image;
            this.PublishDate = post.PublishDate;
            this.Video = post.Video;

            this.NewComment = new Comment();
            this.NewComment.PostId = post.Id;
            this.NewComment.Post = post;
        }
    }
}