using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPG_API.Migrations
{
    public partial class relationEntreQuestItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Quest");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Quest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quest_ItemId",
                table: "Quest",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_Item_ItemId",
                table: "Quest",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Item_ItemId",
                table: "Quest");

            migrationBuilder.DropIndex(
                name: "IX_Quest_ItemId",
                table: "Quest");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Quest");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Quest",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
