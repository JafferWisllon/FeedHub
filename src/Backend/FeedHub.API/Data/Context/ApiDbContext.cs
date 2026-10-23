using FeedHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedHub.API.Data.Context;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    public DbSet<Feed> Feeds { get; set; }
    public DbSet<FeedItem> FeedItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
    }
}
