using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList;
using TodoList.Models;

namespace TodoLists.Controllers
{
    [Route("")]
    [ApiController]
    public class WebTodoListsController : ControllerBase
    {
        private TodoListsService service;

        public WebTodoListsController(TodoListsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public List<TodoListDto> GetTodoLists()
        {

            return service.Read();
        }

        [HttpPost("")]
        public void PostTodoList(TodoListDto list)
        {
            service.Create(list);
        }

        [HttpPatch("{listId}")]
        public void PatchTodoList(int listId, string title)
        {
            service.Update(listId, title);
        }

        [HttpDelete("{listId}")]
        public void DeleteTodoList(int listId)
        {
            service.Delete(listId);
        }

        [HttpGet("collection/today")]
        public ActionResult<IEnumerable<TodoListDto>> GetMyListsWithTasks()
        {
            return Ok(service.GetTodayTodos());
        }


        [HttpGet("dashboard")]
        public ActionResult<IEnumerable<TodoListDto>> GetTodayTodoList()
        {
            return Ok(service.GetDashboard());
        }
    }
}
