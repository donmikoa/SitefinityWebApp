using SitefinityWebApp.Mvc.Models;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Mvc.Models
{
    public sealed class CharityListViewModel
    {
        public CharityListViewModel(IEnumerable<CharityList> results)
        {
            this._results = results;
        }

      

        private readonly IEnumerable<CharityList> _results;
       

        public IEnumerable<CharityList> Results { get { return this._results; } }
        
    }
}