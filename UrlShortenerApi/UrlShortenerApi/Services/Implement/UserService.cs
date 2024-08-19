using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UrlShortenerApi.Data.Contexts;
using UrlShortenerApi.Data.Models;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;
using static System.String;

namespace UrlShortenerApi.Services.Implement;

public class UserService : IUserService
{
    private readonly UsersDbContext _usersDbContext;
    private readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration, UsersDbContext usersDbContext)
    {
        _configuration = configuration;
        _usersDbContext = usersDbContext;
    }

    private const int LengthOfSalt = 10;

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _usersDbContext.Users
            .FirstOrDefaultAsync(user => user.Username.Equals(request.Username));

        if (user == null)
        {
            throw new Exception("User with such name does not exist.");
        }

        var salt = StringToSha256Hash(request.Password).Substring(0, LengthOfSalt);
        var passwordHash = StringToSha256Hash(request.Password + salt);

        if (!user.PasswordHash.Equals(passwordHash))
        {
            throw new Exception("Password was not correct.");
        }

        return new LoginResponse()
        {
            Token = GenerateAuthToken(request.Username)
        };
    }

    private string GenerateAuthToken(string username)
    {
        var claims = new List<Claim> { new("username", username) };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]
            ?? throw new Exception("JWT Secret was not set.")));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(30),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        if (await UserExists(request.Username))
        {
            throw new Exception("User with such name exists.");
        }

        var salt = StringToSha256Hash(request.Password).Substring(0, LengthOfSalt);
        var passwordHash = StringToSha256Hash(request.Password + salt);

        var user = await _usersDbContext.Users.AddAsync(new User()
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            PasswordSalt = salt
        });

        return new RegisterResponse()
        {
            Succeed = true
        };
    }

    private async Task<bool> UserExists(string requestUsername)
    {
        return await _usersDbContext.Users.AnyAsync(user => user.Username.Equals(requestUsername));
    }

    private string StringToSha256Hash(string text)
    {
        if (IsNullOrEmpty(text))
            return Empty;

        var sha = HMACSHA256.Create();


        var textData = System.Text.Encoding.UTF8.GetBytes(text);
        var hash = sha.ComputeHash(textData);

        return BitConverter.ToString(hash).Replace("-", Empty);
    }
}