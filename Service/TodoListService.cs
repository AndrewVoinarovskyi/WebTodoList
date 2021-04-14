using System;
using System.Linq;
using System.Collections.Generic;
using TodoList.Models;

namespace TodoList
{
    public class TodoListService
    {

        private TodoListContext _context;
        private int lastListId = 0;

        public TodoListService(TodoListContext context)
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

        internal List<TodoItemDto> GetIsAllTodos(int listId, bool all)
        {
            IQueryable<TodoItemDto> todoItems = Read(listId);
            var isAllTodos = new List<TodoItemDto> ();

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
    }
}