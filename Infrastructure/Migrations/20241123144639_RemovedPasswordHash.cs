using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSaltTable");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "UserTable",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserTable",
                newName: "PasswordHash");

            migrationBuilder.CreateTable(
                name: "UserSaltTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSaltTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSaltTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSaltTable_UserId",
                table: "UserSaltTable",
                column: "UserId");
        }
    }
}
