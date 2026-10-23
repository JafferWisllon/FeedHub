using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedHub.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class FeedItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(300)", nullable: false),
                    Link = table.Column<string>(type: "NVARCHAR(1000)", nullable: false),
                    PublishAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedItens_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedItens_FeedId",
                table: "FeedItens",
                column: "FeedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedItens");
        }
    }
}
