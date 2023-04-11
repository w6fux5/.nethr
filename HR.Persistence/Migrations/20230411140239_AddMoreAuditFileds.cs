using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreAuditFileds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Tbl_LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Tbl_LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Tbl_LeaveRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Tbl_LeaveRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Tbl_LeaveAllocation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Tbl_LeaveAllocation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tbl_LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateBy", "CreatedAt", "ModifiedBy", "UpdatedAt" },
                values: new object[] { null, new DateTime(2023, 4, 11, 22, 2, 39, 268, DateTimeKind.Local).AddTicks(9076), null, new DateTime(2023, 4, 11, 22, 2, 39, 268, DateTimeKind.Local).AddTicks(9085) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Tbl_LeaveTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Tbl_LeaveTypes");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Tbl_LeaveRequest");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Tbl_LeaveRequest");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Tbl_LeaveAllocation");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Tbl_LeaveAllocation");

            migrationBuilder.UpdateData(
                table: "Tbl_LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 7, 18, 40, 44, 783, DateTimeKind.Local).AddTicks(2587), new DateTime(2023, 4, 7, 18, 40, 44, 783, DateTimeKind.Local).AddTicks(2599) });
        }
    }
}
