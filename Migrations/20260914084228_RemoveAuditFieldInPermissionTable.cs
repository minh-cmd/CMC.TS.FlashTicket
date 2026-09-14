using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMC.TS.FT.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAuditFieldInPermissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Permission");

            migrationBuilder.AddColumn<string>(
                name: "PermissionDescription",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PermissionDisplayName",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionDescription",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "PermissionDisplayName",
                table: "Permission");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Permission",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "Permission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Permission",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateBy",
                table: "Permission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
