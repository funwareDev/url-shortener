using UrlShortenerApi.Data.Dtos;
using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;

namespace UrlShortenerApi.Services.Abstract;

public interface IUrlManagerService
{
    Task<ShortenUrlResponse> Create(ShortenUrlRequest request);
    Task<DeleteUrlResponse> Delete(DeleteUrlRequest request);
    Task<UrlDto> Get(string id);
    Task<GetUrlResponse> Get(GetUrlRequest request);
}