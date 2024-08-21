using UrlShortenerApi.Data.Dtos;
using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Data.Responses;

public class GetUrlResponse
{
    public IEnumerable<UrlDto> Urls { get; set; }
}