using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace SitefinityWebApp.Mvc.Models
{
    public class RecentVideos
    {
        public string Descriptions { get; set; }
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public bool IsValid { get; set; }

        public string VideoId { get; set; }
    }
}