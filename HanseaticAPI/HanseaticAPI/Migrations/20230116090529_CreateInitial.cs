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
                name: "CityProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityid = table.Column<int>(name: "city_id", type: "int", nullable: false),
                    producttype = table.Column<int>(name: "product_type", type: "int", nullable: false),
                    desiredamount = table.Column<int>(name: "desired_amount", type: "int", nullable: false),
                    actualamount = table.Column<int>(name: "actual_amount", type: "int", nullable: false),
                    sellprice = table.Column<int>(name: "sell_price", type: "int", nullable: false),
                    buyprice = table.Column<int>(name: "buy_price", type: "int", nullable: false),
                    minfluctation = table.Column<double>(name: "min_fluctation", type: "float", nullable: false),
                    maxfluctation = table.Column<double>(name: "max_fluctation", type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityProducts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityProducts");
        }
    }
}
