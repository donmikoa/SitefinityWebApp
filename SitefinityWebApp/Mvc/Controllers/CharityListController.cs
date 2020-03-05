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

        public ActionResult Index()
        {
            string requestURL = "Michael";

            // Create web request
            string solrSearchResponse = webRequest(requestURL);

            // Parse results
            JObject parsedResults = JObject.Parse(solrSearchResponse);

            // Store relevant information
            int numberOfResults = int.Parse(parsedResults["response"]["numFound"].ToString());
            IList<JToken> results = parsedResults["response"]["docs"].Children().ToList();

            // Serialize JSON results into .NET objects
            List<CharityList> resultsList = new List<CharityList>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                CharityList searchResult = result.ToObject<CharityList>();
                resultsList.Add(searchResult);
            }

            IEnumerable<CharityList> searchResults = resultsList;

            // Generate ViewModel and return view

            
            ViewBag.alphabets = new List<string>()
                {
          "#", "A", "B", "C", "D", "E", "F", "G", "H", "I" , "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };
            
            return View("Default");
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

