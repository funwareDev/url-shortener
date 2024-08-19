using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Data.Responses;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Controllers;

[Controller]
public class UrlController: ControllerBase
{
    private IUrlShortenerService _shortenerService;

    public UrlController(IUrlShortenerService shortenerService)
    {
        _shortenerService = shortenerService;
    }

    [HttpPost]
    public async Task<IActionResult> ShortenUrl(ShortenUrlRequest request)
    {
        ShortenUrlResponse result;
        try
        {
            result = await _shortenerService.ShortenUrl(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return Ok(result);
    }
}