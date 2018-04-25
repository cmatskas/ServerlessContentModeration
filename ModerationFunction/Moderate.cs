using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Moderation;
using System.Net.Http;
using System.Threading.Tasks;

namespace ModerationFunction
{
    public static class Moderate
    {
        private static readonly HttpClient Client = new HttpClient();

        [FunctionName("Moderate")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req, 
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var textToModerate = (string)data?.textToModerate;

            var moderationHelper = new ModerationHelper(Client, "<your content moderation api key>", "<moderation service region>");
            ModerationResult result = await moderationHelper.ModerateText(textToModerate);

            return data != null
                ? (ActionResult)new OkObjectResult(result.ToString())
                : new BadRequestObjectResult("Please pass a string to evaluate");
        }
    }
}
