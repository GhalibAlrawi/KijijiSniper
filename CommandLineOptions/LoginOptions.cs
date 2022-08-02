using CommandLine;

namespace KijijiSniper {
    [Verb("login", HelpText = "Creates an authentication file needed for some commands like repost")]
    public class LoginOptions : IOptions {
        [Value(0, HelpText = "Email", Required = true)]
        public string Email { get; set; }
        [Value(1, HelpText = "Password", Required = true)]
        public string Password { get; set; }
        [Value(2, HelpText = "Directory to authentication file needed to authenticate client to Kijiji servers", Default = "appdata/auth.json", Required = false)]
        public string AuthDir { get; set; }
    }
}