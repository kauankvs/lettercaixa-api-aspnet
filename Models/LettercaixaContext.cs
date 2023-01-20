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

    public virtual DbSet<FavoriteMovie> FavoriteMovies { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Lettercaixa;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoriteMovie>(entity =>
        {
            entity.HasKey(e => e.FavoriteMovies).HasName("PK__Favorite__0FB4E7986691C7BB");

            entity.Property(e => e.MovieEight)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MovieFive)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MovieFour)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MovieNine)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MovieOne)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MovieSeven)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MovieSix)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MovieTen)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MovieThree)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MovieTwo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.HasOne(d => d.Profile).WithMany(p => p.FavoriteMovies)
                .HasForeignKey(d => d.ProfileId)
                .HasConstraintName("FK__FavoriteM__Profi__3C69FB99");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Profile__290C888476916422");

            entity.ToTable("Profile", tb =>
                {
                    tb.HasTrigger("AddFavMoviesWhenProfileIsCreated");
                    tb.HasTrigger("DelFavMoviesWhenProfileIsDel");
                });

            entity.HasIndex(e => e.Email, "UK_Profile_Email").IsUnique();

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
