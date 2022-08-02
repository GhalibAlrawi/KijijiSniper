using System.Runtime.CompilerServices;
using CommandLine;

namespace KijijiSniper; 
[Verb("autoreply", HelpText = "Automatically responds to \"Is this still available?\" messages")]
public class AutoReplyOptions : IOptions {
	[Value(0, HelpText = "Authentication for whatever account you choose to use", Default = "appdata/auth.json", Required = false)]
	public string AuthDir { get; set; }
	[Value(1, HelpText = "Time interval in minutes for checking messages", Default = 5, Required = false)]
	public double TimeIntervalMins { get; set; }
}