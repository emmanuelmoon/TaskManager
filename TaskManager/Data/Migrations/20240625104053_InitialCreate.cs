using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[] { 1, "admin@gmail.com", new byte[] { 14, 23, 228, 22, 188, 176, 130, 155, 219, 3, 105, 43, 212, 204, 70, 187, 132, 83, 157, 139, 4, 177, 63, 168, 13, 95, 122, 118, 221, 215, 37, 134, 31, 154, 222, 250, 248, 100, 22, 176, 150, 152, 113, 239, 9, 168, 81, 172, 204, 157, 246, 80, 151, 162, 35, 251, 21, 97, 241, 92, 141, 64, 143, 134 }, new byte[] { 111, 129, 151, 106, 122, 80, 123, 120, 180, 13, 178, 37, 65, 103, 6, 197, 138, 114, 72, 138, 104, 18, 4, 4, 229, 80, 17, 74, 37, 140, 67, 186, 16, 248, 46, 249, 51, 38, 201, 239, 55, 48, 205, 215, 240, 245, 181, 186, 69, 28, 73, 162, 54, 161, 203, 166, 133, 185, 189, 79, 101, 222, 96, 153, 89, 86, 161, 223, 16, 200, 240, 171, 51, 108, 62, 44, 46, 75, 246, 181, 56, 218, 146, 71, 218, 228, 30, 149, 189, 236, 21, 2, 12, 27, 0, 146, 65, 63, 171, 221, 199, 175, 203, 154, 175, 51, 111, 32, 109, 193, 221, 172, 191, 182, 24, 109, 144, 252, 31, 218, 103, 123, 160, 199, 92, 182, 164, 98 }, "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Status", "UserId" },
                values: new object[] { 1, new DateTime(2024, 6, 18, 15, 40, 53, 155, DateTimeKind.Local).AddTicks(2861), "Setup Admin Dashboard", new DateTime(2024, 6, 24, 15, 40, 53, 155, DateTimeKind.Local).AddTicks(2901), "Completed", 1 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Status", "UserId" },
                values: new object[] { 2, new DateTime(2024, 6, 23, 15, 40, 53, 155, DateTimeKind.Local).AddTicks(2907), "Create Initial Users", new DateTime(2024, 6, 28, 15, 40, 53, 155, DateTimeKind.Local).AddTicks(2909), "InProgress", 1 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Status", "UserId" },
                values: new object[] { 3, new DateTime(2024, 6, 27, 15, 40, 53, 155, DateTimeKind.Local).AddTicks(2913), "Review System Logs", new DateTime(2024, 7, 2, 15, 40, 53, 155, DateTimeKind.Local).AddTicks(2915), "Pending", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
