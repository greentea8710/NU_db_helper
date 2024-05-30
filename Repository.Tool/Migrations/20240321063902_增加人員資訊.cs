using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class 增加人員資訊 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "People",
                comment: "人員表",
                oldComment: "TPeople");

            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "People",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "信箱");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "People",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "人員名字");

            migrationBuilder.AddColumn<long>(
                name: "PeopleId",
                table: "People",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "PeopleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PeopleId",
                table: "People");

            migrationBuilder.AlterTable(
                name: "People",
                comment: "TPeople",
                oldComment: "人員表");
        }
    }
}
