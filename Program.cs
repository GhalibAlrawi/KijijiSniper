using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using KijijiSniper.DataModels;
using Newtonsoft.Json;
using CommandLine;
using KijijiSniper.API_Types;

namespace KijijiSniper {
	public class Program {
		//TODO automatically calculate optimal scrape frequency through amount of new listings per scrape. Also, if >20 listings detected then new scrape
		static string checkAuthErrorMsg = "please check your authentication file for credential validity, or use the login subcommand to create a new file (default: appdata/auth.json)";
		//no sleepy time
		[DllImport("kernel32.dll", CharSet = CharSet.Auto,SetLastError = true)]
		static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
		[FlagsAttribute]
		public enum EXECUTION_STATE :uint
		{
			ES_AWAYMODE_REQUIRED = 0x00000040,
			ES_CONTINUOUS = 0x80000000,
			ES_DISPLAY_REQUIRED = 0x00000002,
			ES_SYSTEM_REQUIRED = 0x00000001
			// Legacy flag, should not be used.
			// ES_USER_PRESENT = 0x00000004
		}
		static async Task<int> Main(string[] args) {
			SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_AWAYMODE_REQUIRED);

			return await CommandLine.Parser.Default
				.ParseArguments<RepostOptions, ScoutOptions, LoginOptions,
					AutoRepostOptions, SaveOptions, PostOptions, AutoReplyOptions>(args)
				.MapResult(
					(RepostOptions opts) => RunRepostAndReturnExitCode(opts),
					(ScoutOptions opts) => RunScoutAndReturnExitCode(opts),
					(LoginOptions opts) => RunLoginAndReturnExitCode(opts),
					(AutoRepostOptions opts) => RunAutoRepostAndReturnExitCode(opts),
					(SaveOptions opts) => RunSaveAdAndReturnExitCode(opts),
					(PostOptions opts) => RunPostAdAndReturnExitCode(opts),
					(AutoReplyOptions opts) => RunAutoReplyAndReturnExitCode(opts),
					errs => Task.FromResult(0));

