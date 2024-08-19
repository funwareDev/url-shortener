using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;

namespace UrlShortenerApi.Services.Abstract;

public interface IUrlShortenerService
{
    Task<string> ShortenUrl(string longUrl);
}