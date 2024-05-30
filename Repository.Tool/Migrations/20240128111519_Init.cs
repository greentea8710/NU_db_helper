using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Tool.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.CreateTable(
                name: "AppSetting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Module = table.Column<string>(type: "text", nullable: false, comment: "模块"),
                    GroupId = table.Column<long>(type: "bigint", nullable: true, comment: "组ID"),
                    Key = table.Column<string>(type: "text", nullable: false, comment: "键名"),
                    Value = table.Column<string>(type: "text", nullable: false, comment: "键值"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSetting", x => x.Id);
                },
                comment: "系统配置信息表");

            migrationBuilder.CreateTable(
                name: "Function",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "名称"),
                    Sign = table.Column<string>(type: "text", nullable: false, comment: "标记"),
                    Remarks = table.Column<string>(type: "text", nullable: true, comment: "备注"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true, comment: "父级信息"),
                    Type = table.Column<int>(type: "integer", nullable: false, comment: "类型"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Function", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Function_Function_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "系统功能配置表");

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Project = table.Column<string>(type: "text", nullable: false, comment: "项目"),
                    MachineName = table.Column<string>(type: "text", nullable: false, comment: "机器名称"),
                    Level = table.Column<string>(type: "text", nullable: false, comment: "日志等级"),
                    Category = table.Column<string>(type: "text", nullable: false, comment: "类别"),
                    Content = table.Column<string>(type: "text", nullable: false, comment: "内容"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                },
                comment: "日志表");

            migrationBuilder.CreateTable(
                name: "QueueTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "任务名称"),
                    Parameter = table.Column<string>(type: "text", nullable: true, comment: "参数"),
                    PlanTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "计划执行时间"),
                    Count = table.Column<int>(type: "integer", nullable: false, comment: "执行次数"),
                    FirstTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "首次执行时间"),
                    LastTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "最后一次执行时间"),
                    SuccessTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "成功执行时间"),
                    CallbackName = table.Column<string>(type: "text", nullable: true, comment: "回调任务名称"),
                    CallbackParameter = table.Column<string>(type: "text", nullable: true, comment: "回调参数"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueTask", x => x.Id);
                },
                comment: "队列任务表");

            migrationBuilder.CreateTable(
                name: "RegionProvince",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "主键标识ID"),
                    Province = table.Column<string>(type: "text", nullable: false, comment: "省份"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionProvince", x => x.Id);
                },
                comment: "省份信息表");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "角色名称"),
                    Remarks = table.Column<string>(type: "text", nullable: true, comment: "备注信息"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                },
                comment: "角色信息表");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Name = table.Column<string>(type: "citext", nullable: false, comment: "名称"),
                    UserName = table.Column<string>(type: "text", nullable: false, comment: "用户名"),
                    Phone = table.Column<string>(type: "text", nullable: false, comment: "手机号"),
                    Email = table.Column<string>(type: "text", nullable: true, comment: "邮箱"),
                    PassWord = table.Column<string>(type: "text", nullable: false, comment: "密码"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    UpdateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "更新时间"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    UpdateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "编辑人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "用户表");

            migrationBuilder.CreateTable(
                name: "FunctionRoute",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    FunctionId = table.Column<long>(type: "bigint", nullable: true, comment: "功能信息"),
                    Module = table.Column<string>(type: "text", nullable: false, comment: "模块"),
                    Route = table.Column<string>(type: "text", nullable: false, comment: "路由"),
                    Remarks = table.Column<string>(type: "text", nullable: true, comment: "备注"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionRoute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FunctionRoute_Function_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "功能模块对应路由记录表");

            migrationBuilder.CreateTable(
                name: "RegionCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "主键标识ID"),
                    City = table.Column<string>(type: "text", nullable: false, comment: "城市名称"),
                    ProvinceId = table.Column<int>(type: "integer", nullable: false, comment: "所属省份ID"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
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
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "栏目名目"),
                    Sort = table.Column<int>(type: "integer", nullable: false, comment: "排序"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true, comment: "父级栏目ID"),
                    Remarks = table.Column<string>(type: "text", nullable: true, comment: "备注"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "栏目信息表");

            migrationBuilder.CreateTable(
                name: "DataUpdateLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Table = table.Column<string>(type: "text", nullable: false, comment: "外链表名"),
                    TableId = table.Column<long>(type: "bigint", nullable: false, comment: "外链表ID"),
                    Content = table.Column<string>(type: "text", nullable: false, comment: "变动内容"),
                    ActionUserId = table.Column<long>(type: "bigint", nullable: true, comment: "操作人信息"),
                    IpAddress = table.Column<string>(type: "text", nullable: true, comment: "Ip地址"),
                    DeviceMark = table.Column<string>(type: "text", nullable: true, comment: "设备标记"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataUpdateLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataUpdateLog_User_ActionUserId",
                        column: x => x.ActionUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "TDataUpdateLog");

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "文件名称"),
                    Path = table.Column<string>(type: "text", nullable: false, comment: "保存路径"),
                    Length = table.Column<long>(type: "bigint", nullable: false, comment: "文件大小"),
                    Table = table.Column<string>(type: "text", nullable: false, comment: "外链表名"),
                    TableId = table.Column<long>(type: "bigint", nullable: false, comment: "外链表ID"),
                    Sign = table.Column<string>(type: "text", nullable: false, comment: "标记"),
                    Sort = table.Column<int>(type: "integer", nullable: false, comment: "排序"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_File_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "文件表");

            migrationBuilder.CreateTable(
                name: "FunctionAuthorize",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    FunctionId = table.Column<long>(type: "bigint", nullable: false, comment: "功能ID"),
                    RoleId = table.Column<long>(type: "bigint", nullable: true, comment: "角色ID"),
                    UserId = table.Column<long>(type: "bigint", nullable: true, comment: "用户信息"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    UpdateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "更新时间"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    UpdateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "编辑人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionAuthorize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FunctionAuthorize_Function_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FunctionAuthorize_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FunctionAuthorize_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FunctionAuthorize_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FunctionAuthorize_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FunctionAuthorize_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "功能授权配置表");

            migrationBuilder.CreateTable(
                name: "Link",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "名称"),
                    Url = table.Column<string>(type: "text", nullable: false, comment: "地址"),
                    Sort = table.Column<int>(type: "integer", nullable: false, comment: "排序"),
                    Remarks = table.Column<string>(type: "text", nullable: true, comment: "备注"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Link_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Link_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "友情链接表");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    OrderNo = table.Column<string>(type: "text", nullable: false, comment: "订单号"),
                    Type = table.Column<string>(type: "text", nullable: false, comment: "订单类型"),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false, comment: "价格"),
                    SerialNo = table.Column<string>(type: "text", nullable: true, comment: "支付流水号"),
                    State = table.Column<string>(type: "text", nullable: false, comment: "订单状态"),
                    PayType = table.Column<string>(type: "text", nullable: false, comment: "支付方式"),
                    PayState = table.Column<bool>(type: "boolean", nullable: false, comment: "支付状态"),
                    PayTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "支付时间"),
                    PayPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, comment: "实际支付金额"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    UpdateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "更新时间"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    UpdateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "编辑人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "订单表");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    SKU = table.Column<string>(type: "text", nullable: false, comment: "SKU"),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "名称"),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false, comment: "价格"),
                    Detail = table.Column<string>(type: "text", nullable: false, comment: "描述"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    UpdateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "更新时间"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    UpdateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "编辑人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "产品表");

            migrationBuilder.CreateTable(
                name: "TaskSetting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    Name = table.Column<string>(type: "text", nullable: false, comment: "任务名称"),
                    Category = table.Column<string>(type: "text", nullable: false, comment: "任务种类[\"QueueTask\",\"ScheduleTask\"]"),
                    Semaphore = table.Column<int>(type: "integer", nullable: true, comment: "并发值"),
                    Duration = table.Column<int>(type: "integer", nullable: true, comment: "预期持续时间(单位：分)"),
                    Cron = table.Column<string>(type: "text", nullable: true, comment: "Cron 表达式"),
                    IsEnable = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    Remarks = table.Column<string>(type: "text", nullable: true, comment: "备注"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    UpdateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "更新时间"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    UpdateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "编辑人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSetting_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskSetting_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskSetting_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "任务配置");

            migrationBuilder.CreateTable(
                name: "UserBindExternal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "用户信息"),
                    AppName = table.Column<string>(type: "text", nullable: false, comment: "App名称"),
                    AppId = table.Column<string>(type: "text", nullable: false, comment: "AppId"),
                    OpenId = table.Column<string>(type: "text", nullable: false, comment: "用户绑定ID"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBindExternal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBindExternal_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "TUserBindExternal");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false, comment: "角色信息"),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "用户信息"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    UpdateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "更新时间"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    UpdateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "编辑人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "TUserRole");

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "用户ID"),
                    LastId = table.Column<long>(type: "bigint", nullable: true, comment: "上一次有效的 tokenid"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "用户Token记录表");

            migrationBuilder.CreateTable(
                name: "RegionArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "主键标识ID"),
                    Area = table.Column<string>(type: "text", nullable: false, comment: "区域名称"),
                    CityId = table.Column<int>(type: "integer", nullable: false, comment: "所属城市ID"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
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
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false, comment: "类别ID"),
                    Title = table.Column<string>(type: "text", nullable: false, comment: "标题"),
                    Content = table.Column<string>(type: "text", nullable: true, comment: "内容"),
                    IsRecommend = table.Column<bool>(type: "boolean", nullable: false, comment: "是否推荐"),
                    IsDisplay = table.Column<bool>(type: "boolean", nullable: false, comment: "是否显示"),
                    Sort = table.Column<int>(type: "integer", nullable: false, comment: "排序"),
                    ClickCount = table.Column<int>(type: "integer", nullable: false, comment: "点击数"),
                    Digest = table.Column<string>(type: "text", nullable: true, comment: "摘要"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "创建人ID"),
                    DeleteUserId = table.Column<long>(type: "bigint", nullable: true, comment: "删除人ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Article_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Article_User_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "文章表");

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false, comment: "订单ID"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false, comment: "产品ID"),
                    Number = table.Column<int>(type: "integer", nullable: false, comment: "产品数量"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "订单详情表");

            migrationBuilder.CreateTable(
                name: "RegionTown",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "主键标识ID"),
                    Town = table.Column<string>(type: "text", nullable: false, comment: "街道名称"),
                    AreaId = table.Column<int>(type: "integer", nullable: false, comment: "所属区域ID"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记")
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

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识ID"),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "用户ID"),
                    RegionAreaId = table.Column<int>(type: "integer", nullable: true, comment: "地址区域ID"),
                    Address = table.Column<string>(type: "text", nullable: true, comment: "地址详细信息"),
                    Sex = table.Column<bool>(type: "boolean", nullable: true, comment: "性别"),
                    UpdateUserId = table.Column<long>(type: "bigint", nullable: true, comment: "编辑人ID"),
                    CreateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "创建时间"),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "是否删除"),
                    DeleteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "删除时间"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false, comment: "行版本标记"),
                    UpdateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "更新时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfo_RegionArea_RegionAreaId",
                        column: x => x.RegionAreaId,
                        principalTable: "RegionArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfo_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "用户详细信息表");

            migrationBuilder.CreateIndex(
                name: "IX_AppSetting_CreateTime",
                table: "AppSetting",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppSetting_DeleteTime",
                table: "AppSetting",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppSetting_Module",
                table: "AppSetting",
                column: "Module");

            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryId",
                table: "Article",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_CreateTime",
                table: "Article",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Article_CreateUserId",
                table: "Article",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_DeleteTime",
                table: "Article",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_Article_DeleteUserId",
                table: "Article",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Title",
                table: "Article",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreateTime",
                table: "Category",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreateUserId",
                table: "Category",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_DeleteTime",
                table: "Category",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_Category_DeleteUserId",
                table: "Category",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DataUpdateLog_ActionUserId",
                table: "DataUpdateLog",
                column: "ActionUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DataUpdateLog_CreateTime",
                table: "DataUpdateLog",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DataUpdateLog_DeleteTime",
                table: "DataUpdateLog",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_DataUpdateLog_Table",
                table: "DataUpdateLog",
                column: "Table");

            migrationBuilder.CreateIndex(
                name: "IX_DataUpdateLog_TableId",
                table: "DataUpdateLog",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_File_CreateTime",
                table: "File",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_File_CreateUserId",
                table: "File",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_File_DeleteTime",
                table: "File",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_File_DeleteUserId",
                table: "File",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_File_Sign",
                table: "File",
                column: "Sign");

            migrationBuilder.CreateIndex(
                name: "IX_File_Table",
                table: "File",
                column: "Table");

            migrationBuilder.CreateIndex(
                name: "IX_File_TableId",
                table: "File",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Function_CreateTime",
                table: "Function",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Function_DeleteTime",
                table: "Function",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_Function_ParentId",
                table: "Function",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Function_Sign",
                table: "Function",
                column: "Sign");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionAuthorize_CreateTime",
                table: "FunctionAuthorize",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionAuthorize_CreateUserId",
                table: "FunctionAuthorize",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionAuthorize_DeleteTime",
                table: "FunctionAuthorize",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionAuthorize_DeleteUserId",
                table: "FunctionAuthorize",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionAuthorize_FunctionId",
                table: "FunctionAuthorize",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionAuthorize_RoleId",
                table: "FunctionAuthorize",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionAuthorize_UpdateTime",
                table: "FunctionAuthorize",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionAuthorize_UpdateUserId",
                table: "FunctionAuthorize",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionAuthorize_UserId",
                table: "FunctionAuthorize",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionRoute_CreateTime",
                table: "FunctionRoute",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionRoute_DeleteTime",
                table: "FunctionRoute",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionRoute_FunctionId",
                table: "FunctionRoute",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_CreateTime",
                table: "Link",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Link_CreateUserId",
                table: "Link",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_DeleteTime",
                table: "Link",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_Link_DeleteUserId",
                table: "Link",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_Sort",
                table: "Link",
                column: "Sort");

            migrationBuilder.CreateIndex(
                name: "IX_Log_CreateTime",
                table: "Log",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Log_DeleteTime",
                table: "Log",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreateTime",
                table: "Order",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreateUserId",
                table: "Order",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeleteTime",
                table: "Order",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeleteUserId",
                table: "Order",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderNo",
                table: "Order",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UpdateTime",
                table: "Order",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UpdateUserId",
                table: "Order",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_CreateTime",
                table: "OrderDetail",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_DeleteTime",
                table: "OrderDetail",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreateTime",
                table: "Product",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreateUserId",
                table: "Product",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeleteTime",
                table: "Product",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeleteUserId",
                table: "Product",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                table: "Product",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SKU",
                table: "Product",
                column: "SKU");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UpdateTime",
                table: "Product",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UpdateUserId",
                table: "Product",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueTask_CreateTime",
                table: "QueueTask",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_QueueTask_DeleteTime",
                table: "QueueTask",
                column: "DeleteTime");

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

            migrationBuilder.CreateIndex(
                name: "IX_Role_CreateTime",
                table: "Role",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Role_DeleteTime",
                table: "Role",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSetting_CreateTime",
                table: "TaskSetting",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSetting_CreateUserId",
                table: "TaskSetting",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSetting_DeleteTime",
                table: "TaskSetting",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSetting_DeleteUserId",
                table: "TaskSetting",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSetting_UpdateTime",
                table: "TaskSetting",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSetting_UpdateUserId",
                table: "TaskSetting",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreateTime",
                table: "User",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreateUserId",
                table: "User",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_DeleteTime",
                table: "User",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_User_DeleteUserId",
                table: "User",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_User_Phone",
                table: "User",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdateTime",
                table: "User",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdateUserId",
                table: "User",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_UserBindExternal_CreateTime",
                table: "UserBindExternal",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserBindExternal_DeleteTime",
                table: "UserBindExternal",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserBindExternal_UserId",
                table: "UserBindExternal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_CreateTime",
                table: "UserInfo",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_DeleteTime",
                table: "UserInfo",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_RegionAreaId",
                table: "UserInfo",
                column: "RegionAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UpdateTime",
                table: "UserInfo",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UpdateUserId",
                table: "UserInfo",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_CreateTime",
                table: "UserRole",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_CreateUserId",
                table: "UserRole",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_DeleteTime",
                table: "UserRole",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_DeleteUserId",
                table: "UserRole",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UpdateTime",
                table: "UserRole",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UpdateUserId",
                table: "UserRole",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_CreateTime",
                table: "UserToken",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_DeleteTime",
                table: "UserToken",
                column: "DeleteTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId",
                table: "UserToken",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSetting");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "DataUpdateLog");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "FunctionAuthorize");

            migrationBuilder.DropTable(
                name: "FunctionRoute");

            migrationBuilder.DropTable(
                name: "Link");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "QueueTask");

            migrationBuilder.DropTable(
                name: "RegionTown");

            migrationBuilder.DropTable(
                name: "TaskSetting");

            migrationBuilder.DropTable(
                name: "UserBindExternal");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Function");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "RegionArea");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "RegionCity");

            migrationBuilder.DropTable(
                name: "RegionProvince");
        }
    }
}
