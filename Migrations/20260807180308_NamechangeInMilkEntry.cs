using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFarmAPI.Migrations
{
    /// <inheritdoc />
    public partial class NamechangeInMilkEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PId",
                table: "MilkEntries",
                newName: "MilkEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MilkEntryId",
                table: "MilkEntries",
                newName: "PId");
        }
    }
}
