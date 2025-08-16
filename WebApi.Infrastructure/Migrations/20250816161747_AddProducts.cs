using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "unit_price",
                table: "order_item");

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_item_product_id",
                table: "order_item",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_products_product_id",
                table: "order_item",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_item_products_product_id",
                table: "order_item");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropIndex(
                name: "ix_order_item_product_id",
                table: "order_item");

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "unit_price",
                table: "order_item",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
