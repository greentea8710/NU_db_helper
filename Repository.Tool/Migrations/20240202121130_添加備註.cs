using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class 添加備註 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Subject",
                comment: "科目表",
                oldComment: "TSubject");

            migrationBuilder.AlterTable(
                name: "Score",
                comment: "成績表",
                oldComment: "TScore");

            migrationBuilder.AlterTable(
                name: "Classnumber",
                comment: "班級表",
                oldComment: "TClassnumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Subject",
                comment: "TSubject",
                oldComment: "科目表");

            migrationBuilder.AlterTable(
                name: "Score",
                comment: "TScore",
                oldComment: "成績表");

            migrationBuilder.AlterTable(
                name: "Classnumber",
                comment: "TClassnumber",
                oldComment: "班級表");
        }
    }
}
