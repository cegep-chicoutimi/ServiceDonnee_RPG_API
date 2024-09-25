using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPG_API.Migrations
{
    public partial class relationEntreQuestItemNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Item_ItemId",
                table: "Quest");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Quest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_Item_ItemId",
                table: "Quest",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Item_ItemId",
                table: "Quest");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Quest",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_Item_ItemId",
                table: "Quest",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
