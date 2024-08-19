using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Data.Contexts;

public class UrlsDbContext : DbContext
{
    public DbSet<Url> Urls { get; set; }
    
    public UrlsDbContext(DbContextOptions<UrlsDbContext> options)
        : base(options)
    {
    }
}