using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatargram.Models;

namespace Tatargram.Data.Configurations
{
    public class UserFollowingConfig : IEntityTypeConfiguration<UserFollowing>
    {
        public void Configure(EntityTypeBuilder<UserFollowing> builder)
        {
            builder.ToTable("UserFollowings");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User).WithMany(x => x.Followings).HasForeignKey(x => x.UserId);
        }
    }
}