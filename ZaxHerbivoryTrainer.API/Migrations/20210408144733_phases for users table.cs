using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaxHerbivoryTrainer.API.Migrations
{
    public partial class phasesforuserstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedUtc",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FinishingPercent",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PictureCycled",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedPhase1Utc",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedPhase2Utc",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedPhase3Utc",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinishingPercentPhase1",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinishingPercentPhase2",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinishingPercentPhase3",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureCycledPhase1",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureCycledPhase2",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureCycledPhase3",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePhase1",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePhase2",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePhase3",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedPhase1Utc",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FinishedPhase2Utc",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FinishedPhase3Utc",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FinishingPercentPhase1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FinishingPercentPhase2",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FinishingPercentPhase3",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PictureCycledPhase1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PictureCycledPhase2",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PictureCycledPhase3",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TimePhase1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TimePhase2",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TimePhase3",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedUtc",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinishingPercent",
                table: "User",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureCycled",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "User",
                type: "datetime2",
                nullable: true);
        }
    }
}
