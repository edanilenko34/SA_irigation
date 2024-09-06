using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SA.Irrigation.Db.Migrations
{
    /// <inheritdoc />
    public partial class Devices_and_Schedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartCron = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinishBy = table.Column<int>(type: "int", nullable: false),
                    FinishDelta = table.Column<TimeSpan>(type: "time", nullable: true),
                    ParentDeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinishDeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FinishValue = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Devices_FinishDeviceId",
                        column: x => x.FinishDeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Devices_ParentDeviceId",
                        column: x => x.ParentDeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Address",
                table: "Devices",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ModelId",
                table: "Devices",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FinishDeviceId",
                table: "Schedules",
                column: "FinishDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ParentDeviceId",
                table: "Schedules",
                column: "ParentDeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
