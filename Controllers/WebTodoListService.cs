using System;
using System.Linq;
using System.Collections.Generic;
using WebTodoList.Models;

namespace WebTodoList
{
    public class WebTodoListService
    {

        private WebTodoListContext _context;
        private int lastListId = 0;

        public WebTodoListService(WebTodoListContext context)
        {
            this._context = context;
        }

        public IQueryable<TodoItemDto> Read(int listId)
        {          
            var todoItems = from td in _context.TodoItems
                            select new TodoItemDto()
                            {
                                TodoItemDtoId = td.TodoItemDtoId,
                                Title = td.Title,
                                Description = td.Description,
                                DueDate = td.DueDate,
                                Done = td.Done,
                            };
            return todoItems;
        }

        internal IQueryable<TodoItemDto> GetIsAllTodos(int listId, bool all)
        {
            using (var db = _context)
            if (!all)
            {
                return db.TodoItems.Where(table => table.TodoListId == listId && table.Done == false);
            }
            else
            {
                return db.TodoItems.Where(table => table.TodoListId == listId);
            }
        }

        public void Create(int listId, TodoItemDto item)
        {
            using (var db = _context)
            {
                item.TodoListId = listId;
                db.TodoItems.Add(item);
                db.SaveChanges();
            };
        }

        public void Update(int listId, int id, TodoItemDto item)
        {
            using (var db = _context)
            {
                item.TodoListId = listId;
                item.TodoItemDtoId = id;
                db.TodoItems.Update(item);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = _context)
            {
                var deletedItem = db.TodoItems.Where(td => td.TodoItemDtoId == id).Single();
                db.Remove(deletedItem);
                db.SaveChanges();
            }
        }



        // private void Add(int listId, TodoItem item)
        // {
        //     using (var db = _context)
        //     {
        //         while (db.TodoLists.Find(listId) == null)
        //         {
        //             db.TodoLists.Add(new TodoList{Name = $"List {++lastListId}"});
        //         }
        //         db.TodoItems.Add(item);
        //         db.SaveChanges();
        //     }
        
        // }
    }
}