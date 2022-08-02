using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using KijijiSniper.DataModels;

namespace KijijiSniper {
    public class KijijiApi {
        //TODO add token validity checks
        public readonly HttpClient Client;

        private static readonly ProductInfoHeaderValue UserAgentProduct =
            new ProductInfoHeaderValue("Kijiji", "12.15.0");

        private static readonly ProductInfoHeaderValue UserAgentComment =
            new ProductInfoHeaderValue("(iPhone; iOS 13.5.1; en_CA)");

        public KijijiApi(HttpClient client) {
            Client = client;
        }
        public async Task<(HttpResponseMessage, LoginResponseXml)> Login(string email, string password) {
            const string url = "https://mingle.kijiji.ca/api/users/login";

            //Creating payload via form encoding
            var keyValues = new List<KeyValuePair<string, string>> {
                new("username", email),
                new("password", password),
                new("socialAutoRegistration", "false")
            };
            HttpContent payload = new FormUrlEncodedContent(keyValues);

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-CA"));
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);
            httpRequest.Content = payload;

            HttpResponseMessage httpResponse = await Client.SendAsync(httpRequest);
            string content = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                return (httpResponse, null);

            var loginResponse = LoginResponseXml.XMLDeserialize(content);
            return (httpResponse, loginResponse);
        }
        public async Task<(HttpResponseMessage, KijijiProfileResponseXml)> GetProfile(string userId, string token) {
            //todo come back here and we see if we should jus serialize to kijiji profile directly
            string url = $"https://mingle.kijiji.ca/api/users/{userId}/profile/";
            string authHeader = $"id=\"{userId}\", token=\"{token}\"";

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.Add("x-ecg-authorization-user", authHeader);
            httpRequest.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-CA"));
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);

            HttpResponseMessage httpResponse = await Client.SendAsync(httpRequest);
            var content = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                return (httpResponse, null);

