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
    [ControllerToolboxItem(Name = "Charity List", Title = "Charity List", SectionName = "MvcWidgets")]
    public class CharityListController : Controller
    {


        public Dictionary<string, string> charitytype = new Dictionary<string, string>()
        {
            {"charity-american-indian", "American Indian"},
            {"charity-animal-protection", "Animal Protection"},
            {"charity-arts-culture", "Arts & Culture"},
            {"charity-cancer", "Cancer"},
            {"blind-visually-impaired", "Charity - Blind & Visually Impaired"},
            {"charity-child-sponsorship", "Child Sponsorship"},
            {"charity-children-youth", "Children & Youth"},
            {"charity-civil-rights", "Civil Rights"},
            {"charity-community-development-civic-organizations", "Community Development & Civic Organizations"},
            {"charity-education-literacy", "Education & Literacy"},
            {"charity-elderly", "Elderly"},
            {"charity-environment", "Environment"},
            {"charity-health", "health"},
            {"charity-human-services", "Human Services"},
            {"charity-law-public-interest", "Law & Public Interest"},
            {"charity-local", "local"},
            {"charity-national", "National"},
            {"charity-other-charitable-organizations", "Other Charitable Organizations"},
            {"charity-police-firefighter-organizations", "Police & Firefighter Organizations"},
            {"charity-relief-development", "Relief & Development"},
            {"charity-religious", "Religious"},
            {"charity-veterans-military", "Veterans & Military"},

        };

        public Dictionary<string, string> categoryList = new Dictionary<string, string>()
        {
            {"charity-american-indian", "90021-001"},
            {"charity-animal-protection", "90021-050"},
            {"charity-arts-culture", "90021-100"},
            {"charity-cancer", "90021-200"},
            {"charity-child-sponsorships", "90021-250"},
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

        private Dictionary<string, string> categoryList2 = new Dictionary<string, string>()
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

        
        public ActionResult Index(string category)
        {
            string charityCategory = category;
            string requestURL = assembleRequestUrl(charityCategory);

            // Create web request
            string solrSearchResponse = webRequest(requestURL);

            // Parse results
            JObject parsedResults = JObject.Parse(solrSearchResponse);

            
            IList<JToken> results = parsedResults["response"]["docs"].Children().ToList();

            // Serialize JSON results into .NET objects
            List<CharityList> resultsList = new List<CharityList>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                CharityList searchResult = result.ToObject<CharityList>();
                
                resultsList.Add(searchResult);

            }

            resultsList.Sort();
            


            IEnumerable<CharityList> searchList = resultsList;

            CharityListViewModel viewModel = new CharityListViewModel(searchList);

            return this.View("Default",viewModel);
        }



        private string assembleRequestUrl(string categories)
        {
            // Assemble request URL
            string url = "http://192.168.225.84:8080/solr/WGA/select?q=(nationalcharity:true";
            // string url = "http://172.16.1.36:4001/search/?term=" + term;

          
            // if (categories.HasValue) { for (category in categories){ add category into SOLR query} }
            

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

