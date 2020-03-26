using Microsoft.EntityFrameworkCore.Migrations;

namespace Crisis.Migrations
{
    public partial class RemoveVisibleField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Supplier",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Status",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Comment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
