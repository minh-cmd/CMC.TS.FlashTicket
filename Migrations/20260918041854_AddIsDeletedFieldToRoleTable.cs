using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMC.TS.FT.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedFieldToRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Role");
        }
    }
}
