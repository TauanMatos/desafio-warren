using DesafioWarren.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Data.Mappings
{
    public class DailyIncomeMapping : IEntityTypeConfiguration<DailyIncome>
    {
        public void Configure(EntityTypeBuilder<DailyIncome> builder)
        {
            builder.ToTable("DailyIncome");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.BaseAmount)
                .IsRequired()
                .HasColumnName("BaseAmount");

            builder.Property(c => c.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("datetime");

            builder.Property(c => c.DailyYeld)
                .IsRequired()
                .HasColumnName("DailyYeld");

            builder.Property(c => c.AccountId)
                .IsRequired()
                .HasColumnName("AccountId");

            builder.HasOne(c => c.Account)
                .WithMany(p => p.DailyIncome)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ACCOUNT_DAILYINCOME");
        }
    }
}
