using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class Fan
    {
        [Key]
        [Column(Order = 1)]
        public String FirstName { get; set; }

        [Key]
        [Column(Order = 2)]
        public String LastName { get; set; }

        public bool Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public int Seniority { get; set; }
    }
}