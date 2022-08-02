using System;
using Newtonsoft.Json;

namespace KijijiSniper {
    public class Listing {
        /*The attributes class changes depending on the category of listing
         * An cell phone listing will have different attributes than a camera listing
         * im not really sure how id fix this in the future tbh but it will have to be
         * done one day. */
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("date")]
        public DateTime? Date { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("images")]
        public string[] Images { get; set; }
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        public string TimeSincePostedText() {
            //Returns UI-friendly string representation of time passed since posted
            if (Date == null) return "";
            TimeSpan msPassed = new TimeSpan(DateTime.Now.Ticks - Date.Value.Ticks);
            if (msPassed.TotalDays >= 1)
                return GetString((int)msPassed.TotalDays, "day");

            if (msPassed.TotalHours >= 1)
                return GetString((int)msPassed.TotalHours, "hour");

            if (msPassed.TotalMinutes >= 1)
                return GetString((int)msPassed.TotalMinutes, "minute");

            if (msPassed.TotalSeconds >= 10)
                return GetString((int)msPassed.TotalSeconds, "second");

            return "Just now";

            static string GetString(int amount, string unit) => amount == 1 ? $"1 {unit} ago" : $"{amount} {unit}s ago";
        }
        
        public TimeSpan? TimeSincePosted() {
            if (Date == null) return null;
            return DateTime.Now - Date;
        }
    }
    public class Attributes {
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("forsaleby")]
        public string ForSaleBy { get; set; }
        [JsonProperty("fulfillment")]
        public string Fulfillment { get; set; }
        [JsonProperty("payment")]
        public string Payment { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("petofferedby")]
        public string PetOfferedBy { get; set; }
        [JsonProperty("moreinfo")]
        public string MoreInfo { get; set; }
        [JsonProperty("animalwas")]
        public string AnimalWas { get; set; }
        [JsonProperty("phonebrand")]
        public string? PhoneBrand { get; set; }
        [JsonProperty("phonecarrier")]
        public string? PhoneCarrier { get; set; }
    }
}

