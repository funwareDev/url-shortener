using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Controllers;

[ApiController]
[Route("{id}")]
public class UrlRedirectController : ControllerBase
{
    private readonly IUrlManagerService _urlManagerService;

    public UrlRedirectController(IUrlManagerService urlManagerService)
    {
        _urlManagerService = urlManagerService;
    }
    
    [HttpGet]
    public async Task<IActionResult> RedirectToSite(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("ID cannot be null or empty.");
        }

        var result = await _urlManagerService.Get(id);
        return Redirect(result.LongUrl);
    }
}