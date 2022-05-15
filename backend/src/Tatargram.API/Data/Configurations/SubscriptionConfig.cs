// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Tatargram.Models;

// namespace Tatargram.Data.Configurations;

// public class SubscriptionConfig : IEntityTypeConfiguration<Subscription>
// {
//     public void Configure(EntityTypeBuilder<Subscription> builder)
//     {
//         builder.ToTable("Subscriptions");

//         builder.HasKey(x => x.Id);

//         // builder.HasOne(x => x.User)
//         //         .WithMany(x => x.Followers)
//         //         .HasForeignKey(x => x.UserId);

//         // builder.HasOne(x => x.Subscriber)
//         //         .WithMany(x => x.Followings)
//         //         .HasForeignKey(x => x.SubscriberId);

//         builder.HasOne(x => x.User)
//                 .WithMany(x => x.Followers)
//                 .HasForeignKey(x => x.UserId);

//         builder.HasOne(x => x.User)
//                 .WithMany(x => x.Followings)
//                 .HasForeignKey(x => x.UserId);
//     }
// }
