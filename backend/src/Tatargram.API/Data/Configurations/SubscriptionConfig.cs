using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatargram.Models;

namespace Tatargram.Data.Configurations;

public class SubscriptionConfig : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");

        builder.HasOne(x => x.User)
                .WithMany(x => x.Subscriptions);
    }
}
