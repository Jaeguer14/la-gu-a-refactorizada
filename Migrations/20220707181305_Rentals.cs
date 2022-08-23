using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appmovie.Migrations
{
    public partial class Rentals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    RentalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.RentalID);
                    table.ForeignKey(
                        name: "FK_Rental_Partner_PartnerID",
                        column: x => x.PartnerID,
                        principalTable: "Partner",
                        principalColumn: "PartnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalDetail",
                columns: table => new
                {
                    RentalDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalDetail", x => x.RentalDetailID);
                    table.ForeignKey(
                        name: "FK_RentalDetail_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalDetail_Rental_RentalID",
                        column: x => x.RentalID,
                        principalTable: "Rental",
                        principalColumn: "RentalID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rental_PartnerID",
                table: "Rental",
                column: "PartnerID");

            migrationBuilder.CreateIndex(
                name: "IX_RentalDetail_MovieID",
                table: "RentalDetail",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_RentalDetail_RentalID",
                table: "RentalDetail",
                column: "RentalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalDetail");

            migrationBuilder.DropTable(
                name: "Rental");
        }
    }
}
