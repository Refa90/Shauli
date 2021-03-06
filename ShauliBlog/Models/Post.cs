﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class Post : Essay
    {
        [Required]
        public DateTime PublishDate { get; set; }

        public string Image { get; set; }

        public string Video { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Post> RecommendedPosts { get; set; }
    }
}