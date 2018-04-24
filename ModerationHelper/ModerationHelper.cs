using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Moderation
{
    public class ModerationHelper
    {
        private string key;
        private HttpClient httpClient;

        public ModerationHelper(HttpClient client, string apiKey)
        {
            key = apiKey;
            httpClient = client;
        }
        public async Task<ModerationResult> ModerateText(string textToModerate)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            queryString["classify"] = "true";
            var uri = "https://westeurope.api.cognitive.microsoft.com/contentmoderator/moderate/v1.0/ProcessText/Screen?" + queryString;

            HttpResponseMessage response;

            using (var content = new StringContent(textToModerate))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                response = await httpClient.PostAsync(uri, content);
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var moderationResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ModerationResponse>(responseContent);
            return GetModerationResult(moderationResponse);
        }

        private ModerationResult GetModerationResult(ModerationResponse response)
        {
            if (response.Terms == null || response.Terms.Count() == 0)
            {
                return new ModerationResult
                {
                    ContainsProfanities = false,
                    Score = 0,
                    OffendingWords = null
                };
            }

            return new ModerationResult
            {
                ContainsProfanities = true,
                Score = response.Classification.Category3.Score,
                Severity = CalculateSeverity(response.Classification.Category3.Score),
                OffendingWords = response.Terms.Select(x => x.Term).ToList()
            };
        }

        private static string CalculateSeverity(float score)
        {
            string severity = "Low";
            if (score >= 0.33 && score < 0.66)
            {
                severity = "Medium";
            }
            else if (score >= 0.66)
            {
                severity = "High";
            }

            return severity;
        }
    }
}
