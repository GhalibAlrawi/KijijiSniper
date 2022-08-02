using System;

namespace KijijiSniper.DataModels {
    //TODO This is an error waiting to happen
    //include all possible attributes. Not only cell phone specific attributes.
    using System.Xml.Serialization;
    using System.Collections.Generic;
    [XmlRoot(ElementName = "value", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
    public class Value {
        [XmlAttribute(AttributeName = "localized-label")]
        public string Localizedlabel { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "currency-iso-code", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
    public class CurrencyIsoCode {
        [XmlElement(ElementName = "value", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public Value Value { get; set; }
    }

    [XmlRoot(ElementName = "price-type", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
    public class PriceType {
        [XmlElement(ElementName = "value", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "price", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
    public class Price {
        [XmlElement(ElementName = "currency-iso-code", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public CurrencyIsoCode CurrencyIsoCode { get; set; }
        [XmlElement(ElementName = "amount", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string Amount { get; set; }
        [XmlElement(ElementName = "price-type", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public PriceType PriceType { get; set; }
    }

    [XmlRoot(ElementName = "ad-type", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
    public class AdType {
        [XmlElement(ElementName = "value", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string Value2 { get; set; }
    }

    [XmlRoot(ElementName = "ad-address", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
    public class AdAddress {
        [XmlElement(ElementName = "full-address", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string FullAddress { get; set; }
        [XmlElement(ElementName = "street", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string Street { get; set; }
        [XmlElement(ElementName = "additional-delivery-info", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string AdditionalDeliveryInfo { get; set; }
        [XmlElement(ElementName = "city", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string City { get; set; }
        [XmlElement(ElementName = "state", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string State { get; set; }
        [XmlElement(ElementName = "zip-code", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string Zipcode { get; set; }
        [XmlElement(ElementName = "country", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string Country { get; set; }
        [XmlElement(ElementName = "longitude", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string Longitude { get; set; }
        [XmlElement(ElementName = "latitude", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string Latitude { get; set; }
        [XmlElement(ElementName = "radius", Namespace = "http://www.ebayclassifiedsgroup.com/schema/types/v1")]
        public string Radius { get; set; }
    }

    [XmlRoot(ElementName = "ad-status", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
    public class AdStatus {
        [XmlElement(ElementName = "value", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string Value2 { get; set; }
    }

    [XmlRoot(ElementName = "category", Namespace = "http://www.ebayclassifiedsgroup.com/schema/category/v1")]
    public class Category {
        [XmlElement(ElementName = "id-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/category/v1")]
        public string IdName { get; set; }
        [XmlElement(ElementName = "localized-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/category/v1")]
        public string LocalizedName { get; set; }
        [XmlElement(ElementName = "l1-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/category/v1")]
        public string L1name { get; set; }
        [XmlElement(ElementName = "parent-id", Namespace = "http://www.ebayclassifiedsgroup.com/schema/category/v1")]
        public string ParentId { get; set; }
        [XmlElement(ElementName = "children-count", Namespace = "http://www.ebayclassifiedsgroup.com/schema/category/v1")]
        public string ChildrenCount { get; set; }
        [XmlElement(ElementName = "flags", Namespace = "http://www.ebayclassifiedsgroup.com/schema/category/v1")]
        public string Flags { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "location", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
    public class Location {
        [XmlElement(ElementName = "id-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public string IdName { get; set; }
        [XmlElement(ElementName = "localized-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public string LocalizedName { get; set; }
        [XmlElement(ElementName = "longitude", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public string Longitude { get; set; }
        [XmlElement(ElementName = "latitude", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public string Latitude { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "locations", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
    public class Locations {
        [XmlElement(ElementName = "location", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public Location Location { get; set; }
    }

    [XmlRoot(ElementName = "value", Namespace = "http://www.ebayclassifiedsgroup.com/schema/attribute/v1")]
    public class Value2 {
        [XmlAttribute(AttributeName = "localized-label")]
        public string LocalizedLabel { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "attribute", Namespace = "http://www.ebayclassifiedsgroup.com/schema/attribute/v1")]
    public class Attribute {
        [XmlElement(ElementName = "value", Namespace = "http://www.ebayclassifiedsgroup.com/schema/attribute/v1")]
        public Value2 Value2 { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "accessibility-feature")]
        public string Accessibilityfeature { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "localized-label")]
        public string Localizedlabel { get; set; }
        [XmlAttribute(AttributeName = "sub-type")]
        public string Subtype { get; set; }
    }

    [XmlRoot(ElementName = "attributes", Namespace = "http://www.ebayclassifiedsgroup.com/schema/attribute/v1")]
    public class Attributes {
        [XmlElement(ElementName = "attribute", Namespace = "http://www.ebayclassifiedsgroup.com/schema/attribute/v1")]
        public List<Attribute> Attribute { get; set; }
    }

    [XmlRoot(ElementName = "link", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
    public class Link {
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }
    }

    [XmlRoot(ElementName = "link", Namespace = "http://www.ebayclassifiedsgroup.com/schema/picture/v1")]
    public class Link2 {
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }
    }

    [XmlRoot(ElementName = "picture", Namespace = "http://www.ebayclassifiedsgroup.com/schema/picture/v1")]
    public class Picture {
        [XmlElement(ElementName = "link", Namespace = "http://www.ebayclassifiedsgroup.com/schema/picture/v1")]
        public List<Link2> Link2 { get; set; }
    }

    [XmlRoot(ElementName = "pictures", Namespace = "http://www.ebayclassifiedsgroup.com/schema/picture/v1")]
    public class Pictures {
        [XmlElement(ElementName = "picture", Namespace = "http://www.ebayclassifiedsgroup.com/schema/picture/v1")]
        public List<Picture> Picture { get; set; }
    }

    [XmlRoot(ElementName = "contact-method", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
    public class ContactMethod {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "enabled")]
        public string Enabled { get; set; }
    }

    [XmlRoot(ElementName = "contact-methods", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
    public class ContactMethods {
        [XmlElement(ElementName = "contact-method", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public List<ContactMethod> ContactMethod { get; set; }
    }
    [Serializable,XmlRoot(ElementName = "ad", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
    public class Ad : PersistentSettingsBase<Ad> {
        [XmlElement(ElementName = "price", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public Price Price { get; set; }
        [XmlElement(ElementName = "ad-type", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public AdType AdType { get; set; }
        [XmlElement(ElementName = "title", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string Title { get; set; }
        [XmlElement(ElementName = "description", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string Description { get; set; }
        [XmlElement(ElementName = "imprint", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string Imprint { get; set; }
        [XmlElement(ElementName = "ad-address", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public AdAddress AdAddress { get; set; }
        [XmlElement(ElementName = "highest-price", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string HighestPrice { get; set; }
        [XmlElement(ElementName = "ad-status", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public AdStatus AdStatus { get; set; }
        [XmlElement(ElementName = "listing-tags", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string ListingTags { get; set; }
        [XmlElement(ElementName = "user-id", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string Userid { get; set; }
        [XmlElement(ElementName = "ad-source-id", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string AdSourceId { get; set; }
        [XmlElement(ElementName = "ad-channel-id", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string AdChannelId { get; set; }
        [XmlElement(ElementName = "ad-email-channel-id", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string AdEmailChannelId { get; set; }
        [XmlElement(ElementName = "phone", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string Phone { get; set; }
        [XmlElement(ElementName = "rank", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string Rank { get; set; }
        [XmlElement(ElementName = "view-ad-count", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string ViewAdCount { get; set; }
        [XmlElement(ElementName = "phone-click-count", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string PhoneClickCount { get; set; }
        [XmlElement(ElementName = "map-view-count", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string MapViewCount { get; set; }
        [XmlElement(ElementName = "creation-date-time", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string CreationDateTime { get; set; }
        [XmlElement(ElementName = "modification-date-time", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string ModificationDateTime { get; set; }
        [XmlElement(ElementName = "start-date-time", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string StartDateTime { get; set; }
        [XmlElement(ElementName = "end-date-time", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string EndDateTime { get; set; }
        [XmlElement(ElementName = "features-active", Namespace = "http://www.ebayclassifiedsgroup.com/schema/feature/v1")]
        public string FeaturesActive { get; set; }
        [XmlElement(ElementName = "category", Namespace = "http://www.ebayclassifiedsgroup.com/schema/category/v1")]
        public Category Category { get; set; }
        [XmlElement(ElementName = "locations", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public Locations Locations { get; set; }
        [XmlElement(ElementName = "neighborhood", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string Neighborhood { get; set; }
        [XmlElement(ElementName = "attributes", Namespace = "http://www.ebayclassifiedsgroup.com/schema/attribute/v1")]
        public Attributes Attributes { get; set; }
        [XmlElement(ElementName = "link", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public List<Link> Link { get; set; }
        [XmlElement(ElementName = "pictures", Namespace = "http://www.ebayclassifiedsgroup.com/schema/picture/v1")]
        public Pictures Pictures { get; set; }
        [XmlElement(ElementName = "contact-methods", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public ContactMethods ContactMethods { get; set; }
        [XmlElement(ElementName = "poster-contact-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string PosterContactName { get; set; }
        [XmlElement(ElementName = "adSlots", Namespace = "http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
        public string AdSlots { get; set; }
        [XmlAttribute(AttributeName = "types", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Types { get; set; }
        [XmlAttribute(AttributeName = "cat", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Cat { get; set; }
        [XmlAttribute(AttributeName = "loc", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Loc { get; set; }
        [XmlAttribute(AttributeName = "ad", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string _ad { get; set; }
        [XmlAttribute(AttributeName = "pic", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Pic { get; set; }
        [XmlAttribute(AttributeName = "user", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string User { get; set; }
        [XmlAttribute(AttributeName = "feat", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Feat { get; set; }
        [XmlAttribute(AttributeName = "attr", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Attr { get; set; }
        [XmlAttribute(AttributeName = "vid", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Vid { get; set; }
        [XmlAttribute(AttributeName = "notice", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Notice { get; set; }
        [XmlAttribute(AttributeName = "rate", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Rate { get; set; }
        [XmlAttribute(AttributeName = "reply", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Reply { get; set; }
        [XmlAttribute(AttributeName = "feed", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Feed { get; set; }
        [XmlAttribute(AttributeName = "payment", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Payment { get; set; }
        [XmlAttribute(AttributeName = "payment-v2", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Paymentv2 { get; set; }
        [XmlAttribute(AttributeName = "order", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Order { get; set; }
        [XmlAttribute(AttributeName = "notif", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Notif { get; set; }
        [XmlAttribute(AttributeName = "counter", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Counter { get; set; }
        [XmlAttribute(AttributeName = "flag", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Flag { get; set; }
        [XmlAttribute(AttributeName = "sug", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Sug { get; set; }
        [XmlAttribute(AttributeName = "stat", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Stat { get; set; }
        [XmlAttribute(AttributeName = "vrn", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Vrn { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "locale")]
        public string Locale { get; set; }
    }

}


