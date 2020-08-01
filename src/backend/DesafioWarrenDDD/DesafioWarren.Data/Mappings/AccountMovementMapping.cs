using DesafioWarren.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Data.Mappings
{
    public class AccountMovementMapping : IEntityTypeConfiguration<AccountMovement>
    {
        public void Configure(EntityTypeBuilder<AccountMovement> builder)
        {
            builder.ToTable("AccountMovement");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.AccountOperation)
                .IsRequired()
                .HasColumnName("AccountOperation");

            builder.Property(c => c.OperationDate)
                .IsRequired()
                .HasColumnName("OperationDate")
                .HasColumnType("datetime");

            builder.Property(c => c.Amount)
                .IsRequired()
                .HasColumnName("Amount");
            
            builder.Property(c => c.AccountId)
                .IsRequired()
                .HasColumnName("AccountId");

            builder.HasOne(c => c.Account)
                .WithMany(p => p.AccountMovements)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ACCOUNT_ACCOUNTMOVEMENT");
        }
    }
}
