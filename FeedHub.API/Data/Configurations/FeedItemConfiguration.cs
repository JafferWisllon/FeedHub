using FeedHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeedHub.API.Data.Configurations;

public class FeedItemConfiguration : IEntityTypeConfiguration<FeedItem>
{
    public void Configure(EntityTypeBuilder<FeedItem> builder)
    {
        builder.ToTable("FeedItens");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasColumnType("NVARCHAR(300)").IsRequired();
        builder.Property(x => x.Link).HasColumnType("NVARCHAR(1000)").IsRequired();
        builder.Property(X => X.PublishAt).IsRequired(false);
    }
}
