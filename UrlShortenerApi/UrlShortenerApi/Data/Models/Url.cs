using System.ComponentModel.DataAnnotations;

namespace UrlShortenerApi.Data.Models;

public class Url
{
    [Key]
    public string Identificator { get; set; }
    public string LongUrl { get; set; }
    public User CreatedBy { get; set; }
}