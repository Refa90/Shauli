using System.Collections.Generic;


namespace ShauliBlog.Models
{
    public class BlogVisitorModel
    {
        public List<Post> Posts { get; set; }

        public Comment NewComment { get; set; }
    }
}