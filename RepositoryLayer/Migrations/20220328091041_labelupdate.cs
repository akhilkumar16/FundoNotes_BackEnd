using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class labelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labeltables_Notestables_NoteId",
                table: "Labeltables");

            migrationBuilder.AlterColumn<long>(
                name: "NoteId",
                table: "Labeltables",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Labeltables_Notestables_NoteId",
                table: "Labeltables",
                column: "NoteId",
                principalTable: "Notestables",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labeltables_Notestables_NoteId",
                table: "Labeltables");

            migrationBuilder.AlterColumn<long>(
                name: "NoteId",
                table: "Labeltables",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Labeltables_Notestables_NoteId",
                table: "Labeltables",
                column: "NoteId",
                principalTable: "Notestables",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
