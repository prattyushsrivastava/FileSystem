using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Layer.Models;

public partial class FilesDbContext : DbContext
{
    public FilesDbContext()
    {
    }

    public FilesDbContext(DbContextOptions<FilesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<File> Files { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__files__3214EC07D1C9BE6F");

            entity.ToTable("files");

            entity.Property(e => e.Fpath)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("FPath");
            entity.Property(e => e.ImgUrl).HasColumnName("ImgURL");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
