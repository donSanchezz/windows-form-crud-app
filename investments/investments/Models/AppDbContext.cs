using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace investments.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<Security> Securities { get; set; } = null!;
        public virtual DbSet<StatusCode> StatusCodes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-BASU7AT;Initial Catalog=Coupons;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("coupons");

                entity.Property(e => e.CouponId).HasColumnName("coupon_id");

                entity.Property(e => e.IsinCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("isin_code");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_date");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasColumnName("record_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");
            });

            modelBuilder.Entity<Security>(entity =>
            {
                entity.HasKey(e => e.IsinCode);

                entity.ToTable("securities");

                entity.Property(e => e.IsinCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("isin_code");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.StatusId).HasColumnName("status_id");
            });

            modelBuilder.Entity<StatusCode>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("status_codes");

                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("status_id");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
