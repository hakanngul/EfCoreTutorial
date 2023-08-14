using EfCoreTutorial.Common;
using EfCoreTutorial.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTutorial.Data.Context;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentAddress> StudentAddresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        if (optionsBuilder.IsConfigured == false)
        {
            optionsBuilder.UseSqlServer(StringConstants.DbConnectionString);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Mapping Operations in here
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("students");
            entity.Property(i=>i.Id).HasColumnName("id").HasColumnType("int").UseIdentityColumn().ValueGeneratedOnAdd().IsRequired();
            entity.Property(f => f.FirstName).HasColumnName("first_name").HasColumnType("nvarchar").HasMaxLength(100);
            entity.Property(l => l.LastName).HasColumnName("last_name").HasColumnType("nvarchar").HasMaxLength(100);
            entity.Property(n => n.Number).HasColumnName("number");
            entity.Property(b=>b.BirthDate).HasColumnName("birth_date");
            entity.Property(i=> i.AddressId).HasColumnName("address_id").HasColumnType("int");

            entity.HasMany(x=>x.Books)
                .WithOne(x=>x.Student)
                .HasForeignKey(x=>x.StudentId)
                .HasConstraintName("student_book_id_fk");
                
        });
        
        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("teachers");
            entity.Property(i=>i.Id).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().UseIdentityColumn();
            entity.Property(f => f.FirstName).HasColumnName("first_name").HasColumnType("nvarchar").HasMaxLength(100);
            entity.Property(l => l.LastName).HasColumnName("last_name").HasColumnType("nvarchar").HasMaxLength(100);
            entity.Property(b=>b.BirthDate).HasColumnName("birth_date");
        });
        
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("courses");
            entity.Property(i=>i.Id).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().UseIdentityColumn();
            entity.Property(f => f.Name).HasColumnName("name").HasColumnType("nvarchar").HasMaxLength(100);
            entity.Property(b=>b.IsActive).HasColumnName("is_active");
        });

        modelBuilder.Entity<StudentAddress>(entity =>
        {
            entity.ToTable("StudentAddresses");
            entity.Property(i=>i.Id).HasColumnName("id").ValueGeneratedOnAdd().UseIdentityColumn();
            entity.Property(c => c.City).HasColumnName("city").HasMaxLength(100);
            entity.Property(d => d.District).HasColumnName("district").HasMaxLength(100);
            entity.Property(f => f.FullAddress).HasColumnName("full_address").HasMaxLength(1000);
            entity.Property(c => c.Country).HasColumnName("country").HasMaxLength(100);

            entity.HasOne(i => i.Student)
                .WithOne(i => i.Address)
                .HasForeignKey<Student>(i => i.AddressId)
                .HasConstraintName("student_address_student_id_fk");
        });
        
        
        base.OnModelCreating(modelBuilder);
    }
}