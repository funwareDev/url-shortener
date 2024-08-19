using System.ComponentModel.DataAnnotations;

namespace UrlShortenerApi.Data.Requests;

public class RegisterRequest
{
    [Length(5, 15)]
    public string Username { get; set; }
    [MinLength(10)]
    public string Password { get; set; }
}