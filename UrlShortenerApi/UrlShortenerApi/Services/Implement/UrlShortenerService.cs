using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data.Contexts;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Services.Implement;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly UrlsDbContext _urlsDbContext;
    private readonly IConfiguration _configuration;

    public UrlShortenerService(UrlsDbContext urlsDbContext, IConfiguration configuration)
    {
        _urlsDbContext = urlsDbContext;
        _configuration = configuration;
    }

    public async Task<string> ShortenUrl(string longUrl)
    {
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(longUrl);
        var hashBytes = MD5.HashData(inputBytes);

        var hash = Convert.ToHexString(hashBytes);
        var countOfCharacters = int.Parse(_configuration["InitialShortLinkCharactersCount"]
                                          ?? throw new Exception("InitialShortLinkCharactersCount not set in configuration"));

        for (; countOfCharacters < hash.Length; countOfCharacters++)
        {
            for (int j = 0; j < hash.Length - countOfCharacters; j++)
            {
                var shortLinkId = hash.Substring(j, countOfCharacters);
                if (await LinkCanBeUsed(shortLinkId))
                {
                    return shortLinkId;
                }
            }
        }

        throw new Exception("Couldn't shorten link");
    }

    private async Task<bool> LinkCanBeUsed(string linkId)
    {
        return await _urlsDbContext.Urls.AnyAsync(link => link.Identificator.Equals(linkId));
    }
}