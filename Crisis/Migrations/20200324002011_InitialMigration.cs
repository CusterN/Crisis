using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crisis.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.CreateTable(
                    name: "Category",
                    columns: table => new
                    {
                        Id = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Description = table.Column<string>(maxLength: 20, nullable: false),
                        Hint = table.Column<string>(maxLength: 450, nullable: false),
                        Visible = table.Column<bool>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Category", x => x.Id);
                    });

                migrationBuilder.CreateTable(
                    name: "Status",
                    columns: table => new
                    {
                        Id = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Description = table.Column<string>(maxLength: 20, nullable: false),
                        Hint = table.Column<string>(maxLength: 450, nullable: false),
                        Visible = table.Column<bool>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Status", x => x.Id);
                    });

                migrationBuilder.CreateTable(
                    name: "Supplier",
                    columns: table => new
                    {
                        Id = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        SupplierNo = table.Column<string>(maxLength: 6, nullable: false),
                        CreateDate = table.Column<DateTime>(nullable: false),
                        Creator = table.Column<string>(maxLength: 60, nullable: false),
                        Visible = table.Column<bool>(nullable: false),
                        StatusId = table.Column<int>(nullable: false),
                        CategoryId = table.Column<int>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Supplier", x => x.Id);
                        table.UniqueConstraint("AK_Supplier_SupplierNo", x => x.SupplierNo);
                        table.ForeignKey(
                            name: "FK_Supplier_Category_CategoryId",
                            column: x => x.CategoryId,
                            principalTable: "Category",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_Supplier_Status_StatusId",
                            column: x => x.StatusId,
                            principalTable: "Status",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateTable(
                    name: "Comment",
                    columns: table => new
                    {
                        Id = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Body = table.Column<string>(maxLength: 450, nullable: false),
                        Creator = table.Column<string>(maxLength: 60, nullable: false),
                        CreateDate = table.Column<DateTime>(nullable: false),
                        Visible = table.Column<bool>(nullable: false),
                        SupplierId = table.Column<int>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Comment", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Comment_Supplier_SupplierId",
                            column: x => x.SupplierId,
                            principalTable: "Supplier",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_Comment_SupplierId",
                    table: "Comment",
                    column: "SupplierId");

                migrationBuilder.CreateIndex(
                    name: "IX_Supplier_CategoryId",
                    table: "Supplier",
                    column: "CategoryId");

                migrationBuilder.CreateIndex(
                    name: "IX_Supplier_StatusId",
                    table: "Supplier",
                    column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
