using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GOASS.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commande",
                columns: table => new
                {
                    CommandeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.CommandeID);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "Panier",
                columns: table => new
                {
                    PanierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panier", x => x.PanierID);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    ProduitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomProduit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taille = table.Column<int>(type: "int", nullable: false),
                    Quantité = table.Column<int>(type: "int", nullable: false),
                    Sport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageID = table.Column<int>(type: "int", nullable: true),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    ProduitID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.ProduitID);
                    table.ForeignKey(
                        name: "FK_Produits_Image_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Image",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produits_Produits_ProduitID1",
                        column: x => x.ProduitID1,
                        principalTable: "Produits",
                        principalColumn: "ProduitID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemCommande",
                columns: table => new
                {
                    ItemCommandeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProduitID = table.Column<int>(type: "int", nullable: false),
                    CommandeID = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    MontantUnitaire = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCommande", x => x.ItemCommandeID);
                    table.ForeignKey(
                        name: "FK_ItemCommande_Commande_CommandeID",
                        column: x => x.CommandeID,
                        principalTable: "Commande",
                        principalColumn: "CommandeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCommande_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ProduitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPanier",
                columns: table => new
                {
                    ItemPanierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProduitID = table.Column<int>(type: "int", nullable: false),
                    PanierID = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPanier", x => x.ItemPanierID);
                    table.ForeignKey(
                        name: "FK_ItemPanier_Panier_PanierID",
                        column: x => x.PanierID,
                        principalTable: "Panier",
                        principalColumn: "PanierID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPanier_Produits_ProduitID",
                        column: x => x.ProduitID,
                        principalTable: "Produits",
                        principalColumn: "ProduitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCommande_CommandeID",
                table: "ItemCommande",
                column: "CommandeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCommande_ProduitID",
                table: "ItemCommande",
                column: "ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPanier_PanierID",
                table: "ItemPanier",
                column: "PanierID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPanier_ProduitID",
                table: "ItemPanier",
                column: "ProduitID");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_ImageID",
                table: "Produits",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_ProduitID1",
                table: "Produits",
                column: "ProduitID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCommande");

            migrationBuilder.DropTable(
                name: "ItemPanier");

            migrationBuilder.DropTable(
                name: "Commande");

            migrationBuilder.DropTable(
                name: "Panier");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
