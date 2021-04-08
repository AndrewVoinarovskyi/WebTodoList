using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebTodoList
{
    public class WebTodoListContext : DbContext
    {
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

    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Done { get; set; }
    }
}