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
        public string Author { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Author Website")]
        public string AuthorWebsiteAddress { get; set; }

        [Required]
        public string Content { get; set; }
    }
}