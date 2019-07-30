using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogApi.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions options)
            : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CatalogBrand>(ConfigureCatalogBrand);
            builder.Entity<CatalogType>(ConfigureCatalogType);
            builder.Entity<CatalogItem>(ConfigureCatalogItem);
        }

        private void ConfigureCatalogItem(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");

            builder.Property(a => a.Id)
                .ForSqlServerUseSequenceHiLo("Catalog_HiLo")
                .IsRequired(true);

            builder.Property(a => a.Name)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(a => a.Price)
                .IsRequired(true);

            builder.Property(a => a.ImageUrl)
                .IsRequired(false);

            builder.HasOne(a => a.CatalogBrand)
                .WithMany()
                .HasForeignKey(a => a.CatalogBrandId);

            builder.HasOne(a => a.CatalogType)
                .WithMany()
                .HasForeignKey(a => a.CatalogTypeId);
        }

        private void ConfigureCatalogType(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");
            builder.Property(a => a.Id)
                .ForSqlServerUseSequenceHiLo("CatalogType_HiLo")
                .IsRequired();

            builder.Property(a => a.Type)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureCatalogBrand(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");
            builder.Property(a => a.Id)
                .ForSqlServerUseSequenceHiLo("CatalogBrand_HiLo")
                .IsRequired();

            builder.Property(a => a.Brand)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
