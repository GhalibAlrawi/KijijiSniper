using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace KijijiSniper.DataModels {
    public class KijijiUser {
        //TODO expand in future from getProfile thing
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        public async Task<List<Ad>> GetUserAds(HttpClient _client) {
            ProductInfoHeaderValue UserAgentProduct = new ProductInfoHeaderValue("Kijiji", "12.15.0");
            ProductInfoHeaderValue UserAgentComment = new ProductInfoHeaderValue("(iPhone; iOS 13.5.1; en_CA)");

            string url = $"https://mingle.kijiji.ca/api/users/{UserId}/ads/";
            string userAuth = $"id=\"{UserId}\", token=\"{Token}\"";

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpRequest.Headers.Add("x-ecg-ver", "1.67");
            httpRequest.Headers.Add("x-ecg-ab-test-group", "");
            httpRequest.Headers.Add("x-ecg-authorization-user", userAuth);
            httpRequest.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            httpRequest.Headers.UserAgent.Add(UserAgentProduct);
            httpRequest.Headers.UserAgent.Add(UserAgentComment);

            HttpResponseMessage httpResponse = await _client.SendAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            XDocument xmlResponse = XDocument.Parse(await httpResponse.Content.ReadAsStringAsync());
            //serialized elements. they are ad elements
            IEnumerable<XElement> adElements = xmlResponse.Element(XName.Get("{http://www.ebayclassifiedsgroup.com/schema/ad/v1}ads")).Elements(XName.Get("{http://www.ebayclassifiedsgroup.com/schema/ad/v1}ad"));
            //serialize them boys
            List<Ad> deserializedAdsList = adElements.Select(adElement => Ad.XMLDeserialize(adElement.ToString())).ToList();
            //return list of ads
            return deserializedAdsList;
        }
    }
}