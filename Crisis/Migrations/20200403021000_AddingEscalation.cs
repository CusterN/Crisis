using Microsoft.EntityFrameworkCore.Migrations;

namespace Crisis.Migrations
{
    public partial class AddingEscalation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EscalationId",
                table: "Supplier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Escalation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 20, nullable: false),
                    Hint = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_EscalationId",
                table: "Supplier",
                column: "EscalationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Escalation_EscalationId",
                table: "Supplier",
                column: "EscalationId",
                principalTable: "Escalation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Escalation_EscalationId",
                table: "Supplier");

            migrationBuilder.DropTable(
                name: "Escalation");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_EscalationId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "EscalationId",
                table: "Supplier");
        }
    }
}
