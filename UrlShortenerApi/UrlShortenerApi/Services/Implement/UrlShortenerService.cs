using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data.Contexts;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Services.Implement;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly ShortenUrlsContext _urlsDbContext;
    private readonly IConfiguration _configuration;

    public UrlShortenerService(ShortenUrlsContext urlsDbContext, IConfiguration configuration)
    {
        _urlsDbContext = urlsDbContext;
        _configuration = configuration;
    }

    public async Task<string> ShortenUrl(string longUrl)
    {
        if (await UrlAlreadyExists(longUrl))
        {
            throw new Exception("Url was already shortened");
        }
        
        var guid = Guid.NewGuid().ToString().Replace("-", "");
        var countOfCharacters = int.Parse(_configuration["InitialShortLinkCharactersCount"]
                                          ?? throw new Exception("InitialShortLinkCharactersCount not set in configuration"));

        for (; countOfCharacters < guid.Length; countOfCharacters++)
        {
            for (int j = 0; j < guid.Length - countOfCharacters; j++)
            {
                var shortLinkId = guid.Substring(j, countOfCharacters);
                if (!await LinkIdAlreadyExists(shortLinkId))
                {
                    return shortLinkId;
                }
            }
        }

        throw new Exception("Couldn't shorten link");
    }

    private async Task<bool> LinkIdAlreadyExists(string linkId)
    {
        return await _urlsDbContext.Urls.AnyAsync(link => link.Identificator.Equals(linkId));
    }
    
    private async Task<bool> UrlAlreadyExists(string longUrl)
    {
        return await _urlsDbContext.Urls.AnyAsync(link => link.LongUrl.Equals(longUrl));
    }
}