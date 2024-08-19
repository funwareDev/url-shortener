using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Data.Responses;

public class GetUrlResponse
{
    public IEnumerable<Url> Urls { get; set; }
}