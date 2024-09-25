using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RehabManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "92f34d6b-5ec4-4608-9ea2-c9d3642a4f98", "user1@example.com", true, false, null, "USER1@EXAMPLE.COM", "USER1@EXAMPLE.COM", "AQAAAAIAAYagAAAAELJoFvVcBt3u9y1f+KDtj5258uHkGc9Gmwsivha5OKrXFF4MZHn/RV6VM5QPomDpBg==", null, false, "16d0be2b-15fe-4e41-9693-3def94d7f914", false, "user1@example.com" },
                    { "2", 0, "dad3fb94-2dfd-4fa5-9bce-27346c4b1de0", "user2@example.com", true, false, null, "USER2@EXAMPLE.COM", "USER2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEuseSFWbpuZTUCi4wlTzBY/u6ZkFm6kgJCG2fIunMCz+6hZkDQS60+GZbtYPIrOFg==", null, false, "f018d717-65d3-4402-a156-af84b9ce7803", false, "user2@example.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
