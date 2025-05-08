using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrManagement.Migrations
{
    /// <inheritdoc />
    public partial class ShiftLateTimeImplemented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Companies_CompanyComId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_CompanyComId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "CompanyComId",
                table: "Shifts");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Shifts",
                newName: "OutTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Shifts",
                newName: "LateTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "InTime",
                table: "Shifts",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ComId",
                table: "Shifts",
                column: "ComId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Companies_ComId",
                table: "Shifts",
                column: "ComId",
                principalTable: "Companies",
                principalColumn: "ComId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Companies_ComId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_ComId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "InTime",
                table: "Shifts");

            migrationBuilder.RenameColumn(
                name: "OutTime",
                table: "Shifts",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "LateTime",
                table: "Shifts",
                newName: "EndTime");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyComId",
                table: "Shifts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_CompanyComId",
                table: "Shifts",
                column: "CompanyComId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Companies_CompanyComId",
                table: "Shifts",
                column: "CompanyComId",
                principalTable: "Companies",
                principalColumn: "ComId");
        }
    }
}
