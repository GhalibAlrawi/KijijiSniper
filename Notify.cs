using System;
using System.Collections.Generic;
using System.IO;
using NetCoreAudio;

namespace KijijiSniper {
    public class Notify {
        //TODO make update every few mins for indianNotify using queue stuff
        //public time
        //will have notification option implemented for the future

        public static void Notify1(Listing listing) {
            Console.WriteLine(listing.Title);
            Console.WriteLine(listing.Description);
            Console.WriteLine(listing.TimeSincePostedText());
            Console.WriteLine($"Scraped: {DateTime.Now}");
            Console.WriteLine($"Posted: {listing.Date}");
            Console.WriteLine("$" + listing.Attributes.Price);
            Console.WriteLine(listing.Url);
            Console.WriteLine();

            Player player = new Player();
            //player.Play("notification.mp3");
        }

        public static void IndianNotify(List<Listing> listings) {
            Console.Clear();
            foreach (Listing listing in listings) {
                //iPhone x
                //256gb mint condition.Always protected in case. Life proof case included.No charger or headphones. ...
                //27 minutes ago
                //Scraped: 2021-02-20 6:49:18 PM
                //Published: 2021-02-20 6:48:18 PM
                //$460
                //https://www.kijiji.ca/v-cell-phone/calgary/iphone-x/1552031439
                string output = $"{listing.Title}\n" +
                                $"{listing.Description}\n" +
                                $"{listing.TimeSincePostedText()}\n" +
                                $"Scraped: {DateTime.Now}\n" +
                                $"Published: {listing.Title}\n" +
                                $"${listing.Attributes.Price}\n" +
                                $"{listing.Url}\n";
                Console.Write(output);

                //if(listing == listings.Last())
                    File.AppendAllText("log.txt", output);

                Player player = new Player();
                player.Play("notification.mp3");
            }

        }
    }
}
