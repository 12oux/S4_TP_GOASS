using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GOASS.Data.Migrations
{
    public partial class DBCONTEXT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Produit",
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
                    ProduitID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit", x => x.ProduitID);
                    table.ForeignKey(
                        name: "FK_Produit_Image_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Image",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produit_Produit_ProduitID1",
                        column: x => x.ProduitID1,
                        principalTable: "Produit",
                        principalColumn: "ProduitID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produit_ImageID",
                table: "Produit",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_ProduitID1",
                table: "Produit",
                column: "ProduitID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produit");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
