using System;
using System.Collections.Generic;
using Entitites.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public partial class RabbitEditorialContext : DbContext
{
    public RabbitEditorialContext()
    {
    }

    public RabbitEditorialContext(DbContextOptions<RabbitEditorialContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Articletype> Articletypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=RabbitEditorial;Username=postgres;Password=UAZ9233");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("articles_pkey");

            entity.ToTable("articles");

            entity.Property(e => e.ArticleId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("article_id");
            entity.Property(e => e.ArticleTypeId).HasColumnName("article_type_id");
            entity.Property(e => e.Text)
                .HasColumnType("character varying")
                .HasColumnName("text");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.ArticleType).WithMany(p => p.Articles)
                .HasForeignKey(d => d.ArticleTypeId)
                .HasConstraintName("fk_article_id_article_articletypes");
        });

        modelBuilder.Entity<Articletype>(entity =>
        {
            entity.HasKey(e => e.ArticleTypeId).HasName("articletypes_pkey");

            entity.ToTable("articletypes");

            entity.Property(e => e.ArticleTypeId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("article_type_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
