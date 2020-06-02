using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarCenterImplementation.Migrations
{
    public partial class changebuiltcarmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarKits");

            migrationBuilder.AddColumn<int>(
                name: "FinalCost",
                table: "BuiltCars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BuiltCarKits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallationDate = table.Column<DateTime>(nullable: false),
                    RemovedFromStorages = table.Column<bool>(nullable: false),
                    KitCount = table.Column<int>(nullable: false),
                    KitId = table.Column<int>(nullable: false),
                    BuiltCarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuiltCarKits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuiltCarKits_BuiltCars_BuiltCarId",
                        column: x => x.BuiltCarId,
                        principalTable: "BuiltCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuiltCarKits_Kits_KitId",
                        column: x => x.KitId,
                        principalTable: "Kits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "StorageName" },
                values: new object[,]
                {
                    { 1, "Хранилище 1" },
                    { 2, "Хранилище 2" },
                    { 3, "Хранилище 3" },
                    { 4, "Хранилище 4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuiltCarKits_BuiltCarId",
                table: "BuiltCarKits",
                column: "BuiltCarId");

            migrationBuilder.CreateIndex(
                name: "IX_BuiltCarKits_KitId",
                table: "BuiltCarKits",
                column: "KitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuiltCarKits");

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "FinalCost",
                table: "BuiltCars");

            migrationBuilder.CreateTable(
                name: "CarKits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuiltCarId = table.Column<int>(type: "int", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KitCount = table.Column<int>(type: "int", nullable: false),
                    KitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarKits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarKits_BuiltCars_BuiltCarId",
                        column: x => x.BuiltCarId,
                        principalTable: "BuiltCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarKits_Kits_KitId",
                        column: x => x.KitId,
                        principalTable: "Kits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarKits_BuiltCarId",
                table: "CarKits",
                column: "BuiltCarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarKits_KitId",
                table: "CarKits",
                column: "KitId");
        }
    }
}
