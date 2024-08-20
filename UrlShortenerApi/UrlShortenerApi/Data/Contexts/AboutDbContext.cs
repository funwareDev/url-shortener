using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Data.Contexts;

public class AboutDbContext : DbContext
{
    public AboutDbContext(DbContextOptions<AboutDbContext> options) : base(options)
    {
        
    }

    public DbSet<StaticData> Values { get; set; }
}