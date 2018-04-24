using System.Collections.Generic;

namespace Moderation
{
    public class ModerationResult
    {
        public bool ContainsProfanities { get; set; }
        public string Severity { get; set; }
        public float Score { get; set; }
        public List<string> OffendingWords { get; set; }

        public override string ToString()
        {
            if(!ContainsProfanities)
            {
                return "The content has been found to contain no profanities";
            }
            return $"The message has been found to contain profanities with {Severity} severity and Score: {Score}. The offending words where : {string.Join(",", OffendingWords.ToArray())}";
        }
    }
}
