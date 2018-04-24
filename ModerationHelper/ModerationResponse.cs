namespace Moderation
{
    public class ModerationResponse
    {
        public string OriginalText { get; set; }
        public string NormalizedText { get; set; }
        public object Misrepresentation { get; set; }
        public PII PII { get; set; }
        public Classification Classification { get; set; }
        public string Language { get; set; }
        public ProfanityTerm[] Terms { get; set; }
        public Status Status { get; set; }
        public string TrackingId { get; set; }
    }

    public class PII
    {
        public Email[] Email { get; set; }
        public IPA[] IPA { get; set; }
        public Phone[] Phone { get; set; }
        public Address[] Address { get; set; }
        public SSN[] SSN { get; set; }
    }

    public class Email
    {
        public string Detected { get; set; }
        public string SubType { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
    }

    public class IPA
    {
        public string SubType { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
    }

    public class Phone
    {
        public string CountryCode { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
    }

    public class Address
    {
        public string Text { get; set; }
        public int Index { get; set; }
    }

    public class SSN
    {
        public string Text { get; set; }
        public int Index { get; set; }
    }

    public class Classification
    {
        public bool ReviewRecommended { get; set; }
        public Category1 Category1 { get; set; }
        public Category2 Category2 { get; set; }
        public Category3 Category3 { get; set; }
    }

    public class Category1
    {
        public float Score { get; set; }
    }

    public class Category2
    {
        public float Score { get; set; }
    }

    public class Category3
    {
        public float Score { get; set; }
    }

    public class Status
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public object Exception { get; set; }
    }

    public class ProfanityTerm
    {
        public int Index { get; set; }
        public int OriginalIndex { get; set; }
        public int ListId { get; set; }
        public string Term { get; set; }
    }
}
