using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTodoList.Migrations
{
    public partial class InitialD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "todo_lists",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "todo_items",
                newName: "todo_item_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "todo_lists",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "todo_item_id",
                table: "todo_items",
                newName: "id");
        }
    }
}
