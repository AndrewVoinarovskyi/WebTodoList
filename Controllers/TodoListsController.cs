using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoLists;
using TodoLists.Models;

namespace TodoLists.Controllers
{
    [Route("")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private TodoListsService service;

        public TodoListsController(TodoListsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<TodoList> GetTodoLists()
        {
            return service.ReadAll();
        }

        [HttpPost("")]
        public void PostTodoList(TodoList list)
        {
            service.CreateList(list);
        }

        [HttpPatch("{listId}")]
        public void PatchTodoList(int listId, string title)
        {
            service.UpdateTodoList(listId, title);
        }

        [HttpDelete("{listId}")]
        public void DeleteTodoList(int listId)
        {
            service.DeleteList(listId);
        }

        [HttpGet("collection/today")]
        public ActionResult<IEnumerable<TodoList>> GetMyListsWithTasks()
        {
            return Ok(service.GetTodayTodos());
        }


        [HttpGet("dashboard")]
        public ActionResult<IEnumerable<TodoList>> GetTodayTodoList()
        {
            return Ok(service.GetDashboard());
        }
    }
}
