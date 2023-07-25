using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleEcommerceAspNet6.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAddresses_Users_CustomerUserId",
                table: "DeliveryAddresses");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerUserId",
                table: "DeliveryAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAddresses_Users_CustomerUserId",
                table: "DeliveryAddresses",
                column: "CustomerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAddresses_Users_CustomerUserId",
                table: "DeliveryAddresses");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryAddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerUserId",
                table: "DeliveryAddresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAddresses_Users_CustomerUserId",
                table: "DeliveryAddresses",
                column: "CustomerUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
