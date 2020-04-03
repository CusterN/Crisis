using Microsoft.EntityFrameworkCore.Migrations;

namespace Crisis.Migrations
{
    public partial class MakeEscalationRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Escalation_EscalationId",
                table: "Supplier");

            migrationBuilder.AlterColumn<int>(
                name: "EscalationId",
                table: "Supplier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Escalation_EscalationId",
                table: "Supplier",
                column: "EscalationId",
                principalTable: "Escalation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Escalation_EscalationId",
                table: "Supplier");

            migrationBuilder.AlterColumn<int>(
                name: "EscalationId",
                table: "Supplier",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Escalation_EscalationId",
                table: "Supplier",
                column: "EscalationId",
                principalTable: "Escalation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
