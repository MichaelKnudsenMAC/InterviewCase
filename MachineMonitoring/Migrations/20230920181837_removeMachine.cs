using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineMonitoring.Migrations
{
    public partial class removeMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineDeviceState_Machines_MachineId",
                table: "MachineDeviceState");

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
                principalColumn: "MachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineDeviceState_Machines_MachineId",
                table: "MachineDeviceState");

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
    }
}
