using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeyBackend.Migrations
{
    /// <inheritdoc />
    public partial class updatecomment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserInfoId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserInfoId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserInfo",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserInfo",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserInfoId",
                table: "Comments",
                column: "UserInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserInfoId",
                table: "Comments",
                column: "UserInfo",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
