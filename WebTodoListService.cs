using System;
using System.Linq;
using System.Collections.Generic;

namespace WebTodoList
{
    public class WebTodoListService
    {

        private List<TodoItem> todoItems;
        private WebTodoListContext _context;

        public WebTodoListService(WebTodoListContext context)
        {
            this._context = context;
        }

        public List<TodoItem> Read()
        {
            using (var db = _context)
            {
                todoItems = db.TodoItems.ToList();
            }
            return todoItems;
        }

        public void Create(TodoItem item)
        {
            Add(item);
        }

        public void Update(TodoItem item)
        {
            using (var db = _context)
            {
                db.TodoItems.Update(item);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = _context)
            {
                var deletedItem = db.TodoItems.Where(td => td.Id == id).Single();
                db.Remove(deletedItem);
                db.SaveChanges();
            }
        }



        private void Add(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }
    }
}