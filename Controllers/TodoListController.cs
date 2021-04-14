using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Route("lists/{listId}")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private TodoListService service;

        public TodoListController(TodoListService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IQueryable<TodoItemDto> GetTodoItems(int listId)
        {

            return service.Read(listId);
        }

        [HttpGet("tasks")]
        public ActionResult<TodoItemDto> GetTasks(int listId, bool all)
        {
            return Ok(service.GetIsAllTodos(listId, all));
        }

        [HttpPost("")]
        public void PostTodoItem(int listId, TodoItemDto model)
        {
            service.Create(listId, model);
        }

        [HttpPatch("{id}")]
        public void Update(int listId, int id, TodoItemDto model)
        {
            service.Update(listId, id, model);
        }

        [HttpDelete("{id}")]
        public void DeleteTodoItemById(int listId, int id)
        {
            service.Delete(id);
        }
    }
}
