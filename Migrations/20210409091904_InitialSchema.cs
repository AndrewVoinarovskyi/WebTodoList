using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "todo_list_id",
                table: "todo_lists",
                newName: "todo_list_dto_id");

            migrationBuilder.RenameColumn(
                name: "todo_item_id",
                table: "todo_items",
                newName: "todo_item_dto_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "todo_list_dto_id",
                table: "todo_lists",
                newName: "todo_list_id");

            migrationBuilder.RenameColumn(
                name: "todo_item_dto_id",
                table: "todo_items",
                newName: "todo_item_id");
        }
    }
}
