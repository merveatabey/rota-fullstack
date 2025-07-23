using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rota.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuestCount",
                table: "Reservations",
                newName: "ChildCount");

            migrationBuilder.AddColumn<int>(
                name: "AdultCount",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultCount",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ChildCount",
                table: "Reservations",
                newName: "GuestCount");
        }
    }
}
