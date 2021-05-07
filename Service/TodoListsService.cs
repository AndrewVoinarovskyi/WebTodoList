using System;
using Microsoft.EntityFrameworkCore;
using TodoLists;
using TodoLists.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;

namespace TodoLists
{
    public class TodoListsService
    {
        private TodoListsContext _context;
        private IQueryable<TodoList> todoLists;

        public TodoListsService(TodoListsContext context)
        {
            this._context = context;
        }

        public void CreateList(TodoList list)
        {
            _context.TodoLists.Add(list);
            _context.SaveChanges();
        }

         public TodoItem CreateItem(int listId, TodoItem item)
        {
            item.TodoListId = listId;
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public IEnumerable<TodoList> ReadAll()
        {           
            return _context.TodoLists.ToList();
        }

        public IQueryable<TodoItem> Read(int listId)
        {          
            return _context.TodoItems.Where(i => i.TodoListId == listId);
        }

        public void UpdateTodoList(int listId, string title)
        {
            TodoList list = new TodoList{
                Id = listId,
                Title = title,
            };
            _context.TodoLists.Update(list);
            _context.SaveChanges();
        }

        public TodoItem UpdateTodoItem(int listId, int id, TodoItem item)
        {
            item.TodoListId = listId;
            item.Id = id;
            _context.TodoItems.Update(item);
            _context.SaveChanges();

            return item;
        }
        
        internal TodoItem UpdateItemStatus(int listId, int id, JsonPatchDocument<TodoItem> model)
        {
            TodoItem item = _context.TodoItems.Where(item => item.Id == id && item.TodoListId == listId).Single();
            model.ApplyTo(item);
            _context.TodoItems.Update(item);
            _context.SaveChanges();

            return item;
        }

        internal object GetTodayTodos()
        {
            return _context.TodoItems.Include(table => table.TodoList);
        }

        internal List<TodoItem> GetIsAllTodos(int listId, bool all)
        {
            IQueryable<TodoItem> todoItems = Read(listId);
            var isAllTodos = new List<TodoItem> ();

            using (var db = _context)
            if (!all)
            {
                foreach (var item in todoItems)
                {
                    if (!item.Done)
                    {
                        isAllTodos.Add(item);                        
                    }
                }
                return isAllTodos;
            }
            else
            {
                foreach (var item in todoItems)
                {
                    isAllTodos.Add(item);
                }
                return isAllTodos;
            }
        }

        public void DeleteItem(int listId, int id)
        {
            var deletedItem = _context.TodoItems.Where(td => td.Id == id && td.TodoListId == listId).Single();
            _context.Remove(deletedItem);
            _context.SaveChanges();
        }

        public void DeleteList(int id)
        {
            var deletedList = _context.TodoLists.Where(td => td.Id == id).Single();
            _context.Remove(deletedList);
            _context.SaveChanges();
        }

        internal DashboardDto GetDashboard()
        {            
            List<TodayTodosDto> list = new List<TodayTodosDto>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select l.my_list_id, l.title, Count(t.done) from my_lists l left join my_tasks t on l.my_list_id=t.my_list_id and not t.done group by l.my_list_id, l.title order by l.my_list_id";                _context.Database.OpenConnection();
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
                Dashboards = list,
            };

            return output;
        }
    }
}