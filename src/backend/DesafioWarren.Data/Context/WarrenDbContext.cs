using DesafioWarren.Data.Identity;
using DesafioWarren.Data.Mappings;
using DesafioWarren.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioWarren.Data.Context
{
    public class WarrenDbContext : IdentityDbContext<ApplicationUser>
    {
        public WarrenDbContext(DbContextOptions<WarrenDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountMovement> AccountMovements { get; set; }
        public DbSet<DailyIncome> DailyIncomes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>(new ClientMapping().Configure);
            modelBuilder.Entity<Account>(new AccountMapping().Configure);
            modelBuilder.Entity<AccountMovement>(new AccountMovementMapping().Configure);
            modelBuilder.Entity<DailyIncome>(new DailyIncomeMapping().Configure);
        }
    }
}
