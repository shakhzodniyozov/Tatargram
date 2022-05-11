using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatargram.Domain.Entities;

namespace Tatargram.Infrastructure.Persistence.Configurations;

public class PostConfig : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.AuthorId);
    }
}