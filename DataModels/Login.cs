using System.Xml.Serialization;

namespace KijijiSniper.DataModels {
	//Kijiji Json processing crap..


	//[Serializable]
	//[DesignerCategory("code")]
	//[XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	//[XmlRootAttribute("user-logins", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1", IsNullable = false)]
	//public class LoginResponseXml : PersistentSettingsBase<LoginResponseXml> {
	//    [XmlElement(ElementName = "user-login")]
	//    public UserLogin_ UserLogin { get; set; }

	//    [XmlRoot(ElementName = "user-login")]
	//    [Serializable]
	//    [DesignerCategory("code")]
	//    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	//    public class UserLogin_ {
	//        [XmlElement(ElementName = "id")]
	//        public string Id { get; set; }
	//        [XmlElement(ElementName = "email")]
	//        public string Email { get; set; }
	//        [XmlElement(ElementName = "token")]
	//        public string Token { get; set; }
	//    }
	//}

	[XmlRoot(ElementName = "user-login", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
	public class UserLogin {
		[XmlElement(ElementName = "id", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Id { get; set; }
		[XmlElement(ElementName = "email", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Email { get; set; }
		[XmlElement(ElementName = "token", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public string Token { get; set; }
	}

	[XmlRoot(ElementName = "user-logins", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
    public class LoginResponseXml : PersistentSettingsBase<LoginResponseXml> {
		[XmlElement(ElementName = "user-login", Namespace = "http://www.ebayclassifiedsgroup.com/schema/user/v1")]
		public UserLogin UserLogin { get; set; }

        [XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }

		[XmlAttribute(AttributeName = "locale")]
		public string Locale { get; set; }

		//Fuak ton of namespaces:
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