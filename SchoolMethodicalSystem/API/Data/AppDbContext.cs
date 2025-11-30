using System;
using System.Collections.Generic;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwAssignmentsExtended> VwAssignmentsExtendeds { get; set; }

    public virtual DbSet<VwEventsExtended> VwEventsExtendeds { get; set; }

    public virtual DbSet<VwPlansExtended> VwPlansExtendeds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-3PSOM1R\\MSSQLSERVERO;Database=SchoolMethodicalSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assignme__3214EC0780A03CCE");

            entity.ToTable(tb => tb.HasTrigger("TR_Assignments_UpdatedAt"));

            entity.HasIndex(e => e.Date, "IX_Assignments_Date");

            entity.HasIndex(e => e.Status, "IX_Assignments_Status");

            entity.HasIndex(e => e.TeacherId, "IX_Assignments_TeacherId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Event).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("assigned");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK_Assignments_TeacherId");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events__3214EC0722B2D354");

            entity.ToTable(tb => tb.HasTrigger("TR_Events_UpdatedAt"));

            entity.HasIndex(e => e.Date, "IX_Events_Date");

            entity.HasIndex(e => e.ResponsibleId, "IX_Events_ResponsibleId");

            entity.HasIndex(e => e.Status, "IX_Events_Status");

            entity.HasIndex(e => e.Type, "IX_Events_Type");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("in-progress");
            entity.Property(e => e.Topic).HasMaxLength(500);
            entity.Property(e => e.Type).HasMaxLength(100);

            entity.HasOne(d => d.Responsible).WithMany(p => p.Events)
                .HasForeignKey(d => d.ResponsibleId)
                .HasConstraintName("FK_Events_ResponsibleId");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Plans__3214EC0791C922C7");

            entity.ToTable(tb => tb.HasTrigger("TR_Plans_UpdatedAt"));

            entity.HasIndex(e => new { e.StartDate, e.EndDate }, "IX_Plans_DateRange");

            entity.HasIndex(e => e.ResponsibleId, "IX_Plans_ResponsibleId");

            entity.HasIndex(e => e.Status, "IX_Plans_Status");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("draft");

            entity.HasOne(d => d.Responsible).WithMany(p => p.Plans)
                .HasForeignKey(d => d.ResponsibleId)
                .HasConstraintName("FK_Plans_ResponsibleId");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC072E98D8DD");

            entity.HasIndex(e => e.ExpiresAt, "IX_RefreshTokens_ExpiresAt");

            entity.HasIndex(e => e.Token, "IX_RefreshTokens_Token");

            entity.HasIndex(e => e.UserId, "IX_RefreshTokens_UserId");

            entity.HasIndex(e => e.Token, "UQ__RefreshT__1EB4F8178074913A").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Token).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RefreshTokens_UserId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07DE6F2BFF");

            entity.ToTable(tb => tb.HasTrigger("TR_Users_UpdatedAt"));

            entity.HasIndex(e => e.Login, "IX_Users_Login");

            entity.HasIndex(e => e.Role, "IX_Users_Role");

            entity.HasIndex(e => e.Status, "IX_Users_Status");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B1B70326D").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");
        });

        modelBuilder.Entity<VwAssignmentsExtended>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Assignments_Extended");

            entity.Property(e => e.Event).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TeacherName).HasMaxLength(255);
        });

        modelBuilder.Entity<VwEventsExtended>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Events_Extended");

            entity.Property(e => e.ResponsibleName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Topic).HasMaxLength(500);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<VwPlansExtended>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Plans_Extended");

            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Period).HasMaxLength(4000);
            entity.Property(e => e.ResponsibleName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