			static async Task<int> RunScoutAndReturnExitCode(ScoutOptions opts) {
				if (opts.TimeIntervalInMinutes < 1) {
					Console.WriteLine("Time interval (minutes) cannot be under 1 minute");
				}
				var api = new KijijiApi(new HttpClient());
				Console.WriteLine("Reading File...");
				var json = await File.ReadAllTextAsync(opts.SearchParametersDir);
				Console.WriteLine("Serializing search parameters to object...");
				var searchParams = JsonConvert.DeserializeObject<SearchParameters>(json);
				var timeInterval = TimeSpan.FromMinutes(opts.TimeIntervalInMinutes);
				//todo implement this proplerly https://stackoverflow.com/questions/177856/how-do-i-trap-ctrl-c-sigint-in-a-c-sharp-console-app
				var cts = new CancellationTokenSource();
				var ctkn = cts.Token;
				Console.WriteLine("Started scouting");
				await api.Scout(searchParams, timeInterval, Notify.Notify1, ctkn);
				return 0;
			}
			static async Task<int> RunRepostAndReturnExitCode(RepostOptions opts) {
				//todo add logic to detect if auth file is filled or smtg, prompt user to login if not
				KijijiApi api = new KijijiApi(new HttpClient());

				Console.WriteLine("Reading authentication file");
				KijijiUser user = await TryReadAuth(opts.AuthDir);
				
				if (opts.AdId.ToLower() == "all") {
					Console.WriteLine("Getting user's ads");
					(HttpResponseMessage httpResponse, List<Ad> ads) = await api.GetUserAds(user.UserId, user.Token);
					if (!httpResponse.IsSuccessStatusCode) {
						await PrintHttpExceptionMessage(httpResponse);
						return 1;
					}

					// Some fields need to be removed from retrieved ads object in order to make it postable
					var postableAds = ads.Select(ad => api.KijijiAdToPostable(user, ad));

					foreach (Ad ad in ads) {	
						Console.WriteLine($"Deleting {ad.Title}");
						httpResponse = await api.DeleteListing(user, ad.Id);
						if (!httpResponse.IsSuccessStatusCode) {
							await PrintHttpExceptionMessage(httpResponse);
							return 1;
						}
						await Task.Delay(1000);
					}

					// Waiting to avoid Kijiji duplicate ad detection
					Console.WriteLine("All ads deleted, waiting 3 minutes before reposting...");
					await Task.Delay(TimeSpan.FromMinutes(3));

					Console.WriteLine("Posting ads");
					foreach (var ad in postableAds) {
						(httpResponse, string _) = await api.PostListing(user, ad.ToString(), "xml");
						if (!httpResponse.IsSuccessStatusCode) {
							await PrintHttpExceptionMessage(httpResponse);
						}
						await Task.Delay(1000);
					}

					Console.WriteLine("All ads successfully posted");
					//todo check if number of ads on users profile matches before and after and if not archive them
					return 0;
				}
				else {
					Console.WriteLine("Getting ad");
					(var httpResponse, var ad) = await api.GetAd(opts.AdId);
					if (!httpResponse.IsSuccessStatusCode) {
						await PrintHttpExceptionMessage(httpResponse);
						return 1;
					}

					XDocument requestPayload = api.KijijiAdToPostable(user, ad);
					//todo add postable ad obj and print the name of wtv u got

					Console.WriteLine("Deleting ad");
					httpResponse = await api.DeleteListing(user, opts.AdId);

					if (!httpResponse.IsSuccessStatusCode) {
						await PrintHttpExceptionMessage(httpResponse);
						return 1;
					}

					Console.WriteLine("Waiting 3 minutes before reposting...");
					await Task.Delay(TimeSpan.FromMinutes(3));

					Console.WriteLine("Posting listing");
					(httpResponse, var newAdName) = await api.PostListing(user, requestPayload.ToString(), "xml");
					if (!httpResponse.IsSuccessStatusCode) {
						await PrintHttpExceptionMessage(httpResponse);
						return 1;
					}

					Console.WriteLine("Ad successfully reposted");
					return 0;
				}
			}
			static async Task<int> RunLoginAndReturnExitCode(LoginOptions opts) {
				var api = new KijijiApi(new HttpClient());

				Console.WriteLine("Attempting login");
				(var httpResponse, var loginResponse) = await api.Login(opts.Email, opts.Password);
				if (!httpResponse.IsSuccessStatusCode) {
					await PrintHttpExceptionMessage(httpResponse);
					return 1;
				}
				var userId = loginResponse.UserLogin.Id;
				var token = loginResponse.UserLogin.Token;
				Console.WriteLine("Login Successful");

				Console.WriteLine("Retrieving profile");
				(httpResponse, var profile) = await api.GetProfile(userId, token);
				if (!httpResponse.IsSuccessStatusCode) {
					await PrintHttpExceptionMessage(httpResponse);
					return 1;
				}

				Console.WriteLine("Profile Retrieval successful");

				Console.WriteLine("Creating KijijiUser Object");
				var user = new KijijiUser() {
					Email = opts.Email,
					Token = token,
					UserId = userId,
					Username = profile.UserDisplayName
				};

				Console.WriteLine("Serializing Object to JSON");
				var json = JsonConvert.SerializeObject(user);
				Console.WriteLine("Writing JSON to file");
				await File.WriteAllTextAsync(opts.AuthDir, json);
				Console.WriteLine("Authentication completed");
				return 0;
			}
			static async Task<int> RunAutoRepostAndReturnExitCode(AutoRepostOptions opts) {
				var api = new KijijiApi(new HttpClient());

				Console.WriteLine("Reading authentication file");
				var user = await TryReadAuth(opts.AuthDir);
				
				string[] adIds = opts.AdIds.Split(',');

				while (true) {
					//Get the users ads
					Console.WriteLine("Getting user's ads");
					var userAds = new List<Ad>();
					if (opts.AdIds.ToLower() == "all") {
						(var httpResponse, userAds) = await api.GetUserAds(user.UserId, user.Token);
						if (!httpResponse.IsSuccessStatusCode) {
							await PrintHttpExceptionMessage(httpResponse);
							return 1;
						}	
					}
					else {
						foreach (string adId in adIds) {
							(HttpResponseMessage httpResponse, Ad ad) = await api.GetAd(adId);
							if (!httpResponse.IsSuccessStatusCode) {
								await PrintHttpExceptionMessage(httpResponse);
								return 1;
							}
							userAds.Add(ad);
						}
					}
					
					//Convert all the ads to a postable format
					var postableAds = userAds.Select(ad => api.KijijiAdToPostable(user, ad)).ToList();
					
					//Deleting ads
					foreach (Ad ad in userAds) {
						Console.WriteLine($"Deleting {ad.Title}");
						var httpResponse = await api.DeleteListing(user, ad.Id);
						if (!httpResponse.IsSuccessStatusCode) {
							await PrintHttpExceptionMessage(httpResponse);
							return 1;
						}
						await Task.Delay(1000);
					}

					Console.WriteLine("All ads deleted, waiting 3 minutes before reposting...");
					await Task.Delay(TimeSpan.FromMinutes(3));

					Console.WriteLine($"Posting ads");
					for (int i = 0; i < postableAds.Count(); i++) {
						(var httpResponse, var newAdId) = await api.PostListing(user, postableAds[i].ToString());
						if (httpResponse.IsSuccessStatusCode) {
							if (opts.AdIds != "all") adIds[i] = newAdId;
							if(postableAds[i] == postableAds.Last()) Console.WriteLine("All ads posted");
						}
						else {
							Console.WriteLine("Error encountered while posting ad");
							await PrintHttpExceptionMessage(httpResponse);
							Console.WriteLine($"Saving ad as {userAds[i].Id}.xml , post ad later with post command");
							await SaveAd(postableAds[i], $"{userAds[i].Id}.xml");
						}
						await Task.Delay(1000);
					}

					Console.WriteLine($"Waiting {TimeSpan.FromMinutes(opts.TimeIntervalMins).ToString()}");
					await Task.Delay(TimeSpan.FromMinutes(opts.TimeIntervalMins));
				}
			}
			static async Task<int> RunSaveAdAndReturnExitCode(SaveOptions opts) {
				var api = new KijijiApi(new HttpClient());
				var (httpResponse, ad) = await api.GetAd(opts.AdId);
				if (!httpResponse.IsSuccessStatusCode) { 
					await PrintHttpExceptionMessage(httpResponse);
					return 1;
				}
				
				Console.WriteLine("Ad retrieved successfully");
				Console.WriteLine("Serializing XML");
				
				string xmlAd;
				XmlSerializer xsSubmit = new XmlSerializer(typeof(Ad));

				using(var sww = new StringWriter())
				{
					using(XmlWriter writer = XmlWriter.Create(sww)) {
						await writer.WriteProcessingInstructionAsync("xml", "version='1.0'");
						xsSubmit.Serialize(writer, ad);
						xmlAd = sww.ToString();
					}
				}

				Console.WriteLine($"Writing ad to specified directory: {opts.OutputDir}");
				await File.WriteAllTextAsync(opts.OutputDir+".xml",xmlAd);
				Console.WriteLine("Done");
				return 0;
			}
			static async Task<int> RunPostAdAndReturnExitCode(PostOptions opts) {
				var api = new KijijiApi(new HttpClient());
				KijijiUser user = await TryReadAuth(opts.AuthDir);
				XmlSerializer ser = new XmlSerializer(typeof(Ad));
				Ad ad;
				using (XmlReader reader = XmlReader.Create(opts.AdDir))
				{
					ad = (Ad) ser.Deserialize(reader);
				}

				var postableAd = api.KijijiAdToPostable(user, ad);
				await api.PostListing(user, postableAd.ToString(), "xml");
				Console.WriteLine("done");
				return 0;
			}

