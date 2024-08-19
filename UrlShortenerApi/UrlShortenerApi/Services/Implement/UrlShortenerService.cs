using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Services.Implement;

public class UrlShortenerService : IUrlShortenerService
{
    public Task<ShortenUrlResponse> ShortenUrl(ShortenUrlRequest request)
    {
        throw new NotImplementedException();
    }
}