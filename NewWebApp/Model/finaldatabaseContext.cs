using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NewWebApp.Model
{
    public partial class finaldatabaseContext : DbContext
    {
        public finaldatabaseContext()
        {
        }

        public finaldatabaseContext(DbContextOptions<finaldatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Disaster> Disasters { get; set; }
        public virtual DbSet<GoodsAllocation> GoodsAllocations { get; set; }
        public virtual DbSet<GoodsDonation> GoodsDonations { get; set; }
        public virtual DbSet<MonetaryDonation> MonetaryDonations { get; set; }
        public virtual DbSet<MoneyAllocation> MoneyAllocations { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDefinedCategory> UserDefinedCategories { get; set; }
        public IEnumerable<object> MoneyAllocation { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:apprsevere.database.windows.net,1433;Initial Catalog=finaldatabase;Persist Security Info=False;User ID=Nkqubela;Password=7lazarous@7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Disaster>(entity =>
            {
                entity.ToTable("Disaster");

                entity.Property(e => e.AidTypes)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Aid_types");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");
            });

            modelBuilder.Entity<GoodsAllocation>(entity =>
            {
                entity.HasKey(e => e.AllocationId)
                    .HasName("PK__Goods_Al__B3C6D64B187CA447");

                entity.ToTable("Goods_Allocation");

                entity.Property(e => e.AllocationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Allocation_Date");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Disaster)
                    .WithMany(p => p.GoodsAllocations)
                    .HasForeignKey(d => d.DisasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Goods_All__Disas__18EBB532");

                entity.HasOne(d => d.GoodsDonation)
                    .WithMany(p => p.GoodsAllocations)
                    .HasForeignKey(d => d.GoodsDonationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Goods_All__Goods__19DFD96B");
            });

            modelBuilder.Entity<GoodsDonation>(entity =>
            {
                entity.HasKey(e => e.DonationId)
                    .HasName("PK__Goods_do__C5082EFBCA15C234");

                entity.ToTable("Goods_donation");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .HasColumnName("Category_Name");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DonationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Donation_Date");

                entity.Property(e => e.DonorName)
                    .HasMaxLength(200)
                    .HasColumnName("Donor_Name");

                entity.Property(e => e.NumberOfItems)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Number_of_Items");

                entity.HasOne(d => d.UserDefinedCategory)
                    .WithMany(p => p.GoodsDonations)
                    .HasForeignKey(d => d.UserDefinedCategoryId)
                    .HasConstraintName("FK__Goods_don__UserD__08B54D69");
            });

            modelBuilder.Entity<MonetaryDonation>(entity =>
            {
                entity.HasKey(e => e.DonationId)
                    .HasName("PK__Monetary__C5082EFBC79C652E");

                entity.ToTable("Monetary_Donations");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DonationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Donation_Date");

                entity.Property(e => e.DonorName)
                    .HasMaxLength(200)
                    .HasColumnName("Donor_Name");
            });

            modelBuilder.Entity<MoneyAllocation>(entity =>
            {
                entity.HasKey(e => e.AllocationId)
                    .HasName("PK__Money_Al__B3C6D64B78AAAE10");

                entity.ToTable("Money_Allocation");

                entity.Property(e => e.AllocationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Allocation_Date");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Disaster)
                    .WithMany(p => p.MoneyAllocations)
                    .HasForeignKey(d => d.DisasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Money_All__Disas__160F4887");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.Property(e => e.AmountSpent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Purchase_Date");

                entity.HasOne(d => d.Disaster)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.DisasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchases__Disas__1CBC4616");

                entity.HasOne(d => d.GoodsDonation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.GoodsDonationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchases__Goods__1DB06A4F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Password)
                    .HasName("PK__User__87909B14BB2B9376");

                entity.ToTable("User");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserDefinedCategory>(entity =>
            {
                entity.ToTable("UserDefinedCategory");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Category_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
