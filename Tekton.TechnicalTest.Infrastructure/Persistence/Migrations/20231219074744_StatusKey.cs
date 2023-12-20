using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tekton.TechnicalTest.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StatusKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusKey",
                table: "Status",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusKey",
                table: "Status");
        }
    }
}
