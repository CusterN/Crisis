using Microsoft.EntityFrameworkCore.Migrations;

namespace Crisis.Migrations
{
    public partial class ChangingFieldNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "FileTitle",
                table: "Attachment");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Attachment",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Attachment");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Attachment",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileTitle",
                table: "Attachment",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
