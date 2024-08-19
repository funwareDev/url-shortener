using System.ComponentModel.DataAnnotations;

namespace UrlShortenerApi.Data.Requests;

public class GetUrlRequest
{
    [Range(1, 25)]
    public int Count { get; set; }
}