using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Infrastructure.Migrations
{
    public partial class AddVersionToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Vehicles",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Vehicles");
        }
    }
}