            var objectResponse = KijijiProfileResponseXml.XMLDeserialize(content);
            return (httpResponse, objectResponse);
        }
        public async Task<(HttpResponseMessage, List<Ad>)> GetUserAds(string userId, string token) {
            string url = $"https://mingle.kijiji.ca/api/users/{userId}/ads/";
            string authHeader = $"id=\"{userId}\", token=\"{token}\"";

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.Add("x-ecg-authorization-user", authHeader);
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);

            HttpResponseMessage httpResponse = await Client.SendAsync(httpRequest);
            string content = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode) {
                return (httpResponse, null);
            }

            XDocument xmlResponse = XDocument.Parse(content);

            IEnumerable<XElement> adElements = xmlResponse
                .Element(XName.Get("{http://www.ebayclassifiedsgroup.com/schema/ad/v1}ads"))
                .Elements(XName.Get("{http://www.ebayclassifiedsgroup.com/schema/ad/v1}ad"));

            List<Ad> deserializedAdsList =
                adElements.Select(adElement => Ad.XMLDeserialize(adElement.ToString())).ToList();

            return (httpResponse, deserializedAdsList);
        }
        public XDocument KijijiAdToPostable(KijijiUser user, Ad ad) {
            //Check design docs or this method wont make any sense
            //TODO add valid xml check code
            XDocument ad_ = XDocument.Parse(ad.XMLSerialize());

            //XML Namespaces
            string nsTypes = "{http://www.ebayclassifiedsgroup.com/schema/types/v1}";
            string nsAd = "{http://www.ebayclassifiedsgroup.com/schema/ad/v1}";
            string nsFeature = "{http://www.ebayclassifiedsgroup.com/schema/feature/v1}";
            string nsCategory = "{http://www.ebayclassifiedsgroup.com/schema/category/v1}";
            string nsLocation = "{http://www.ebayclassifiedsgroup.com/schema/location/v1}";
            string nsPicture = "{http://www.ebayclassifiedsgroup.com/schema/picture/v1}";
            string nsAttribute = "{http://www.ebayclassifiedsgroup.com/schema/attribute/v1}";

            var keptElements = new List<string> {
                nsAd + "price",
                nsAd + "title",
                nsCategory + "category",
                nsLocation + "locations",
                nsAd + "description",
                nsAd + "ad-address",
                nsAd + "listing-tags",
                nsAd + "neighborhood",
                nsAttribute + "attributes",
                nsPicture + "pictures",
                nsAd + "ad-type",
                nsAd + "ad"
            };
            //TODO add  optional phone number
            var addedElements = new Dictionary<string, string> {
                {nsAd + "email", user.Email},
                {nsAd + "poster-contact-name", user.Username},
                {nsAd + "account-id", user.UserId},
            };

            foreach (XElement element in
                     ad_.Root.Elements()
                         .ToList()
                         .Where(element => !keptElements
                             .Contains(element.Name.ToString()))) {
                element.Remove();
            }

            foreach ((string name, string value) in addedElements)
                ad_.Root.Add(new XElement(name, value));

            ad_.Element(XName.Get(nsAd + "ad")).Attribute(XName.Get("id")).Remove();

            return ad_;
        }
        public async Task<HttpResponseMessage> DeleteListing(KijijiUser user, string adId) {
            string url = $"https://mingle.kijiji.ca/api/users/{user.UserId}/ads/{adId}";
            string userAuth = $"id=\"{user.UserId}\", token=\"{user.Token}\"";

            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.Add("x-ecg-authorization-user", userAuth);
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);

            HttpResponseMessage httpResponse = await Client.SendAsync(httpRequest);
            return httpResponse;
        }
        //TODO check if node is instslled b4 this
        public List<Listing> SearchAds(SearchParameters parameters) {
            string paramsCla = parameters.InCla();
            string command = "queryKijiji.js " + paramsCla;

            Process proc = new Process {
                StartInfo = {CreateNoWindow = true, FileName = "node.exe", Arguments = command}
            };
            proc.Start();
            proc.WaitForExit();

            string data = File.ReadAllText(parameters.Output + ".json");
            string json = data.Substring(1);

            File.Delete(parameters.Output + ".json");

            //To avoid Json.Net converting timezones
            //TODO make persistent throughout project, optimizes creating an object for every search
            var jsonSerializerSettings = new JsonSerializerSettings {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                DateParseHandling = DateParseHandling.DateTimeOffset
            };

            if (data.StartsWith("0")) {
                return JsonConvert.DeserializeObject<List<Listing>>(json, jsonSerializerSettings);
            }
            else {
                dynamic error = JsonConvert.DeserializeObject(json, jsonSerializerSettings);
                throw new Exception(error.Message); //todo make better
            }
        }
        public async Task Scout(SearchParameters parameters, TimeSpan scoutInterval, Action<Listing> notify,
                                CancellationToken token) {
            if (token.IsCancellationRequested)
                return;

            var lastSearchTime = DateTime.Now;
            await Task.Delay(scoutInterval, token);
            while (!token.IsCancellationRequested) {
                var latestScrape = SearchAds(parameters);

                foreach (Listing listing in latestScrape.Where(listing => listing.Date > lastSearchTime))
                    notify.Invoke(listing);
                //might miss some listings if this takes forever^
                //todo, run profiler
                lastSearchTime = DateTime.Now;

                await Task.Delay(scoutInterval, token);
            }
        }
        public async Task ScoutMultiple(List<SearchParameters> searchParametersList, TimeSpan scoutInterval,
                                        Action<Listing> notify, CancellationToken token) {
            //The term "Search Parameters" can both be singular and plural in this context,
            //for this reason, we have SearchParameters and SearchParametersList (to imply a list of search params)
            scoutInterval /= searchParametersList.Count;

            //Bind the search parameters object with the last time it was scraped with
            var searchTimeBind = searchParametersList
                .Select(searchParameters => new SearchTimeBind(searchParameters, DateTime.Now)).ToList();

            await Task.Delay(scoutInterval);
            for (int i = 0; !token.IsCancellationRequested; i++) {
                var searchParameters = searchTimeBind[i].SearchParameters;
                var latestScrapeTime = searchTimeBind[i].LatestSearchTime;

                var latestScrape = SearchAds(searchParameters);
                foreach (Listing listing in latestScrape.Where(listing => listing.Date > latestScrapeTime)) {
                    notify.Invoke(listing);
                }

                searchTimeBind[i].LatestSearchTime = DateTime.Now;

                i++;
                //Reset index to allow for cycling through list
                if (i == searchParametersList.Count) {
                    i = 0;
                }

                await Task.Delay(scoutInterval, token);
            }
        }
        public async Task<(HttpResponseMessage, String)> PostListing(KijijiUser user, string payload, string contentType = "xml") {
            string url = $"https://mingle.kijiji.ca/api/users/{user.UserId}/ads";
            string userAuth = $"id=\"{user.UserId}\", token=\"{user.Token}\"";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.Add("x-ecg-authorization-user", userAuth);
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);
            httpRequest.Content = new StringContent(payload);
            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue($"application/{contentType}");

            var httpResponse = await Client.SendAsync(httpRequest);
            var newAdId = httpResponse.Headers.Location.ToString().Split('/').Last();
            return (httpResponse, newAdId);

        }
        public async Task<(HttpResponseMessage, Ad)> GetAd(string adId) {
            string url = $"https://mingle.kijiji.ca/api/ads/{adId}";

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);

            HttpResponseMessage httpResponse = await Client.SendAsync(httpRequest);
            string content = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                return (httpResponse, null);

            return (httpResponse, Ad.XMLDeserialize(content));
        }
        public async Task<(HttpResponseMessage, XDocument)> GetAdXml(string adId) {
            string url = $"https://mingle.kijiji.ca/api/ads/{adId}";

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);

            HttpResponseMessage httpResponse = await Client.SendAsync(httpRequest);
            string content = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                return (httpResponse, null);

            var xmlDoc = XDocument.Parse(content);
            return (httpResponse, xmlDoc);
        }

        public async Task<(HttpResponseMessage, Conversations)> GetConversations(KijijiUser user ,int size = 20, int pageSize = 0){
            string url = $"https://mingle.kijiji.ca/api/users/{user.UserId}/conversations?size={size}&page={pageSize}";
            string userAuth = $"id=\"{user.UserId}\", token=\"{user.Token}\"";

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.Add("x-ecg-authorization-user", userAuth);
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);

            HttpResponseMessage httpResponse = await Client.SendAsync(httpRequest);
            
            string stringContent = await httpResponse.Content.ReadAsStringAsync();
            Conversations conversations = Conversations.XMLDeserialize(stringContent);
            
            
            return (httpResponse, conversations);
        }
        public async Task<(HttpResponseMessage, Conversation)> GetConversation(KijijiUser user, string conversationId, bool markAsRead = false, int tail = 100) {
            // kijiji servers don't use url encoding for this, this is a vulnerability for them
            string url = $"https://mingle.kijiji.ca/api/users/{user.UserId}/conversations/{conversationId}?tail={tail}";
            string userAuth = $"id=\"{user.UserId}\", token=\"{user.Token}\"";

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.Add("x-ecg-authorization-user", userAuth);
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);

            HttpResponseMessage httpResponse = await Client.SendAsync(httpRequest);
            
            string stringContent = await httpResponse.Content.ReadAsStringAsync();
            Conversation messages = Conversation.XMLDeserialize(stringContent);
            
            return (httpResponse, messages);
        }
        public async Task<HttpResponseMessage> SendMessage(KijijiUser user, string conversationId, string adId, string message) {
            string url = $"https://mingle.kijiji.ca/api/replies/reply-to-ad-conversation";
            string userAuth = $"id=\"{user.UserId}\", token=\"{user.Token}\"";
            
            //too lazy to do things the right way
            string xml =
                $"<reply:reply-to-ad-conversation xmlns:types=\"http://www.ebayclassifiedsgroup.com/schema/types/v1\" xmlns:cat=\"http://www.ebayclassifiedsgroup.com/schema/category/v1\" xmlns:loc=\"http://www.ebayclassifiedsgroup.com/schema/location/v1\" xmlns:ad=\"http://www.ebayclassifiedsgroup.com/schema/ad/v1\" xmlns:attr=\"http://www.ebayclassifiedsgroup.com/schema/attribute/v1\" xmlns:pic=\"http://www.ebayclassifiedsgroup.com/schema/picture/v1\" xmlns:user=\"http://www.ebayclassifiedsgroup.com/schema/user/v1\" xmlns:rate=\"http://www.ebayclassifiedsgroup.com/schema/rate/v1\" xmlns:reply=\"http://www.ebayclassifiedsgroup.com/schema/reply/v1\" locale=\"en-CA\"> <reply:ad-id>{adId}</reply:ad-id> <reply:reply-message>{message}</reply:reply-message> <reply:conversation-id>{conversationId}</reply:conversation-id> <reply:reply-direction> <types:value>TO_BUYER</types:value> </reply:reply-direction> </reply:reply-to-ad-conversation>";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            httpRequest.Headers.Add("x-ecg-ver", "3.6");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.Add("x-ecg-authorization-user", userAuth);
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);
            httpRequest.Content = new StringContent(xml);
            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            HttpResponseMessage httpResponse = await Client.SendAsync(httpRequest);
            
            return (httpResponse);
        }
    }
    internal class SearchTimeBind {
        public SearchTimeBind(SearchParameters searchParameters, DateTime latestSearchTime) {
            SearchParameters = searchParameters;
            LatestSearchTime = latestSearchTime;
        }

        internal SearchParameters SearchParameters { get; set; }
        internal DateTime LatestSearchTime { get; set; }
    }
}