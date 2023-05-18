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
        => optionsBuilder.UseSqlServer(Settings.ConnectionStringDb);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoriteMovie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC07C5214818");

            entity.ToTable("FavoriteMovie");

            entity.HasOne(d => d.Profile).WithMany(p => p.FavoriteMovies)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteM__Profi__6383C8BA");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA1260185AA00D50");

            entity.ToTable("Post");

            entity.Property(e => e.Comment)
                .HasMaxLength(510)
                .IsUnicode(false);

            entity.HasOne(d => d.Profile).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__ProfileId__60A75C0F");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Profile__290C88E4BFF5978F");

            entity.ToTable("Profile");

            entity.HasIndex(e => e.Username, "UQ__Profile__536C85E45D9F8C70").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Profile__A9D1053469F33C14").IsUnique();

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
