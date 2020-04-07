using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.News.Model;

namespace SitefinityWebApp.Mvc.Models
{
    public class RecentNewsModel
    {
        public RecentNewsModel(IQueryable<NewsItem> items)
        {
            this.Items = items;
        }
        public IQueryable<NewsItem> Items
        {
            get;
            private set;
        }
    }
}