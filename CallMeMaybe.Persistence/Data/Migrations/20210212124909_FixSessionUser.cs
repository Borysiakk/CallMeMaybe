using Microsoft.EntityFrameworkCore.Migrations;

namespace CallMeMaybe.Persistence.Data.Migrations
{
    public partial class FixSessionUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "SessionUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "SessionUsers");
        }
    }
}
