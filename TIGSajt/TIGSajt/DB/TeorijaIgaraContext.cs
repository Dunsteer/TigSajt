using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TIGSajt.DB
{
    public partial class TeorijaIgaraContext : DbContext
    {
        public TeorijaIgaraContext()
        {
        }

        public TeorijaIgaraContext(DbContextOptions<TeorijaIgaraContext> options)
            : base(options)
        {
        }

        public const string connString = "Server=.;Database=TeorijaIgara;Trusted_Connection=True;";

        public virtual DbSet<Statistics> Statistics { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0");

            modelBuilder.Entity<Statistics>(entity =>
            {
                entity.HasOne(d => d.GuestStudent)
                    .WithMany(p => p.StatisticsGuestStudent)
                    .HasForeignKey(d => d.GuestStudentId)
                    .HasConstraintName("FK_Statistics_Student");

                entity.HasOne(d => d.HomeStudent)
                    .WithMany(p => p.StatisticsHomeStudent)
                    .HasForeignKey(d => d.HomeStudentId)
                    .HasConstraintName("FK_Statistics_Student1");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(500);
            });
        }
    }
}
