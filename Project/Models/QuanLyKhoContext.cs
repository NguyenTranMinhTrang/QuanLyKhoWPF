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

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Input> Inputs { get; set; }

        public virtual DbSet<InputInfo> InputInfos { get; set; }

        public virtual DbSet<Object> Objects { get; set; }

        public virtual DbSet<Output> Outputs { get; set; }

        public virtual DbSet<OutputInfo> OutputInfos { get; set; }

        public virtual DbSet<Suplier> Supliers { get; set; }

        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer("Server=.\\sqlexpress;User ID=DESKTOP-QABED6N\\\\\\\\DELL;Password=;Database=QuanLyKho;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0710BD9DCD");

                entity.ToTable("Customer");

                entity.Property(e => e.ContractDate).HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(200);
                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Input>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Input__3214EC07A2E2110D");

                entity.ToTable("Input");

                entity.Property(e => e.Id).HasMaxLength(128);
                entity.Property(e => e.DateInput).HasColumnType("datetime");
            });

            modelBuilder.Entity<InputInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__InputInf__3214EC07D8B6B6EE");

                entity.ToTable("InputInfo");

                entity.Property(e => e.Id).HasMaxLength(128);
                entity.Property(e => e.IdInput).HasMaxLength(128);
                entity.Property(e => e.IdObject).HasMaxLength(128);
                entity.Property(e => e.InputPrice).HasDefaultValueSql("((0))");
                entity.Property(e => e.OutputPrice).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdInputNavigation).WithMany(p => p.InputInfos)
                    .HasForeignKey(d => d.IdInput)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InputInfo__IdInp__4AB81AF0");

                entity.HasOne(d => d.IdObjectNavigation).WithMany(p => p.InputInfos)
                    .HasForeignKey(d => d.IdObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InputInfo__IdObj__4BAC3F29");
            });

            modelBuilder.Entity<Object>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Object__3214EC07E16B3722");

                entity.ToTable("Object");

                entity.Property(e => e.Id).HasMaxLength(128);
                entity.Property(e => e.Qrcode).HasColumnName("QRCode");

                entity.HasOne(d => d.IdSuplierNavigation).WithMany(p => p.Objects)
                    .HasForeignKey(d => d.IdSuplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Object__IdSuplie__4D94879B");

                entity.HasOne(d => d.IdUnitNavigation).WithMany(p => p.Objects)
                    .HasForeignKey(d => d.IdUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Object__IdUnit__4CA06362");
            });

            modelBuilder.Entity<Output>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Output__3214EC072262A71A");

                entity.ToTable("Output");

                entity.Property(e => e.Id).HasMaxLength(128);
                entity.Property(e => e.DateOutput).HasColumnType("datetime");
            });

            modelBuilder.Entity<OutputInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__OutputIn__3214EC07EDC28BE5");

                entity.ToTable("OutputInfo");

                entity.Property(e => e.Id).HasMaxLength(128);
                entity.Property(e => e.IdInputInfo).HasMaxLength(128);
                entity.Property(e => e.IdObject).HasMaxLength(128);
                entity.Property(e => e.IdOutputInfo).HasMaxLength(128);

                entity.HasOne(d => d.IdNavigation).WithOne(p => p.OutputInfo)
                    .HasForeignKey<OutputInfo>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutputInfo__Id__403A8C7D");

                entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.OutputInfos)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutputInf__IdCus__4E88ABD4");

                entity.HasOne(d => d.IdInputInfoNavigation).WithMany(p => p.OutputInfos)
                    .HasForeignKey(d => d.IdInputInfo)
                    .HasConstraintName("FK__OutputInf__IdInp__6E01572D");

                entity.HasOne(d => d.IdObjectNavigation).WithMany(p => p.OutputInfos)
                    .HasForeignKey(d => d.IdObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OutputInf__IdObj__4F7CD00D");
            });

            modelBuilder.Entity<Suplier>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Suplier__3214EC07F73047F4");

                entity.ToTable("Suplier");

                entity.Property(e => e.ContractDate).HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(200);
                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Unit__3214EC070EB8900C");

                entity.ToTable("Unit");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07A980FCB5");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__IdRole__5070F446");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC076862A7E5");

                entity.ToTable("UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
