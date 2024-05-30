using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class 刪除四個無用的表 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_RegionArea_RegionAreaId",
                table: "UserInfo");

            migrationBuilder.DropTable(
                name: "RegionTown");

            migrationBuilder.DropTable(
                name: "RegionArea");

            migrationBuilder.DropTable(
                name: "RegionCity");

            migrationBuilder.DropTable(
                name: "RegionProvince");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_RegionAreaId",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "RegionAreaId",
                table: "UserInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionAreaId",
                table: "UserInfo",
                type: "integer",
                nullable: true,
                comment: "地址区域ID");

            migrationBuilder.CreateTable(
                name: "RegionProvince",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "主键标识ID"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    Province = table.Column<string>(type: "text", nullable: false, comment: "省份"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionProvince", x => x.Id);
                },
                comment: "省份信息表");

            migrationBuilder.CreateTable(
                name: "RegionCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "主键标识ID"),
                    ProvinceId = table.Column<int>(type: "integer", nullable: false, comment: "所属省份ID"),
                    City = table.Column<string>(type: "text", nullable: false, comment: "城市名称"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionCity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionCity_RegionProvince_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "RegionProvince",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "城市信息表");

            migrationBuilder.CreateTable(
                name: "RegionArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "主键标识ID"),
                    CityId = table.Column<int>(type: "integer", nullable: false, comment: "所属城市ID"),
                    Area = table.Column<string>(type: "text", nullable: false, comment: "区域名称"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionArea_RegionCity_CityId",
                        column: x => x.CityId,
                        principalTable: "RegionCity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "区域信息表");

            migrationBuilder.CreateTable(
                name: "RegionTown",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "主键标识ID"),
                    AreaId = table.Column<int>(type: "integer", nullable: false, comment: "所属区域ID"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    Town = table.Column<string>(type: "text", nullable: false, comment: "街道名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionTown", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionTown_RegionArea_AreaId",
                        column: x => x.AreaId,
                        principalTable: "RegionArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "TRegionTown");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_RegionAreaId",
                table: "UserInfo",
                column: "RegionAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionArea_Area",
                table: "RegionArea",
                column: "Area");

            migrationBuilder.CreateIndex(
                name: "IX_RegionArea_CityId",
                table: "RegionArea",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionArea_CreateTime",
                table: "RegionArea",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RegionArea_DeleteTime",
                table: "RegionArea",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_RegionCity_City",
                table: "RegionCity",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_RegionCity_CreateTime",
                table: "RegionCity",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RegionCity_DeleteTime",
                table: "RegionCity",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_RegionCity_ProvinceId",
                table: "RegionCity",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionProvince_CreateTime",
                table: "RegionProvince",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RegionProvince_DeleteTime",
                table: "RegionProvince",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_RegionProvince_Province",
                table: "RegionProvince",
                column: "Province");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTown_AreaId",
                table: "RegionTown",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTown_CreateTime",
                table: "RegionTown",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTown_DeleteTime",
                table: "RegionTown",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTown_Town",
                table: "RegionTown",
                column: "Town");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_RegionArea_RegionAreaId",
                table: "UserInfo",
                column: "RegionAreaId",
                principalTable: "RegionArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
