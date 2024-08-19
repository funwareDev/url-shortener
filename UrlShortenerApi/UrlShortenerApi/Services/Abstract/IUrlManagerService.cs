using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;

namespace UrlShortenerApi.Services.Abstract;

public interface IUrlManagerService
{
    Task<ShortenUrlResponse> Add(ShortenUrlRequest request);
    Task<DeleteUrlResponse> Delete(DeleteUrlRequest request);
    Task<Url> Get(string id);
    Task<GetUrlResponse> Get(GetUrlRequest request);
}