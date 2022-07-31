using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcApp.Migrations
{
	public partial class initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "products",
					columns: table => new
					{
						id = table.Column<Guid>(type: "uuid", nullable: false),
						name = table.Column<string>(type: "text", nullable: false),
						price = table.Column<decimal>(type: "numeric", nullable: false),
						type = table.Column<string>(type: "text", nullable: false),
						summary = table.Column<string>(type: "text", nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("pk_products", x => x.id);
					});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "products");
		}
	}
}
