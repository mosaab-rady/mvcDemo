using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcApp.Migrations
{
    public partial class addFabrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fabrics",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    material_and_care = table.Column<List<string>>(type: "text[]", nullable: true),
                    why_we_made_this = table.Column<string>(type: "text", nullable: false),
                    stretch = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    anti_billing = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    buttery_soft = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fabrics", x => x.name);
                });

            migrationBuilder.CreateIndex(
                name: "ix_fabrics_name",
                table: "fabrics",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fabrics");
        }
    }
}
