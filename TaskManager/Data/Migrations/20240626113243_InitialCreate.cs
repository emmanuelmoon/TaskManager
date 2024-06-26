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
                values: new object[] { 1, "admin@gmail.com", new byte[] { 244, 233, 150, 49, 6, 153, 218, 27, 53, 28, 65, 102, 160, 166, 14, 188, 80, 240, 71, 240, 82, 133, 11, 206, 50, 180, 134, 30, 143, 42, 226, 71, 49, 191, 244, 108, 148, 28, 249, 123, 1, 163, 101, 39, 202, 138, 202, 240, 247, 188, 38, 40, 40, 96, 167, 120, 195, 160, 61, 185, 86, 5, 18, 225 }, new byte[] { 218, 78, 89, 89, 120, 5, 158, 36, 169, 157, 29, 227, 98, 67, 143, 113, 211, 99, 2, 105, 110, 163, 54, 20, 66, 101, 64, 173, 132, 106, 224, 253, 194, 34, 84, 182, 145, 196, 120, 134, 78, 176, 170, 125, 29, 253, 219, 148, 88, 193, 143, 217, 153, 199, 48, 131, 210, 43, 152, 227, 224, 22, 211, 32, 208, 150, 253, 77, 20, 49, 55, 169, 222, 42, 115, 34, 234, 143, 35, 170, 1, 221, 75, 139, 106, 194, 62, 235, 253, 183, 15, 96, 212, 44, 254, 18, 75, 253, 25, 246, 182, 220, 28, 109, 178, 49, 12, 215, 55, 150, 76, 233, 252, 120, 241, 156, 133, 201, 252, 27, 187, 90, 242, 179, 96, 81, 64, 69 }, "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Status", "UserId" },
                values: new object[] { 1, new DateTime(2024, 6, 19, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8547), "Setup Admin Dashboard", new DateTime(2024, 6, 25, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8574), "Completed", 1 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Status", "UserId" },
                values: new object[] { 2, new DateTime(2024, 6, 24, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8581), "Create Initial Users", new DateTime(2024, 6, 29, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8584), "In Progress", 1 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Status", "UserId" },
                values: new object[] { 3, new DateTime(2024, 6, 28, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8587), "Review System Logs", new DateTime(2024, 7, 3, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8589), "Pending", 1 });

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
