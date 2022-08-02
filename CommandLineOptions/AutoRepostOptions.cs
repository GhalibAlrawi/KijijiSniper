using CommandLine;

namespace KijijiSniper;

[Verb("autorepost",
	HelpText = "Automatically reposts ad(s) every set amount of time")]
class AutoRepostOptions : IOptions {
	[Value(0, HelpText = "A single ad ID or multiple IDs seperated by commas (no space), 'all' to repost all user's ads", Required = true)]
	public string AdIds { get; set; }
	
	[Value(1, HelpText = "Time interval (in minutes) the ads will be reposted at")]
	public double TimeIntervalMins { get; set; }
	
	[Value(2, HelpText = "Authentication file needed for posting an ad", Required = false, Default = "appdata/auth.json")]
	public string AuthDir { get; set; }
	
}