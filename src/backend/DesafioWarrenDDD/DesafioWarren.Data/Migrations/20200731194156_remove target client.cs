using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioWarren.Data.Migrations
{
    public partial class removetargetclient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TARGETACCOUNT_ACCOUNTMOVEMENT",
                table: "AccountMovement");

            migrationBuilder.DropIndex(
                name: "IX_AccountMovement_TargetAccountId",
                table: "AccountMovement");

            migrationBuilder.DropColumn(
                name: "TargetAccountId",
                table: "AccountMovement");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AccountMovement",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AccountMovement");

            migrationBuilder.AddColumn<int>(
                name: "TargetAccountId",
                table: "AccountMovement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccountMovement_TargetAccountId",
                table: "AccountMovement",
                column: "TargetAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TARGETACCOUNT_ACCOUNTMOVEMENT",
                table: "AccountMovement",
                column: "TargetAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
