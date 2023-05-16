using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LettercaixaAPI.Models;

public partial class LettercaixaDbContext : DbContext
{
    public LettercaixaDbContext()
    {
    }

    public LettercaixaDbContext(DbContextOptions<LettercaixaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FavoriteMovie> FavoriteMovies { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=LettercaixaDB;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoriteMovie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC0773C87B4D");

            entity.ToTable("FavoriteMovie");

            entity.HasOne(d => d.Profile).WithMany(p => p.FavoriteMovies)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteM__Profi__5070F446");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA126018B9E6B741");

            entity.ToTable("Post");

            entity.Property(e => e.Comment)
                .HasMaxLength(510)
                .IsUnicode(false);

            entity.HasOne(d => d.Profile).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__ProfileId__4D94879B");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Profile__290C88E4BE9DB438");

            entity.ToTable("Profile");

            entity.HasIndex(e => e.Username, "UQ__Profile__536C85E4ECBCDAC4").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Profile__A9D105343EB320F1").IsUnique();

            entity.Property(e => e.Birth).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
