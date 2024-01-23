using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Models;

namespace Server.EFConfigs;

public class ProductEFConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id).HasName("product_pkey");

        builder.HasOne(p => p.Category).WithMany(c => c.Products).HasConstraintName("product_category_id_fkey");
    }
}
