namespace Domain.Entities
{
    public class UrlMapping
    {
        public string _id { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string ShortenedUrl { get; set; } = string.Empty;
        public string RouteId { get; set; } = string.Empty;

    }
}
