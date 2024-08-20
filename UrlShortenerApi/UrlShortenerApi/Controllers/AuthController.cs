
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginResponse result;
        
        try
        {
            result = await _userService.Login(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok(result);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        RegisterResponse result;
        
        try
        {
            result = await _userService.Register(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok(result);
    }
}