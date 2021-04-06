using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlzLogin.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuperiorWorkOrderNumber = table.Column<int>(type: "int", nullable: false),
                    CustomerOrderNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPartNumber = table.Column<int>(type: "int", nullable: false),
                    ITAR = table.Column<bool>(type: "bit", nullable: false),
                    HOT = table.Column<bool>(type: "bit", nullable: false),
                    Metal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThicknessMin = table.Column<int>(type: "int", nullable: false),
                    ThicknessMax = table.Column<int>(type: "int", nullable: false),
                    SerialNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverallRequirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCarrier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingSpeed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobReciever = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Metals");
        }
    }
}
