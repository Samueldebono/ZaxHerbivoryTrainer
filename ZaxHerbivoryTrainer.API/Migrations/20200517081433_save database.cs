using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaxHerbivoryTrainer.API.Migrations
{
    public partial class savedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "RoleType",
                table: "AuthUsers",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleType",
                table: "AuthUsers");
        }
    }
}
