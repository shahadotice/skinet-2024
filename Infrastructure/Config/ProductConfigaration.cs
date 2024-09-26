using System;
using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProductConfigaration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x=>x.Price).HasColumnType("decimal(18,2)");
         builder.Property(x=>x.Name).IsRequired();
    }
}
