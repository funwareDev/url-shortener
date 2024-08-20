using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Data.Requests;

public class UpdateStaticDataRequest
{
    public StaticData StaticData { get; set; }
}