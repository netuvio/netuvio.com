using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProjectLocalizationPk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectLocalizations",
                table: "ProjectLocalizations");

            migrationBuilder.DropIndex(
                name: "IX_ProjectLocalizations_ProjectId",
                table: "ProjectLocalizations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectLocalizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectLocalizations",
                table: "ProjectLocalizations",
                columns: new[] { "ProjectId", "Locale" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectLocalizations",
                table: "ProjectLocalizations");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProjectLocalizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectLocalizations",
                table: "ProjectLocalizations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLocalizations_ProjectId",
                table: "ProjectLocalizations",
                column: "ProjectId");
        }
    }
}
