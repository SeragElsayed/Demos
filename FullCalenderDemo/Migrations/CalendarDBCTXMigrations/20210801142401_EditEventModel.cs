using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FullCalenderDemo.Migrations.CalendarDBCTXMigrations
{
    public partial class EditEventModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Events_EventId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Events_EventId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_EventId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Customers_EventId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Events",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Events",
                newName: "EndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Events_CustomerId",
                table: "Events",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ServiceId",
                table: "Events",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Customers_CustomerId",
                table: "Events",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Services_ServiceId",
                table: "Events",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Customers_CustomerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Services_ServiceId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CustomerId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ServiceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Events",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Events",
                newName: "EndDate");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_EventId",
                table: "Services",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EventId",
                table: "Customers",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Events_EventId",
                table: "Customers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Events_EventId",
                table: "Services",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
