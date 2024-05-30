using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class 刪除users表 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Account = table.Column<string>(type: "text", nullable: false, comment: "帳號"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                },
                comment: "登入人員表");

            migrationBuilder.CreateIndex(
                name: "IX_users_CreateTime",
                table: "users",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_users_DeleteTime",
                table: "users",
                column: "DeleteTime");
        }
    }
}
