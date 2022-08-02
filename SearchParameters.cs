using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace KijijiSniper {
    public class SearchParameters {
        //TODO add location.alberta.whatever the fuck support by deserializing json library in the repo

        //Mandartory parameters
        [JsonProperty("locationId")]
        public int LocationId { get; set; }
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        //Extra field for KijijiScraperInterface. Specifys output filename
        [JsonProperty("output")]
        public string Output { get; set; } = "KijijiListings";

        //Some known parameters available when using either the "api" (default) or "html" scraperType
        [JsonProperty("minPrice")]
        public decimal? MinPrice { get; set; }
        [JsonProperty("maxPrice")]
        public decimal? MaxPrice { get; set; }
        [JsonProperty("adType")]
        public string AdType { get; set; }

        //Some known parameters available when using the "api" (default) scraperType
        // ReSharper disable once CommentTypo
        //TODO change Q to query this dumb fucking code reeeeeeeEEEEEEEEEEEEEEEEEEEE
        // ReSharper disable once CommentTypo
        //honestly tho what fag cares its Q intead of query? idk man
        [JsonProperty("q")]
        public string Q { get; set; }
        [JsonProperty("sortType")]
        public string SortType { get; set; }
        [JsonProperty("distance")]
        public int? Distance { get; set; }
        [JsonProperty("priceType")]
        public string PriceType { get; set; }

        //Some known parameters available when using the "html" scraperType
        [JsonProperty("scraperType")]
        public string ScraperType { get; set; }
        [JsonProperty("keywords")]
        public string Keywords { get; set; }
        [JsonProperty("sortByName")]
        public string SortByName { get; set; }
        

        //SearchParameters merged here because its simpler
        //and doesn't make sense to have seperated
        [JsonProperty("pageDelayMs")]
        public int? PageDelayMs { get; set; }
        [JsonProperty("minResults")]
        public int? MinResults { get; set; }
        [JsonProperty("maxResults")]
        public int? MaxResults { get; set; }
        [JsonProperty("scrapeResultDetails")]
        public bool? ScrapeResultDetails { get; set; } 
        [JsonProperty("resultDetailsDelayMs")]
        public int? ResultDetailsDelayMs { get; set; }

        //Not tested, test later.
        public string InCla() {

            //Made specifically to work with Minimist
            //TODO make it not make cla for empty fields
            //TODO implement protection against quotiation in object field

            //i don't know how this works it just does
            IEnumerable<string> collection = GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetValue(this) != null)
                .Select(p => $"--{char.ToLowerInvariant(p.Name[0])}{p.Name.Substring(1)}=\"{p.GetValue(this)}\"");

            return string.Join(" ", collection);

        }
    }
}
