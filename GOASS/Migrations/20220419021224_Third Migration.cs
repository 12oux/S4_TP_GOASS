using Microsoft.EntityFrameworkCore.Migrations;

namespace GOASS.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemCommande_CommandeID",
                table: "ItemCommande");

            migrationBuilder.AddColumn<int>(
                name: "ProduitID",
                table: "Commande",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemCommande_CommandeID",
                table: "ItemCommande",
                column: "CommandeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commande_ProduitID",
                table: "Commande",
                column: "ProduitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_Produits_ProduitID",
                table: "Commande",
                column: "ProduitID",
                principalTable: "Produits",
                principalColumn: "ProduitID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commande_Produits_ProduitID",
                table: "Commande");

            migrationBuilder.DropIndex(
                name: "IX_ItemCommande_CommandeID",
                table: "ItemCommande");

            migrationBuilder.DropIndex(
                name: "IX_Commande_ProduitID",
                table: "Commande");

            migrationBuilder.DropColumn(
                name: "ProduitID",
                table: "Commande");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCommande_CommandeID",
                table: "ItemCommande",
                column: "CommandeID");
        }
    }
}
