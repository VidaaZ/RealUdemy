using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealProject.DataAccess.Migrations
{
    public partial class renameSessionPaymentIntentIdToPaymentIntentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionPaymentIntentId",
                table: "OrderHeader",
                newName: "PaymentIntentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentIntentId",
                table: "OrderHeader",
                newName: "SessionPaymentIntentId");
        }
    }
}
