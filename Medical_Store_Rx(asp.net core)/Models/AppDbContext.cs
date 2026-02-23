using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contraindication> Contraindications { get; set; }

    public virtual DbSet<CurrentmedPhr> CurrentmedPhrs { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DiseasesPhr> DiseasesPhrs { get; set; }

    public virtual DbSet<Medicalstore> Medicalstores { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Rider> Riders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AHSAN\\AHSAN;Database=fyp1;User Id=sa;Password=memories@2k19;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contraindication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contrain__3213E83FC7CEDC12");

            entity.ToTable("contraindication");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BaseName)
                .HasMaxLength(150)
                .HasColumnName("base_name");
            entity.Property(e => e.Disease)
                .HasMaxLength(100)
                .HasColumnName("disease");
            entity.Property(e => e.Message)
                .HasMaxLength(300)
                .HasColumnName("message");
            entity.Property(e => e.Severity)
                .HasMaxLength(20)
                .HasColumnName("severity");
            entity.Property(e => e.WithBase)
                .HasMaxLength(150)
                .HasColumnName("with_base");
        });

        modelBuilder.Entity<CurrentmedPhr>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__currentm__3213E83FEA045C88");

            entity.ToTable("currentmed_phr");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrentMed)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("current_med");
            entity.Property(e => e.ProfileId).HasColumnName("profile_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.CurrentmedPhrs)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__currentme__profi__1D7B6025");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CId).HasName("PK__customer__213EE77497312130");

            entity.ToTable("customer");

            entity.HasIndex(e => e.Email, "UX_customer_email").IsUnique();

            entity.Property(e => e.CId)
                .ValueGeneratedNever()
                .HasColumnName("c_id");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.CIdNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.CId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__customer__dob__7EF6D905");
        });

        modelBuilder.Entity<DiseasesPhr>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__diseases__3213E83F075D0B4C");

            entity.ToTable("diseases_phr");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Disease)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("disease");
            entity.Property(e => e.ProfileId).HasColumnName("profile_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.DiseasesPhrs)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__diseases___profi__3EDC53F0");
        });

        modelBuilder.Entity<Medicalstore>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__medicals__A2F2A30CF97565C6");

            entity.ToTable("medicalstore");

            entity.Property(e => e.StoreId)
                .ValueGeneratedNever()
                .HasColumnName("store_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Images)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("images");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.Store).WithOne(p => p.Medicalstore)
                .HasForeignKey<Medicalstore>(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__medicalst__passw__03BB8E22");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedId).HasName("PK__medicine__1F550E12390833EC");

            entity.ToTable("medicine");

            entity.Property(e => e.MedId).HasColumnName("med_id");
            entity.Property(e => e.BaseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("base_name");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Store).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_medicine_store");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__46596229405EC190");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.PrespId).HasColumnName("presp_id");
            entity.Property(e => e.RiderId).HasColumnName("rider_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.TotalBill).HasColumnName("total_bill");

            entity.HasOne(d => d.Cust).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_patient");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_orders_medicine");

            entity.HasOne(d => d.Presp).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PrespId)
                .HasConstraintName("FK_orders_prescription");

            entity.HasOne(d => d.Rider).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RiderId)
                .HasConstraintName("FK_orders_rider");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_store");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__order_it__52020FDD1C291D64");

            entity.ToTable("order_items");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.MedName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("med_name");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Medicine).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_ite__medic__4A4E069C");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_ite__order__4959E263");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__prescrip__3213E83FD9CB6A37");

            entity.ToTable("prescriptions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustId).HasColumnName("cust_id");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.MedName)
                .HasMaxLength(500)
                .HasColumnName("med_name");
            entity.Property(e => e.PotencyMl)
                .HasMaxLength(500)
                .HasColumnName("potency_ml");
            entity.Property(e => e.Profileid).HasColumnName("profileid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RxImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("rx_image");

            entity.HasOne(d => d.Cust).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.CustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_prescriptions_patient");

            entity.HasOne(d => d.Profile).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.Profileid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_prescriptions_profile");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__profiles__3213E83F2DA9E1F1");

            entity.ToTable("profiles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Relation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relation");

            entity.HasOne(d => d.Cus).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.CusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__profiles__contac__10216507");
        });

        modelBuilder.Entity<Rider>(entity =>
        {
            entity.HasKey(e => e.RiderId).HasName("PK__Rider__0A354F05E6FF6C93");

            entity.ToTable("Rider");

            entity.Property(e => e.RiderId)
                .ValueGeneratedNever()
                .HasColumnName("rider_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.MedId).HasColumnName("med_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Photo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.Rating)
                .HasDefaultValue(5.0m)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("rating");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalOrders)
                .HasDefaultValue(0)
                .HasColumnName("total_orders");
            entity.Property(e => e.Vehinfo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vehinfo");

            entity.HasOne(d => d.Med).WithMany(p => p.Riders)
                .HasForeignKey(d => d.MedId)
                .HasConstraintName("FK_medicalstore_rider");

            entity.HasOne(d => d.RiderNavigation).WithOne(p => p.Rider)
                .HasForeignKey<Rider>(d => d.RiderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_riderid");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3214EC0709520EF3");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UX_user_email").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
