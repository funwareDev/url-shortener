using System.IdentityModel.Tokens.Jwt;
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

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await GetUserByUserName(request.Username);

        if (user == null)
        {
            throw new Exception("User with such name does not exist.");
        }

        var salt = user.PasswordSalt;
        var passwordHash = StringToSha256Hash(request.Password + salt);

        if (!user.PasswordHash.Equals(passwordHash))
        {
            throw new Exception("Password was not correct.");
        }

        return new LoginResponse()
        {
            Token = GenerateAuthToken(user)
        };
    }

    private string GenerateAuthToken(User user)
    {
        var claims = new List<Claim> { new(ClaimTypes.GivenName, user.Username), new(ClaimTypes.Role, user.Role.ToString()) };

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

        var salt = Guid.NewGuid().ToString().Replace("-", "");
        var passwordHash = StringToSha256Hash(request.Password + salt);

        var makeUserAdmin = request is { Role: not null, Secret: not null } && request.Secret.Equals(_configuration["AdminSecret"]);

        await _usersDbContext.Users.AddAsync(new User()
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            PasswordSalt = salt,
            Role = makeUserAdmin ? Role.Admin : Role.User
        });
        await _usersDbContext.SaveChangesAsync();

        return new RegisterResponse()
        {
            Succeed = true
        };
    }

    public async Task<bool> UserExists(string requestUsername)
    {
        return await _usersDbContext.Users.AnyAsync(user => user.Username.Equals(requestUsername));
    }

    public async Task<User> GetUserByUserName(string requestUsername)
    {
        return await _usersDbContext.Users.FirstAsync(user => user.Username.Equals(requestUsername));
    }

    private string StringToSha256Hash(string text)
    {
        if (IsNullOrEmpty(text))
        {
            return Empty;
        }

        var sha = SHA256.Create();
        var textData = Encoding.UTF8.GetBytes(text);
        var hash = sha.ComputeHash(textData);

        return BitConverter.ToString(hash).Replace("-", Empty);
    }
} 