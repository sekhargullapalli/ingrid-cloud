using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterMind.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mmGame",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guid = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mmGame", x => x.GameID);
                });

            migrationBuilder.CreateTable(
                name: "mmGamePattern",
                columns: table => new
                {
                    GamePatternID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Level = table.Column<int>(nullable: false),
                    CodePeg1 = table.Column<string>(nullable: false),
                    CodePeg2 = table.Column<string>(nullable: false),
                    CodePeg3 = table.Column<string>(nullable: false),
                    CodePeg4 = table.Column<string>(nullable: false),
                    KeyPeg1 = table.Column<string>(nullable: false),
                    KeyPeg2 = table.Column<string>(nullable: false),
                    KeyPeg3 = table.Column<string>(nullable: false),
                    KeyPeg4 = table.Column<string>(nullable: false),
                    GameID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mmGamePattern", x => x.GamePatternID);
                    table.ForeignKey(
                        name: "FK_mmGamePattern_mmGame_GameID",
                        column: x => x.GameID,
                        principalTable: "mmGame",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mmGamePattern_GameID",
                table: "mmGamePattern",
                column: "GameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mmGamePattern");

            migrationBuilder.DropTable(
                name: "mmGame");
        }
    }
}
