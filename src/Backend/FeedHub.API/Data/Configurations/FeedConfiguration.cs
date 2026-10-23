using FeedHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeedHub.API.Data.Configurations;

public class FeedConfiguration : IEntityTypeConfiguration<Feed>
{
    public void Configure(EntityTypeBuilder<Feed> builder)
    {
        builder.ToTable("Feeds");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Url).HasColumnType("VARCHAR(2048)").IsRequired();
        builder.Property(x => x.Name).HasColumnType("VARCHAR(100)").IsRequired(false);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();

        builder.HasMany(x => x.FeedItems)
            .WithOne(x => x.Feed)
            .HasForeignKey(x => x.FeedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
