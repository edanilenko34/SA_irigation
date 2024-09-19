using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SA.Irrigation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Fix_link_null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Models_ModelId",
                table: "Devices");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModelId",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Models_ModelId",
                table: "Devices",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Models_ModelId",
                table: "Devices");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModelId",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Models_ModelId",
                table: "Devices",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
