using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class myMigration01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hoodies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoodies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    schedule_start = table.Column<DateTime>(type: "date", nullable: false),
                    schedule_end = table.Column<DateTime>(type: "date", nullable: false),
                    date_prestation = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artistes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stage_Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Id_Music = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_schedule = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artistes_Musics_Id_Music",
                        column: x => x.Id_Music,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artistes_Schedules_Id_schedule",
                        column: x => x.Id_schedule,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pseudo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: true),
                    IdTicket = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tickets_IdTicket",
                        column: x => x.IdTicket,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artistes_Id_Music",
                table: "Artistes",
                column: "Id_Music");

            migrationBuilder.CreateIndex(
                name: "IX_Artistes_Id_schedule",
                table: "Artistes",
                column: "Id_schedule");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdTicket",
                table: "Users",
                column: "IdTicket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artistes");

            migrationBuilder.DropTable(
                name: "Hoodies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Musics");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
