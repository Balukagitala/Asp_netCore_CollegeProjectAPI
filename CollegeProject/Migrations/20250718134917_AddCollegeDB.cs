using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddCollegeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "Bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(20)", nullable: false),
                    Password = table.Column<string>(type: "Varchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "Varchar(20)", nullable: false),
                    RoleId = table.Column<long>(type: "Bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(150)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserActivity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleControllerMapping",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControllerName = table.Column<string>(type: "varchar(250)", nullable: false),
                    ActionName = table.Column<string>(type: "varchar(150)", nullable: false),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleControllerMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleControllerMapping_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleControllerMapping_RoleId",
                table: "RoleControllerMapping",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "RoleControllerMapping");

            migrationBuilder.DropTable(
                name: "UserActivity");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
