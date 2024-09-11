using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPG_API.Migrations
{
    public partial class NewRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Character_CharacterId",
                table: "Quest");

            migrationBuilder.DropIndex(
                name: "IX_Quest_CharacterId",
                table: "Quest");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Quest");

            migrationBuilder.CreateTable(
                name: "CharacterQuest",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    QuestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterQuest", x => new { x.CharactersId, x.QuestsId });
                    table.ForeignKey(
                        name: "FK_CharacterQuest_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterQuest_Quest_QuestsId",
                        column: x => x.QuestsId,
                        principalTable: "Quest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterQuest_QuestsId",
                table: "CharacterQuest",
                column: "QuestsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterQuest");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Quest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quest_CharacterId",
                table: "Quest",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_Character_CharacterId",
                table: "Quest",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
