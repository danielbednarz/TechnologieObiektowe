using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkProject.Migrations.MainDatabaseMigrations
{
    public partial class ChangeTableNameToRecipeMedicaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicamentRecipes");

            migrationBuilder.CreateTable(
                name: "RecipeMedicaments",
                columns: table => new
                {
                    MedicamentId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMedicaments", x => new { x.MedicamentId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_RecipeMedicaments_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeMedicaments_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMedicaments_RecipeId",
                table: "RecipeMedicaments",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeMedicaments");

            migrationBuilder.CreateTable(
                name: "MedicamentRecipes",
                columns: table => new
                {
                    MedicamentId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentRecipes", x => new { x.MedicamentId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_MedicamentRecipes_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentRecipes_RecipeId",
                table: "MedicamentRecipes",
                column: "RecipeId");
        }
    }
}
