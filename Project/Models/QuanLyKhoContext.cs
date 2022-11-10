using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.Models
{
    public partial class QuanLyKhoContext : DbContext
    {
        public QuanLyKhoContext()
        {
        }

        public QuanLyKhoContext(DbContextOptions<QuanLyKhoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Input> Inputs { get; set; } = null!;
        public virtual DbSet<InputInfo> InputInfos { get; set; } = null!;
        public virtual DbSet<Object> Objects { get; set; } = null!;
        public virtual DbSet<Output> Outputs { get; set; } = null!;
        public virtual DbSet<OutputInfo> OutputInfos { get; set; } = null!;
        public virtual DbSet<Suplier> Supliers { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer("Server=.\\sqlexpress;User ID=DESKTOP-QABED6N\\DELL;Password=;Database=QuanLyKho;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.ContractDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Input>(entity =>
            {
                entity.ToTable("Input");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.DateInput).HasColumnType("datetime");
            });

            modelBuilder.Entity<InputInfo>(entity =>
            {
                entity.ToTable("InputInfo");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.IdInput).HasMaxLength(128);

                entity.Property(e => e.IdObject).HasMaxLength(128);

                entity.Property(e => e.InputPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.OutputPrice).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdInputNavigation)
                    .WithMany(p => p.InputInfos)
                    .HasForeignKey(d => d.IdInput)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InputInfo__IdInp__4AB81AF0");

                entity.HasOne(d => d.IdObjectNavigation)
                    .WithMany(p => p.InputInfos)
                    .HasForeignKey(d => d.IdObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InputInfo__IdObj__4BAC3F29");
            });

            modelBuilder.Entity<Object>(entity =>
            {
                entity.ToTable("Object");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Qrcode).HasColumnName("QRCode");

                entity.HasOne(d => d.IdSuplierNavigation)
                    .WithMany(p => p.Objects)
                    .HasForeignKey(d => d.IdSuplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Object__IdSuplie__4D94879B");

                entity.HasOne(d => d.IdUnitNavigation)
                    .WithMany(p => p.Objects)
                    .HasForeignKey(d => d.IdUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Object__IdUnit__4CA06362");
            });

            modelBuilder.Entity<Output>(entity =>
            {
                entity.ToTable("Output");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.DateOutput).HasColumnType("datetime");
            });

            modelBuilder.Entity<OutputInfo>(entity =>
            {
                entity.ToTable("OutputInfo");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.IdObject).HasMaxLength(128);

                entity.Property(e => e.IdOutputInfo).HasMaxLength(128);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.OutputInfo)
                    .HasForeignKey<OutputInfo>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutputInfo__Id__403A8C7D");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.OutputInfos)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutputInf__IdCus__4E88ABD4");

                entity.HasOne(d => d.IdObjectNavigation)
                    .WithMany(p => p.OutputInfos)
                    .HasForeignKey(d => d.IdObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutputInf__IdObj__4F7CD00D");
            });

            modelBuilder.Entity<Suplier>(entity =>
            {
                entity.ToTable("Suplier");

                entity.Property(e => e.ContractDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__IdRole__5070F446");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
