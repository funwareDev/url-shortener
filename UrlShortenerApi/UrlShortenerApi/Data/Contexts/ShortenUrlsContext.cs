using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Data.Contexts;

public class ShortenUrlsContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Url> Urls { get; set; }
    public DbSet<StaticData> StaticDataValues { get; set; }
    
    public ShortenUrlsContext(DbContextOptions<ShortenUrlsContext> options)
        : base(options)
    {
    }
}