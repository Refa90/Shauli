using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class MapMarker
    {
        [Key]
        public int Id { get; set; }

        public String Label { get; set; }

        public float Lat { get; set; }

        public float Lng { get; set; }

    }
}