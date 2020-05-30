using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaxHerbivoryTrainer.API.Migrations
{
    public partial class LoginTokenTablechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenString",
                table: "Tokens");

            migrationBuilder.AddColumn<string>(
                name: "UserGuid",
                table: "Tokens",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tokens");

            migrationBuilder.AddColumn<string>(
                name: "TokenString",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
