using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatargram.Models;

namespace Tatargram.Data.Configurations
{
    public class UserFollowerConfig : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.ToTable("UserFollowers");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User).WithMany(x => x.Followers).HasForeignKey(x => x.UserId);
        }
    }
}