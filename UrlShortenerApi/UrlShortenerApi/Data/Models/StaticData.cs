using System.ComponentModel.DataAnnotations;

namespace UrlShortenerApi.Data.Models;

public class StaticData
{
    [Key]
    public string Name { get; set; } = null!;
    public string Content { get; set; } = null!;
}