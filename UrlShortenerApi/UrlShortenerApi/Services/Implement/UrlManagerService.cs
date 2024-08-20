using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data.Contexts;
using UrlShortenerApi.Data.Dtos;
using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Extensions;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Services.Implement;

public class UrlManagerService : IUrlManagerService
{
    private readonly IUrlShortenerService _shortenerService;
    private readonly ShortenUrlsContext _urlsDbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public UrlManagerService(IUrlShortenerService shortenerService,
        ShortenUrlsContext urlsDbContext,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService)
    {
        _shortenerService = shortenerService;
        _urlsDbContext = urlsDbContext;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    public async Task<ShortenUrlResponse> Create(ShortenUrlRequest request)
    {
        var shortId = await _shortenerService.ShortenUrl(request.LongUrl);

        var claims = GetClaims();
        var username = claims.First(c => c.Type == ClaimTypes.GivenName).Value;

        var user = await _userService.GetUserByUserName(username);
        
        await _urlsDbContext.Urls.AddAsync(new Url()
        {
            Identificator = shortId,
            LongUrl = request.LongUrl,
            CreatedBy = user,
            CreatedDate = DateTime.Now.ToUniversalTime()
        });
        await _urlsDbContext.SaveChangesAsync();

        return new ShortenUrlResponse()
        {
            ShortUrl = shortId
        };
    }

    public async Task<DeleteUrlResponse> Delete(DeleteUrlRequest request)
    {
        var url = await _urlsDbContext.Urls
            .Include(url => url.CreatedBy)
            .FirstOrDefaultAsync(url => url.Identificator.Equals(request.UrlId));

        if (url is null)
        {
            throw new ArgumentException("Url with such identificator does not exist");
        }

        var claims = GetClaims().ToList();
        var roleClaim = claims.First(c => c.Type == ClaimTypes.Role).Value;
        var role = roleClaim.ParseEnum<Role>();

        var usernameClaim = claims.First(c => c.Type == ClaimTypes.GivenName).Value;
        var user = await _userService.GetUserByUserName(usernameClaim);

        if (!url.CreatedBy.Id.Equals(user.Id) && role != Role.Admin)
        {
            throw new ArgumentException("You have no rights to delete urls that weren't created by you");
        }

        _urlsDbContext.Urls.Remove(url);
        await _urlsDbContext.SaveChangesAsync();

        return new DeleteUrlResponse()
        {
            Succeed = true
        };
    }

    private IEnumerable<Claim> GetClaims()
    {
        return _httpContextAccessor.HttpContext!.User.Claims;
    }

    public async Task<UrlDto> Get(string id)
    {
        var result = await _urlsDbContext.Urls.Include(url => url.CreatedBy)
            .FirstOrDefaultAsync(url => url.Identificator.Equals(id));

        if (result is null)
        {
            throw new ArgumentException("Url with such identificator does not exist");
        }

        return new UrlDto()
        {
            Identificator = result.Identificator,
            CreatedBy = result.CreatedBy.Username,
            CreatedDate = result.CreatedDate,
            LongUrl = result.LongUrl
        };
    }

    public async Task<GetUrlResponse> Get(GetUrlRequest request)
    {
        var count = await _urlsDbContext.Urls.CountAsync();
        var countToSkip = count < request.Count ? 0 : count - request.Count;
        
        var result = await _urlsDbContext.Urls
            .Skip(countToSkip)
            .Take(request.Count)
            .ToListAsync();

        result.Reverse();

        return new GetUrlResponse()
        {
            Urls = result
        };
    }
}