			static async Task<int> RunAutoReplyAndReturnExitCode(AutoReplyOptions opts) {
				while (true) {
					KijijiApi api = new KijijiApi(new HttpClient());
					KijijiUser user = await TryReadAuth(opts.AuthDir);

					Console.WriteLine("Getting user's DMs");
					var (httpResponse, conversations) = await api.GetConversations(user);
					if (!httpResponse.IsSuccessStatusCode) {
						await PrintHttpExceptionMessage(httpResponse);
						return 1;
					}

					Console.WriteLine("Selecting specific conversations (is this still available?)");
					var inquiries = conversations.ShortConvo.Where(c =>
						c.LastMessage.Msgcontent == "Hi, is this still available?" || c.LastMessage.Msgcontent ==
						"Hi, I'm interested! Please contact me if this is still available.").ToList();

					Console.WriteLine($"{inquiries.Count()} conversations found");
					foreach (var inquiry in inquiries) {
						Console.WriteLine($"Responding to {inquiry.LastMessage.Sendername} on {inquiry.Adsubject}");
						httpResponse = await api.SendMessage(user, inquiry.Uid, inquiry.Adid,
							"Yes, it's still available.");
						if (!httpResponse.IsSuccessStatusCode) {
							await PrintHttpExceptionMessage(httpResponse);
							return 1;
						}
					}

					Console.WriteLine($"Waiting {TimeSpan.FromMinutes(opts.TimeIntervalMins).ToString()}");
					await Task.Delay(TimeSpan.FromMinutes(opts.TimeIntervalMins));
				}

				return 0;
			}
			
