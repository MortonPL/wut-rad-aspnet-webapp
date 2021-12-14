using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace NTR.Entities {
    public class StorageContext : DbContext {
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<UserMonth> UserMonths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("server=localhost;database=bmoroz;user=bmoroz;password=hiperbalbinka");
        }
    }
}
