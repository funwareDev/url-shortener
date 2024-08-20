using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;

namespace UrlShortenerApi.Services.Abstract;

public interface IAboutService
{
    Task Create(StaticData staticData);
    Task<GetStaticDataResponse> Get();
    Task<UpdateStaticDataResponse> Update(UpdateStaticDataRequest request);
    Task Delete(string staticDataName);
}