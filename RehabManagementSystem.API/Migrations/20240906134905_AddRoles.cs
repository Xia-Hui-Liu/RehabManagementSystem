using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RehabManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c99cc70d-36ef-4dfb-8ed5-959c2dacb6ff", "AQAAAAIAAYagAAAAENt/7ZhSmU5MQMdpCQ+HapW9hyWGFeA1lbNwGh4SPWpXZiu7LIlgBLuUa/M7WlLR+Q==", "931f27d0-dff1-4ece-a64d-83c4a4e04488" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7c6cfa7-6240-44ed-96f5-df722a10fec6", "AQAAAAIAAYagAAAAEPnSOaEgciPioLBeNAOONL8d9Iw98DrPKgKdoVN5U/nxGBt80htqiZPyhkPWxcw2cg==", "dad6b24b-7aeb-48f2-a629-9a5c7896b292" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92f34d6b-5ec4-4608-9ea2-c9d3642a4f98", "AQAAAAIAAYagAAAAELJoFvVcBt3u9y1f+KDtj5258uHkGc9Gmwsivha5OKrXFF4MZHn/RV6VM5QPomDpBg==", "16d0be2b-15fe-4e41-9693-3def94d7f914" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dad3fb94-2dfd-4fa5-9bce-27346c4b1de0", "AQAAAAIAAYagAAAAEEuseSFWbpuZTUCi4wlTzBY/u6ZkFm6kgJCG2fIunMCz+6hZkDQS60+GZbtYPIrOFg==", "f018d717-65d3-4402-a156-af84b9ce7803" });
        }
    }
}
