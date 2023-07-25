using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleEcommerceAspNet6.Migrations
{
    public partial class addShipper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShipperId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipperId",
                table: "Orders",
                column: "ShipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippers_ShipperId",
                table: "Orders",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "ShipperId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippers_ShipperId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipperId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "Orders");
        }
    }
}
