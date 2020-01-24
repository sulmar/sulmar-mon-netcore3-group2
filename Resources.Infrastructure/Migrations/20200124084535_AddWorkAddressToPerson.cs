using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Infrastructure.Migrations
{
    public partial class AddWorkAddressToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkAddress",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkAddress",
                table: "Persons");
        }
    }
}
