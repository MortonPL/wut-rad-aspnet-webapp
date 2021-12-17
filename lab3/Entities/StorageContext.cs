using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using EFCore.NamingConventions;

namespace NTR.Entities {
    public class StorageContext : DbContext {
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserMonth> UserMonths { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<ApprovedActivity> ApprovedActivities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subactivity> Subactivities { get; set; }

        public StorageContext(){}

        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = "server=localhost;database=ntr;user=bmoroz;password=hiperbalbinka";
            optionsBuilder
                .UseMySql(conn, ServerVersion.AutoDetect(conn));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    
            modelBuilder.Entity<UserActivity>(entity => {
                entity.HasKey(ua => new { ua.Pid, ua.Month, ua.UserName, ua.ProjectId });
                entity.Property(ua =>ua.Pid).ValueGeneratedOnAdd();
                entity.Property(ua => ua.Time).IsRequired();
                entity.Property(ua => ua.Date).IsRequired();
                entity.Property(ua => ua.SubactivityId).IsRequired();
                entity.Property(ua => ua.Description);
                entity.HasOne(ua => ua.Subactivity).WithMany(s => s.UserActivities).HasForeignKey(ua => new { ua.ProjectId, ua.SubactivityId });
                entity.HasOne(ua => ua.Project).WithMany(p => p.UserActivities).HasForeignKey(ua => new { ua.ProjectId});
                entity.HasOne(ua => ua.UserMonth).WithMany(um => um.UserActivities).HasForeignKey(ua => new { ua.Month, ua.UserName });
            });

            modelBuilder.Entity<Project>(entity => {
                entity.HasKey(p => p.ProjectId);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Budget).IsRequired();
                entity.Property(p => p.Active).IsRequired();
                entity.Property(p=> p.ManagerName).IsRequired();
                entity.HasOne(p => p.Manager).WithMany(u => u.Projects).HasForeignKey(p => p.ManagerName);
            });

            modelBuilder.Entity<UserMonth>(entity => {
                entity.HasKey(um => new { um.Month, um.UserName });
                entity.Property(um => um.Frozen).IsRequired();
                entity.HasOne(um => um.User).WithMany(u => u.UserMonths).HasForeignKey(um => um.UserName);
            });

            modelBuilder.Entity<ApprovedActivity>(entity => {
                entity.HasKey(aa => new { aa.Month, aa.UserName, aa.ProjectId });
                entity.Property(aa => aa.Time).IsRequired();
                entity.HasOne(aa => aa.UserMonth).WithMany(um => um.ApprovedActivities).HasForeignKey(aa => new { aa.Month, aa.UserName });
                entity.HasOne(aa => aa.Project).WithMany(p => p.ApprovedActivities).HasForeignKey(aa => aa.ProjectId);
            });

            modelBuilder.Entity<Subactivity>(entity => {
                entity.HasKey(s => new { s.ProjectId, s.SubactivityId });
                entity.HasOne(s => s.Project).WithMany(p => p.Subactivities).HasForeignKey(s => s.ProjectId);
            });

            modelBuilder.Entity<User>(entity => {
                entity.HasKey(u => u.Name);
            });
        }
    }
}
