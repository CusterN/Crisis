using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crisis.Migrations
{
    public partial class AddingReopenDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReopenDate",
                table: "Supplier",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReopenDate",
                table: "Supplier");
        }
    }
}
