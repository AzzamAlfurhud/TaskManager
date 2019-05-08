using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class initialRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "tasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CommentId",
                table: "comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpTaskId",
                table: "comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_IdentityUserId",
                table: "tasks",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_StatusId",
                table: "tasks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_CommentId",
                table: "comments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_EmpTaskId",
                table: "comments",
                column: "EmpTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_IdentityUserId",
                table: "comments",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_CommentId",
                table: "comments",
                column: "CommentId",
                principalTable: "comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_tasks_EmpTaskId",
                table: "comments",
                column: "EmpTaskId",
                principalTable: "tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_IdentityUserId",
                table: "comments",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_AspNetUsers_IdentityUserId",
                table: "tasks",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_statuses_StatusId",
                table: "tasks",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_CommentId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_tasks_EmpTaskId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_IdentityUserId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_AspNetUsers_IdentityUserId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_statuses_StatusId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_IdentityUserId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_StatusId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_comments_CommentId",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_EmpTaskId",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_IdentityUserId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "EmpTaskId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "comments");
        }
    }
}
