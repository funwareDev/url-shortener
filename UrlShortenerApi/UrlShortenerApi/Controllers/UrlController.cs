using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UrlController: ControllerBase
{
    private IUrlManagerService _urlManagerService;

    public UrlController(IUrlManagerService urlManagerService)
    {
        _urlManagerService = urlManagerService;
    }

    [HttpPost("short-url")]
    [Authorize]
    public async Task<IActionResult> ShortenUrl(ShortenUrlRequest request)
    {
        ShortenUrlResponse result;
        try
        {
            result = await _urlManagerService.Create(request);
        }
        catch (Exception e)
        {
            throw;
        }
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Get(GetUrlRequest request)
    {
        GetUrlResponse result;
        try
        {
            result = await _urlManagerService.Get(request);
        }
        catch (Exception e)
        {
            throw;
        }
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteUrlRequest request)
    {
        DeleteUrlResponse result;
        try
        {
            result = await _urlManagerService.Delete(request);
        }
        catch (Exception e)
        {
            throw;
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _urlManagerService.Get(id);

        return Ok(result);
    }
}