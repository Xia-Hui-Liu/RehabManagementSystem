using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RehabManagementSystem.Domain;
using System;

namespace RehabManagementSystem.Database;

    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           // Seed data for Roles
    var adminRole = new IdentityRole
    {
        Id = "1",
        Name = "Admin",
        NormalizedName = "ADMIN"
    };
    
    var userRole = new IdentityRole
    {
        Id = "2",
        Name = "User",
        NormalizedName = "USER"
    };

    modelBuilder.Entity<IdentityRole>().HasData(adminRole, userRole);

    // Seed data for Employees
    var user1 = new Employee
    {
        Id = "1",
        UserName = "user1@example.com",
        NormalizedUserName = "USER1@EXAMPLE.COM",
        Email = "user1@example.com",
        NormalizedEmail = "USER1@EXAMPLE.COM",
        EmailConfirmed = true,
        PasswordHash = new PasswordHasher<Employee>().HashPassword(null!, "Password123!"),
        SecurityStamp = Guid.NewGuid().ToString(),
        FirstName = "FirstName1",
        LastName = "LastName1"
    };

    var user2 = new Employee
    {
        Id = "2",
        UserName = "user2@example.com",
        NormalizedUserName = "USER2@EXAMPLE.COM",
        Email = "user2@example.com",
        NormalizedEmail = "USER2@EXAMPLE.COM",
        EmailConfirmed = true,
        PasswordHash = new PasswordHasher<Employee>().HashPassword(null!, "Password123!"),
        SecurityStamp = Guid.NewGuid().ToString(),
        FirstName = "FirstName2",
        LastName = "LastName2"
    };

    modelBuilder.Entity<Employee>().HasData(user1, user2);

    // Seed User-Role relationships
    modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        new IdentityUserRole<string>
        {
            UserId = user1.Id, // Assign user1 to Admin
            RoleId = adminRole.Id
        },
        new IdentityUserRole<string>
        {
            UserId = user2.Id, // Assign user2 to User
            RoleId = userRole.Id
        }
    );
        }
    }