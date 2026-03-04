using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Fyp1Context : DbContext
{
    public Fyp1Context()
    {
    }

    public Fyp1Context(DbContextOptions<Fyp1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Medicine> Medicines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AHSAN\\AHSAN;Database=fyp1;User Id=sa;Password=memories@2k19;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedId).HasName("PK__medicine__1F550E12390833EC");

            entity.ToTable("medicine");

            entity.Property(e => e.MedId).HasColumnName("med_id");
            entity.Property(e => e.BaseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("base_name");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PillsPerPack).HasColumnName("pills_per_pack");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.Strength)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("strength");
            entity.Property(e => e.TotalPillsStock).HasColumnName("total_pills_stock");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
