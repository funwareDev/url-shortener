using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;

namespace UrlShortenerApi.Services.Abstract;

public interface IAboutService
{
    Task<StaticData> Get();
    Task<UpdateStaticDataResponse> Update(UpdateStaticDataRequest request);
}