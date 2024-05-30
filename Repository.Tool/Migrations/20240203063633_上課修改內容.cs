using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class 上課修改內容 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classnumber");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "Studentname",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Score");

            migrationBuilder.RenameColumn(
                name: "Subjectname",
                table: "Subject",
                newName: "Name");

            migrationBuilder.AddColumn<long>(
                name: "ClassId",
                table: "Student",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "ClassId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ScoreDate",
                table: "Score",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                comment: "日期");

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Score",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "StudentId");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                table: "Score",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "SubjectId");

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Number = table.Column<int>(type: "integer", nullable: false, comment: "班級號碼"),
                    Grade = table.Column<int>(type: "integer", nullable: false, comment: "年級"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                },
                comment: "班級表");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassId",
                table: "Student",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_StudentId",
                table: "Score",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_SubjectId",
                table: "Score",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_CreateTime",
                table: "Class",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Class_DeleteTime",
                table: "Class",
                column: "DeleteTime");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Student_StudentId",
                table: "Score",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Subject_SubjectId",
                table: "Score",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Class_ClassId",
                table: "Student",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Student_StudentId",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_Subject_SubjectId",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Class_ClassId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropIndex(
                name: "IX_Student_ClassId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Score_StudentId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_SubjectId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ScoreDate",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Score");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subject",
                newName: "Subjectname");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Score",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "日期");

            migrationBuilder.AddColumn<string>(
                name: "Studentname",
                table: "Score",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "學生名稱");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Score",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "科目");

            migrationBuilder.CreateTable(
                name: "Classnumber",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Classnumber = table.Column<int>(type: "integer", nullable: false, comment: "班級號碼"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    Grade = table.Column<int>(type: "integer", nullable: false, comment: "年級"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    Studentnum = table.Column<int>(type: "integer", nullable: false, comment: "學生人數")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classnumber", x => x.Id);
                },
                comment: "班級表");

            migrationBuilder.CreateIndex(
                name: "IX_Classnumber_CreateTime",
                table: "Classnumber",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Classnumber_DeleteTime",
                table: "Classnumber",
                column: "DeleteTime");
        }
    }
}
