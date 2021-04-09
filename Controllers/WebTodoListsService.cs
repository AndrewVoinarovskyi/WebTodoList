using WebTodoList;
using System.Linq;
using System.Collections.Generic;
using WebTodoList.Models;
using System;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace WebTodoLists
{
    public class WebTodoListsService
    {
        private WebTodoListContext _context;
        private List<TodoListDto> todoLists;

        public WebTodoListsService(WebTodoListContext context)
        {
            this._context = context;
        }

        public void Create(TodoListDto list)
        {
            using (var db = _context)
            {
                db.TodoLists.Add(list);
                db.SaveChanges();
            }
        }

        public List<TodoListDto> Read()
        {
            using (var db = _context)
            {
                todoLists = db.TodoLists.ToList();
            }
            return todoLists;
        }

        public void Update(int listId, string title)
        {
            using (var db = _context)
            {
                TodoListDto list = new TodoListDto{
                    TodoListDtoId = listId,
                    Title = title,
                };
                db.TodoLists.Update(list);
                db.SaveChanges();
            }
        }

        internal object GetTodayTodos()
        {
            return _context.TodoItems.Include(table => table.TodoList);
        }

        public void Delete(int id)
        {
            using (var db = _context)
            {
                var deletedList = db.TodoLists.Where(td => td.TodoListDtoId == id).Single();
                db.Remove(deletedList);
                db.SaveChanges();
            }
        }

        internal DashboardDto GetDashboard()
        {            
            List<TodayTodosDto> list = new List<TodayTodosDto>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select todo_lists.todo_list_id, todo_lists.title, Count(todo_items.done) from todo_items right join todo_lists on todo_lists.todo_list_id=todo_items.todo_list_id  where todo_items.done=false group by todo_lists.todo_list_id, todo_lists.title order by todo_lists.todo_list_id";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        list.Add(new TodayTodosDto()
                        {
                            Id = result.GetInt32(0),
                            Title = result.IsDBNull(1) ? null : result.GetString(1),
                            
                            Count = result.GetInt32(2)
                        });
                    }

                }
            }

            DashboardDto output = new DashboardDto()
            {
                Count = _context.TodoItems.Where(b => b.DueDate == DateTime.Today).Count(),
                Dashboards = list
            };

            return output;
        }
    }
}