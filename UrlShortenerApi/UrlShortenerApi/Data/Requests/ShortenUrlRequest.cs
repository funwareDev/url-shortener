namespace UrlShortenerApi.Data.Requests;

public class ShortenUrlRequest
{
    public required string LongUrl { get; set; } = null!;
}