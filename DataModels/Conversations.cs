   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */

   using System.Collections.Generic;
   using System.Xml.Serialization;
   using KijijiSniper.API_Types;

   namespace KijijiSniper.DataModels
{
	[XmlRoot(ElementName="ad-super-state", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	public class Adsuperstate {
		[XmlElement(ElementName="value", Namespace="http://www.ebayclassifiedsgroup.com/schema/ad/v1")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName="user-message", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	public class LastMessage {
		[XmlElement(ElementName="sender-id", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Senderid { get; set; }
		[XmlElement(ElementName="sender-name", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Sendername { get; set; }
		[XmlElement(ElementName="direction", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Direction { get; set; }
		[XmlElement(ElementName="msg-content", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Msgcontent { get; set; }
		[XmlElement(ElementName="answered", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Answered { get; set; }
		[XmlElement(ElementName="post-time-stamp", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Posttimestamp { get; set; }
		[XmlElement(ElementName="structured-msg-id", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Structuredmsgid { get; set; }
		[XmlElement(ElementName="read", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Read { get; set; }
		[XmlAttribute(AttributeName="id")]
		public string Id { get; set; }
	}

	[XmlRoot(ElementName="user-conversation", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	public class ShortConvo {
		[XmlElement(ElementName="ad-id", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adid { get; set; }
		[XmlElement(ElementName="category-id", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Categoryid { get; set; }
		[XmlElement(ElementName="location-id", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Locationid { get; set; }
		[XmlElement(ElementName="ad-owner-id", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adownerid { get; set; }
		[XmlElement(ElementName="ad-owner-email", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adowneremail { get; set; }
		[XmlElement(ElementName="ad-owner-name", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adownername { get; set; }
		[XmlElement(ElementName="ad-owner-pictures", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adownerpictures { get; set; }
		[XmlElement(ElementName="ad-replier-id", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adreplierid { get; set; }
		[XmlElement(ElementName="ad-replier-email", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adreplieremail { get; set; }
		[XmlElement(ElementName="ad-replier-name", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adrepliername { get; set; }
		[XmlElement(ElementName="ad-replier-pictures", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adreplierpictures { get; set; }
		[XmlElement(ElementName="ad-subject", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adsubject { get; set; }
		[XmlElement(ElementName="ad-first-img-url", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Adfirstimgurl { get; set; }
		[XmlElement(ElementName="ad-super-state", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public Adsuperstate Adsuperstate { get; set; }
		[XmlElement(ElementName="num-unread-msg", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Numunreadmsg { get; set; }
		[XmlElement(ElementName="flagged-buyer", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Flaggedbuyer { get; set; }
		[XmlElement(ElementName="flagged-seller", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Flaggedseller { get; set; }
		[XmlElement(ElementName="eligible-for-reply", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Eligibleforreply { get; set; }
		[XmlElement(ElementName="user-message", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public Message LastMessage { get; set; }
		[XmlAttribute(AttributeName="uid")]
		public string Uid { get; set; }
	}

	[XmlRoot(ElementName="paging", Namespace="http://www.ebayclassifiedsgroup.com/schema/types/v1")]
	public class Paging {
		[XmlElement(ElementName="numFound", Namespace="http://www.ebayclassifiedsgroup.com/schema/types/v1")]
		public string NumFound { get; set; }
		[XmlElement(ElementName="link", Namespace="http://www.ebayclassifiedsgroup.com/schema/types/v1")]
		public List<Link> Link { get; set; }
	}

	[XmlRoot(ElementName="user-conversations", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	public class Conversations : PersistentSettingsBase<Conversations> {
		[XmlElement(ElementName="total-conversation-count", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Totalconversationcount { get; set; }
		[XmlElement(ElementName="total-unread-conversation-count", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Totalunreadconversationcount { get; set; }
		[XmlElement(ElementName="user-conversation", Namespace="http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public List<ShortConvo> ShortConvo { get; set; }
		[XmlElement(ElementName="paging", Namespace="http://www.ebayclassifiedsgroup.com/schema/types/v1")]
		public Paging Paging { get; set; }
		[XmlAttribute(AttributeName="types", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Types { get; set; }
		[XmlAttribute(AttributeName="cat", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Cat { get; set; }
		[XmlAttribute(AttributeName="loc", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Loc { get; set; }
		[XmlAttribute(AttributeName="ad", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Ad { get; set; }
		[XmlAttribute(AttributeName="pic", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Pic { get; set; }
		[XmlAttribute(AttributeName="user", Namespace="http://www.w3.org/2000/xmlns/")]
		public string User { get; set; }
		[XmlAttribute(AttributeName="feat", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Feat { get; set; }
		[XmlAttribute(AttributeName="attr", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Attr { get; set; }
		[XmlAttribute(AttributeName="vid", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Vid { get; set; }
		[XmlAttribute(AttributeName="notice", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Notice { get; set; }
		[XmlAttribute(AttributeName="rate", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Rate { get; set; }
		[XmlAttribute(AttributeName="reply", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Reply { get; set; }
		[XmlAttribute(AttributeName="feed", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Feed { get; set; }
		[XmlAttribute(AttributeName="payment", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Payment { get; set; }
		[XmlAttribute(AttributeName="payment-v2", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Paymentv2 { get; set; }
		[XmlAttribute(AttributeName="order", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Order { get; set; }
		[XmlAttribute(AttributeName="notif", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Notif { get; set; }
		[XmlAttribute(AttributeName="counter", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Counter { get; set; }
		[XmlAttribute(AttributeName="flag", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Flag { get; set; }
		[XmlAttribute(AttributeName="sug", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Sug { get; set; }
		[XmlAttribute(AttributeName="stat", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Stat { get; set; }
		[XmlAttribute(AttributeName="vrn", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Vrn { get; set; }
		[XmlAttribute(AttributeName="version")]
		public string Version { get; set; }
		[XmlAttribute(AttributeName="locale")]
		public string Locale { get; set; }
	}

}
