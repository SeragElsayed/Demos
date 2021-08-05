using Microsoft.EntityFrameworkCore.Migrations;

namespace FullCalenderDemo.Migrations.CalendarDBCTXMigrations
{
    public partial class addServiceAndCustomerModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Events_Customer",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Customer",
                table: "Customers",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Customer",
                table: "Customers",
                newName: "IX_Customers_EventId");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Events_EventId",
                table: "Customers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Events_EventId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Customers",
                newName: "Customer");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_EventId",
                table: "Customers",
                newName: "IX_Customers_Customer");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Events_Customer",
                table: "Customers",
                column: "Customer",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
