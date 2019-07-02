using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.EntityConfiguration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(f => f.FirstName)
                .HasMaxLength(50)
                 .IsUnicode();

            builder.Property(l => l.LastName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(true);
        }
    }
}
