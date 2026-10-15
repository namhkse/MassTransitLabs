using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderSaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderSaga",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AcceptDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSaga", x => x.CorrelationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSaga");
        }
    }
}
