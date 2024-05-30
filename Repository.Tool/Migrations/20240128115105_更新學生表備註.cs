using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class 更新學生表備註 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Student",
                comment: "學生表",
                oldComment: "TStudent");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Student",
                type: "integer",
                nullable: false,
                comment: "座號",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Number");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Student",
                type: "text",
                nullable: false,
                comment: "姓名",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Name");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Student",
                type: "boolean",
                nullable: false,
                comment: "性別",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldComment: "Gender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Student",
                comment: "TStudent",
                oldComment: "學生表");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Student",
                type: "integer",
                nullable: false,
                comment: "Number",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "座號");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Student",
                type: "text",
                nullable: false,
                comment: "Name",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "姓名");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Student",
                type: "boolean",
                nullable: false,
                comment: "Gender",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldComment: "性別");
        }
    }
}
