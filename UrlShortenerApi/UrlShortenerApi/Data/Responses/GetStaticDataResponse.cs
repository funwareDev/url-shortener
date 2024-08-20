using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Data.Responses;

public class GetStaticDataResponse
{
    public IEnumerable<StaticData> StaticDatas { get; set; }
}