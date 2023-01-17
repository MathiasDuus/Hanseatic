using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HanseaticAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<int>(type: "int", nullable: false),
                    DesiredAmount = table.Column<int>(type: "int", nullable: false),
                    ActualAmount = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    MinAmountFluctation = table.Column<double>(type: "float", nullable: false),
                    MaxAmountFluctation = table.Column<double>(type: "float", nullable: false),
                    Save = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityProducts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityProducts_CityId",
                table: "CityProducts",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityProducts");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
