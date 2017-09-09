namespace ShauliBlog.Models
{
    public class Comment : Essay
    {
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}