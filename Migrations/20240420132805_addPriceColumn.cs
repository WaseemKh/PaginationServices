using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pagination_API.Migrations
{
    public partial class addPriceColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "items",
                type: "decimal(10,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "items");
        }
    }
}
