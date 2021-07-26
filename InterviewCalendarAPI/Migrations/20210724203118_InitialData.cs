using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InterviewCalendarAPI.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "CandidateId", "Email", "LastUpdateRegistration", "Name", "Phone", "Registration" },
                values: new object[] { 1, null, new DateTime(2021, 7, 24, 20, 31, 17, 955, DateTimeKind.Utc).AddTicks(3555), "John", null, new DateTime(2021, 7, 24, 20, 31, 17, 955, DateTimeKind.Utc).AddTicks(3361) });

            migrationBuilder.InsertData(
                table: "Interviewers",
                columns: new[] { "InterviewerId", "Email", "LastUpdateRegistration", "Name", "Phone", "Registration" },
                values: new object[] { 1, null, new DateTime(2021, 7, 24, 20, 31, 17, 956, DateTimeKind.Utc).AddTicks(2484), "Mary", null, new DateTime(2021, 7, 24, 20, 31, 17, 956, DateTimeKind.Utc).AddTicks(2307) });

            migrationBuilder.InsertData(
                table: "Interviewers",
                columns: new[] { "InterviewerId", "Email", "LastUpdateRegistration", "Name", "Phone", "Registration" },
                values: new object[] { 2, null, new DateTime(2021, 7, 24, 20, 31, 17, 956, DateTimeKind.Utc).AddTicks(2778), "Diana", null, new DateTime(2021, 7, 24, 20, 31, 17, 956, DateTimeKind.Utc).AddTicks(2777) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "CandidateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Interviewers",
                keyColumn: "InterviewerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Interviewers",
                keyColumn: "InterviewerId",
                keyValue: 2);
        }
    }
}
