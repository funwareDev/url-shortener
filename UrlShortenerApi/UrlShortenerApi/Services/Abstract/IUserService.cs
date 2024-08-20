using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;

namespace UrlShortenerApi.Services.Abstract;

public interface IUserService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<RegisterResponse> Register(RegisterRequest request);
    Task<bool> UserExists(string requestUsername);
    Task<User?> GetUserByUserName(string requestUsername);
}