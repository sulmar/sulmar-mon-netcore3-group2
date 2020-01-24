using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Infrastructure.Migrations
{
    public partial class AddGarageAddressToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GarageAddress",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GarageAddress",
                table: "Vehicles");
        }
    }
}
