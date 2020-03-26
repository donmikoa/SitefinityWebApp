using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.IO.Compression;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Newtonsoft.Json.Linq;
using SitefinityWebApp.Mvc.Models;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;



namespace SitefinityWebApp.Mvc.Controllers
{

    [ControllerToolboxItem(Name = "A-Z Charity", Title = "A-Z Charity", SectionName = "MvcWidgets")]
    public class AllCharityController : Controller
    {

        public Dictionary<string, string> categoryList = new Dictionary<string, string>()
        {
            {"select", null},
            {"charity-american-indian", "90021-001"},
            {"charity-animal-protection", "90021-050"},
            {"charity-arts-culture", "90021-100"},
            {"charity-cancer", "90021-200"},
            {"charity-child-sponsorship", "90021-250"},
            {"charity-children-youth", "90021-300"},
            {"charity-civil-rights", "90021-350"},
            {"charity-community-development-civic-organizations", "90021-400"},
            {"charity-education-literacy", "90021-450"},
            {"charity-elderly", "90021-500"},
            {"charity-environment", "90021-550"},
            {"charity-health", "90021-600"},
            {"charity-human-services", "90021-650"},
            {"charity-law-public-interest", "90021-700"},
            {"charity-local", "90021-000"},
            {"charity-national", "90031-000"},
            {"charity-other-charitable-organizations", "90021-950"},
            {"charity-police-firefighter-organizations", "90021-750"},
            {"charity-relief-development", "90021-800"},
            {"charity-religious", "90021-850"},
            {"charity-veterans-military", "90021-900"},
            {"blind-visually-impaired", "90021-150"}

        };

        [RelativeRoute("{category}")]
        public ActionResult Index(string category)
        {
            if (category == "select")
            {
                category = null;
            }
            string requestURL = assembleRequestUrl(category);

            // Create web request
            string solrSearchResponse = webRequest(requestURL);

            // Parse results
            JObject parsedResults = JObject.Parse(solrSearchResponse);


            IList<JToken> results = parsedResults["response"]["docs"].Children().ToList();

            // Serialize JSON results into .NET objects
            List<AllCharity> resultsList = new List<AllCharity>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                AllCharity searchResult = result.ToObject<AllCharity>();

                resultsList.Add(searchResult);

            }

            resultsList.Sort();



            IEnumerable<AllCharity> searchList = resultsList;

            AllCharityViewModel viewModel = new AllCharityViewModel(searchList);

            return this.View("Default", viewModel);
        }





        private string assembleRequestUrl(string categories)
        {
            // Assemble request URL
            string url = "http://192.168.225.84:8080/solr/WGA/select?q=(*:*";
            // string url = "http://172.16.1.36:4001/search/?term=" + term;



            if (categories != null)
            {
                url += " AND tobcode:";
                url += categoryList[categories];
                url += ")";

            }
            else
            {
                url += ")";
            }


            // choose fields to return
            url += "&fl=title,reporturl&rows=1000000";

            // sort the response

            //url += "&sort=title ASC";

            // format response as json
            url += "&wt=json";


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

