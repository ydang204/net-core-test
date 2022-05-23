using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreTest.Entities;

namespace NetCoreTest.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(68);
            builder.Property(e => e.FirstName).HasMaxLength(255);
            builder.Property(e => e.LastName).HasMaxLength(255);
            builder.Property(e => e.Email).HasMaxLength(255);
            builder.Property(e => e.Phone).HasMaxLength(255);
            builder.Property(e => e.Address).HasMaxLength(255);
            builder.Property(e => e.VerifyEmailToken).HasMaxLength(500);
            builder.Property(e => e.PasswordHash).HasMaxLength(500);
        }
    }
}