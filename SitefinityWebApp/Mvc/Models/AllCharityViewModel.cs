using SitefinityWebApp.Mvc.Models;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Services;
namespace SitefinityWebApp.Mvc.Models
{
    public sealed class AllCharityViewModel
    {
        public AllCharityViewModel(IEnumerable<AllCharity> results)
        {
            this._results = results;
        }



        private readonly IEnumerable<AllCharity> _results;


        public IEnumerable<AllCharity> Results { get { return this._results; } }

    }
}