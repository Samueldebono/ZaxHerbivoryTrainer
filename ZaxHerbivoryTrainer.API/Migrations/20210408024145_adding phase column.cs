using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaxHerbivoryTrainer.API.Migrations
{
    public partial class addingphasecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Phase",
                table: "UsersGuess",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phase",
                table: "UsersGuess");
        }
    }
}
