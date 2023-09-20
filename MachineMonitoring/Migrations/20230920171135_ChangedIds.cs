using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineMonitoring.Migrations
{
    public partial class ChangedIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineDeviceState_Machines_MachineId",
                table: "MachineDeviceState");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Machines",
                newName: "MachineId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MachineDeviceState",
                newName: "MachineDeviceStateId");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "MachineDeviceState",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineDeviceState_Machines_MachineId",
                table: "MachineDeviceState",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "MachineId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineDeviceState_Machines_MachineId",
                table: "MachineDeviceState");

            migrationBuilder.RenameColumn(
                name: "MachineId",
                table: "Machines",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MachineDeviceStateId",
                table: "MachineDeviceState",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "MachineDeviceState",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineDeviceState_Machines_MachineId",
                table: "MachineDeviceState",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id");
        }
    }
}
