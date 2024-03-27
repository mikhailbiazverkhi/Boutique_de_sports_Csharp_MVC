using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_test.Migrations
{
    /// <inheritdoc />
    public partial class PanierModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StatutAchat",
                table: "Panier",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeAchat",
                table: "Panier",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatutAchat",
                table: "Panier");

            migrationBuilder.DropColumn(
                name: "TimeAchat",
                table: "Panier");
        }
    }
}
