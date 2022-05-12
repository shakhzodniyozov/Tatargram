using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatargram.Models;

namespace Tatargram.Data.Configurations;

public class ImageConfig : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");

        builder.Ignore(x => x.RelativePaths);

        builder.HasOne(x => x.User)
                .WithMany(x => x.Photos)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

        builder.HasOne(x => x.Post)
                .WithMany(x => x.Photos)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
    }
}