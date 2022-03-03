using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Collaborator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTables",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FristName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTables", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Notestables",
                columns: table => new
                {
                    NoteId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Discription = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Backgroundcolour = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Delete = table.Column<bool>(nullable: false),
                    Pin = table.Column<bool>(nullable: false),
                    Archive = table.Column<bool>(nullable: false),
                    Reminder = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notestables", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notestables_UserTables_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTables",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Colltables",
                columns: table => new
                {
                    CollId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollEmail = table.Column<string>(nullable: true),
                    NoteId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colltables", x => x.CollId);
                    table.ForeignKey(
                        name: "FK_Colltables_Notestables_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notestables",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Colltables_UserTables_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTables",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colltables_NoteId",
                table: "Colltables",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Colltables_UserId",
                table: "Colltables",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notestables_UserId",
                table: "Notestables",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colltables");

            migrationBuilder.DropTable(
                name: "Notestables");

            migrationBuilder.DropTable(
                name: "UserTables");
        }
    }
}
