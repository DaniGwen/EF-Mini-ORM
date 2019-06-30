using Microsoft.EntityFrameworkCore.Migrations;

namespace BillsPaymentSystem.Data.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "BankAccounts",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "BankAccounts",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
