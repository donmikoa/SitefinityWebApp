using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SitefinityWebApp.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "CharitySearchResultsWidget", Title = "Charity Search Results", SectionName = "MvcWidgets")]
    public class CharitySearchResultsController : Controller
    {

        private const int ResultsPerPage = 6;
        // # of words apart 2 separate words in search term can be
        private const int TermProximity = 4;
        private Dictionary<string, string> categoryList = new Dictionary<string, string>()
        {
            {"American Indian", "90021-001"},
            {"Animal Protection", "90021-050"},
            {"Arts & Culture", "90021-100"},
            {"Cancer", "90021-200"},
            {"Child Sponsorship", "90021-250"},
            {"Children & Youth", "90021-300"},
            {"Civil Rights", "90021-350"},
            {"Community Development & Civic Organizations", "90021-400"},
            {"Education & Literacy", "90021-450"},
            {"Elderly", "90021-500"},
            {"Environment", "90021-550"},
            {"Health", "90021-600"},
            {"Human Services", "90021-650"},
            {"Law & Public Interest", "90021-700"},
            {"Local", "90021-000"},
            {"National", "90031-000"},
            {"Other Charitable Organizations", "90021-950"},
            {"Police & Firefighter Organizations", "90021-750"},
            {"Relief & Development", "90021-800"},
            {"Religious", "90021-850"},
            {"Veterans & Military", "90021-900"},
            {"Blind & Visually Impaired", "90021-150"}
        };

        public ActionResult Index(string term, string[] category = null, bool? national = null, bool? accredited = null, int page = 1)
        {
            string requestURL = assembleRequestUrl(term, page, national, accredited, category);

            // Create web request
            string solrSearchResponse = webRequest(requestURL);

            // Parse results
            JObject parsedResults = JObject.Parse(solrSearchResponse);

            // Store relevant information
            int numberOfResults = int.Parse(parsedResults["response"]["numFound"].ToString());
            IList<JToken> results = parsedResults["response"]["docs"].Children().ToList();

            // Serialize JSON results into .NET objects
            List<CharitySearchResult> resultsList = new List<CharitySearchResult>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                CharitySearchResult searchResult = result.ToObject<CharitySearchResult>();
                resultsList.Add(searchResult);
            }

            IEnumerable<CharitySearchResult> searchResults = resultsList;

            // Generate ViewModel and return view
            CharitySearchResultsViewModel viewModel = new CharitySearchResultsViewModel(searchResults, term, page, numberOfResults, (int)numberOfResults / CharitySearchResultsController.ResultsPerPage);

            return this.View("Default", viewModel);
        }
         


        private string assembleRequestUrl(string term, int page, bool? isNational, bool? isAccredited, string[] categories = null)
        {
            // Assemble request URL
            string url = "http://192.168.225.84:8080/solr/WGA/select?q=(";
            // string url = "http://172.16.1.36:4001/search/?term=" + term;

            // Parse page query params into request

            // Search term
            url += "(title:\"" + term + "\"~" + CharitySearchResultsController.TermProximity + " OR alternativenames:\"" + term + "\"~" + CharitySearchResultsController.TermProximity + ")";

            // if (categories.HasValue) { for (category in categories){ add category into SOLR query} }
            if (categories != null)
            {
                url += " AND (";

                List<string> tobs = new List<string>();
                foreach (string category in categories)
                {
                    try
                    {
                        tobs.Add("tobcode: " + categoryList[category]);
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("Exception: Category \"" + category + "\" is not a recognized category.");
                    }
                    
                }

                url += String.Join(" OR ", tobs) + ")";
            }

            // if (isNational.HasValue) { pass boolean value into SOLR query }
            if (isNational.HasValue)
            {
                url += " AND nationalcharity: " + isNational.Value;
            }

            // if (accredited.HasValue && accredited) { pass accredited:true into SOLR query}
            if (isAccredited.HasValue && isAccredited.Value)
            {
                url += " AND accredited:true";
            }

            // need to figure out location sorting

            //close q= parameter
            url += ")";

            // choose fields to return
            url += "&fl=title,address,city,state,postalcode,accredited,sealholder,classificationtext";

            // format response as json
            url += "&wt=json";

            // if (page.HasValue) { add onto request start: page, rows: CharitySearchResultsController.PageSize }
            url += "&start=" + ((page - 1) * CharitySearchResultsController.ResultsPerPage) + "&rows=" + CharitySearchResultsController.ResultsPerPage;
            

            return url;
        }

        private string webRequest(string requestURL)
        {
            string webResponse = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                webResponse = reader.ReadToEnd();
            }
            return webResponse;
        }
    }
}