using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable
//dotnet ef dbcontext scaffold "Server=.\SQLEXPRESS;Database=Crypto;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -f
namespace back.Models
{
    public partial class CryptoContext : DbContext
    {
        public CryptoContext()
        {
        }

        public CryptoContext(DbContextOptions<CryptoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Btc> Btcs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Crypto;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Btc>(entity =>
            {
                entity.ToTable("BTC");

                entity.Property(e => e.Idate)
                    .HasPrecision(0)
                    .HasColumnName("IDate");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
