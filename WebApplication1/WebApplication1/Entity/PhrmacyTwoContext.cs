using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.Entity
{
    public partial class PhrmacyTwoContext : DbContext
    {
        public PhrmacyTwoContext()
        {
        }

        public PhrmacyTwoContext(DbContextOptions<PhrmacyTwoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<NotificationId> NotificationIds { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-63T0OB5\\SQLEXPRESS;Database=PhrmacyTwo;Trusted_Connection=True; User Id=BLACK;password=20012001;Integrated Security=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("Medicine");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NotificationId>(entity =>
            {
                entity.HasKey(e => e.NotificationId1);

                entity.ToTable("NotificationId");

                entity.Property(e => e.NotificationId1).HasColumnName("NotificationId");

                entity.Property(e => e.NotificationDate).HasColumnType("date");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.NotificationIds)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationId_Admin");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.NotificationIds)
                    .HasForeignKey(d => d.MedicineId)
                    .HasConstraintName("fk_med_id");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.NotificationIds)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationId_Supplier");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("fk_customer_id");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.MedicineId)
                    .HasConstraintName("fk_medicine_id");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("fk_suppiler_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
