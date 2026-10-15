using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderSaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptDate",
                table: "OrderSaga");

            migrationBuilder.DropColumn(
                name: "SubmitDate",
                table: "OrderSaga");

            migrationBuilder.AddColumn<string>(
                name: "FailureReason",
                table: "OrderSaga",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrderSaga",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailureReason",
                table: "OrderSaga");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderSaga");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptDate",
                table: "OrderSaga",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitDate",
                table: "OrderSaga",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
