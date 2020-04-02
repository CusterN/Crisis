using Microsoft.EntityFrameworkCore.Migrations;

namespace Crisis.Migrations
{
    public partial class AddingAttachmentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttachmentTypeId",
                table: "Attachment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AttachmentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 20, nullable: false),
                    Hint = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_AttachmentTypeId",
                table: "Attachment",
                column: "AttachmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_AttachmentType_AttachmentTypeId",
                table: "Attachment",
                column: "AttachmentTypeId",
                principalTable: "AttachmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_AttachmentType_AttachmentTypeId",
                table: "Attachment");

            migrationBuilder.DropTable(
                name: "AttachmentType");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_AttachmentTypeId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "AttachmentTypeId",
                table: "Attachment");
        }
    }
}
