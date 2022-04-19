using Microsoft.EntityFrameworkCore.Migrations;

namespace GOASS.Migrations
{
    public partial class FinalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "ItemCommande",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemCommande_ImageID",
                table: "ItemCommande",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCommande_Image_ImageID",
                table: "ItemCommande",
                column: "ImageID",
                principalTable: "Image",
                principalColumn: "ImageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCommande_Image_ImageID",
                table: "ItemCommande");

            migrationBuilder.DropIndex(
                name: "IX_ItemCommande_ImageID",
                table: "ItemCommande");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "ItemCommande");
        }
    }
}
