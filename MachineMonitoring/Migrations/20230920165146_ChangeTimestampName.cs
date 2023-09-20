using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineMonitoring.Migrations
{
    public partial class ChangeTimestampName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "MachineDeviceState",
                newName: "DeviceStateChangedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceStateChangedAt",
                table: "MachineDeviceState",
                newName: "Timestamp");
        }
    }
}
