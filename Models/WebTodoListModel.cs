using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebTodoList.Models
{
    public class WebTodoListContext : DbContext
    {
        public DbSet<TodoListDto> TodoLists { get; set; }
        public DbSet<TodoItemDto> TodoItems { get; set; }
        public DbSet<TodayTodosDto> DashboardDtos { get; set; }

        public WebTodoListContext(DbContextOptions<WebTodoListContext> options) : base(options) { }

        // public WebTodoListContext()
        // {
        // }

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

    public class TodoListDto
    {
        public int TodoListDtoId { get; set; } 
        public string Title { get; set; }

        public List<TodoItemDto> TodoItems { get; set; }
    }

    public class TodoItemDto
    {
        public int TodoItemDtoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Done { get; set; }

        public int TodoListId { get; set; }
        public TodoListDto TodoList { get; set; }
    }
}