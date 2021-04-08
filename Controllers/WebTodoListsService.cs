using WebTodoList;
using System.Linq;
using System.Collections.Generic;
using WebTodoList.Models;
using System;

namespace WebTodoLists
{
    public class WebTodoListsService
    {
        private WebTodoListContext _context;
        private List<TodoList> todoLists;

        public WebTodoListsService(WebTodoListContext context)
        {
            this._context = context;
        }

        public void Create(TodoList list)
        {
            using (var db = _context)
            {
                db.TodoLists.Add(list);
                db.SaveChanges();
            }
        }

        public List<TodoList> Read()
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
                TodoList list = new TodoList{
                    TodoListId = listId,
                    Title = title,
                };
                
                db.TodoLists.Update(list);
                    // .Where(list => list.TodoListId == listId)
                    // .Single().Title = title;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = _context)
            {
                var deletedList = db.TodoLists.Where(td => td.TodoListId == id).Single();
                db.Remove(deletedList);
                db.SaveChanges();
            }
        }
    }
}