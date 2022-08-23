using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appmovie.Migrations
{
    public partial class estaalquiladaTempModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaAlquilada",
                table: "Movie",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RentalDetailTemp",
                columns: table => new
                {
                    RentalDetailTempID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalDetailTemp", x => x.RentalDetailTempID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalDetailTemp");

            migrationBuilder.DropColumn(
                name: "EstaAlquilada",
                table: "Movie");
        }
    }
}
