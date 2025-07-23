using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rota.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeletedGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Users_GuideId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_GuideId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "GuideId",
                table: "Tours");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GuideId",
                table: "Tours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_GuideId",
                table: "Tours",
                column: "GuideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Users_GuideId",
                table: "Tours",
                column: "GuideId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
