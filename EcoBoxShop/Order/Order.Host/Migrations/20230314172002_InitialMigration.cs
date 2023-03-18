using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Host.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "order_list_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "order_list_item_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "OrderList",
                columns: table => new
                {
                    OrderListId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderList", x => x.OrderListId);
                });

            migrationBuilder.CreateTable(
                name: "OrderListItem",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    CatalogItemId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    SubTitle = table.Column<string>(type: "text", nullable: false),
                    PictureUrl = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderListId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderListItem", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_OrderListItem_OrderList_OrderListId",
                        column: x => x.OrderListId,
                        principalTable: "OrderList",
                        principalColumn: "OrderListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderListItem_OrderListId",
                table: "OrderListItem",
                column: "OrderListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderListItem");

            migrationBuilder.DropTable(
                name: "OrderList");

            migrationBuilder.DropSequence(
                name: "order_list_hilo");

            migrationBuilder.DropSequence(
                name: "order_list_item_hilo");
        }
    }
}
