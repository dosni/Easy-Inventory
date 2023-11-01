using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class EditTransactinType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "TransactionLines",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TransactionType",
                table: "TransactionLines",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");
        }
    }
}
