namespace UrlShortenerApi.Data.Responses;

public class ShortenUrlResponse
{
    public string ShortUrl { get; set; }
    public List<string> Errors { get; set; } = new();
}