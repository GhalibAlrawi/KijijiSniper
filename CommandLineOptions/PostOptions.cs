using CommandLine;

namespace KijijiSniper;
[Verb("post",
	HelpText = "Posts locally saved ad (xml file) from the save command")]
public class PostOptions : IOptions {
	[Value(0, HelpText = "Directory to ad that will be posted", Required = true)]
	public string AdDir { get; set; }
	[Value(1, HelpText = "Directory to authentication file needed to authenticate client to Kijiji servers", Default = "appdata/auth.json", Required = false)]
	public string AuthDir { get; set; }
}