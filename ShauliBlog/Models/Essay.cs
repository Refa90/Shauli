using System.ComponentModel.DataAnnotations;

namespace ShauliBlog.Models
{
    public abstract class Essay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Headline { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Author { get; set; }

        [Required]
        [Url]
        public string AuthorWebsiteAddress { get; set; }

        [Required]
        public string Content { get; set; }
    }
}