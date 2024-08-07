using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.Migrations
{
    /// <inheritdoc />
    public partial class the3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_AspNetUsers_UserId",
                table: "RecurringTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_Category_CategoryId",
                table: "RecurringTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AspNetUsers_UserId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_CategoryId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecurringTransaction",
                table: "RecurringTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "transactions");

            migrationBuilder.RenameTable(
                name: "RecurringTransaction",
                newName: "recurringTransactions");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "categories");

            migrationBuilder.RenameColumn(
                name: "Receipt",
                table: "transactions",
                newName: "Url");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_UserId",
                table: "transactions",
                newName: "IX_transactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_CategoryId",
                table: "transactions",
                newName: "IX_transactions_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransaction_UserId",
                table: "recurringTransactions",
                newName: "IX_recurringTransactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransaction_CategoryId",
                table: "recurringTransactions",
                newName: "IX_recurringTransactions_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_transactions",
                table: "transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recurringTransactions",
                table: "recurringTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_recurringTransactions_AspNetUsers_UserId",
                table: "recurringTransactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_recurringTransactions_categories_CategoryId",
                table: "recurringTransactions",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_AspNetUsers_UserId",
                table: "transactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_categories_CategoryId",
                table: "transactions",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recurringTransactions_AspNetUsers_UserId",
                table: "recurringTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_recurringTransactions_categories_CategoryId",
                table: "recurringTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_AspNetUsers_UserId",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_categories_CategoryId",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_transactions",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recurringTransactions",
                table: "recurringTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "transactions",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "recurringTransactions",
                newName: "RecurringTransaction");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Transaction",
                newName: "Receipt");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_UserId",
                table: "Transaction",
                newName: "IX_Transaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_CategoryId",
                table: "Transaction",
                newName: "IX_Transaction_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_recurringTransactions_UserId",
                table: "RecurringTransaction",
                newName: "IX_RecurringTransaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_recurringTransactions_CategoryId",
                table: "RecurringTransaction",
                newName: "IX_RecurringTransaction_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecurringTransaction",
                table: "RecurringTransaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_AspNetUsers_UserId",
                table: "RecurringTransaction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_Category_CategoryId",
                table: "RecurringTransaction",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AspNetUsers_UserId",
                table: "Transaction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_CategoryId",
                table: "Transaction",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
