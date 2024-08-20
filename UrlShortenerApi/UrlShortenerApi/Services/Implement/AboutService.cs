using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Services.Implement;

public class AboutService : IAboutService
{
    public Task<StaticData> Get()
    {
        throw new NotImplementedException();
    }

    public Task<UpdateStaticDataResponse> Update(UpdateStaticDataRequest request)
    {
        throw new NotImplementedException();
    }
}