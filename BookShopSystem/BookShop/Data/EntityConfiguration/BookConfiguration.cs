using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(true);

            builder.Property(p => p.Description)
                .HasMaxLength(1000)
                .IsUnicode()
                .IsRequired(true);
        }
    }
}
