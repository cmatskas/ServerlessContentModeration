using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moderation;

namespace TextModeration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Running analysis");
            var client = new HttpClient();
            var moderationHelper = new ModerationHelper(client, "949379d77c9e41c6bc7332de29e44663");
            var result = await moderationHelper.ModerateText("Hello World");
            Console.WriteLine(result.ToString());
            Console.ReadKey();
        }
    }
}
