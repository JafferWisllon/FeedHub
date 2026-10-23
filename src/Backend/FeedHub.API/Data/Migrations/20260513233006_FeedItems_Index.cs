using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedHub.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class FeedItems_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeedItens_FeedId",
                table: "FeedItens");

            migrationBuilder.CreateIndex(
                name: "IX_FeedItems_FeedId_Link",
                table: "FeedItens",
                columns: new[] { "FeedId", "Link" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeedItems_FeedId_Link",
                table: "FeedItens");

            migrationBuilder.CreateIndex(
                name: "IX_FeedItens_FeedId",
                table: "FeedItens",
                column: "FeedId");
        }
    }
}
