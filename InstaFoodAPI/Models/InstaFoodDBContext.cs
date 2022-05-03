using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InstaFoodAPI.Models
{
    public partial class InstaFoodDBContext : DbContext
    {
        public InstaFoodDBContext()
        {
        }

        public InstaFoodDBContext(DbContextOptions<InstaFoodDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<LogBook> LogBooks { get; set; }
        public virtual DbSet<Pay> Pays { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Town> Towns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("SERVER=.;DATABASE=InstaFoodDB;User Id=FoodUser;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("PK__Client__C1961B3326E04E6B");

                entity.ToTable("Client");

                entity.HasIndex(e => e.Email, "UQ__Client__A9D1053483BE9F5A")
                    .IsUnique();

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.IdDistrict)
                    .HasName("PK__District__DE8D982E6C3D7702");

                entity.ToTable("District");

                entity.Property(e => e.NameDistrict)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTownNavigation)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.IdTown)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKDistrict37673");
            });

            modelBuilder.Entity<LogBook>(entity =>
            {
                entity.ToTable("LogBook");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Host)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Modify)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Operation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Table)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.User)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pay>(entity =>
            {
                entity.HasKey(e => e.IdPay)
                    .HasName("PK__Pay__2ACE3A3D6F375488");

                entity.ToTable("Pay");

                entity.Property(e => e.MethodPay)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__Product__2E8946D4BC5ED7C2");

                entity.ToTable("Product");

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.Express)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ImgUrl)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Published)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("('1')");

                entity.HasOne(d => d.IdRestNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdRest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProduct858492");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.IdProv)
                    .HasName("PK__Province__E40D970BFB87D5A3");

                entity.ToTable("Province");

                entity.Property(e => e.NameProv)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.IdPurchase)
                    .HasName("PK__Purchase__D99D14A5C69089A6");

                entity.ToTable("Purchase");

                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Voucher)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPurchase717279");

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.IdDistrict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPurchase23261");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPurchase908728");

                entity.HasOne(d => d.IdProvNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.IdProv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPurchase713976");

                entity.HasOne(d => d.IdTownNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.IdTown)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPurchase402122");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.IdRest)
                    .HasName("PK__Restaura__8C7670110ACDEDA0");

                entity.ToTable("Restaurant");

                entity.HasIndex(e => e.Email, "UQ__Restaura__A9D1053482ED093A")
                    .IsUnique();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(1440)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NameRest)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumPay1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumPay2)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumPay3)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Schedule)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.IdDistrict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKRestaurant975666");

                entity.HasOne(d => d.IdPay1Navigation)
                    .WithMany(p => p.RestaurantIdPay1Navigations)
                    .HasForeignKey(d => d.IdPay1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKRestaurant655076");

                entity.HasOne(d => d.IdPay2Navigation)
                    .WithMany(p => p.RestaurantIdPay2Navigations)
                    .HasForeignKey(d => d.IdPay2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKRestaurant655075");

                entity.HasOne(d => d.IdPay3Navigation)
                    .WithMany(p => p.RestaurantIdPay3Navigations)
                    .HasForeignKey(d => d.IdPay3)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKRestaurant655074");

                entity.HasOne(d => d.IdProvNavigation)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.IdProv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKRestaurant258686");

                entity.HasOne(d => d.IdTownNavigation)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.IdTown)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKRestaurant429459");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(e => e.IdTown)
                    .HasName("PK__Town__860C032101348EDF");

                entity.ToTable("Town");

                entity.Property(e => e.NameTown)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProvNavigation)
                    .WithMany(p => p.Towns)
                    .HasForeignKey(d => d.IdProv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTown643459");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
