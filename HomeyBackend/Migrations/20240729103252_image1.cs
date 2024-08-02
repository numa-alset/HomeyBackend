using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeyBackend.Migrations
{
    /// <inheritdoc />
    public partial class image1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceImage_Places_PlaceId",
                table: "PlaceImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceImage",
                table: "PlaceImage");

            migrationBuilder.RenameTable(
                name: "PlaceImage",
                newName: "PlaceImages");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceImage_PlaceId",
                table: "PlaceImages",
                newName: "IX_PlaceImages_PlaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceImages",
                table: "PlaceImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceImages_Places_PlaceId",
                table: "PlaceImages",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceImages_Places_PlaceId",
                table: "PlaceImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceImages",
                table: "PlaceImages");

            migrationBuilder.RenameTable(
                name: "PlaceImages",
                newName: "PlaceImage");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceImages_PlaceId",
                table: "PlaceImage",
                newName: "IX_PlaceImage_PlaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceImage",
                table: "PlaceImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceImage_Places_PlaceId",
                table: "PlaceImage",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
