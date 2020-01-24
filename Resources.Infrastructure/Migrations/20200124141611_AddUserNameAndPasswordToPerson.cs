using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Infrastructure.Migrations
{
    public partial class AddUserNameAndPasswordToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashPassword",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Persons");
        }
    }
}
