using Newtonsoft.Json;

namespace KijijiSniper.API_Types
{
	public partial class UnreadConvosCount
	{
		[JsonProperty("unreadConversationsCount")]
		public long UnreadConversationsCount { get; set; }

		[JsonProperty("unreadConversationsAsPoster")]
		public long UnreadConversationsAsPoster { get; set; }

		[JsonProperty("unreadConversationsAsReplier")]
		public long UnreadConversationsAsReplier { get; set; }
	}
}