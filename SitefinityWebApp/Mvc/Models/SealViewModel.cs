using SitefinityWebApp.Mvc.Models;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Mvc.Models
{
    public sealed class SealViewModel
    {
        public SealViewModel(IEnumerable<Seal> results)
        {
            this._results = results;
        }



        private readonly IEnumerable<Seal> _results;


        public IEnumerable<Seal> Results { get { return this._results; } }

    }
}