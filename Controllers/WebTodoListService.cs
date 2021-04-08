using System;
using System.Linq;
using System.Collections.Generic;
using WebTodoList.Models;

namespace WebTodoList
{
    public class WebTodoListService
    {

        private List<TodoItem> todoItems;
        private WebTodoListContext _context;
        private int lastListId = 0;

        public WebTodoListService(WebTodoListContext context)
        {
            this._context = context;
        }

        public List<TodoItem> Read(int listId)
        {
            using (var db = _context)
            {
                todoItems = db.TodoItems
                            .Where(x => x.TodoListId == listId)
                            .ToList();
            }
            return todoItems;
        }

        public void Create(int listId, TodoItem item)
        {
            using (var db = _context)
            {
                item.TodoListId = listId;
                db.TodoItems.Add(item);
                db.SaveChanges();
            };
        }

        public void Update(int listId, int id, TodoItem item)
        {
            using (var db = _context)
            {
                item.TodoListId = listId;
                item.TodoItemId = id;
                db.TodoItems.Update(item);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = _context)
            {
                var deletedItem = db.TodoItems.Where(td => td.TodoItemId == id).Single();
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