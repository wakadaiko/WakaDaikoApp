using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WakaDaikoApp.Migrations
{
    /// <inheritdoc />
    public partial class WakaAzure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetronomePreferances",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetronomePreferances",
                table: "AspNetUsers");
        }
    }
}
