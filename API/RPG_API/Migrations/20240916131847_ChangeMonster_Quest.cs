using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPG_API.Migrations
{
    public partial class ChangeMonster_Quest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Monster_MonsterId",
                table: "Quest");

            migrationBuilder.DropIndex(
                name: "IX_Quest_MonsterId",
                table: "Quest");

            migrationBuilder.DropColumn(
                name: "MonsterId",
                table: "Quest");

            migrationBuilder.CreateTable(
                name: "MonsterQuest",
                columns: table => new
                {
                    MonsterId = table.Column<int>(type: "int", nullable: false),
                    QuestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterQuest", x => new { x.MonsterId, x.QuestId });
                    table.ForeignKey(
                        name: "FK_MonsterQuest_Monster_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonsterQuest_Quest_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterQuest_QuestId",
                table: "MonsterQuest",
                column: "QuestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonsterQuest");

            migrationBuilder.AddColumn<int>(
                name: "MonsterId",
                table: "Quest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quest_MonsterId",
                table: "Quest",
                column: "MonsterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_Monster_MonsterId",
                table: "Quest",
                column: "MonsterId",
                principalTable: "Monster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
