using CommandLine;

namespace KijijiSniper {
    [Verb("scout",
        HelpText = "Actively monitors Kijiji for newly posted ads and sends notifications of detected ads through console")]
    public class ScoutOptions : IOptions {
        [Value(0, HelpText = "Directory to JSON search parameters", Required = true)]
        public string SearchParametersDir { get; set; }

        [Value(1, HelpText = "Time interval between searches in minutes", Required = true)]
        public double TimeIntervalInMinutes { get; set; }

        [Value(2, HelpText = "Console output config file", Required = false, Default = "/appdata/ConsoleOutput.ini")]
        public string ConsoleOutputCfgDir { get; set; }
    }
}