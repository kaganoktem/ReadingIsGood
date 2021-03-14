using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadingIsGood.Migrations
{
    public partial class UpdatedCreateDockerMSSQLDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "BookDeliveryInformations",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "bookId",
                table: "BookDeliveryInformations",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BooksStocks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BookDeliveryStatus",
                table: "BookDeliveryInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "BookDeliveryInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookDeliveryInformations_OrderId",
                table: "BookDeliveryInformations",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDeliveryInformations_Orders_OrderId",
                table: "BookDeliveryInformations",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDeliveryInformations_Orders_OrderId",
                table: "BookDeliveryInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_BookDeliveryInformations_OrderId",
                table: "BookDeliveryInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BookDeliveryStatus",
                table: "BookDeliveryInformations");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "BookDeliveryInformations");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookDeliveryInformations",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookDeliveryInformations",
                newName: "bookId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "BooksStocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
