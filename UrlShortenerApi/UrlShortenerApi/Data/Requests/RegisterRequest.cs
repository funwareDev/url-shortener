using System.ComponentModel.DataAnnotations;
using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Data.Requests;

public class RegisterRequest
{
    [Length(5, 15)]
    public string Username { get; set; }
    
    [Length(10, 32)]
    //Minimum eight characters, at least one letter and one number
    public string Password { get; set; }
    public Role? Role { get; set; }
    public string? Secret { get; set; }
}