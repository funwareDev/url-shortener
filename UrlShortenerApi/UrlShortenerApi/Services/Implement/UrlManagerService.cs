using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Services.Implement;

public class UrlManagerService : IUrlManagerService
{
    private readonly IUrlShortenerService _shortenerService;

    public UrlManagerService(IUrlShortenerService shortenerService)
    {
        _shortenerService = shortenerService;
    }

    public async Task<ShortenUrlResponse> Add(ShortenUrlRequest request)
    {
        var result = await _shortenerService.ShortenUrl(request.LongUrl);
        
        return new ShortenUrlResponse()
        {
            ShortUrl = result
        };
    }

    public Task<DeleteUrlResponse> Delete(DeleteUrlRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GetUrlResponse> Get(GetUrlRequest request)
    {
        throw new NotImplementedException();
    }
}