			static async Task PrintHttpExceptionMessage(HttpResponseMessage httpResponse) {
				Console.WriteLine("HTTP error from server encountered");
				switch (httpResponse.StatusCode) {
					case HttpStatusCode.Forbidden:
						Console.WriteLine("Forbidden from requested operation");
						break;
					case HttpStatusCode.Unauthorized:
						Console.WriteLine("Unauthorized, credentials likely not valid for requested operation (i.e., reposting a post that doesn't belong to specified account)");
						break;
					case HttpStatusCode.BadRequest:
						Console.WriteLine("Inputted information is not valid");
						break;
					case HttpStatusCode.InternalServerError:
						Console.WriteLine("Internal server error, if inputted information is valid, the server could be blocking your IP due to too many requests, or the server could be down");
						break;
					default:
						Console.WriteLine("A HTTP error has occured");
						Console.WriteLine("Code: " + httpResponse.StatusCode);
						Console.WriteLine("Reason Phrase: " + httpResponse.ReasonPhrase);
						Console.WriteLine("Content Returned: " + await httpResponse.Content.ReadAsStringAsync());
						break;
				}
			}
			static async Task<KijijiUser> TryReadAuth(string authDir) {
				try {
					string fileContent = await File.ReadAllTextAsync(authDir);
					return JsonConvert.DeserializeObject<KijijiUser>(fileContent);
				}
				catch (Exception e) {
					switch (e) {
						case FileNotFoundException:
							Console.WriteLine(
								"Authentication file not found, specify the correct directory or restore default directory: appdata/auth.json");
							break;
						case JsonReaderException:
							Console.WriteLine(
								"Error while attempting to parse JSON data inside authentication file, use the 'login' command to create a valid JSON file.");
							break;
						case JsonException:
							Console.WriteLine("Error while attempting to parse JSON data from authentication file");
							break;
					}

					Console.WriteLine(e.Message);
				}

				return null;
			}
			static async Task SaveAd(XDocument ad, string path) {
				string xmlAd;
				XmlSerializer xsSubmit = new XmlSerializer(typeof(Ad));

				using(var sww = new StringWriter())
				{
					using(XmlWriter writer = XmlWriter.Create(sww)) {
						await writer.WriteProcessingInstructionAsync("xml", "version='1.0'");
						xsSubmit.Serialize(writer, ad);
						xmlAd = sww.ToString();
					}
				}

				await File.WriteAllTextAsync(path,xmlAd);
			}
		}
	}
}