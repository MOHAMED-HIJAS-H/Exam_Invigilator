using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Exam_Invigilator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedVenues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "HallNumber", "Name" },
                values: new object[,]
                {
                    { 1, "H1", "Block A" },
                    { 2, "H2", "Block B" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
