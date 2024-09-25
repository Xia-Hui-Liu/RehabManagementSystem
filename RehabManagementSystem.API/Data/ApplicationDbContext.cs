// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;
// using System;

// public class ApplicationDbContext : IdentityDbContext<IdentityUser>
// {
//     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//         : base(options)
//     {
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);

//         // Seed data for IdentityUser
//         modelBuilder.Entity<IdentityUser>().HasData(
//             new IdentityUser
//             {
//                 Id = "1",
//                 UserName = "user1@example.com",
//                 NormalizedUserName = "USER1@EXAMPLE.COM",
//                 Email = "user1@example.com",
//                 NormalizedEmail = "USER1@EXAMPLE.COM",
//                 EmailConfirmed = true,
//                 PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password123!"),
//                 SecurityStamp = Guid.NewGuid().ToString()
//             },
//             new IdentityUser
//             {
//                 Id = "2",
//                 UserName = "user2@example.com",
//                 NormalizedUserName = "USER2@EXAMPLE.COM",
//                 Email = "user2@example.com",
//                 NormalizedEmail = "USER2@EXAMPLE.COM",
//                 EmailConfirmed = true,
//                 PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password123!"),
//                 SecurityStamp = Guid.NewGuid().ToString()
//             }
//         );

//         // Seed roles
//         modelBuilder.Entity<IdentityRole>().HasData(
//             new IdentityRole
//             {
//                 Id = "1",
//                 Name = "Admin",
//                 NormalizedName = "ADMIN"
//             },
//             new IdentityRole
//             {
//                 Id = "2",
//                 Name = "User",
//                 NormalizedName = "USER"
//             }
//         );

//         // Seed user-role relationships
//         modelBuilder.Entity<IdentityUserRole<string>>().HasData(
//             new IdentityUserRole<string>
//             {
//                 UserId = "1",
//                 RoleId = "1" // Assign Admin role to user1
//             },
//             new IdentityUserRole<string>
//             {
//                 UserId = "2",
//                 RoleId = "2" // Assign User role to user2
//             }
//         );
//     }
// }
