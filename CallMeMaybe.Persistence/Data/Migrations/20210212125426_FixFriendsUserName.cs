using Microsoft.EntityFrameworkCore.Migrations;

namespace CallMeMaybe.Persistence.Data.Migrations
{
    public partial class FixFriendsUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FriendName",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendName",
                table: "Friends");
        }
    }
}
