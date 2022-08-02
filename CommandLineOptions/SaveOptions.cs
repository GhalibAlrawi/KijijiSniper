using System.ComponentModel.DataAnnotations;
using CommandLine;

namespace KijijiSniper {
	[Verb("save",
		HelpText =
			"Locally saves specified ad from Kijiji in xml file, the post command can be used to later post the ad again")]
	public class SaveOptions : IOptions {
		[Value(0, HelpText = "Ad ID", Required = true)]
		public string AdId { get; set; }

		[Value(1, HelpText = "Directory the listing will be saved to", Required = true)]
		public string OutputDir { get; set; }
	}
}