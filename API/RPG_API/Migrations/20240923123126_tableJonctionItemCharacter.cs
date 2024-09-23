using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPG_API.Migrations
{
    public partial class tableJonctionItemCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEquipped",
                table: "Item");

            migrationBuilder.CreateTable(
                name: "JonctionItemCharacter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JonctionItemCharacter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JonctionItemCharacter_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JonctionItemCharacter_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_JonctionItemCharacter_CharacterId",
                table: "JonctionItemCharacter",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_JonctionItemCharacter_ItemId",
                table: "JonctionItemCharacter",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JonctionItemCharacter");

            migrationBuilder.AddColumn<bool>(
                name: "IsEquipped",
                table: "Item",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
