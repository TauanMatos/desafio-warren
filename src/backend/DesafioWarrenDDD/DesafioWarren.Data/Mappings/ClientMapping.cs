using DesafioWarren.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Address)
                .IsRequired()
                .HasColumnName("Address");

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnName("CPF");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnName("Name");

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasColumnName("Phone");

            builder.Property(c => c.Gender)
                .IsRequired()
                .HasColumnName("Gender");

 
            builder.HasOne(c => c.Account)
                .WithOne(c => c.Client)
                .HasForeignKey<Account>(c => c.ClientId);
        }
    }
}
