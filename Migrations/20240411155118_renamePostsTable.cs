using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPskillify_Forum.Migrations
{
    /// <inheritdoc />
    public partial class renamePostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Pots_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Pots_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Pots_SubForums_SubForumId",
                table: "Pots");

            migrationBuilder.DropForeignKey(
                name: "FK_Pots_Users_AuthorId",
                table: "Pots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pots",
                table: "Pots");

            migrationBuilder.RenameTable(
                name: "Pots",
                newName: "Posts");

            migrationBuilder.RenameIndex(
                name: "IX_Pots_SubForumId",
                table: "Posts",
                newName: "IX_Posts_SubForumId");

            migrationBuilder.RenameIndex(
                name: "IX_Pots_AuthorId",
                table: "Posts",
                newName: "IX_Posts_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_SubForums_SubForumId",
                table: "Posts",
                column: "SubForumId",
                principalTable: "SubForums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SubForums_SubForumId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Pots");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_SubForumId",
                table: "Pots",
                newName: "IX_Pots_SubForumId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorId",
                table: "Pots",
                newName: "IX_Pots_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pots",
                table: "Pots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Pots_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Pots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Pots_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Pots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pots_SubForums_SubForumId",
                table: "Pots",
                column: "SubForumId",
                principalTable: "SubForums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pots_Users_AuthorId",
                table: "Pots",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
