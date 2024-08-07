using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.Migrations
{
    /// <inheritdoc />
    public partial class the5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "recurringTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "recurringTransactions");
        }
    }
}
