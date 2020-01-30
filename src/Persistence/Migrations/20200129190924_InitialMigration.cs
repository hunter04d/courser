using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Courses",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DayOfWeek = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StartTime_Hour = table.Column<byte>(nullable: true),
                    StartTime_Minute = table.Column<byte>(nullable: true),
                    EndTime_Hour = table.Column<byte>(nullable: true),
                    EndTime_Minute = table.Column<byte>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Courses", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Courses");
        }
    }
}
