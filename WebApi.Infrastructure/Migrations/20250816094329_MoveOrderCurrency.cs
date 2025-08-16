using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveOrderCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency",
                table: "order_item");

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "order_item",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
