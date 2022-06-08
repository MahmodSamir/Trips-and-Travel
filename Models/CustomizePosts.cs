using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Trips_and_Travel.CustomValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips_and_Travel.Models
{
    [MetadataType(typeof(CustomAttr))]
    public partial class Post
    {
    }

    public class CustomAttr
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public Nullable<System.DateTime> PostDate { get; set; }

        [Display(Name = "TripDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = false)]
        public System.DateTime Date { get; set; }
        [Display(Name = "TripDestination")]
        public string Destination { get; set; }
    }
}