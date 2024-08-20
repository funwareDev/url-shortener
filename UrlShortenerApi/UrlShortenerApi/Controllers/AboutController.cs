﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.Data.Requests;
using UrlShortenerApi.Services.Abstract;

namespace UrlShortenerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AboutController : ControllerBase
{
    private readonly IAboutService _aboutService;

    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateStaticDataRequest request)
    {
        var result = await _aboutService.Update(request);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _aboutService.Get();
        return Ok(result);
    }
}