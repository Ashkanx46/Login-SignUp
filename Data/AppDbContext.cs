using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using login.Models;
using Microsoft.EntityFrameworkCore;

namespace login.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // سازنده بدون پارامتر برای design-time
        public AppDbContext()
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=users.db");
            }
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // صراحتاً کلید اصلی رو معرفی می‌کنیم

            base.OnModelCreating(modelBuilder);
        }
    }
}