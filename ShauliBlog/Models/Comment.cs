using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web;

namespace ShauliBlog.Models
{
    public class Comment : Essay
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}