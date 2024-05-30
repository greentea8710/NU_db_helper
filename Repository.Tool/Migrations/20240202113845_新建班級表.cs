using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class 新建班級表 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Order",
                type: "numeric(2,1)",
                precision: 2,
                scale: 1,
                nullable: false,
                comment: "价格",
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldComment: "价格");

            migrationBuilder.CreateTable(
                name: "Classnumber",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Classnumber = table.Column<int>(type: "integer", nullable: false, comment: "班級號碼"),
                    Studentnum = table.Column<int>(type: "integer", nullable: false, comment: "學生人數"),
                    Grade = table.Column<int>(type: "integer", nullable: false, comment: "年級"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classnumber", x => x.Id);
                },
                comment: "TClassnumber");

            migrationBuilder.CreateIndex(
                name: "IX_Classnumber_CreateTime",
                table: "Classnumber",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Classnumber_DeleteTime",
                table: "Classnumber",
                column: "DeleteTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classnumber");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Order",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                comment: "价格",
                oldClrType: typeof(decimal),
                oldType: "numeric(2,1)",
                oldPrecision: 2,
                oldScale: 1,
                oldComment: "价格");
        }
    }
}
