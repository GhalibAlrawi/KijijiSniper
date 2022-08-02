using CommandLine;

namespace KijijiSniper {
    [Verb("repost", HelpText = "Reposts an ad from Kijiji, needs authentication and takes 3 minutes")]
    public class RepostOptions : IOptions {
        [Value(0, HelpText = "Ad ID of the ad you want to repost, 'all' to repost all user's ads ", Required = true)]
        public string AdId { get; set; }

        [Value(1, HelpText = "Directory to authentication file needed to authenticate client to Kijiji servers", Default = "appdata/auth.json", Required = false)]
        public string AuthDir { get; set; }
    }
}