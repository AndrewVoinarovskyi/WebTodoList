using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebTodoList.Models
{
    public class WebTodoListContext : DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        public WebTodoListContext(DbContextOptions<WebTodoListContext> options) : base(options) { }

        // public WebTodoListContext()
        // {
        // }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
    }

    public class TodoList
    {
        public int TodoListId { get; set; } 
        public string Title { get; set; }

        public List<TodoItem> TodoItems { get; set; }
    }

    public class TodoItem
    {
        public int TodoItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Done { get; set; }

        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; }
    }
}