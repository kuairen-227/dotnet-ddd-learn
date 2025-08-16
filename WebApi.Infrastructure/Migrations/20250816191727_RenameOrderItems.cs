using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_item_orders_order_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_products_product_id",
                table: "order_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item",
                table: "order_item");

            migrationBuilder.RenameTable(
                name: "order_item",
                newName: "order_items");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_product_id",
                table: "order_items",
                newName: "ix_order_items_product_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_order_id",
                table: "order_items",
                newName: "ix_order_items_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_items",
                table: "order_items",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_items_orders_order_id",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_items_products_product_id",
                table: "order_items",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_items_orders_order_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "fk_order_items_products_product_id",
                table: "order_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_items",
                table: "order_items");

            migrationBuilder.RenameTable(
                name: "order_items",
                newName: "order_item");

            migrationBuilder.RenameIndex(
                name: "ix_order_items_product_id",
                table: "order_item",
                newName: "ix_order_item_product_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_items_order_id",
                table: "order_item",
                newName: "ix_order_item_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item",
                table: "order_item",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_orders_order_id",
                table: "order_item",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_products_product_id",
                table: "order_item",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
