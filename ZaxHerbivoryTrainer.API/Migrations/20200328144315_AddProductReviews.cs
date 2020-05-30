using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaxHerbivoryTrainer.API.Migrations
{
    public partial class AddProductReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedUtc",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "FinishingPercent",
                table: "User",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "HashUser",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureCycled",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FinishedUtc",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FinishingPercent",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HashUser",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PictureCycled",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "User");
        }
    }
}
