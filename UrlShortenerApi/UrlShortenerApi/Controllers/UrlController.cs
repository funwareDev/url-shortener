using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Controllers;

[Controller]
[Route("[controller]")]
[ApiVersion(1.0)]
public class UrlController: ControllerBase
{
    private IUrlManagerService _urlManagerService;

    public UrlController(IUrlManagerService urlManagerService)
    {
        _urlManagerService = urlManagerService;
    }

    [HttpPost]
    public async Task<IActionResult> ShortenUrl(ShortenUrlRequest request)
    {
        ShortenUrlResponse result;
        try
        {
            result = await _urlManagerService.Add(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return Ok(result);
    }
    
    [HttpPost("")]
    public async Task<IActionResult> Get(ShortenUrlRequest request)
    {
        ShortenUrlResponse result;
        try
        {
            result = await _urlManagerService.Add(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return Ok(result);
    }
}