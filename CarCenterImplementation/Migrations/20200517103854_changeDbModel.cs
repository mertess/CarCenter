using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarCenterImplementation.Migrations
{
    public partial class changeDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarKits_Cars_CarId",
                table: "CarKits");

            migrationBuilder.DropIndex(
                name: "IX_CarKits_CarId",
                table: "CarKits");

            migrationBuilder.DropColumn(
                name: "SoldDate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "CarKits");

            migrationBuilder.AddColumn<int>(
                name: "BuiltCarId",
                table: "CarKits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BuiltCars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoldDate = table.Column<DateTime>(nullable: true),
                    CarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuiltCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuiltCars_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarKits_BuiltCarId",
                table: "CarKits",
                column: "BuiltCarId");

            migrationBuilder.CreateIndex(
                name: "IX_BuiltCars_CarId",
                table: "BuiltCars",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarKits_BuiltCars_BuiltCarId",
                table: "CarKits",
                column: "BuiltCarId",
                principalTable: "BuiltCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarKits_BuiltCars_BuiltCarId",
                table: "CarKits");

            migrationBuilder.DropTable(
                name: "BuiltCars");

            migrationBuilder.DropIndex(
                name: "IX_CarKits_BuiltCarId",
                table: "CarKits");

            migrationBuilder.DropColumn(
                name: "BuiltCarId",
                table: "CarKits");

            migrationBuilder.AddColumn<DateTime>(
                name: "SoldDate",
                table: "Cars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "CarKits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarKits_CarId",
                table: "CarKits",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarKits_Cars_CarId",
                table: "CarKits",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
