using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SA.Irrigation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Added_FinishCron : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishDelta",
                table: "Schedules");

            migrationBuilder.AddColumn<string>(
                name: "FinishCron",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishCron",
                table: "Schedules");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FinishDelta",
                table: "Schedules",
                type: "time",
                nullable: true);
        }
    }
}
