namespace UrlShortenerApi.Data.Dtos;

public class UrlDto
{
    public string Identificator { get; set; }
    public string LongUrl { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}