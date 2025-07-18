using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Exam_Invigilator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update_AllocationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "HallNumber", "Name" },
                values: new object[,]
                {
                    { 2, "H3", "Block C" },
                    { 3, "H4", "Block D" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
