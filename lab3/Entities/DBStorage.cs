using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using EFCore.NamingConventions;

namespace NTR.Entities {
    public class StorageContext : DbContext {
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<UserMonth> UserMonths { get; set; }

        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Debugger.DebugLog(new List<string>{"balbinka"});
            optionsBuilder
                .UseNpgsql("Server=localhost;Port=5432;Database=bmoroz;User Id=bmoroz;Password=hiperbalbinka")
                .UseSnakeCaseNamingConvention();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    
            modelBuilder.Entity<UserActivity>(entity => {
                entity.HasKey(e => e.Date);
                entity.HasKey(e => e.ProjectId);
                entity.Property(e => e.Subactivity);
                entity.Property(e => e.Time).IsRequired();
                entity.Property(e => e.ApprovedTime);
                entity.Property(e => e.ApprovalDate);
                entity.Property(e => e.Description);
                entity.HasOne(e => e.Project).WithMany(f => f.UserActivities);
                entity.HasOne(e => e.UserMonth).WithMany(f => f.UserActivities);
            });

            modelBuilder.Entity<Project>(entity => {
                entity.HasKey(e => e.ProjectId);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Manager).IsRequired();
                entity.Property(e => e.Budget).IsRequired();
                entity.Property(e => e.Active).IsRequired();
                entity.Property(e => e.Subactivities);
            });

            modelBuilder.Entity<UserMonth>(entity => {
                entity.HasKey(e => e.Date);
                entity.HasKey(e => e.User);
                entity.Property(e => e.Frozen).IsRequired();
            });
        }
    }
}
