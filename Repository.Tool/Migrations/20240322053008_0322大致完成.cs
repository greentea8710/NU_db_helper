using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class _0322大致完成 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    MailId = table.Column<long>(type: "bigint", nullable: false, comment: "MailId"),
                    Content = table.Column<string>(type: "text", nullable: false, comment: "信件內文"),
                    SenderEmail = table.Column<string>(type: "text", nullable: false, comment: "寄件者"),
                    ReceiverEmail = table.Column<string>(type: "text", nullable: false, comment: "收件者"),
                    Subject = table.Column<string>(type: "text", nullable: false, comment: "標題"),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "成功寄送時間"),
                    Success = table.Column<bool>(type: "boolean", nullable: false, comment: "是否寄送成功"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail", x => x.Id);
                },
                comment: "信件資訊");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_CreateTime",
                table: "Mail",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_DeleteTime",
                table: "Mail",
                column: "DeleteTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mail");
        }
    }
}
