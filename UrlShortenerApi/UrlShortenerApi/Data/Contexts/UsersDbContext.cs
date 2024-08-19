using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data.Models;

namespace UrlShortenerApi.Data.Contexts;

public class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
}