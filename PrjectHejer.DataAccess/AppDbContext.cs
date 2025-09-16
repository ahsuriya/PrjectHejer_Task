using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PrjectHejer.DataAccess.Models;

namespace PrjectHejer.DataAccess;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<EntityImage> EntityImages { get; set; }

    public virtual DbSet<EntityType> EntityTypes { get; set; }

    public virtual DbSet<Lead> Leads { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D86BBB6712");

            entity.Property(e => e.CustomerId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<EntityImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EntityIm__3214EC0775E31B26");

            entity.HasIndex(e => new { e.EntityId, e.EntityTypeId }, "IX_EntityImages_Entity");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.EntityType).WithMany(p => p.EntityImages)
                .HasForeignKey(d => d.EntityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityImages_EntityTypes");
        });

        modelBuilder.Entity<EntityType>(entity =>
        {
            entity.HasKey(e => e.EntityTypeId).HasName("PK__EntityTy__E45C98F30DAC40D8");

            entity.HasIndex(e => e.Name, "UQ__EntityTy__737584F6C7349320").IsUnique();

            entity.Property(e => e.EntityTypeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.LeadId).HasName("PK__Leads__73EF78FA88A7F7B7");

            entity.Property(e => e.LeadId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.LeadName).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
