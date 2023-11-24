using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHelpDeskApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsItUser = table.Column<bool>(type: "bit", nullable: false),
                    IsLoggedInuser = table.Column<bool>(type: "bit", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNum = table.Column<int>(type: "int", nullable: false),
                    TicketTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResolutionSummary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedToName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "AssignedToName", "ClosedDate", "CreatedBy", "CreatedDate", "Priority", "ResolutionSummary", "Status", "TicketDescription", "TicketNum", "TicketTitle", "UserId" },
                values: new object[,]
                {
                    { 1, "Sally Smith", new DateTime(2023, 10, 29, 19, 26, 8, 281, DateTimeKind.Local).AddTicks(3551), "Alberta Crocodile", new DateTime(2023, 10, 22, 15, 49, 8, 281, DateTimeKind.Local).AddTicks(3522), "Medium", "HDMI cable that connected the monitor to the PC was busted. Replaced it with a new one.", "Closed", "The light on the tower turns on, but nothing ever shows up on the monitor. The monitor is on. Please help.", 100, "Desktop won't turn on", null },
                    { 2, "Albert Gator", new DateTime(2023, 11, 4, 17, 37, 8, 281, DateTimeKind.Local).AddTicks(3557), "John Doe", new DateTime(2023, 11, 2, 15, 49, 8, 281, DateTimeKind.Local).AddTicks(3555), "Low", "User was not connected to VPN. Once connected, the issue was resolved.", "Closed", "On Friday, I was able to connect to the Accounting server; however, now when I try to connect I receive a 'cannot connect to server' error message.", 101, "Can't connect to internal Accounting server", null },
                    { 3, "Albert Gator", null, "Alberta Crocodile", new DateTime(2023, 11, 24, 13, 16, 8, 281, DateTimeKind.Local).AddTicks(3561), "High", "", "Open", "Receiving a '404 error' on every webpage. This is an urgent request.", 101, "Can't connect to Internet", null },
                    { 4, "Sally Smith", null, "John Doe", new DateTime(2023, 11, 23, 12, 49, 8, 281, DateTimeKind.Local).AddTicks(3564), "Medium", "", "Open", "Hello, this is a request to purchase 2 licenses for Quickbooks Enterprise 2023. These licenses will be for the new hires starting next week.", 101, "Request for new Quickbooks licenses", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Department", "FirstName", "IsItUser", "IsLoggedInuser", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "IT", "Sally", true, false, "Smith", "ItPassword1", "ItUser1" },
                    { 2, "IT", "Albert", true, false, "Gator", "ItPassword2", "ItUser2" },
                    { 3, "Accounting", "John", false, false, "Doe", "NonItPassword1", "NonItUser1" },
                    { 4, "Support", "Alberta", false, false, "Crocodile", "NonItPassword2", "NonItUser2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
