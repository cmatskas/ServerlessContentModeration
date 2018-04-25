using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moderation;

namespace TextModeration
{
    class Program
    {
        private static string key = "<your moderation service API key>";
        private static string region = "westeurope";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Running analysis");
            var client = new HttpClient();
            var moderationHelper = new ModerationHelper(client, key, region);
            var result = await moderationHelper.ModerateText("Hello World");
            Console.WriteLine(result.ToString());
            Console.ReadKey();
        }
    }
}
