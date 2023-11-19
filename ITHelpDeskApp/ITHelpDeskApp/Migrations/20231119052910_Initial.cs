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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedToUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "AssignedToUserId", "ClosedDate", "CreatedBy", "CreatedDate", "Priority", "Status", "TicketDescription", "TicketNum", "TicketTitle" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 10, 24, 4, 6, 10, 402, DateTimeKind.Local).AddTicks(1906), "Alberta Crocodile", new DateTime(2023, 10, 17, 0, 29, 10, 402, DateTimeKind.Local).AddTicks(1878), "Medium", "Closed", "The light on the tower turns on, but nothing ever shows up on the monitor. The monitor is on. Please help.", 100, "Desktop won't turn on" },
                    { 2, 2, new DateTime(2023, 10, 30, 2, 17, 10, 402, DateTimeKind.Local).AddTicks(1913), "John Doe", new DateTime(2023, 10, 28, 0, 29, 10, 402, DateTimeKind.Local).AddTicks(1911), "Low", "Closed", "On Friday, I was able to connect to the Accounting server; however, now when I try to connect I receive a 'cannot connect to server' error message.", 101, "Can't connect to internal Accounting server" },
                    { 3, 2, null, "Alberta Crocodile", new DateTime(2023, 11, 18, 21, 56, 10, 402, DateTimeKind.Local).AddTicks(1916), "High", "Open", "Receiving a '404 error' on every webpage. This is an urgent request.", 101, "Can't connect to Internet" },
                    { 4, 1, null, "John Doe", new DateTime(2023, 11, 17, 21, 29, 10, 402, DateTimeKind.Local).AddTicks(1919), "Medium", "Open", "Hello, this is a request to purchase 2 licenses for Quickbooks Enterprise 2023. These licenses will be for the new hires starting next week.", 101, "Request for new Quickbooks licenses" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedToUserId",
                table: "Tickets",
                column: "AssignedToUserId");
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
