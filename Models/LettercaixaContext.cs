using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LettercaixaAPI.Models;

public partial class LettercaixaContext : DbContext
{
    public LettercaixaContext()
    {
    }

    public LettercaixaContext(DbContextOptions<LettercaixaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Lettercaixa;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__CE74FAF5489AAC38");

            entity.ToTable("Favorite");

            entity.HasIndex(e => e.ProfileId, "UQ__Favorite__290C888592A78C18").IsUnique();

            entity.Property(e => e.FavoriteId)
                .ValueGeneratedNever()
                .HasColumnName("FavoriteID");
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.HasOne(d => d.Profile).WithOne(p => p.Favorite)
                .HasForeignKey<Favorite>(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorite__Profil__628FA481");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA126038D0F00B54");

            entity.ToTable("Post");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Comment)
                .HasMaxLength(250)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MovieId).HasColumnName("MovieID");
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.Score).HasColumnType("numeric(18, 0)");

            entity.HasOne(d => d.Profile).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Profile__290C888476916422");

            entity.ToTable("Profile");

            entity.HasIndex(e => e.Email, "UK_Profile_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Profile__536C85E44AF38398").IsUnique();

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.Birth).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(220)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(220)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(220)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
