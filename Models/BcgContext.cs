using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bcg_bot.Models;

public partial class BcgContext : DbContext
{
    public BcgContext()
    {
    }

    public BcgContext(DbContextOptions<BcgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comand> Comands { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bcg;Username=postgres;Password=Griver2070");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Comands_pkey");

            entity.ToTable("comands");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Comands_Id_seq\"'::regclass)");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.ChatId)
                .ValueGeneratedNever()
                .HasColumnName("chatId");
            entity.Property(e => e.BirthDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birthDate");
            entity.Property(e => e.BmstuGroup)
                .HasMaxLength(255)
                .HasColumnName("bmstu_group");
            entity.Property(e => e.Code)
                .ValueGeneratedOnAdd()
                .HasColumnName("code");
            entity.Property(e => e.ComandLine)
                .HasMaxLength(255)
                .HasColumnName("comandLine");
            entity.Property(e => e.DateReg).HasColumnName("dateReg");
            entity.Property(e => e.Expirience).HasColumnName("expirience ");
            entity.Property(e => e.Fio)
                .HasMaxLength(255)
                .HasColumnName("fio");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.PrevComand)
                .HasMaxLength(255)
                .HasColumnName("prevComand");
            entity.Property(e => e.University)
                .HasMaxLength(255)
                .HasColumnName("university");
            entity.Property(e => e.UserType).HasColumnName("userType");

            entity.HasOne(d => d.ComandNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Comand)
                .HasConstraintName("users_Comand_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
