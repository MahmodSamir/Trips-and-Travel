using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trips_and_Travel.Models;

namespace Trips_and_Travel.ViewModel
{
    public class Multiple
    {
        public IEnumerable<Post> posting { get; set; }
        public IEnumerable<User> use { get; set; }
    }
}