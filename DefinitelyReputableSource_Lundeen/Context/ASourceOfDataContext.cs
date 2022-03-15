using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DefinitelyReputableSource_Lundeen.Models;

namespace DefinitelyReputableSource_Lundeen.Context
{
    public partial class ASourceOfDataContext : DbContext
    {
        public static IConfiguration Configuration;
        public ASourceOfDataContext()
        {
        }

        public ASourceOfDataContext(DbContextOptions<ASourceOfDataContext> options, IConfiguration Config)
            : base(options)
        {
            Configuration = Config;
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string conn = Configuration.GetConnectionString("Conn");
                optionsBuilder.UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.DailyCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasMany(d => d.Students)
                    .WithMany(p => p.Classes)
                    .UsingEntity<Dictionary<string, object>>(
                        "ClassStudent",
                        l => l.HasOne<Student>().WithMany().HasForeignKey("StudentsId"),
                        r => r.HasOne<Class>().WithMany().HasForeignKey("ClassesId"),
                        j =>
                        {
                            j.HasKey("ClassesId", "StudentsId");

                            j.ToTable("ClassStudent");

                            j.HasIndex(new[] { "StudentsId" }, "IX_ClassStudent_StudentsId");
                        });
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(1);

                entity.Property(e => e.TotalMoney).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
