using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evently.Modules.Events.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                schema: "events",
                table: "TicketTypes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "EventEntityId",
                schema: "events",
                table: "TicketTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                schema: "events",
                table: "Events",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTypes_EventEntityId",
                schema: "events",
                table: "TicketTypes",
                column: "EventEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                schema: "events",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoryId",
                schema: "events",
                table: "Events",
                column: "CategoryId",
                principalSchema: "events",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketTypes_Events_EventEntityId",
                schema: "events",
                table: "TicketTypes",
                column: "EventEntityId",
                principalSchema: "events",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoryId",
                schema: "events",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketTypes_Events_EventEntityId",
                schema: "events",
                table: "TicketTypes");

            migrationBuilder.DropIndex(
                name: "IX_TicketTypes_EventEntityId",
                schema: "events",
                table: "TicketTypes");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryId",
                schema: "events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventEntityId",
                schema: "events",
                table: "TicketTypes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "events",
                table: "Events");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                schema: "events",
                table: "TicketTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
