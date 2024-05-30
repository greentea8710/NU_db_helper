using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class 手機號碼可不填寫 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Student",
                type: "text",
                nullable: true,
                comment: "手機號碼",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "手機號碼");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Student",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "手機號碼",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "手機號碼");
        }
    }
}
