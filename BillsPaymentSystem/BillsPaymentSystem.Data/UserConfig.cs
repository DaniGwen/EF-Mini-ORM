using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Data
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsRequired();

            builder.HasMany(u => u.PaymentMethods)
                .WithOne(p => p.User);
        }
    }
}

