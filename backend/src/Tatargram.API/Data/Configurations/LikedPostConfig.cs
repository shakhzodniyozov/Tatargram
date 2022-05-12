using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatargram.Models;

namespace Tatargram.Data.Configurations;

public class LikedPostConfig : IEntityTypeConfiguration<LikedPost>
{
    public void Configure(EntityTypeBuilder<LikedPost> builder)
    {
        builder.ToTable("LikedPosts");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Post)
            .WithMany(x => x.LikedUsers)
            .HasForeignKey(x => x.PostId);

        builder.HasOne(x => x.User)
                .WithMany(x => x.LikedPosts)
                .HasForeignKey(x => x.UserId);
    }
}