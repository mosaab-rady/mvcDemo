using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcApp.Migrations
{
    public partial class addFabricToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fabric_name",
                table: "products",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_fabric_name",
                table: "products",
                column: "fabric_name");

            migrationBuilder.AddForeignKey(
                name: "fk_products_fabrics_fabric_name",
                table: "products",
                column: "fabric_name",
                principalTable: "fabrics",
                principalColumn: "name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_fabrics_fabric_name",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_products_fabric_name",
                table: "products");

            migrationBuilder.DropColumn(
                name: "fabric_name",
                table: "products");
        }
    }
}
