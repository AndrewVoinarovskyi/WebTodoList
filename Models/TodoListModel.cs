using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoLists.Models
{
    public class TodoListsContext : DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodayTodosDto> DashboardDtos { get; set; }

        public TodoListsContext(DbContextOptions<TodoListsContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<TodayTodosDto>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.ToView("View_BlogPostCounts");
                    });
        }
        
    }

    public class TodoList
    {
        public int Id { get; set; } 
        public string Title { get; set; }

        public List<TodoItem> TodoItems { get; set; }
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Done { get; set; }

        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; }
    }
}