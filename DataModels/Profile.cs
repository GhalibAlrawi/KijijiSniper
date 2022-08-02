using System.Xml.Serialization;

namespace KijijiSniper.DataModels {
	[XmlRoot(ElementName = "user-address", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	public class UserAddress {
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

	[XmlRoot(ElementName = "dealer-information", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	public class DealerInformation {
		[XmlElement(ElementName = "dealer-type", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string DealerType { get; set; }
		[XmlElement(ElementName = "dealer-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string DealerName { get; set; }
	}

	[XmlRoot(ElementName = "user-profile", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	public class KijijiProfileResponseXml : PersistentSettingsBase<KijijiProfileResponseXml> {
		[XmlElement(ElementName = "user-id", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Userid { get; set; }
		[XmlElement(ElementName = "user-display-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserDisplayName { get; set; }
		[XmlElement(ElementName = "user-nickname", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserNickname { get; set; }
		[XmlElement(ElementName = "user-phone-number", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserPhoneNumber { get; set; }
		[XmlElement(ElementName = "user-external-references", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserExternalReferences { get; set; }
		[XmlElement(ElementName = "user-registration-date", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserRegistrationDate { get; set; }
		[XmlElement(ElementName = "user-active-ad-count", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserActiveAdCount { get; set; }
		[XmlElement(ElementName = "user-email", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserEmail { get; set; }
		[XmlElement(ElementName = "user-address", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public UserAddress UserAddress { get; set; }
		[XmlElement(ElementName = "user-tagline", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserTagLine { get; set; }
		[XmlElement(ElementName = "user-photo-url", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserPhotoUrl { get; set; }
		[XmlElement(ElementName = "user-website-url", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserWebsiteUrl { get; set; }
		[XmlElement(ElementName = "pictures", Namespace = "http://www.ebayclassifiedsgroup.com/schema/picture/v1")]
		public string Pictures { get; set; }
		[XmlElement(ElementName = "user-logos", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserLogos { get; set; }
		[XmlElement(ElementName = "dealer-information", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public DealerInformation DealerInformation { get; set; }
		[XmlElement(ElementName = "hide-ad-visit-counter", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string HideAdVisitCounter { get; set; }
		[XmlElement(ElementName = "send-marketing-email", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string SendMarketingEmail { get; set; }
		[XmlElement(ElementName = "send-upsell-email", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string SendUpsellEmail { get; set; }
		[XmlElement(ElementName = "hashed-user-id", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string HashedUserId { get; set; }
		[XmlElement(ElementName = "hashed-user-email", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string HashedUserEmail { get; set; }
		[XmlElement(ElementName = "hashed-user-email-hex", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string HashedUserEmailHex { get; set; }
		[XmlElement(ElementName = "hashed-user-email-md5", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string HashedUserEmailMd5 { get; set; }
		[XmlElement(ElementName = "user-photo-enabled", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserPhotoEnabled { get; set; }
		[XmlElement(ElementName = "user-responsiveness", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string UserResponsiveness { get; set; }
		[XmlElement(ElementName = "reply-rate", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string ReplyRate { get; set; }
		[XmlElement(ElementName = "read-indicator-enabled", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string ReadIndicatorEnabled { get; set; }
		[XmlElement(ElementName = "average-review-score", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string AverageReviewScore { get; set; }
		[XmlElement(ElementName = "total-reviews", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string TotalReviews { get; set; }
		[XmlAttribute(AttributeName = "types", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Types { get; set; }
		[XmlAttribute(AttributeName = "cat", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Cat { get; set; }
		[XmlAttribute(AttributeName = "loc", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Loc { get; set; }
		[XmlAttribute(AttributeName = "ad", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Ad { get; set; }
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
	}
}
