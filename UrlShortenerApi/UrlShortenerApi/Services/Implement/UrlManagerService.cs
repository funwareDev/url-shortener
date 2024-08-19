using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data.Contexts;
using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Services.Implement;

public class UrlManagerService : IUrlManagerService
{
    private readonly IUrlShortenerService _shortenerService;
    private readonly UrlsDbContext _urlsDbContext;

    public UrlManagerService(IUrlShortenerService shortenerService, UrlsDbContext urlsDbContext)
    {
        _shortenerService = shortenerService;
        _urlsDbContext = urlsDbContext;
    }

    public async Task<ShortenUrlResponse> Add(ShortenUrlRequest request)
    {
        var result = await _shortenerService.ShortenUrl(request.LongUrl);

        return new ShortenUrlResponse()
        {
            ShortUrl = result
        };
    }

    public async Task<DeleteUrlResponse> Delete(DeleteUrlRequest request)
    {
        var result = await _urlsDbContext.Urls.FirstOrDefaultAsync(url => url.Identificator.Equals(request.UrlId));

        if (result is null)
        {
            throw new ArgumentException("Url with such identificator does not exist");
        }

        return new DeleteUrlResponse()
        {
            Result = true
        };
    }

    public async Task<Url> Get(string id)
    {
        var result = await _urlsDbContext.Urls.FirstOrDefaultAsync(url => url.Identificator.Equals(id));

        if (result is null)
        {
            throw new ArgumentException("Url with such identificator does not exist");
        }
        
        return result;
    }

    public async Task<GetUrlResponse> Get(GetUrlRequest request)
    {
        var count = await _urlsDbContext.Urls.CountAsync(); 
        var result = await _urlsDbContext.Urls
            .Skip(count - request.Count)
            .Take(request.Count)
            .Reverse()
            .ToListAsync();

        return new GetUrlResponse()
        {
            Urls = result
        };
    }
}