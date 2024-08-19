using Microsoft.AspNetCore.Identity.Data;
using UrlShortenerApi.Data.Responses;

namespace UrlShortenerApi.Services.Abstract;

public interface IUserService
{
    Task<LoginResponse> Login(LoginRequest request);
}