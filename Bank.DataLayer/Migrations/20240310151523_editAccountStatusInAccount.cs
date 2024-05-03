using Microsoft.EntityFrameworkCore.Migrations;

namespace Bankbank.Migrations
{
    public partial class editAccountStatusInAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountStatus",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "AccountStatus",
                table: "Accounts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
