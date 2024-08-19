using Microsoft.AspNetCore.Identity.Data;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Services.Implement;

public class UserService : IUserService
{
    public Task<LoginResponse> Login(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}