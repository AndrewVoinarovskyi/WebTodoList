using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoLists.Models;
using TodoLists;
using Microsoft.AspNetCore.JsonPatch;

namespace TodoLists.Controllers
{
    [Route("lists/{listId}")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private TodoListsService service;

        public TodoListController(TodoListsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IQueryable<TodoItem> GetTodoItems(int listId)
        {

            return service.Read(listId);
        }

        [HttpGet("tasks")]
        public ActionResult<TodoItem> GetTasks(int listId, bool all)
        {
            return Ok(service.GetIsAllTodos(listId, all));
        }

        [HttpPost("")]
        public ActionResult<TodoItem> PostTodoItem(int listId, TodoItem model)
        {
            return Ok(service.CreateItem(listId, model));
        }

        [HttpPut("{id}")]
        public ActionResult<TodoItem> Update(int listId, int id, TodoItem model)
        {
            return Ok(service.UpdateTodoItem(listId, id, model));
        }

        [HttpPatch("{id}")]
        public TodoItem Update(int listId, int id, [FromBody]JsonPatchDocument<TodoItem> patchDoc)
        {
            return service.UpdateItemStatus(listId, id, patchDoc);
        }

        [HttpDelete("{id}")]
        public void DeleteTodoItemById(int listId, int id)
        {
            service.DeleteItem(listId, id);
        }
    }
}
