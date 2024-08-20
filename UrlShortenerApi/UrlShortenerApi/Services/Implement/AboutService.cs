using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data.Contexts;
using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Services.Implement;

public class AboutService : IAboutService
{
    private readonly ShortenUrlsContext _urlsContext;

    public AboutService(ShortenUrlsContext urlsContext)
    {
        _urlsContext = urlsContext;
    }

    public async Task<GetStaticDataResponse> Get()
    {
        return new GetStaticDataResponse()
        {
            StaticDatas = await _urlsContext.StaticDataValues.ToListAsync()
        };
    }

    public async Task<UpdateStaticDataResponse> Update(UpdateStaticDataRequest request)
    {
        var staticData = await _urlsContext.StaticDataValues.FirstOrDefaultAsync(data => data.Name.Equals(request.StaticData.Name));
        
        if (staticData == null)
        {
            throw new Exception("Such data does not exist");
        }

        staticData.Content = request.StaticData.Content;
        _urlsContext.StaticDataValues.Update(staticData);

        await _urlsContext.SaveChangesAsync();

        return new UpdateStaticDataResponse()
        {
            Succeed = true
        };
    }

    public async Task Create(StaticData staticData)
    {
        await _urlsContext.AddAsync(staticData);
        await _urlsContext.SaveChangesAsync();
    }

    public async Task Delete(string staticDataName)
    {
        var staticData =
            await _urlsContext.StaticDataValues.FirstOrDefaultAsync(data => data.Name.Equals(staticDataName));
        _urlsContext.StaticDataValues.Remove(staticData ?? throw new Exception("Such data does not exist"));
        await _urlsContext.SaveChangesAsync();
    }
}