using SitefinityWebApp.Mvc.Models;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Mvc.Models
{
    public sealed class CharitySearchResultsViewModel
    {
        public CharitySearchResultsViewModel(IEnumerable<CharitySearchResult> results, string term, int currentPage, int resultsCount, int pageCount)
        {
            this._results = results;
            this._term = term;
            this._currentPage = currentPage;
            this._resultsCount = resultsCount;
            this._pageCount = pageCount;
        }

        // Private properties
        private readonly IEnumerable<CharitySearchResult> _results;
        private readonly string _term;
        private readonly int _currentPage;
        private readonly int _pageCount;
        private readonly int _resultsCount;

        // Public Getters
        public IEnumerable<CharitySearchResult> Results { get { return this._results; } }
        public string Term { get { return this._term; } }
        public int CurrentPage { get { return this._currentPage; } }
        public int PageCount { get { return this._pageCount; } }
        public int ResultsCount { get { return this._resultsCount; } }
        
        public string NextPageUrl { get { return this.CreatePageUrl(this.CurrentPage + 1); } }
        public string PreviousPageUrl { get { return this.CreatePageUrl(this.CurrentPage - 1); } }
        public string PageUrl(int page) { return CreatePageUrl(page); }

        // Create url for provided page number
        private string CreatePageUrl(int page)
        {
            StringBuilder sb = new StringBuilder();
            var currentNode = SiteMap.CurrentNode;
            if (currentNode != null)
                sb.Append(currentNode.Url);
            else
                sb.Append(VirtualPathUtility.RemoveTrailingSlash(SystemManager.CurrentHttpContext.Request.Path));

            // Add params back into url
            var requestParams = SystemManager.CurrentHttpContext.Request.QueryString;
            sb.Append("/?page=" + page);
            if (requestParams["term"] != null) { sb.Append("&term=" + requestParams["term"]); }
            if (requestParams["accredited"] != null) { sb.Append("&accredited=" + requestParams["accredited"]); }
            if (requestParams["national"] != null) { sb.Append("&national=" + requestParams["national"]); }
            if (requestParams["category"] != null)
            {
                string[] categories = requestParams["category"].Split(',');
                foreach (string category in categories)
                {
                    sb.Append("&category=" + category);
                }
            }

            return sb.ToString();
        }

        
    }
}