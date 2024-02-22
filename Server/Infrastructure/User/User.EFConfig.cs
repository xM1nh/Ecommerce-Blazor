using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Models;

namespace Server.Infrastructure.EFConfigs;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id).HasName("user_pkey");

        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.Password).IsRequired();
    }
}
