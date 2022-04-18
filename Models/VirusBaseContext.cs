using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VirusDatabaseNew
{
    public partial class VirusBaseContext : DbContext
    {
        public VirusBaseContext()
        {
        }

        public VirusBaseContext(DbContextOptions<VirusBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CountriesVariant> CountriesVariants { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Symptom> Symptoms { get; set; } = null!;
        public virtual DbSet<SymptomsVariant> SymptomsVariants { get; set; } = null!;
        public virtual DbSet<Variant> Variants { get; set; } = null!;
        public virtual DbSet<Virus> Viruses { get; set; } = null!;
        public virtual DbSet<VirusGroup> VirusGroups { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-0SH5QUI; Database=VirusBase; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountriesVariant>(entity =>
            {
                entity.HasKey(e => new { e.CountryId, e.VariantId });

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountriesVariants)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountriesVariants_Countries");

                entity.HasOne(d => d.Variant)
                    .WithMany(p => p.CountriesVariants)
                    .HasForeignKey(d => d.VariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountriesVariants_Variants");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryName).HasMaxLength(64);
            });

            modelBuilder.Entity<Symptom>(entity =>
            {
                entity.Property(e => e.SymptomName).HasMaxLength(64);
            });

            modelBuilder.Entity<SymptomsVariant>(entity =>
            {
                entity.HasKey(e => new { e.VariantId, e.SymptomId });

                entity.Property(e => e.Severity).HasMaxLength(32);

                entity.HasOne(d => d.Symptom)
                    .WithMany(p => p.SymptomsVariants)
                    .HasForeignKey(d => d.SymptomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SymptomsVariants_SymptomsVariants");

                entity.HasOne(d => d.Variant)
                    .WithMany(p => p.SymptomsVariants)
                    .HasForeignKey(d => d.VariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SymptomsVariants_Variants");
            });

            modelBuilder.Entity<Variant>(entity =>
            {
                entity.Property(e => e.VariantDateDiscovered).HasColumnType("date");

                entity.Property(e => e.VariantName).HasMaxLength(64);

                entity.Property(e => e.VariantOrigin).HasMaxLength(64);

                entity.HasOne(d => d.Virus)
                    .WithMany(p => p.Variants)
                    .HasForeignKey(d => d.VirusId)
                    .HasConstraintName("FK_Variants_Viruses");
            });

            modelBuilder.Entity<Virus>(entity =>
            {
                entity.Property(e => e.VirusDateDiscovered).HasColumnType("date");

                entity.Property(e => e.VirusName).HasMaxLength(64);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Viruses)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Viruses_VirusGroups");
            });

            modelBuilder.Entity<VirusGroup>(entity =>
            {
                entity.Property(e => e.GroupDateDiscovered).HasColumnType("date");

                entity.Property(e => e.GroupInfo).HasMaxLength(512);

                entity.Property(e => e.GroupName).HasMaxLength(64);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
