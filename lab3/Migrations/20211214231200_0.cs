using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NTR.Migrations
{
    public partial class _0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    project_id = table.Column<string>(type: "text", nullable: false),
                    manager = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    budget = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    subactivities = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "user_months",
                columns: table => new
                {
                    user = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<string>(type: "text", nullable: true),
                    frozen = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_months", x => x.user);
                });

            migrationBuilder.CreateTable(
                name: "user_activities",
                columns: table => new
                {
                    project_id = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<string>(type: "text", nullable: true),
                    approval_date = table.Column<string>(type: "text", nullable: true),
                    project_id1 = table.Column<string>(type: "text", nullable: false),
                    subactivity = table.Column<string>(type: "text", nullable: true),
                    time = table.Column<int>(type: "integer", nullable: false),
                    approved_time = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    user_month_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_activities", x => x.project_id);
                    table.ForeignKey(
                        name: "fk_user_activities_projects_project_id",
                        column: x => x.project_id1,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_activities_user_months_user_month_id",
                        column: x => x.user_month_id,
                        principalTable: "user_months",
                        principalColumn: "user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_activities_project_id1",
                table: "user_activities",
                column: "project_id1");

            migrationBuilder.CreateIndex(
                name: "ix_user_activities_user_month_id",
                table: "user_activities",
                column: "user_month_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_activities");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "user_months");
        }
    }
}
